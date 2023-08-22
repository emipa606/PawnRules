using HarmonyLib;
using PawnRules.Data;
using RimWorld;
using Verse;

namespace PawnRules.Patch;

[HarmonyPatch(typeof(Pawn_GuestTracker), "SetGuestStatus")]
internal static class RimWorld_Pawn_GuestTracker_SetGuestStatus
{
    private static void Prefix(Pawn_GuestTracker __instance, Faction newHost,
        GuestStatus guestStatus = GuestStatus.Guest)
    {
        if (!Registry.IsActive)
        {
            return;
        }

        var pawn = Traverse.Create(__instance).Field<Pawn>("pawn").Value;
        var guestType = PawnType.Colonist;

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

        if (pawn != null && pawn.def.race?.Animal == true)
        {
            guestType = PawnType.Animal;
        }

        Registry.FactionUpdate(pawn, newHost, guestType);
    }
}