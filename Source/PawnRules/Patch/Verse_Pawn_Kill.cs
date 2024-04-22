﻿using HarmonyLib;
using PawnRules.Data;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(Pawn), nameof(Pawn.Kill))]
internal static class Verse_Pawn_Kill
{
    private static void Postfix(Pawn __instance)
    {
        if (!Registry.IsActive)
        {
            return;
        }

        Registry.DeleteRules(__instance);
    }
}