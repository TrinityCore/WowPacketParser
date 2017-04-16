using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CriteriaProgress
    {
        public int Id;
        public ulong Quantity;
        public ulong Player;
        public int Flags;
        public Data Date;
        public UnixTime TimeFromStart;
        public UnixTime TimeFromCreate;

        public static void Read6(Packet packet, params object[] idx)
        {
            packet.ReadInt32<CriteriaId>("Id", idx);
            packet.ReadUInt64("Quantity", idx);
            packet.ReadPackedGuid128("Player", idx);
            packet.ReadPackedTime("Date", idx);
            packet.ReadTime("TimeFromStart", idx);
            packet.ReadTime("TimeFromCreate", idx);

            packet.ResetBitReader();
            packet.ReadBits("Flags", 4, idx); // some flag... & 1 -> delete
        }
    }
}
