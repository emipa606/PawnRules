using HarmonyLib;
using PawnRules.Data;
using RimWorld;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(PawnUtility), nameof(PawnUtility.TrySpawnHatchedOrBornPawn))]
internal static class RimWorld_PawnUtility_TrySpawnHatchedOrBornPawn
{
    public static void Postfix(bool __result, Pawn pawn, Thing motherOrEgg)
    {
        if (!Registry.IsActive || !__result || pawn == null)
        {
            return;
        }

        if (pawn.RaceProps.Animal)
        {
            if (!pawn.CanHaveRules())
            {
                return;
            }

            Registry.DeleteRules(pawn);
            Registry.GetOrDefaultRules(pawn);

            return;
        }

        if (motherOrEgg is Pawn mother && (mother.Faction?.IsPlayer ?? true))
        {
            Registry.CloneRules(mother, pawn);
        }
    }
}