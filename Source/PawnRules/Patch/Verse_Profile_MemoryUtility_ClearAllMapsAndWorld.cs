using HarmonyLib;
using PawnRules.Data;
using Verse.Profile;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(MemoryUtility), nameof(MemoryUtility.ClearAllMapsAndWorld))]
internal static class Verse_Profile_MemoryUtility_ClearAllMapsAndWorld
{
    public static void Prefix()
    {
        Registry.Clear();
    }
}