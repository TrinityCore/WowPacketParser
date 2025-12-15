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
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadPackedGuid128("BNetAccountGUID");
            var allowedEditorCount = packet.ReadUInt32("AllowedEditorCount");
            packet.ReadByteE<HousingResult>("Result");

            for (var i = 0; i < allowedEditorCount; ++i)
                packet.ReadPackedGuid128("AllowedEditor", i);
        }

        [Parser(Opcode.SMSG_HOUSING_FIXTURE_SET_EDITOR_MODE_ACTIVE_RESPONSE)]
        public static void HandleHousingFixtureSetEditorModeActiveResponse(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadPackedGuid128("BNetAccountGUID");
            packet.ReadByteE<HousingResult>("Result");
        }

        [Parser(Opcode.SMSG_HOUSING_ROOM_SET_EDITOR_MODE_ACTIVE_RESPONSE)]
        public static void HandleHousingRoomSetEditorModeActiveResponse(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadByteE<HousingResult>("Result");
            packet.ReadBool("Active");
        }
        
        [Parser(Opcode.SMSG_HOUSING_EXTERIOR_SET_EXTERIOR_LOCK_STATE)]
        public static void HandleHousingExteriorLockHouseExterior(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadByteE<HousingResult>("Result");
            packet.ReadBit("IsLocked");
        }
        
        [Parser(Opcode.SMSG_HOUSING_CURRENT_HOUSE_INFO_RESPONSE)]
        public static void HandleHousingCurrentHouseInfoResponse(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadPackedGuid128("NeighborhoodGUID");
            packet.ReadUInt32("Unk0");
            packet.ReadByte("Unk1");
            packet.ReadByte("Unk2");
            packet.ReadByte("Unk3");
        }
        
        [Parser(Opcode.CMSG_HOUSING_ROOM_REMOVE_ROOM)]
        public static void HandleHousingRemoveRoom(Packet packet)
        {
            packet.ReadPackedGuid128("RoomGUID");
        }
        
        [Parser(Opcode.SMSG_HOUSING_ROOM_REMOVE_ROOM_RESPONSE)]
        public static void HandleHousingRemoveRoomResponse(Packet packet)
        {
            packet.ReadPackedGuid128("RoomGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.CMSG_HOUSING_DECOR_SELECT_DECOR)]
        public static void HousingDecorSelect(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadBool("Selected");
        }
        
        [Parser(Opcode.CMSG_HOUSING_DECOR_START_PLACING_NEW_DECOR)]
        public static void HousingDecorStartPlacing(Packet packet)
        {
            packet.ReadUInt32("DecorID");
            packet.ReadUInt32("Field_4");
        }
        
        [Parser(Opcode.CMSG_HOUSING_DECOR_REMOVE_PLACED_DECOR_ENTRY)]
        public static void HousingDecorRemovePlacedEntry(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
        }
        
        [Parser(Opcode.CMSG_HOUSING_DECOR_COMMIT_DYES_FOR_SELECTED_DECOR)]
        public static void HousingDecorCommitDyesForSelection(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            for (var i = 0; i < 3; ++i)
            {
               packet.ReadInt32("DyeColorID", i);
            }
        }
        
        [Parser(Opcode.CMSG_HOUSING_REQUEST_CURRENT_HOUSE_INFO)]
        public static void HandleHousingNull(Packet packet)
        {
        }
    }
}
