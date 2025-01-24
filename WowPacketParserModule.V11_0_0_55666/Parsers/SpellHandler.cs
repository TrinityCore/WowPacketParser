using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_CANCEL_SPELL_VISUAL_KIT)]
        public static void HandleCancelSpellVisualKit(Packet packet)
        {
            packet.ReadPackedGuid128("Source");
            packet.ReadInt32("SpellVisualKitID");
            packet.ReadBit("MountedVisual");
        }
    }
}
