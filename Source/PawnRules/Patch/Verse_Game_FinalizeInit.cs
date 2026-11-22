using HarmonyLib;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(Game), nameof(Game.FinalizeInit))]
internal static class Verse_Game_FinalizeInit
{
    public static void Postfix()
    {
        Mod.LoadWorld();
    }
}