namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UpdateRaidTarget
    {
        public ulong Target;
        public byte PartyIndex;
        public byte Symbol;

        [Parser(Opcode.CMSG_UPDATE_RAID_TARGET)]
        public static void HandleUpdateRaidTarget(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("Target");
            packet.ReadByte("Symbol");
        }
    }
}
