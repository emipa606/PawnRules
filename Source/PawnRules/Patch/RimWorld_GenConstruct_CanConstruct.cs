﻿using HarmonyLib;
using PawnRules.Data;
using RimWorld;
using Verse;
using Verse.AI;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(GenConstruct), nameof(GenConstruct.CanConstruct), typeof(Thing), typeof(Pawn), typeof(bool),
    typeof(bool),
    typeof(JobDef))]
internal static class RimWorld_GenConstruct_CanConstruct
{
    private static void Postfix(ref bool __result, Thing t, Pawn p, bool checkSkills = true, bool forced = false)
    {
        if (!Registry.IsActive || __result == false)
        {
            return;
        }

        var rules = p.GetRules();
        if (rules == null || rules.AllowArtisan || !checkSkills)
        {
            return;
        }

        if (t.def.entityDefToBuild is not ThingDef thingDef || !thingDef.HasComp(typeof(CompQuality)))
        {
            return;
        }

        if (forced && !JobFailReason.HaveReason && !rules.AllowArtisan)
        {
            JobFailReason.Is(Lang.Get("Rules.NotArtisanReason"), Lang.Get("Rules.NotArtisanJob", t.LabelCap));
        }

        __result = false;
    }
}