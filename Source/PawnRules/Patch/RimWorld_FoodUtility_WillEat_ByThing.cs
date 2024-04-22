using HarmonyLib;
using RimWorld;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(FoodUtility), nameof(FoodUtility.WillEat), typeof(Pawn), typeof(Thing), typeof(Pawn), typeof(bool),
    typeof(bool))]
public static class RimWorld_FoodUtility_WillEat_ByThing
{
    public static void Postfix(ref bool __result, Pawn p, Thing food, Pawn getter = null)
    {
        RimWorld_FoodUtility_WillEat_ByThingDef.Postfix(ref __result, p, food.def, getter);
    }
}