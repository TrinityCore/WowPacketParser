using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_START_MIRROR_TIMER, ClientVersionBuild.V11_0_7_58123)]
        public static void HandleStartMirrorTimer(Packet packet)
        {
            packet.ReadByteE<MirrorTimerType>("Timer");
            packet.ReadUInt32("Value");
            packet.ReadUInt32("MaxValue");
            packet.ReadInt32("Scale");
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadBit("Paused");
        }

        [Parser(Opcode.SMSG_PAUSE_MIRROR_TIMER, ClientVersionBuild.V11_0_7_58123)]
        public static void HandlePauseMirrorTimer(Packet packet)
        {
            packet.ReadByteE<MirrorTimerType>("Timer");
            packet.ReadBit("Paused");
        }

        [Parser(Opcode.SMSG_STOP_MIRROR_TIMER, ClientVersionBuild.V11_0_7_58123)]
        public static void HandleStopMirrorTimer(Packet packet)
        {
            packet.ReadByteE<MirrorTimerType>("Timer");
        }

        [Parser(Opcode.SMSG_SET_ANIM_TIER, ClientVersionBuild.V11_2_0_62213)]
        public static void HandleSetAnimTier(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadByte("Tier");
        }
    }
}
