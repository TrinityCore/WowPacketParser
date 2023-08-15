using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_INIT_EXTRA_AURA_INFO_OBSOLETE)] // 2.4.3
        public static void HandleInitExtraAuraInfo(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            int index = 0;
            while (packet.CanRead())
            {
                packet.ReadByte("Slot", index);
                packet.ReadUInt32<SpellId>("SpellID", index);
                packet.ReadInt32("Max Duration", index);
                packet.ReadUInt32("Duration", index);
                index++;
            }
        }

        [Parser(Opcode.SMSG_SET_EXTRA_AURA_INFO_OBSOLETE)] // 2.4.3
        [Parser(Opcode.SMSG_SET_EXTRA_AURA_INFO_NEED_UPDATE_OBSOLETE)] // 2.4.3
        public static void HandleSetExtraAuraInfo(Packet packet)
        {
            packet.ReadPackedGuid();
            packet.ReadByte("Slot");
            packet.ReadUInt32<SpellId>("SpellID");
            if (packet.CanRead())
            {
                packet.ReadInt32("Max Duration");
                packet.ReadUInt32("Duration");
            }
        }

        [Parser(Opcode.SMSG_CLEAR_EXTRA_AURA_INFO_OBSOLETE)] // 2.4.3
        public static void HandleClearExtraAuraInfo(Packet packet)
        {
            packet.ReadPackedGuid();
            packet.ReadUInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_UPDATE_AURA_DURATION)] // 2.4.3
        public static void HandleUpdateAuraDuration(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadUInt32("Duration");
        }
    }
}
