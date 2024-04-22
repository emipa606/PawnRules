using HarmonyLib;
using PawnRules.Data;
using RimWorld;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(FoodUtility), nameof(FoodUtility.WillEat), typeof(Pawn), typeof(ThingDef), typeof(Pawn),
    typeof(bool),
    typeof(bool))]
public static class RimWorld_FoodUtility_WillEat_ByThingDef
{
    public static void Postfix(ref bool __result, Pawn p, ThingDef food, Pawn getter = null)
    {
        if (!Registry.IsActive)
        {
            return;
        }

        if (getter != null && Registry.ExemptedTrainer == getter)
        {
            return;
        }

        if (Registry.AllowTrainingFood && getter?.CurJobDef != null &&
            (getter.CurJobDef == JobDefOf.Tame || getter.CurJobDef == JobDefOf.Train))
        {
            return;
        }

        if (!p.RaceProps.CanEverEat(food))
        {
            return;
        }

        if (Mod.Instance.Settings.NoAnimals && p.RaceProps.Animal)
        {
            return;
        }

        var restriction = p.GetRules()?.GetRestriction(RestrictionType.Food);
        if (restriction == null || restriction.IsVoid || p.InMentalState && (getter == null || getter != p))
        {
            return;
        }

        __result = restriction.AllowsFood(food, p);
    }
}