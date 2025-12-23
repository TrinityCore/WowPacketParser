using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class HousingHandler
    {
        
        [Parser(Opcode.CMSG_HOUSING_DECOR_CATALOG_CREATE_SEARCHER)]
        public static void HousingDecorCatalogCreateSearcher(Packet packet)
        {
            packet.ReadPackedGuid128("BnetAccountID");
        }
        
        [Parser(Opcode.CMSG_HOUSING_DECOR_CATALOG_DESTROY_ENTRY)]
        public static void HandleHousingDecorCatalogDestroyEntry(Packet packet)
        {
            packet.ReadPackedGuid128("BnetAccountID");
            packet.ReadUInt16("CatalogEntryID");
            packet.ReadUInt32("Field_10");
        }

        [Parser(Opcode.CMSG_HOUSING_DECOR_CATALOG_DESTROY_ALL_ENTRY_COPIES)]
        public static void HandleHousingDecorCatalogDestroyAllEntryCopies(Packet packet)
        {
            packet.ReadUInt16("CatalogEntryID");
            packet.ReadUInt16("Field_4");
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

        [Parser(Opcode.CMSG_HOUSING_DECOR_MOVE_DECOR)]
        public static void HandleHousingDecorMove(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadVector3("Position");
            packet.ReadQuaternion("Rotation");
            packet.ReadPackedGuid128("AttachParentGUID");
            packet.ReadPackedGuid128("RoomGUID");
            packet.ReadPackedGuid128("Field_70");
            packet.ReadInt32("Field_80");
            packet.ReadByte("Field_85");
            packet.ReadByte("Field_86");
            packet.ReadBool("Field_87");
        }
        
        [Parser(Opcode.CMSG_HOUSING_DECOR_REMOVE_PLACED_DECOR_ENTRY)]
        public static void HousingDecorRemovePlacedEntry(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
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
            packet.ReadUInt32("CatalogEntryID");
            packet.ReadUInt32("Field_4");
        }
        
        [Parser(Opcode.CMSG_HOUSING_ROOM_REMOVE_ROOM)]
        public static void HandleHousingRoomRemove(Packet packet)
        {
            packet.ReadPackedGuid128("RoomGUID");
        }
        
        [Parser(Opcode.CMSG_HOUSING_ROOM_ROTATE_ROOM)]
        public static void HousingRoomRotate(Packet packet)
        {
            packet.ReadPackedGuid128("RoomGUID");
            packet.ReadBool("IsLeft");
        }
        
        [Parser(Opcode.CMSG_HOUSING_SERVICES_GET_OTHERS_PLAYER_OWNED_HOUSES)]
        public static void HandleHousingServiceGetOthersPlayerOwnedHouses(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
        }
        
        [Parser(Opcode.CMSG_HOUSING_SERVICES_REQUEST_PLAYER_CHARACTER_LIST)]
        public static void HandleHousingServicesRequestPlayerCharacterList(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
        }
        
        [Parser(Opcode.CMSG_HOUSING_SERVICES_SEARCH_BNET_FRIEND_NEIGHBORHOODS)]
        public static void HandleHousingServiceSearchBnetFriendNeighborhoods(Packet packet)
        {
            packet.ReadPackedGuid128("BNetAccountGUID");
        }

        [Parser(Opcode.CMSG_NEIGHBORHOOD_INTERACT_WITH_CORNERSTONE)]
        public static void HandleNeighborhoodInteractWithCornerstone(Packet packet)
        {
            packet.ReadUInt32("PlotID");
            packet.ReadPackedGuid128("CornerstoneGUID");
        }
        
        [Parser(Opcode.CMSG_QUERY_NEIGHBORHOOD_INFO)]
        public static void HandleQueryNeighborhoodInfo(Packet packet)
        {
            packet.ReadPackedGuid128("NeighborhoodGUID");
        }
        
        [Parser(Opcode.SMSG_HOUSING_CURRENT_HOUSE_INFO_RESPONSE)]
        public static void HandleHousingCurrentHouseInfoResponse(Packet packet)
        {
            ReadHouse(packet, "House");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_DECOR_CATALOG_CREATE_SEARCHER_RESPONSE)]
        public static void HousingDecorCatalogCreateSearcherResponse(Packet packet)
        {
            packet.ReadPackedGuid128("BnetAccountID");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_DECOR_COMMIT_DYES_FOR_SELECTED_DECOR_RESPONSE)]
        public static void HandleHousingDecorCommitDyesForSelectedDecorResponse(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_DECOR_SELECT_DECOR_RESPONSE)]
        public static void HandleHousingDecorSelectDecorResponse(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadUInt32("Field_16");
            packet.ReadByteE<HousingResult>("Result");
            packet.ReadBit("Selected");
            packet.ReadBit("Field_17");
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

        [Parser(Opcode.SMSG_HOUSING_EXTERIOR_SET_EXTERIOR_LOCK_STATE)]
        public static void HandleHousingExteriorLockHouseExterior(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadByteE<HousingResult>("Result");
            packet.ReadBit("IsLocked");
        }

        [Parser(Opcode.SMSG_HOUSING_FIXTURE_SET_EDITOR_MODE_ACTIVE_RESPONSE)]
        public static void HandleHousingFixtureSetEditorModeActiveResponse(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadPackedGuid128("BNetAccountGUID");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_ROOM_REMOVE_ROOM_RESPONSE)]
        public static void HandleHousingRoomRemoveResponse(Packet packet)
        {
            packet.ReadPackedGuid128("RoomGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_ROOM_SET_EDITOR_MODE_ACTIVE_RESPONSE)]
        public static void HandleHousingRoomSetEditorModeActiveResponse(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadByteE<HousingResult>("Result");
            packet.ReadBool("Active");
        }
        
        [Parser(Opcode.SMSG_HOUSING_ROOM_UPDATE_RESULT)]
        public static void HousingRoomUpdateResult(Packet packet)
        {
            packet.ReadPackedGuid128("RoomGUID");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_SERVICES_GET_OTHERS_PLAYER_OWNED_HOUSES_RESPONSE)]
        [Parser(Opcode.SMSG_HOUSING_SERVICES_GET_PLAYER_OWNED_HOUSES_RESPONSE)]
        public static void HandleHousingServiceGetOwnedHousesResponse(Packet packet)
        {
            var count =  packet.ReadUInt32("Count");
            packet.ReadByteE<HousingResult>("Result");
            for (uint i = 0; i < count; i++)
            {
                ReadHouse(packet, i);
            }
        }

        [Parser(Opcode.SMSG_INVALIDATE_NEIGHBORHOOD_NAME)]
        public static void HandleInvalidateNeighborhoodName(Packet packet)
        {
            packet.ReadPackedGuid128("NeighborhoodGUID");
        }

        [Parser(Opcode.SMSG_QUERY_NEIGHBORHOOD_NAME_RESPONSE)]
        public static void HandleQueryNeighborhoodNameResponse(Packet packet)
        {
            packet.ReadPackedGuid128("NeighborhoodGUID");
            packet.ReadBool("Field_08");            
            var nameLen = packet.ReadBits(8);
            packet.ReadWoWString("NeighborhoodName", nameLen);
        }

        [Parser(Opcode.CMSG_HOUSING_DECOR_SET_EDITOR_MODE_ACTIVE)]
        [Parser(Opcode.CMSG_HOUSING_FIXTURE_SET_EDITOR_MODE_ACTIVE)]
        [Parser(Opcode.CMSG_HOUSING_ROOM_SET_EDITOR_MODE_ACTIVE)]
        public static void HandleHousingSetEditorModeActive(Packet packet)
        {
            packet.ReadBool("Active");
        }
        
        [Parser(Opcode.CMSG_HOUSE_INTERIOR_LEAVE_HOUSE)]
        [Parser(Opcode.CMSG_HOUSING_REQUEST_CURRENT_HOUSE_INFO)]
        [Parser(Opcode.CMSG_HOUSING_SERVICES_GET_PLAYER_OWNED_HOUSES)]
        [Parser(Opcode.CMSG_HOUSING_SERVICES_HOUSE_FINDER_REQUEST_NEIGHBORHOODS)]
        public static void HandleHousingNull(Packet packet)
        {
        }
        
        private static void ReadHouse(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadPackedGuid128("HouseGUID", indexes);
            packet.ReadPackedGuid128("OwnerGUID", indexes);
            packet.ReadPackedGuid128("NeighborhoodGUID", indexes);

            packet.ReadByte("PlotID", indexes);
            packet.ReadInt32("AccessFlags", indexes);

            var hasMoveOutTime = packet.ReadBit("HasMoveOutTime", indexes);
            if (hasMoveOutTime)
                packet.ReadTime64("MoveOutTime", indexes);
        }
    }
}
