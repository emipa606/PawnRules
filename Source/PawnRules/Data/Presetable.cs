using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using PawnRules.Interface;
using PawnRules.Patch;
using Verse;

namespace PawnRules.Data;

internal abstract class Presetable : IExposable, ILoadReferenceable
{
    private const int MaxIdLength = 20;

    private static readonly string VoidName = Lang.Get("Preset.None");

    private static int _count;
    protected readonly int Id;

    protected Presetable()
    {
        _count++;
        Id = _count;
    }

    protected Presetable(string name) : this()
    {
        Name = name;
    }

    public IPresetableType Type { get; protected set; }
    public string Name { get; set; }
    public bool IsVoid { get; private set; }

    public bool IsPreset => !Name.NullOrEmpty();

    public void ExposeData()
    {
        if (Scribe.mode != LoadSaveMode.Saving || IsPreset)
        {
            var encodedName = Scribe.mode == LoadSaveMode.Saving && Name?.Contains("\\n") == true
                ? Convert.ToBase64String(Encoding.UTF8.GetBytes(Name))
                : null;
            Name = ScribePlus.LookValue(Name, "name");
            encodedName = ScribePlus.LookValue(encodedName, "nameEncoded");
            // RimWorld's string parser turns a literal backslash-n into a newline.
            if (Scribe.mode == LoadSaveMode.LoadingVars && encodedName != null)
            {
                Name = Encoding.UTF8.GetString(Convert.FromBase64String(encodedName));
            }
        }

        ExposePresetData();
    }

    public string GetUniqueLoadID()
    {
        return $"{Mod.Id}_{GetPresetId()}";
    }

    protected abstract string GetPresetId();

    protected abstract void ExposePresetData();
    internal abstract bool IsIgnored();
    public abstract XElement ToXml();

    public static void SetName<T>(IPresetableType type, Action<T> onCreate) where T : Presetable
    {
        var localType = type;

        Dialog_SetName.Open(Lang.Get("Dialog_SetName.PresetTitleNew"), Lang.Get("Dialog_SetName.PresetLabel"), OnCommit,
            name => NameIsValid<T>(type, name));
        return;

        void OnCommit(string name)
        {
            var preset = Registry.CreatePreset<T>(localType, name);
            onCreate(preset);
        }
    }

    public static void SetName<T>(T preset, Action<T> onRename) where T : Presetable
    {
        var localPreset = preset;

        Dialog_SetName.Open(Lang.Get("Dialog_SetName.PresetTitle", preset.Name.Bold()),
            Lang.Get("Dialog_SetName.PresetLabel"), OnCommit, name => NameIsValid<T>(preset.Type, name), preset.Name);
        return;

        void OnCommit(string name)
        {
            onRename(Registry.RenamePreset(localPreset, name));
        }
    }

    private static bool NameIsValid<T>(IPresetableType type, string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MaxIdLength ||
            string.Equals(name, Lang.Get("Preset.None"), StringComparison.OrdinalIgnoreCase) ||
            string.Equals(name, Lang.Get("Preset.Personalized"), StringComparison.OrdinalIgnoreCase) ||
            Registry.PresetNameExists<T>(type, name))
        {
            return false;
        }

        for (var i = 0; i < name.Length; i++)
        {
            if (char.IsControl(name[i]))
            {
                return false;
            }

            if (XmlConvert.IsXmlChar(name[i]))
            {
                continue;
            }

            if (i + 1 >= name.Length || !XmlConvert.IsXmlSurrogatePair(name[i + 1], name[i]))
            {
                return false;
            }

            i++;
        }

        return true;
    }

    public static T CreateVoidPreset<T>(IPresetableType type) where T : Presetable
    {
        var preset = (T)Activator.CreateInstance(typeof(T), type, VoidName);
        preset?.IsVoid = true;
        return preset;
    }

    public static void ResetIds()
    {
        _count = 0;
    }
}