using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class EquipmentSetHandler
    {
        [Parser(Opcode.SMSG_EQUIPMENT_SET_ID)]
        public static void HandleEquipmentSetSaved(Packet packet)
        {
            packet.ReadInt32("Type");
            packet.ReadUInt32("SetID");
            packet.ReadUInt64("GUID");
        }
    }
}
