using System.IO;
using Mlie;
using PawnRules.Data;
using PawnRules.Interface;
using PawnRules.Patch;
using RimWorld;
using UnityEngine;
using Verse;

namespace PawnRules;

internal class Mod : Verse.Mod
{
    public const string Id = "PawnRules";
    public const string Name = "Pawn Rules";
    public const string Version = "1.5.0";

    public static readonly DirectoryInfo ConfigDirectory =
        new DirectoryInfo(Path.Combine(GenFilePaths.ConfigFolderPath, Id));

    private static string currentVersion;

    public Mod(ModContentPack contentPack) : base(contentPack)
    {
        Instance = this;
        Settings = GetSettings<PawnRulesSettings>();

        currentVersion = VersionFromManifest.GetVersionFromModMetaData(contentPack.ModMetaData);
        FirstTimeUser = !ConfigDirectory.Exists;
        ConfigDirectory.Create();

        Log("Initialized");
    }

    internal PawnRulesSettings Settings { get; }
    public static Mod Instance { get; private set; }
    public static bool FirstTimeUser { get; private set; }

    public static void Log(string message)
    {
        Verse.Log.Message(PrefixMessage(message));
    }

    public static void Warning(string message)
    {
        Verse.Log.Warning(PrefixMessage(message));
    }

    public static void Message(string message)
    {
        Messages.Message(message, MessageTypeDefOf.TaskCompletion, false);
    }

    public static string PrefixMessage(string message)
    {
        return $"[{Name} v{Version}] {message}";
    }

    public override string SettingsCategory()
    {
        return Name;
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var rect = inRect.GetHGrid(1f, -1f, 400f, -1f)[2];

        var listing = new Listing_Standard();
        listing.Begin(rect);

        if (Registry.IsActive)
        {
            if (listing.ButtonText(Lang.Get("Button.RemoveMod"), Lang.Get("Button.RemoveModDesc")))
            {
                Dialog_Alert.Open(Lang.Get("Button.RemoveModConfirm"), Dialog_Alert.Buttons.YesNo,
                    Registry.DeactivateMod);
            }
        }
        else
        {
            listing.Label(Lang.Get("Settings.NoGame"));
        }

        listing.Gap();
        listing.CheckboxLabeled("PaRu.NotAnimals".Translate(), ref Settings.NoAnimals);

        if (currentVersion != null)
        {
            listing.Gap();
            GUI.contentColor = Color.gray;
            listing.Label("PaRu.ModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing.End();
    }

    public static void OnStartup()
    {
        Patcher.ApplyLanguageOverrides();
    }

    public static void LoadWorld()
    {
        AddonManager.AcceptingAddons = false;
        Registry.Initialize();
    }

    internal class Exception(string message) : System.Exception(PrefixMessage(message));
}