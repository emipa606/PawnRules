using Verse;

namespace PawnRules;

/// <summary>
///     Definition of the settings for the mod
/// </summary>
internal class PawnRulesSettings : ModSettings
{
    public bool NoAnimals;

    /// <summary>
    ///     Saving and loading the values
    /// </summary>
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref NoAnimals, "NoAnimals");
    }
}