namespace WowPacketParserModule.BattleNet.V37165.Enums
{
    public enum ClosingReason
    {
        PacketTooLarge = 1,
        PacketCorrupt = 2,
        PacketInvalid = 3,
        PacketIncorrect = 4,
        HeaderCorrupt = 5,
        HeaderIgnored = 6,
        HeaderIncorrect = 7,
        PacketRejected = 8,
        ChannelUnhandled = 9,
        CommandUnhandled = 10,
        CommandBadPermissions = 11,
        DirectCall = 12,
        Timeout = 13
    }
}
