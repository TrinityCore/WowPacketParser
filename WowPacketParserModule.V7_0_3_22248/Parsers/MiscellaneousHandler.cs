using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
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
            packet.ReadBit("NoHandler"); // not accessed in handler
            packet.ReadBit("TrialBoostEnabled");

            packet.ReadInt32("TokenPollTimeSeconds");
            packet.ReadInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
        }

        [Parser(Opcode.SMSG_INITIAL_SETUP)]
        public static void HandleInitialSetup(Packet packet)
        {
            packet.ReadByte("ServerExpansionLevel");
            packet.ReadByte("ServerExpansionTier");
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            packet.ReadInt32("DifficultyID");
            packet.ReadByte("IsTournamentRealm");

            packet.ReadBit("XRealmPvpAlert");
            var hasRestrictedAccountMaxLevel = packet.ReadBit("HasRestrictedAccountMaxLevel");
            var hasRestrictedAccountMaxMoney = packet.ReadBit("HasRestrictedAccountMaxMoney");
            var hasInstanceGroupSize = packet.ReadBit("HasInstanceGroupSize");

            if (hasRestrictedAccountMaxLevel)
                packet.ReadInt32("RestrictedAccountMaxLevel");

            if (hasRestrictedAccountMaxMoney)
                packet.ReadInt32("RestrictedAccountMaxMoney");

            if (hasInstanceGroupSize)
                packet.ReadInt32("InstanceGroupSize");
        }
    }
}
