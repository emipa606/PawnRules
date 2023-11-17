using HarmonyLib;
using PawnRules.Data;
using RimWorld;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(Pawn_FoodRestrictionTracker), "CurrentFoodRestriction", MethodType.Getter)]
internal static class RimWorld_Pawn_FoodRestrictionTracker_CurrentFoodRestriction
{
    private static bool Prefix(ref FoodRestriction __result, Pawn ___pawn)
    {
        if (!Registry.IsActive)
        {
            return true;
        }

        if (Mod.Instance.Settings.NoAnimals && ___pawn.RaceProps.Animal)
        {
            return true;
        }

        __result = new FoodRestriction(-1, Lang.Get("FoodRestrictionsOverridden"));
        return false;
    }
}