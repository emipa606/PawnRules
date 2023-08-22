﻿using System.Linq;
using PawnRules.API;

namespace PawnRules.Data;

internal class PawnType : IPresetableType
{
    public static readonly PawnType Colonist = new PawnType("Colonist", Lang.Get("PawnType.Colonist"),
        Lang.Get("PawnType.ColonistPlural"), OptionTarget.Colonist);

    public static readonly PawnType Animal = new PawnType("Animal", Lang.Get("PawnType.Animal"),
        Lang.Get("PawnType.AnimalPlural"), OptionTarget.Animal);

    public static readonly PawnType Guest = new PawnType("Guest", Lang.Get("PawnType.Guest"),
        Lang.Get("PawnType.GuestPlural"), OptionTarget.Guest);

    public static readonly PawnType Prisoner = new PawnType("Prisoner", Lang.Get("PawnType.Prisoner"),
        Lang.Get("PawnType.PrisonerPlural"), OptionTarget.Prisoner);

    public static readonly PawnType Slave = new PawnType("Slave", Lang.Get("PawnType.Slave"),
        Lang.Get("PawnType.SlavePlural"), OptionTarget.Slave);

    public static readonly PawnType[] List =
    {
        Colonist,
        Animal,
        Guest,
        Prisoner,
        Slave
    };

    private PawnType(string id, string label, string labelPlural, OptionTarget target = default)
    {
        Id = id;
        Label = label;
        LabelPlural = labelPlural;
        AsTarget = target;
    }

    public OptionTarget AsTarget { get; }

    public string Id { get; }
    public string Label { get; }
    public string LabelPlural { get; }

    public bool IsTargetted(OptionTarget target)
    {
        return AsTarget != default && (AsTarget & target) == AsTarget;
    }

    public static PawnType FromTarget(OptionTarget target)
    {
        return List.FirstOrDefault(type => type.AsTarget == target);
    }

    public static PawnType FromId(string id)
    {
        return List.FirstOrDefault(type => type.Id == id);
    }
}