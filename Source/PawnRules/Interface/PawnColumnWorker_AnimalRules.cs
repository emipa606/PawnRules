using System;
using PawnRules.Data;
using PawnRules.Patch;
using RimWorld;
using UnityEngine;
using Verse;

namespace PawnRules.Interface;

public class PawnColumnWorker_AnimalRules : PawnColumnWorker
{
    public override bool VisibleCurrently => !Mod.Instance.Settings.NoAnimals;

    public override void DoHeader(Rect rect, PawnTable table)
    {
        def.label = Lang.Get("PresetType.Rules");
        base.DoHeader(rect, table);
        var buttonRect = new Rect(rect.x, rect.y + rect.height - 65f, Mathf.Min(rect.width, 360f), 32f);
        if (Widgets.ButtonText(buttonRect, Lang.Get("Dialog_Rules.OpenDefaults")))
        {
            Dialog_Rules.OpenDefaults(PawnType.Animal);
        }
    }

    public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
    {
        if (pawn.CanHaveRules())
        {
            RimWorld_PawnColumnWorker_FoodRestriction_DoAssignFoodRestrictionButtons.DrawButtons(rect, pawn);
        }
    }

    public override int GetMinWidth(PawnTable table)
    {
        return Mathf.Max(base.GetMinWidth(table), 194);
    }

    public override int GetOptimalWidth(PawnTable table)
    {
        return Mathf.Clamp(251, GetMinWidth(table), GetMaxWidth(table));
    }

    public override int GetMinHeaderHeight(PawnTable table)
    {
        return Mathf.Max(base.GetMinHeaderHeight(table), 65);
    }

    public override int Compare(Pawn a, Pawn b)
    {
        return string.Compare(Registry.GetRules(a)?.GetDisplayName(), Registry.GetRules(b)?.GetDisplayName(),
            StringComparison.OrdinalIgnoreCase);
    }
}