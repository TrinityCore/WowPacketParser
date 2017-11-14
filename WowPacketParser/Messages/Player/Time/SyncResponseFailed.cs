using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Player.Time
{
    public unsafe struct SyncResponseFailed
    {
        public uint SequenceIndex;

        [Parser(Opcode.CMSG_TIME_SYNC_RESPONSE_FAILED)]
        public static void HandleTimeSyncRespFailed(Packet packet)
        {
            packet.ReadUInt32("SequenceIndex");
        }
    }
}
