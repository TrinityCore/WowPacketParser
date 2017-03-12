using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAreaTriggerRePath
    {
        public CliAreaTriggerSpline AreaTriggerSpline;
        public ulong TriggerGUID;

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_PATH, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V7_0_3_22248)]
        public static void HandleAreaTriggerRePath6(Packet packet)
        {
            packet.ReadPackedGuid128("TriggerGUID");
            CliAreaTriggerSpline.Read6(packet);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_PATH, ClientVersionBuild.V7_0_3_22248)]
        public static void HandleAreaTriggerRePath7(Packet packet)
        {
            packet.ReadPackedGuid128("TriggerGUID");
            CliAreaTriggerSpline.Read7(packet);
        }
    }
}
