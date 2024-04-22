using HarmonyLib;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(PawnGenerator), nameof(PawnGenerator.GeneratePawn), typeof(PawnGenerationRequest))]
public static class Verse_PawnGenerator_GeneratePawn_ByOther
{
    public static void Postfix(ref Pawn __result)
    {
        Verse_PawnGenerator_GeneratePawn_ByRequest.Postfix(ref __result);
    }
}