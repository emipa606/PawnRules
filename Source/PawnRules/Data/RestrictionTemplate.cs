﻿using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace PawnRules.Data;

internal class RestrictionTemplate
{
    private static readonly PawnKindDef[] AnimalCache = DefDatabase<PawnKindDef>.AllDefs
        .Where(kindDef => kindDef.RaceProps.Animal).OrderBy(animal => animal.label).ToArray();

    private static readonly ThingDef[] FoodCache = DefDatabase<ThingDef>.AllDefs.Where(food => food.IsIngestible)
        .ToList().OrderBy(food => food.category).ThenBy(food => food.FirstThingCategory?.index)
        .ThenBy(food => food.label).ToArray();

    public readonly IEnumerable<Category> Categories;

    private RestrictionTemplate(IEnumerable<Category> list)
    {
        Categories = list;
    }

    public void ToggleAll(bool value)
    {
        foreach (var category in Categories)
        {
            foreach (var member in category.Members)
            {
                member.Value = value;
            }
        }
    }

    public static bool IsValidDefName(string defName, RestrictionType type)
    {
        if (type == RestrictionType.Food)
        {
            return FoodCache.Any(def => def.defName == defName);
        }

        if (type == RestrictionType.Bonding)
        {
            return AnimalCache.Any(def => def.defName == defName);
        }

        throw new Mod.Exception("Invalid restriction type");
    }

    private static RestrictionTemplate GetFoodsCategorized(Restriction restriction)
    {
        var list = new Dictionary<string, Category>
            { [ThingCategoryDefOf.FoodMeals.LabelCap] = new Category(ThingCategoryDefOf.FoodMeals.LabelCap) };

        foreach (var food in FoodCache)
        {
            var category = GetFoodCategory(food);

            if (!list.ContainsKey(category))
            {
                list[category] = new Category(category);
            }

            list[category].Members.Add(new Toggle(food, restriction.Allows(food)));
        }

        return new RestrictionTemplate(list.Values.ToArray());
    }

    private static string GetFoodCategory(ThingDef self)
    {
        return self.category == ThingCategory.Item && self.FirstThingCategory != null
            ? self.FirstThingCategory.LabelCap
            : self.category.ToString();
    }

    private static RestrictionTemplate GetAnimalsCategorized(Restriction restriction)
    {
        var list = new Dictionary<string, Category>();

        var categories = new[]
        {
            Lang.Get("AnimalCategory.Pet"),
            Lang.Get("AnimalCategory.Farm"),
            Lang.Get("AnimalCategory.Herd"),
            Lang.Get("AnimalCategory.Predator"),
            Lang.Get("AnimalCategory.Insect"),
            Lang.Get("AnimalCategory.Other")
        };

        foreach (var animal in AnimalCache)
        {
            string category;

            if (animal.RaceProps.petness > 0.5f)
            {
                category = categories[0];
            }
            else if (animal.race.tradeTags?.Contains("AnimalFarm") ?? false)
            {
                category = categories[1];
            }
            else if (animal.RaceProps.herdAnimal)
            {
                category = categories[2];
            }
            else if (animal.RaceProps.predator)
            {
                category = categories[3];
            }
            else if (animal.race.tradeTags?.Contains("AnimalInsect") ?? false)
            {
                category = categories[4];
            }
            else
            {
                category = categories[5];
            }

            if (!list.ContainsKey(category))
            {
                list[category] = new Category(category);
            }

            list[category].Members.Add(new Toggle(animal, restriction.Allows(animal)));
        }

        return new RestrictionTemplate(list.Values.OrderBy(member => Array.IndexOf(categories, member.Label))
            .ToArray());
    }

    public static RestrictionTemplate Build(RestrictionType type, Restriction restriction)
    {
        if (type == RestrictionType.Food)
        {
            return GetFoodsCategorized(restriction);
        }

        return type == RestrictionType.Bonding ? GetAnimalsCategorized(restriction) : null;
    }

    public class Category
    {
        public Category(string label)
        {
            Label = label;
        }

        public string Label { get; }
        public List<Toggle> Members { get; } = new List<Toggle>();

        public MultiCheckboxState GetListState()
        {
            if (Members.Count < 1)
            {
                return MultiCheckboxState.Off;
            }

            var first = Members.First().Value;
            return Members.All(q => q.Value == first)
                ? first ? MultiCheckboxState.On : MultiCheckboxState.Off
                : MultiCheckboxState.Partial;
        }

        public void UpdateState(MultiCheckboxState state)
        {
            if (state == MultiCheckboxState.Off)
            {
                SetAll(false);
            }
            else if (state == MultiCheckboxState.On)
            {
                SetAll(true);
            }
        }

        private void SetAll(bool state)
        {
            foreach (var member in Members)
            {
                member.Value = state;
            }
        }
    }

    public class Toggle
    {
        public bool Value;

        public Toggle(Def def, bool value)
        {
            Def = def;
            Value = value;
        }

        public Def Def { get; }
    }
}