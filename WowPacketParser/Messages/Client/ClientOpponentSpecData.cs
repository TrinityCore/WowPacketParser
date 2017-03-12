using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientOpponentSpecData
    {
        public ulong Guid;
        public int SpecializationID;

        public static void Read6(Packet packet, params object[] idx)
        {
            packet.ReadInt32("SpecializationID", idx);
            packet.ReadInt32("Unk", idx);
            packet.ReadPackedGuid128("Guid", idx);
        }
    }
}
