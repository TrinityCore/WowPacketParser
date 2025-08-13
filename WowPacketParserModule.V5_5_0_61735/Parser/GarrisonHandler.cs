using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class GarrisonHandler
    {
        private static void ReadGarrisonShipment(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("ShipmentRecID", indexes);
            packet.ReadInt64("ShipmentID", indexes);
            packet.ReadInt64("AssignedFollowerDbID", indexes);
            packet.ReadTime64("CreationTime", indexes);
            packet.ReadInt32("ShipmentDuration", indexes);
            packet.ReadInt32("BuildingType", indexes);
        }

        [Parser(Opcode.SMSG_GET_SHIPMENT_INFO_RESPONSE)]
        public static void HandleGetShipmentInfoResponse(Packet packet)
        {
            packet.ReadBit("Success");

            packet.ReadInt32("ShipmentID");
            packet.ReadInt32("MaxShipments");
            var characterShipmentCount = packet.ReadInt32("CharacterShipmentCount");
            packet.ReadInt32("PlotInstanceID");

            for (int i = 0; i < characterShipmentCount; i++)
                ReadGarrisonShipment(packet, i);
        }

        [Parser(Opcode.SMSG_GET_LANDING_PAGE_SHIPMENTS_RESPONSE)]
        public static void HandleGetLandingPageShipmentsResponse(Packet packet)
        {
            packet.ReadUInt32("UnkUInt32");
            uint shipmentsCount = packet.ReadUInt32("ShipmentsCount");
            for (uint i = 0; i < shipmentsCount; i++)
                ReadGarrisonShipment(packet, "Shipment", i);
        }

        [Parser(Opcode.SMSG_REPLACE_TROPHY_RESPONSE)]
        public static void HandleReplaceTrophyResponse(Packet packet)
        {
            packet.ReadBit("Success");
        }

        [Parser(Opcode.SMSG_GET_TROPHY_LIST_RESPONSE)]
        public static void HandleGetTrophyListResponse(Packet packet)
        {
            packet.ReadBit("Success");
            var trophyCount = packet.ReadInt32("TrophyCount");
            for (int i = 0; i < trophyCount; i++)
            {
                packet.ReadInt32("TrophyID", i);
                packet.ReadUInt32E<TrophyLockCode>("LockCode", i);
                packet.ReadInt32("AchievementRequired", i);
            }
        }

        [Parser(Opcode.SMSG_GET_SELECTED_TROPHY_ID_RESPONSE)]
        public static void HandleGetSelectedTrophyIDResponse(Packet packet)
        {
            packet.ReadInt32("TrophyID");
            packet.ReadBit("Success");
        }

        [Parser(Opcode.SMSG_COVENANT_RENOWN_SEND_CATCHUP_STATE)]
        public static void HandleGarrisonCovenantRenownSendCatchupState(Packet packet)
        {
            packet.ReadBit("CatchupState");
        }
    }
}
