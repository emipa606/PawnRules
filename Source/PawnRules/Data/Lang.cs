using Verse;

namespace PawnRules.Data;

internal static class Lang
{
    public static string Get(string key, params object[] args)
    {
        return string.Format((Mod.Id + "." + key).Translate(), args);
    }
}