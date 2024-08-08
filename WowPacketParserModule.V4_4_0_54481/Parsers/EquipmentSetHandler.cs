using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class EquipmentSetHandler
    {
        [Parser(Opcode.SMSG_EQUIPMENT_SET_ID)]
        public static void HandleEquipmentSetSaved(Packet packet)
        {
            packet.ReadUInt64("GUID");
            packet.ReadInt32("Type");
            packet.ReadUInt32("SetID");
        }
    }
}
