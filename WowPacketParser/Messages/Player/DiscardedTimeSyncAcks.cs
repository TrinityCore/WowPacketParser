using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct DiscardedTimeSyncAcks
    {
        public uint MaxSequenceIndex;

        [Parser(Opcode.CMSG_DISCARDED_TIME_SYNC_ACKS)]
        public static void HandleDiscardedTimeSyncAcks(Packet packet)
        {
            packet.ReadUInt32("MaxSequenceIndex");
        }
    }
}
