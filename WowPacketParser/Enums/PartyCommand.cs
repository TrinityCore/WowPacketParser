namespace WowPacketParser.Enums
{
    public enum PartyCommand
    {
        Invite   = 0,
        Uninvite = 1,
        Leave    = 2,
        Swap     = 3,
        Unk6     = 6, // Teleport? Seen with Result: LfgTeleportIncombat
    }
}
