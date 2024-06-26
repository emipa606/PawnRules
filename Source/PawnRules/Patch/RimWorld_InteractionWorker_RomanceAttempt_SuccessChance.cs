﻿using HarmonyLib;
using PawnRules.Data;
using RimWorld;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(InteractionWorker_RomanceAttempt), nameof(InteractionWorker_RomanceAttempt.SuccessChance))]
internal static class RimWorld_InteractionWorker_RomanceAttempt_SuccessChance
{
    private static bool Prefix(ref float __result, Pawn initiator, Pawn recipient)
    {
        if (!Registry.IsActive)
        {
            return true;
        }

        var initiatorCanCourt = initiator.GetRules()?.AllowCourting ?? true;
        var recipientCanCourt = recipient.GetRules()?.AllowCourting ?? true;

        if (initiatorCanCourt && recipientCanCourt)
        {
            return true;
        }

        __result = 0f;
        return false;
    }
}