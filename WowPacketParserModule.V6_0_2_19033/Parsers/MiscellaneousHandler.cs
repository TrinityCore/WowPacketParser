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
        [Parser(Opcode.SMSG_INVALIDATE_PLAYER)]
        public static void HandleReadGuid(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOAD_SCREEN)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.ReadEntry<Int32>(StoreNameType.Map, "MapID");
            packet.ReadBit("Showing");

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

                var declinedNamesLen = new int[5];
                for (var j = 0; j < 5; ++j)
                {
                    packet.ResetBitReader();
                    declinedNamesLen[j] = (int)packet.ReadBits(7);
                }

                for (var j = 0; j < 5; ++j)
                    packet.ReadWoWString("DeclinedNames", declinedNamesLen[j], i, j);

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

        [Parser(Opcode.SMSG_ACCOUNT_MOUNT_UPDATE)]
        public static void HandleAccountMountUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            var int32 = packet.ReadInt32("MountSpellIDsCount");
            var int16 = packet.ReadInt32("MountIsFavoriteCount");

            for (int i = 0; i < int32; i++)
                packet.ReadInt32("MountSpellIDs", i);

            packet.ResetBitReader();

            for (int i = 0; i < int16; i++)
                packet.ReadBit("MountIsFavorite", i);
        }

        [Parser(Opcode.SMSG_ACCOUNT_TOYS_UPDATE)]
        public static void HandleAccountToysUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            var int32 = packet.ReadInt32("ToySpellIDsCount");
            var int16 = packet.ReadInt32("ToyIsFavoriteCount");

            for (int i = 0; i < int32; i++)
                packet.ReadInt32("ToySpellIDs", i);

            packet.ResetBitReader();

            for (int i = 0; i < int16; i++)
                packet.ReadBit("ToyIsFavorite", i);
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            var sound = packet.ReadUInt32("SoundKitID");
            packet.ReadPackedGuid128("SourceObjectGUID");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_RANDOM_ROLL)]
        public static void HandleRandomRoll(Packet packet)
        {
            packet.ReadInt32("Min");
            packet.ReadInt32("Max");
            packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_ZONE_UNDER_ATTACK)]
        public static void HandleZoneUpdate(Packet packet)
        {
            packet.ReadEntry<Int32>(StoreNameType.Zone, "AreaID");
        }

        [Parser(Opcode.CMSG_PAGE_TEXT_QUERY)]
        public static void HandlePageTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("Guid");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            var pageText = new PageText();

            packet.ReadUInt32("Entry");
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var entry = packet.ReadUInt32("Entry");
            pageText.NextPageID = packet.ReadUInt32("Next Page");

            packet.ResetBitReader();
            var textLen = packet.ReadBits(12);
            pageText.Text = packet.ReadWoWString("Page Text", textLen);

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(entry, pageText, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_SET_AI_ANIM_KIT)]
        public static void HandleSetAIAnimKit(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadUInt16("AnimKitID");
        }

        [Parser(Opcode.SMSG_DISPLAY_PROMOTION)]
        public static void HandleDisplayPromotion(Packet packet)
        {
            packet.ReadUInt32("PromotionID");
        }

        [Parser(Opcode.SMSG_SET_ALL_TASK_PROGRESS)]
        [Parser(Opcode.SMSG_UPDATE_TASK_PROGRESS)]
        public static void HandleSetAllTaskProgress(Packet packet)
        {
            var int4 = packet.ReadInt32("TaskProgressCount");
            for (int i = 0; i < int4; i++)
            {
                packet.ReadUInt32("TaskID", i);
                packet.ReadUInt32("FailureTime", i);
                packet.ReadUInt32("Flags", i);

                var int3 = packet.ReadInt32("ProgressCounts", i);
                for (int j = 0; j < int3; j++)
                    packet.ReadInt16("Counts", i, j);
            }
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG)]
        public static void HandleTutorialFlag(Packet packet)
        {
            var bits16 = packet.ReadBits("TutorialAction", 2); // 0 == Update, 1 == Clear?, 2 == Reset

            if (bits16 == 0)
                packet.ReadInt32("TutorialBit");
        }

        [Parser(Opcode.SMSG_START_ELAPSED_TIMERS)]
        public static void HandleStartElapsedTimers(Packet packet)
        {
            var int3 = packet.ReadInt32("ElaspedTimerCounts");
            for (int i = 0; i < int3; i++)
            {
                packet.ReadUInt32("TimerID", i);
                packet.ReadTime("CurrentDuration", i);
            }
        }
    }
}

