using System.Reflection;
using HarmonyLib;
using PawnRules.Data;
using Verse;

namespace PawnRules.Patch;

[StaticConstructorOnStartup]
internal static class Patcher
{
    static Patcher()
    {
        Harmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    private static Harmony Harmony { get; } = new(Mod.Id);

    public static void ApplyLanguageOverrides()
    {
        OverrideLanguageKey("FoodPolicy", Lang.Get("PresetType.Rules"));
    }

    private static void OverrideLanguageKey(string key, string value)
    {
        if (!LanguageDatabase.activeLanguage.keyedReplacements.Remove(key, out var original))
        {
            return;
        }

        original.value = value;
        LanguageDatabase.activeLanguage.keyedReplacements.Add(key, original);
    }
}