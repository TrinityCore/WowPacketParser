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
            packet.Translator.ReadByte("Slot Id");
            var actionId = packet.Translator.StartBitStream(0, 4, 7, 2, 5, 3, 1, 6);
            packet.Translator.ParseBitStream(actionId, 7, 3, 0, 2, 1, 4, 5, 6);
            packet.AddValue("Action Id", BitConverter.ToUInt32(actionId, 0));
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(3, 5, 6, 4, 2, 7, 0, 1);
            packet.Translator.ParseBitStream(guid, 7, 6, 4, 0, 3, 1, 2, 5);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_REALM_SPLIT)]
        public static void HandleServerRealmSplit(Packet packet)
        {
            var len = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("Split Date", len);
            packet.Translator.ReadInt32E<ClientSplitState>("Client State");
            packet.Translator.ReadInt32E<PendingSplitState>("Split State");
        }

        [Parser(Opcode.CMSG_INSPECT)]
        public static void HandleClientInspect(Packet packet)
        {
            var guid = new byte[8];

            guid[7] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();


            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.WriteGuid("Player GUID", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_HONOR_STATS)]
        public static void HandleRequestHonorStats(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 3, 6, 0, 1, 5, 4, 7);
            packet.Translator.ParseBitStream(guid, 1, 2, 6, 4, 7, 0, 3, 5);
            packet.Translator.WriteGuid("Player GUID", guid);
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            var state = packet.Translator.ReadInt32E<WeatherState>("State");
            var grade = packet.Translator.ReadSingle("Grade");
            var unk = packet.Translator.ReadBit("Unk bit");

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
            pos.O = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.Translator.ReadInt32<MapId>("Map");
            packet.AddValue("Position", pos);

            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Area Trigger Id");
            packet.Translator.ReadBit("Unk bit1");
            packet.Translator.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.Translator.ReadInt32("Scroll of Resurrections Remaining");
            packet.Translator.ReadInt32("Realm Id?");
            packet.Translator.ReadByte("Complain System Status");
            packet.Translator.ReadInt32("Unused Int32");
            packet.Translator.ReadInt32("Scroll of Resurrections Per Day");
            var sessionTimeAlert = packet.Translator.ReadBit("Session Time Alert");
            packet.Translator.ReadBit("IsVoiceChatAllowedByServer");
            packet.Translator.ReadBit("Scroll of Resurrection Enabled");
            packet.Translator.ReadBit("GMItemRestorationButtonEnabled");
            var quickTicket = packet.Translator.ReadBit("EuropaTicketSystemEnabled");
            packet.Translator.ReadBit("HasTravelPass");
            packet.Translator.ReadBit("Something with web ticket");

            if (quickTicket)
            {
                packet.Translator.ReadInt32("Unk5");
                packet.Translator.ReadInt32("Unk6");
                packet.Translator.ReadInt32("Unk7");
                packet.Translator.ReadInt32("Unk8");
            }

            if (sessionTimeAlert)
            {
                packet.Translator.ReadInt32("Session Alert Period");
                packet.Translator.ReadInt32("Session Alert DisplayTime");
                packet.Translator.ReadInt32("Session Alert Delay");
            }
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY_BLOB)]
        public static void HandleHotfixInfo(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 20);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Hotfixed entry", i);
                packet.Translator.ReadUInt32E<DB2Hash>("Hotfix DB2 File", i);
                packet.Translator.ReadTime("Hotfix date", i);
            }
        }

        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            packet.Translator.ReadBits("Unk Bits", 9);
            packet.Translator.ReadCString("Server Location");
        }

        [Parser(Opcode.CMSG_UNKNOWN_2979)]
        public static void HandleUnknow2979(Packet packet)
        {
            packet.Translator.ReadBits("String length", 9);
            packet.Translator.ReadCString("File");
        }
    }
}
