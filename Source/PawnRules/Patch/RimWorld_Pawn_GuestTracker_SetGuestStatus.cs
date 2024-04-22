using HarmonyLib;
using PawnRules.Data;
using RimWorld;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(Pawn_GuestTracker), nameof(Pawn_GuestTracker.SetGuestStatus))]
internal static class RimWorld_Pawn_GuestTracker_SetGuestStatus
{
    private static void Prefix(Faction newHost, Pawn ___pawn, GuestStatus guestStatus = GuestStatus.Guest)
    {
        if (!Registry.IsActive)
        {
            return;
        }


        var guestType = PawnType.Colonist;

        if (Mod.Instance.Settings.NoAnimals && ___pawn.RaceProps.Animal)
        {
            return;
        }

        switch (guestStatus)
        {
            case GuestStatus.Guest:
                guestType = PawnType.Guest;
                break;
            case GuestStatus.Prisoner:
                guestType = PawnType.Prisoner;
                break;
            case GuestStatus.Slave:
                guestType = PawnType.Slave;
                break;
        }

        if (___pawn != null && ___pawn.def.race?.Animal == true)
        {
            guestType = PawnType.Animal;
        }

        Registry.FactionUpdate(___pawn, newHost, guestType);
    }
}