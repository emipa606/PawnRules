using HarmonyLib;
using PawnRules.Data;
using RimWorld;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(Pawn_FoodRestrictionTracker), "Configurable", MethodType.Getter)]
internal static class RimWorld_Pawn_FoodRestrictionTracker_Configurable
{
    private static bool Prefix(ref bool __result, Pawn ___pawn)
    {
        if (!Registry.IsActive)
        {
            return true;
        }

        if (Mod.Instance.Settings.NoAnimals && ___pawn.RaceProps.Animal)
        {
            return true;
        }

        __result = false;
        return false;
    }
}