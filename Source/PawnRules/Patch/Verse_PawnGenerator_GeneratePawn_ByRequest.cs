using HarmonyLib;
using PawnRules.Data;
using RimWorld;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(PawnGenerator), nameof(PawnGenerator.GeneratePawn), typeof(PawnKindDef), typeof(Faction))]
public static class Verse_PawnGenerator_GeneratePawn_ByRequest
{
    public static void Postfix(ref Pawn __result)
    {
        if (!Registry.IsActive)
        {
            return;
        }

        if (__result == null || (!__result.Faction?.IsPlayer ?? true) && (!__result.HostFaction?.IsPlayer ?? true))
        {
            return;
        }

        Registry.GetOrDefaultRules(__result);
    }
}