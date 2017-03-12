using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct EarnedAchievement
    {
        public int Id;
        public Data Date;
        public ulong Owner;
        public uint VirtualRealmAddress;
        public uint NativeRealmAddress;

        public static void ReadEarnedAchievement602(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Id", idx);
            packet.ReadPackedTime("Date", idx);
            packet.ReadPackedGuid128("Owner", idx);
            packet.ReadInt32("VirtualRealmAddress", idx);
            packet.ReadInt32("NativeRealmAddress", idx);
        }
    }
}
