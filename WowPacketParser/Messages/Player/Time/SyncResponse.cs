using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Player.Time
{
    public unsafe struct SyncResponse
    {
        public uint ClientTime;
        public uint SequenceIndex;

        [Parser(Opcode.CMSG_TIME_SYNC_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleTimeSyncResp(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595)) // no idea when this was added exactly
            {
                packet.ReadUInt32("Ticks");
                packet.ReadUInt32("Counter");
            }
            else
            {
                packet.ReadUInt32("Counter");
                packet.ReadUInt32("Ticks");
            }
        }
    }
}
