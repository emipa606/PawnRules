using HarmonyLib;
using RimWorld;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(FoodPolicy), nameof(FoodPolicy.Allows), typeof(Thing))]
public static class RimWorld_FoodRestriction_Allows_ByThingDef
{
    public static void Postfix(ref bool __result)
    {
        __result = true;
    }
}