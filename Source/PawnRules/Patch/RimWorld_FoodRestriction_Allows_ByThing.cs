using HarmonyLib;
using RimWorld;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(FoodPolicy), nameof(FoodPolicy.Allows), typeof(ThingDef))]
public static class RimWorld_FoodRestriction_Allows_ByThing
{
    private static void Postfix(ref bool __result)
    {
        __result = true;
    }
}