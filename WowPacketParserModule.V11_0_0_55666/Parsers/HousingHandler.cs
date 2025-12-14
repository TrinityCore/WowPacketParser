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
        
        [Parser(Opcode.SMSG_HOUSING_DECOR_SET_EDITOR_MODE_ACTIVE_RESPONSE)]
        public static void HandleHousingDecorSetEditorModeActiveResponse(Packet packet)
        {
            packet.ReadPackedGuid128("BNetAccountGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            var allowedEditorCount = packet.ReadUInt32("AllowedEditorCount");
            packet.ReadByteE<HousingResult>("Result");

            for (var i = 0; i < allowedEditorCount; ++i)
                packet.ReadPackedGuid128("AllowedEditor", i);
        }
        [Parser(Opcode.CMSG_HOUSING_REQUEST_CURRENT_HOUSE_INFO)]
        public static void HandleHousingNull(Packet packet)
        {
        }
    }
}
