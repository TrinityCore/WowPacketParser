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

        public static void ReadMirrorVarSingle(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBits("UpdateType", 1, idx);
            var nameLength = (int)packet.ReadBits(24);
            var valueLength = (int)packet.ReadBits(24);

            var name = packet.ReadDynamicString(nameLength);
            var value = packet.ReadDynamicString(valueLength);

            packet.AddValue(name, value, idx);
        }

        [Parser(Opcode.SMSG_MIRROR_VARS)]
        public static void HandleMirrorVars(Packet packet)
        {
            var count = packet.ReadUInt32();
            for (var i = 0u; i < count; ++i)
                ReadMirrorVarSingle(packet, i);
        }
    }
}
