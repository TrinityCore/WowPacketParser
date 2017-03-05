using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.CMSG_SET_ACTION_BUTTON)]
        public static void HandleSetActionButton(Packet packet)
        {
            packet.ReadByte("Slot Id");
            var actionId = packet.StartBitStream(0, 4, 7, 2, 5, 3, 1, 6);
            packet.ParseBitStream(actionId, 7, 3, 0, 2, 1, 4, 5, 6);
            packet.AddValue("Action Id", BitConverter.ToUInt32(actionId, 0));
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.StartBitStream(3, 5, 6, 4, 2, 7, 0, 1);
            packet.ParseBitStream(guid, 7, 6, 4, 0, 3, 1, 2, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_REALM_SPLIT)]
        public static void HandleServerRealmSplit(Packet packet)
        {
            var len = packet.ReadBits(7);
            packet.ReadWoWString("Split Date", len);
            packet.ReadInt32E<ClientSplitState>("Client State");
            packet.ReadInt32E<PendingSplitState>("Split State");
        }

        [Parser(Opcode.CMSG_INSPECT)]
        public static void HandleClientInspect(Packet packet)
        {
            var guid = new byte[8];

            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();


            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Player GUID", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_HONOR_STATS)]
        public static void HandleRequestHonorStats(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 3, 6, 0, 1, 5, 4, 7);
            packet.ParseBitStream(guid, 1, 2, 6, 4, 7, 0, 3, 5);
            packet.WriteGuid("Player GUID", guid);
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            var state = packet.ReadInt32E<WeatherState>("State");
            var grade = packet.ReadSingle("Grade");
            var unk = packet.ReadBit("Unk bit");

            Storage.WeatherUpdates.Add(new WeatherUpdate
            {
                MapId = CoreParsers.MovementHandler.CurrentMapId,
                ZoneId = 0, // fixme
                State = state,
                Grade = grade,
                Unk = unk
            }, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld434(Packet packet)
        {
            var pos = new Vector4();
            pos.O = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");
            packet.AddValue("Position", pos);

            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.ReadEntry("Area Trigger Id");
            packet.ReadBit("Unk bit1");
            packet.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadInt32("Scroll of Resurrections Remaining");
            packet.ReadInt32("Realm Id?");
            packet.ReadByte("Complain System Status");
            packet.ReadInt32("Unused Int32");
            packet.ReadInt32("Scroll of Resurrections Per Day");
            var sessionTimeAlert = packet.ReadBit("Session Time Alert");
            packet.ReadBit("IsVoiceChatAllowedByServer");
            packet.ReadBit("Scroll of Resurrection Enabled");
            packet.ReadBit("GMItemRestorationButtonEnabled");
            var quickTicket = packet.ReadBit("EuropaTicketSystemEnabled");
            packet.ReadBit("HasTravelPass");
            packet.ReadBit("Something with web ticket");

            if (quickTicket)
            {
                packet.ReadInt32("Unk5");
                packet.ReadInt32("Unk6");
                packet.ReadInt32("Unk7");
                packet.ReadInt32("Unk8");
            }

            if (sessionTimeAlert)
            {
                packet.ReadInt32("Session Alert Period");
                packet.ReadInt32("Session Alert DisplayTime");
                packet.ReadInt32("Session Alert Delay");
            }
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY_BLOB)]
        public static void HandleHotfixInfo(Packet packet)
        {
            var count = packet.ReadBits("Count", 20);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Hotfixed entry", i);
                packet.ReadUInt32E<DB2Hash>("Hotfix DB2 File", i);
                packet.ReadTime("Hotfix date", i);
            }
        }

        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            packet.ReadBits("Unk Bits", 9);
            packet.ReadCString("Server Location");
        }

        [Parser(Opcode.CMSG_UNKNOWN_2979)]
        public static void HandleUnknow2979(Packet packet)
        {
            packet.ReadBits("String length", 9);
            packet.ReadCString("File");
        }
    }
}
