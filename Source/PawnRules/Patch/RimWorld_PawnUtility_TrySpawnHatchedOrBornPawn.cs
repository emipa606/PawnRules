﻿using HarmonyLib;
using PawnRules.Data;
using RimWorld;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(PawnUtility), nameof(PawnUtility.TrySpawnHatchedOrBornPawn))]
internal static class RimWorld_PawnUtility_TrySpawnHatchedOrBornPawn
{
    private static void Postfix(bool __result, Pawn pawn, Thing motherOrEgg)
    {
        if (!Registry.IsActive || !__result || pawn == null || motherOrEgg is not Pawn mother ||
            (!mother.Faction?.IsPlayer ?? false))
        {
            return;
        }

        Registry.CloneRules(mother, pawn);
    }
}