using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class HousingHandler
    {        
        [Parser(Opcode.CMSG_HOUSING_DECOR_SET_EDITOR_MODE_ACTIVE)]
        [Parser(Opcode.CMSG_HOUSING_FIXTURE_SET_EDITOR_MODE_ACTIVE)]
        [Parser(Opcode.CMSG_HOUSING_ROOM_SET_EDITOR_MODE_ACTIVE)]
        public static void HandleHousingSetEditorModeActive(Packet packet)
        {
            packet.ReadBool("Active");
        }
        
        [Parser(Opcode.CMSG_HOUSING_REQUEST_CURRENT_HOUSE_INFO)]
        public static void HandleHousingNull(Packet packet)
        {
        }
    }
}
