using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class HousingHandler
    {
        [Parser(Opcode.CMSG_HOUSING_DECOR_REQUEST_STORAGE)]
        public static void HousingDecorRequestStorage(Packet packet)
        {
            packet.ReadPackedGuid128("BnetAccountID");
        }
        
        [Parser(Opcode.CMSG_HOUSING_DECOR_DELETE_FROM_STORAGE)]
        public static void HandleHousingDecorDeleteFromStorage(Packet packet)
        {
            packet.ReadPackedGuid128("BnetAccountID");
            packet.ReadUInt16("CatalogEntryID");
            packet.ReadUInt32("Field_10");
        }

        [Parser(Opcode.CMSG_HOUSING_DECOR_DELETE_FROM_STORAGE_BY_ID)]
        public static void HandleHousingDecorDeleteFromStorageById(Packet packet)
        {
            packet.ReadUInt16("CatalogEntryID");
            packet.ReadUInt16("Field_4");
        }
        
        [Parser(Opcode.CMSG_HOUSING_DECOR_SET_DYE_SLOTS)]
        public static void HousingDecorSetDyeSlots(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            for (var i = 0; i < 3; ++i)
            {
                packet.ReadInt32("DyeColorID", i);
            }
        }

        [Parser(Opcode.CMSG_HOUSING_DECOR_MOVE)]
        public static void HandleHousingDecorMove(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadVector3("Position");
            packet.ReadVector3("Rotation");
            packet.ReadSingle("Scale");
            packet.ReadPackedGuid128("ParentDecorGUID");
            packet.ReadPackedGuid128("RoomGUID");
            packet.ReadPackedGuid128("ParentHouseFixtureGUID");
            packet.ReadInt32("PlacedComponentID");
            packet.ReadByte("AddedFlags");
            packet.ReadByte("RemovedFlags");
            packet.ReadBit("IncludeChildren");
        }
        
        [Parser(Opcode.CMSG_HOUSING_DECOR_REMOVE)]
        public static void HousingDecorRemove(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
        }

        [Parser(Opcode.CMSG_HOUSING_DECOR_LOCK)]
        public static void HousingDecorLock(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadBool("Lock");
        }
        
        [Parser(Opcode.CMSG_HOUSING_DECOR_PLACE)]
        public static void HousingDecorPlace(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadVector3("Position");
            packet.ReadVector3("Rotation");
            packet.ReadSingle("Scale");
            packet.ReadPackedGuid128("AttachParentGUID");
            packet.ReadPackedGuid128("RoomGUID");
            packet.ReadByte("Field_61");
            packet.ReadByte("Field_62");
            packet.ReadInt32("Field_63");
        }
        
        [Parser(Opcode.CMSG_HOUSING_DECOR_REDEEM_DEFERRED_DECOR)]
        public static void HousingDecorRedeemDeferredDecor(Packet packet)
        {
            packet.ReadUInt32("CatalogEntryID");
            packet.ReadUInt32("Field_4");
        }
        
        [Parser(Opcode.CMSG_HOUSING_GET_PLAYER_PERMISSIONS)]
        public static void HousingGetPlayerPermission(Packet packet)
        {
            packet.ReadByte("Field_0");
            packet.ReadPackedGuid128("HouseGUID");
        }
        
        [Parser(Opcode.CMSG_HOUSING_ROOM_REMOVE)]
        public static void HandleHousingRoomRemove(Packet packet)
        {
            packet.ReadPackedGuid128("RoomGUID");
        }
        
        [Parser(Opcode.CMSG_HOUSING_ROOM_ROTATE)]
        public static void HousingRoomRotate(Packet packet)
        {
            packet.ReadPackedGuid128("RoomGUID");
            packet.ReadBool("IsLeft");
        }
        
        [Parser(Opcode.CMSG_HOUSING_SVCS_PLAYER_VIEW_HOUSES_BY_PLAYER)]
        public static void HandleHousingSvcsPlayerViewHousesByPlayer(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
        }
        
        [Parser(Opcode.CMSG_HOUSING_SVCS_GET_POTENTIAL_HOUSE_OWNERS)]
        public static void HandleHousingSvcsGetPotentialHouseOwners(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
        }
        
        [Parser(Opcode.CMSG_HOUSING_SVCS_GET_BNET_FRIEND_NEIGHBORHOODS)]
        public static void HandleHousingSvcsGetBneFriendNeighborhoods(Packet packet)
        {
            packet.ReadPackedGuid128("BNetAccountGUID");
        }

        [Parser(Opcode.CMSG_NEIGHBORHOOD_OPEN_CORNERSTONE_UI)]
        public static void HandleNeighborhoodOpenCornerstoneUi(Packet packet)
        {
            packet.ReadUInt32("PlotID");
            packet.ReadPackedGuid128("CornerstoneGUID");
        }
        
        [Parser(Opcode.CMSG_QUERY_NEIGHBORHOOD_INFO)]
        public static void HandleQueryNeighborhoodInfo(Packet packet)
        {
            packet.ReadPackedGuid128("NeighborhoodGUID");
        }
        
        [Parser(Opcode.SMSG_NEIGHBORHOOD_PLAYER_ENTER_PLOT)]
        public static void HandleNeighborhoodPlayerEnterPlot(Packet packet)
        {
            packet.ReadPackedGuid128("AreaTriggerGuid");
        }
        
        [Parser(Opcode.SMSG_HOUSING_GET_CURRENT_HOUSE_INFO_RESPONSE)]
        public static void HandleHousingGetCurrentHouseInfoResponse(Packet packet)
        {
            ReadHouse(packet, "House");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_GET_PLAYER_PERMISSIONS_RESPONSE)]
        public static void HousingGetPlayerPermissionResponse(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadByteE<HousingResult>("Result");
            packet.ReadByte("Field_09");
        }
        
        [Parser(Opcode.SMSG_HOUSING_DECOR_REQUEST_STORAGE_RESPONSE)]
        public static void HousingDecorRequestStorageResponse(Packet packet)
        {
            packet.ReadPackedGuid128("BnetAccountID");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_DECOR_SYSTEM_SET_DYE_SLOTS_RESPONSE)]
        public static void HandleHousingDecorSystemSetDyeSlotsResponse(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_DECOR_MOVE_RESPONSE)]
        public static void HandleHousingDecorMoveResponse(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadUInt32("Field_09");
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadByteE<HousingResult>("Result");
            packet.ReadByte("Field_26");
        }
        
        [Parser(Opcode.SMSG_HOUSING_DECOR_REMOVE_RESPONSE)]
        public static void HandleHousingDecorRemoveResponse(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadPackedGuid128("UnknownGUID");
            packet.ReadUInt32("Field_32");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_DECOR_LOCK_RESPONSE)]
        public static void HandleHousingDecorLockResponse(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadUInt32("Field_16");
            packet.ReadByteE<HousingResult>("Result");
            packet.ReadBit("Locked");
            packet.ReadBit("Field_17");
        }

        [Parser(Opcode.SMSG_HOUSING_DECOR_SET_EDIT_MODE_RESPONSE)]
        public static void HandleHousingDecorSetEditModeResponse(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadPackedGuid128("BNetAccountGUID");
            var allowedEditorCount = packet.ReadUInt32("AllowedEditorCount");
            packet.ReadByteE<HousingResult>("Result");

            for (var i = 0; i < allowedEditorCount; ++i)
                packet.ReadPackedGuid128("AllowedEditor", i);
        }

        [Parser(Opcode.SMSG_HOUSING_REDEEM_DEFERRED_DECOR_RESPONSE)]
        public static void HandleHousingRedeemDeferredDecorResponse(Packet packet)
        {
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadByteE<HousingResult>("Result");
            packet.ReadUInt32("Field_13");
        }

        [Parser(Opcode.SMSG_HOUSING_DECOR_PLACE_RESPONSE)]
        public static void HandleHousingDecorPlaceResponse(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadUInt32("Field_09");
            packet.ReadPackedGuid128("DecorGUID");
            packet.ReadByteE<HousingResult>("Result");
        }

        [Parser(Opcode.SMSG_HOUSE_EXTERIOR_LOCK_RESPONSE)]
        public static void HandleHousingExteriorLockResponse(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadByteE<HousingResult>("Result");
            packet.ReadBit("IsLocked");
        }

        [Parser(Opcode.SMSG_HOUSING_FIXTURE_SET_EDIT_MODE_RESPONSE)]
        public static void HandleHousingFixtureSetEditModeResponse(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadPackedGuid128("BNetAccountGUID");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_ROOM_REMOVE_RESPONSE)]
        public static void HandleHousingRoomRemoveResponse(Packet packet)
        {
            packet.ReadPackedGuid128("RoomGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_ROOM_SET_LAYOUT_EDIT_MODE_RESPONSE)]
        public static void HandleHousingRoomSetLayoutEditModeResponse(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadByteE<HousingResult>("Result");
            packet.ReadBool("Active");
        }
        
        [Parser(Opcode.SMSG_HOUSING_ROOM_UPDATE_RESPONSE)]
        public static void HousingRoomUpdateResponse(Packet packet)
        {
            packet.ReadPackedGuid128("RoomGUID");
            packet.ReadByteE<HousingResult>("Result");
        }
        
        [Parser(Opcode.SMSG_HOUSING_HOUSE_STATUS_RESPONSE)]
        public static void HandleHousingHouseStatusResponse(Packet packet)
        {
            packet.ReadPackedGuid128("HouseGUID");
            packet.ReadPackedGuid128("BnetAccountID");
            packet.ReadPackedGuid128("OwnerGUID");
            packet.ReadUInt32("Field_024");
        }
        
        [Parser(Opcode.SMSG_HOUSING_SVCS_PLAYER_VIEW_HOUSES_RESPONSE)]
        [Parser(Opcode.SMSG_HOUSING_SVCS_GET_PLAYER_HOUSES_INFO_RESPONSE)]
        public static void HandleHousingSvcsGetHousesInfoResponse(Packet packet)
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
            bool result = packet.ReadBool("Result");
            if (!result)
                return;
            
            var nameLen = packet.ReadBits(8);
            packet.ReadWoWString("NeighborhoodName", nameLen);
        }

        [Parser(Opcode.CMSG_HOUSING_DECOR_SET_EDIT_MODE)]
        [Parser(Opcode.CMSG_HOUSING_FIXTURE_SET_EDIT_MODE)]
        [Parser(Opcode.CMSG_HOUSING_ROOM_SET_LAYOUT_EDIT_MODE)]
        public static void HandleHousingSetEditMode(Packet packet)
        {
            packet.ReadBool("Active");
        }
        
        [Parser(Opcode.CMSG_HOUSING_HOUSE_STATUS)]
        [Parser(Opcode.CMSG_HOUSE_INTERIOR_LEAVE_HOUSE)]
        [Parser(Opcode.CMSG_HOUSING_GET_CURRENT_HOUSE_INFO)]
        [Parser(Opcode.CMSG_HOUSING_SVCS_GET_PLAYER_HOUSES_INFO)]
        [Parser(Opcode.CMSG_HOUSING_SVCS_GET_HOUSE_FINDER_INFO)]
        [Parser(Opcode.SMSG_NEIGHBORHOOD_PLAYER_LEAVE_PLOT)]
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
