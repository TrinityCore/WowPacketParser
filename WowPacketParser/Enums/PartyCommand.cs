namespace WowPacketParser.Enums
{
    public enum PartyCommand
    {
        Invite    = 0,
        Uninvite  = 1,
        Leave     = 2,
        PartySwap = 3,
        RaidSwap  = 4, // Used in 4.2.2+
        Unk6      = 6  // Teleport? Seen with Result: LfgTeleportIncombat
    }
}
