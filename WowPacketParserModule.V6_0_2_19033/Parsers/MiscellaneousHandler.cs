using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class MiscellaneousHandler
    {
        [HasSniffData]
        [Parser(Opcode.CMSG_LOAD_SCREEN)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.ReadEntry<Int32>(StoreNameType.Map, "Map");
            packet.ReadBit("Loading");

            packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            var state = packet.ReadEnum<WeatherState>("State", TypeCode.Int32);
            var grade = packet.ReadSingle("Intensity");
            var unk = packet.ReadBit("Abrupt"); // Type

            Storage.WeatherUpdates.Add(new WeatherUpdate
            {
                MapId = CoreParsers.MovementHandler.CurrentMapId,
                ZoneId = 0, // fixme
                State = state,
                Grade = grade,
                Unk = unk
            }, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN)]
        public static void HandleFeatureSystemStatusGlueScreen(Packet packet)
        {
            // educated guess order
            packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("CharUndeleteEnabled");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");

            packet.ReadInt32("ScrollOfResurrectionRequestsRemaining");
            packet.ReadInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.ReadInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");

            packet.ResetBitReader();

            packet.ReadBit("VoiceEnabled");
            var bit84 = packet.ReadBit("EuropaTicketSystemStatus");
            packet.ReadBit("ScrollOfResurrectionEnabled");
            packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("ItemRestorationButtonEnabled");
            packet.ReadBit("BrowserEnabled");
            var bit44 = packet.ReadBit("SessionAlert");
            packet.ReadBit("RecruitAFriendSendingEnabled");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("Unk bit21");
            packet.ReadBit("Unk bit22");
            packet.ReadBit("Unk bit90");

            if (bit84)
            {
                packet.ReadBit("Unk bit0");
                packet.ReadBit("Unk bit1");
                packet.ReadBit("TicketSystemEnabled");
                packet.ReadBit("SubmitBugEnabled");

                packet.ReadInt32("MaxTries");
                packet.ReadInt32("PerMilliseconds");
                packet.ReadInt32("TryCount");
                packet.ReadInt32("LastResetTimeBeforeNow");
            }

            if (bit44)
            {
                packet.ReadInt32("Delay");
                packet.ReadInt32("Period");
                packet.ReadInt32("DisplayTime");
            }
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            packet.ReadInt32("DifficultyID");
            packet.ReadByte("IsTournamentRealm");
            packet.ReadTime("WeeklyReset");

            var bit32 = packet.ReadBit();
            var bit20 = packet.ReadBit();
            var bit56 = packet.ReadBit();
            var bit44 = packet.ReadBit();

            if (bit32)
                packet.ReadInt32("IneligibleForLootMask");

            if (bit20)
                packet.ReadInt32("InstanceGroupSize");

            if (bit56)
                packet.ReadInt32("RestrictedAccountMaxLevel");

            if (bit44)
                packet.ReadInt32("RestrictedAccountMaxMoney");
        }

        [Parser(Opcode.CMSG_WHO)]
        public static void HandleWhoRequest(Packet packet)
        {
            var bits728 = packet.ReadBits("WhoWordCount", 4);

            packet.ReadInt32("MinLevel");
            packet.ReadInt32("MaxLevel");
            packet.ReadInt32("RaceFilter");
            packet.ReadInt32("ClassFilter");

            packet.ResetBitReader();

            var bits2 = packet.ReadBits(6);
            var bits57 = packet.ReadBits(9);
            var bits314 = packet.ReadBits(7);
            var bits411 = packet.ReadBits(9);
            var bit169 = packet.ReadBits(3);

            packet.ReadBit("ShowEnemies");
            packet.ReadBit("ShowArenaPlayers");
            packet.ReadBit("ExactName");
            var bit708 = packet.ReadBit("HasServerInfo");

            packet.ReadWoWString("Name", bits2);
            packet.ReadWoWString("VirtualRealmName", bits57);
            packet.ReadWoWString("Guild", bits314);
            packet.ReadWoWString("GuildVirtualRealmName", bits411);

            for (var i = 0; i < bit169; ++i)
            {
                packet.ResetBitReader();
                var bits0 = packet.ReadBits(7);
                packet.ReadWoWString("Word", bits0, i);
            }

            // WhoRequestServerInfo
            if (bit708)
            {
                packet.ReadInt32("FactionGroup");
                packet.ReadInt32("Locale");
                packet.ReadInt32("RequesterVirtualRealmAddress");
            }

            for (var i = 0; i < bits728; ++i)
                packet.ReadEntry<UInt32>(StoreNameType.Area, "Area", i);
        }

        [Parser(Opcode.SMSG_WHO)]
        public static void HandleWho(Packet packet)
        {
            var bits568 = packet.ReadBits("List count", 6);

            for (var i = 0; i < bits568; ++i)
            {
                packet.ResetBitReader();
                packet.ReadBit("IsDeleted", i);
                var bits15 = packet.ReadBits(6);

                var count = new int[5];
                for (var j = 0; j < 5; ++j)
                {
                    packet.ResetBitReader();
                    count[i] = (int)packet.ReadBits(7);
                }

                for (var j = 0; j < 5; ++j)
                    packet.ReadWoWString("DeclinedNames", count[i], i, j);

                packet.ReadPackedGuid128("AccountID", i);
                packet.ReadPackedGuid128("BnetAccountID", i);
                packet.ReadPackedGuid128("GuidActual", i);

                packet.ReadInt32("VirtualRealmAddress", i);

                packet.ReadEnum<Race>("Race", TypeCode.Byte, i);
                packet.ReadEnum<Gender>("Sex", TypeCode.Byte, i);
                packet.ReadEnum<Class>("ClassId", TypeCode.Byte, i);
                packet.ReadByte("Level", i);

                packet.ReadWoWString("Name", bits15, i);

                packet.ReadPackedGuid128("GuildGUID", i);

                packet.ReadInt32("GuildVirtualRealmAddress", i);
                packet.ReadInt32("AreaID", i);

                packet.ResetBitReader();
                var bits460 = packet.ReadBits(7);
                packet.ReadBit("IsGM", i);

                packet.ReadWoWString("GuildName", bits460, i);
            }
        }

        [Parser(Opcode.CMSG_PING)]
        public static void HandleClientPing(Packet packet)
        {
            packet.ReadInt32("Serial");
            packet.ReadInt32("Latency");
        }

        [Parser(Opcode.SMSG_PONG)]
        public static void HandleServerPong(Packet packet)
        {
            packet.ReadInt32("Serial");
        }

        [Parser(Opcode.SMSG_INITIAL_SETUP)]
        public static void HandleInitialSetup(Packet packet)
        {
            var int6 = packet.ReadInt32("QuestsCompletedCount");

            packet.ReadByte("ServerExpansionLevel");
            packet.ReadByte("ServerExpansionTier");

            packet.ReadInt32("ServerRegionID");
            packet.ReadTime("RaidOrigin");

            for (var i = 0; i < int6; ++i)
                packet.ReadByte("QuestsCompleted", i);
        }

        [Parser(Opcode.CMSG_AREATRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.ReadEntry("Area Trigger Id");
            packet.ReadBit("Entered");
            packet.ReadBit("FromClient");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }
    }
}

