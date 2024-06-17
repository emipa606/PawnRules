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

    public static Harmony Harmony { get; } = new Harmony(Mod.Id);

    public static void ApplyLanguageOverrides()
    {
        OverrideLanguageKey("FoodPolicy", Lang.Get("PresetType.Rules"));
    }

    public static void OverrideLanguageKey(string key, string value)
    {
        if (!LanguageDatabase.activeLanguage.keyedReplacements.Remove(key, out var original))
        {
            return;
        }

        original.value = value;
        LanguageDatabase.activeLanguage.keyedReplacements.Add(key, original);
    }
}