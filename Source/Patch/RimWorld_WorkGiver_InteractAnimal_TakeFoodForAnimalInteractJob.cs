﻿using HarmonyLib;
using PawnRules.Data;
using RimWorld;
using Verse;

namespace PawnRules.Patch
{
    [HarmonyPatch(typeof(WorkGiver_InteractAnimal), "TakeFoodForAnimalInteractJob")]
    internal static class RimWorld_WorkGiver_InteractAnimal_TakeFoodForAnimalInteractJob
    {
        private static void Prefix(Pawn pawn)
        {
            if (!Registry.IsActive || !Registry.AllowTrainingFood) { return; }

            Registry.ExemptedTrainer = pawn;
        }

        private static void Postfix()
        {
            if (!Registry.IsActive || (Registry.ExemptedTrainer == null)) { return; }

            Registry.ExemptedTrainer = null;
        }
    }
}
