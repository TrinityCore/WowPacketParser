using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Player.Time
{
    public unsafe struct SyncResponseDropped
    {
        public uint SequenceIndexFirst;
        public uint SequenceIndexLast;

        [Parser(Opcode.CMSG_TIME_SYNC_RESPONSE_DROPPED)]
        public static void HandleTimeSyncResponseDropped(Packet packet)
        {
            packet.ReadUInt32("SequenceIndexFirst");
            packet.ReadUInt32("SequenceIndexLast");
        }
    }
}
