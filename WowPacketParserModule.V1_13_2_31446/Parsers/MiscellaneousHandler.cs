using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN)]
        public static void HandleFeatureSystemStatusGlueScreen(Packet packet)
        {
            packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("CommerceSystemEnabled");
            packet.ReadBit("Unk14");
            packet.ReadBit("WillKickFromWorld");
            packet.ReadBit("IsExpansionPreorderInStore");
            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("IsCompetitiveModeEnabled");
            packet.ReadBit("NoHandler"); // not accessed in handler
            packet.ReadBit("TrialBoostEnabled");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("LiveRegionCharacterListEnabled");
            packet.ReadBit("LiveRegionCharacterCopyEnabled");
            packet.ReadBit("LiveRegionAccountCopyEnabled");
            packet.ReadBit("UnkBit1");
            var unkBit2 = packet.ReadBit("UnkBit2");

            packet.ReadUInt32("TokenPollTimeSeconds");
            packet.ReadUInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
            packet.ReadInt64("TokenBalanceAmount");
            packet.ReadInt32("MaxCharactersPerRealm");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V1_13_3_32790)) // no idea when this was added exactly
                packet.ReadInt32("UnkInt");

            packet.ReadUInt32("BpayStoreProductDeliveryDelay");
            packet.ReadInt32("ActiveCharacterUpgradeBoostType");
            packet.ReadInt32("ActiveClassTrialBoostType");
            packet.ReadInt32("MinimumExpansionLevel");
            packet.ReadInt32("MaximumExpansionLevel");

            if (unkBit2)
                packet.ReadInt32("UnkBit2_Int32");
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG)]
        public static void HandleTutorialFlag620(Packet packet)
        {
            var action = packet.ReadBitsE<TutorialAction>("TutorialAction", 2);

            if (action == TutorialAction.Update)
                packet.ReadInt32E<Tutorial>("TutorialBit");
        }

        [Parser(Opcode.CMSG_REPORT_KEYBINDING_EXECUTION_COUNTS)]
        public static void HandleReportKeybindingExecutionCounts(Packet packet)
        {
            var count = packet.ReadBits("KeyBindingsCount", 10);
            packet.ResetBitReader();

            for (var i = 0; i < count; i++)
            {
                var len1 = packet.ReadBits(6);
                var len2 = packet.ReadBits(6);
                packet.ResetBitReader();
                packet.ReadUInt32("ExecutionCount", i);
                packet.ReadWoWString("Key", len1, i);
                packet.ReadWoWString("Action", len2, i);
            }
        }

        [Parser(Opcode.CMSG_WHO)]
        public static void HandleWhoRequest(Packet packet)
        {
            var areaCount = packet.ReadBits(4);

            packet.ReadInt32("MinLevel");
            packet.ReadInt32("MaxLevel");
            packet.ReadInt64("RaceFilter");
            packet.ReadInt32("ClassFilter");

            packet.ResetBitReader();

            var nameLen = packet.ReadBits(6);
            var virtualRealmNameLen = packet.ReadBits(9);
            var guildLen = packet.ReadBits(7);
            var guildVirtualRealmNameLen = packet.ReadBits(9);
            var wordCount = packet.ReadBits(3);

            packet.ReadBit("ShowEnemies");
            packet.ReadBit("ShowArenaPlayers");
            packet.ReadBit("ExactName");
            var hasServerInfo = packet.ReadBit("HasServerInfo");
            packet.ResetBitReader();

            for (var i = 0; i < wordCount; ++i)
            {
                var bits0 = packet.ReadBits(7);
                packet.ReadWoWString("Word", bits0, i);
                packet.ResetBitReader();
            }

            packet.ReadWoWString("Name", nameLen);
            packet.ReadWoWString("VirtualRealmName", virtualRealmNameLen);
            packet.ReadWoWString("Guild", guildLen);
            packet.ReadWoWString("GuildVirtualRealmName", guildVirtualRealmNameLen);

            // WhoRequestServerInfo
            if (hasServerInfo)
            {
                packet.ReadInt32("FactionGroup");
                packet.ReadInt32("Locale");
                packet.ReadInt32("RequesterVirtualRealmAddress");
            }

            for (var i = 0; i < areaCount; ++i)
                packet.ReadUInt32<AreaId>("Area", i);
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            CoreParsers.MovementHandler.CurrentDifficultyID = packet.ReadUInt32<DifficultyId>("DifficultyID");
            packet.ReadByte("IsTournamentRealm");

            packet.ReadBit("XRealmPvpAlert");
            var hasRestrictedAccountMaxLevel = packet.ReadBit("HasRestrictedAccountMaxLevel");
            var hasRestrictedAccountMaxMoney = packet.ReadBit("HasRestrictedAccountMaxMoney");
            var hasInstanceGroupSize = packet.ReadBit("HasInstanceGroupSize");

            if (hasRestrictedAccountMaxLevel)
                packet.ReadUInt32("RestrictedAccountMaxLevel");

            if (hasRestrictedAccountMaxMoney)
                packet.ReadUInt64("RestrictedAccountMaxMoney");

            if (hasInstanceGroupSize)
                packet.ReadUInt32("InstanceGroupSize");
        }
    }
}
