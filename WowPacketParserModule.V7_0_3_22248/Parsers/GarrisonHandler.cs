using System.Reflection.Emit;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class GarrisonHandler
    {
        public static void ReadGarrisonMission(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadUInt64("DbID", indexes);
            packet.Translator.ReadUInt32("MissionRecID", indexes);
            packet.Translator.ReadTime("OfferTime", indexes);
            packet.Translator.ReadUInt32("OfferDuration", indexes);
            packet.Translator.ReadTime("StartTime", indexes);
            packet.Translator.ReadUInt32("TravelDuration", indexes);
            packet.Translator.ReadUInt32("MissionDuration", indexes);
            packet.Translator.ReadUInt32("MissionState", indexes);
            packet.Translator.ReadUInt32("Unknown1", indexes);
            packet.Translator.ReadUInt32("Unknown2", indexes);
        }

        public static void ReadGarrisonMissionOvermaxRewards(Packet packet, params object[] indexes)
        {
            var missionRewardCount = packet.Translator.ReadInt32("MissionRewardCount", indexes);
            for (int i = 0; i < missionRewardCount; i++)
            {
                packet.Translator.ReadInt32<ItemId>("ItemID", indexes, i);
                packet.Translator.ReadUInt32("Quantity", indexes, i);
                packet.Translator.ReadInt32("CurrencyID", indexes, i);
                packet.Translator.ReadUInt32("CurrencyQuantity", indexes, i);
                packet.Translator.ReadUInt32("FollowerXP", indexes, i);
                packet.Translator.ReadUInt32("BonusAbilityID", indexes, i);
                packet.Translator.ReadInt32("Unknown", indexes, i);
            }
        }

        public static void ReadGarrisonBuildingInfo(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadUInt32("GarrPlotInstanceID", indexes);
            packet.Translator.ReadUInt32("GarrBuildingID", indexes);
            packet.Translator.ReadUInt32("TimeBuilt", indexes);
            packet.Translator.ReadUInt32("CurrentGarSpecID", indexes);
            packet.Translator.ReadUInt32("TimeSpecCooldown", indexes);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Active", indexes);
        }

        public static void ReadGarrisonFollower(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadUInt64("DbID", indexes);
            packet.Translator.ReadUInt32("GarrFollowerID", indexes);
            packet.Translator.ReadUInt32("Quality", indexes);
            packet.Translator.ReadUInt32("FollowerLevel", indexes);
            packet.Translator.ReadUInt32("ItemLevelWeapon", indexes);
            packet.Translator.ReadUInt32("ItemLevelArmor", indexes);
            packet.Translator.ReadUInt32("Xp", indexes);
            packet.Translator.ReadUInt32("Durability", indexes);
            packet.Translator.ReadUInt32("CurrentBuildingID", indexes);
            packet.Translator.ReadUInt32("CurrentMissionID", indexes);
            var abilityCount = packet.Translator.ReadInt32("AbilityCount", indexes);
            packet.Translator.ReadInt32("ZoneSupportSpellID", indexes);
            packet.Translator.ReadInt32("FollowerStatus", indexes);

            for (int i = 0; i < abilityCount; i++)
                packet.Translator.ReadInt32("AbilityID", indexes, i);

            packet.Translator.ResetBitReader();

            var len = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("CustomName", len, indexes);
        }

        public static void ReadGarrisonTalents(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("GarrTalentID", indexes);
            packet.Translator.ReadInt32("ResearchStartTime", indexes);
            packet.Translator.ReadInt32("Flags", indexes);
        }

        [Parser(Opcode.SMSG_DISPLAY_TOAST)]
        public static void HandleDisplayToast(Packet packet)
        {
            packet.Translator.ReadUInt64("Quantity");

            packet.Translator.ReadByte("DisplayToastMethod");
            packet.Translator.ReadUInt32("QuestID");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Mailed");

            var type = packet.Translator.ReadBits("Type", 2);
            if (type == 0)
            {
                packet.Translator.ReadBit("BonusRoll");
                V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet);
                packet.Translator.ReadInt32("SpecializationID");
                packet.Translator.ReadInt32("ItemQuantity?");
            }

            if (type == 1)
                packet.Translator.ReadInt32("CurrencyID");
        }

        [Parser(Opcode.SMSG_GET_GARRISON_INFO_RESULT)]
        public static void HandleGetGarrisonInfoResult(Packet packet)
        {
            packet.Translator.ReadInt32("FactionIndex");
            var garrisonCount = packet.Translator.ReadUInt32("GarrisonCount");

            for (int i = 0; i < garrisonCount; i++)
            {
                packet.Translator.ReadInt32("GarrTypeID", i);
                packet.Translator.ReadInt32("GarrSiteID", i);
                packet.Translator.ReadInt32("GarrSiteLevelID", i);

                var garrisonBuildingInfoCount = packet.Translator.ReadUInt32("GarrisonBuildingInfoCount", i);
                var garrisonPlotInfoCount = packet.Translator.ReadUInt32("GarrisonPlotInfoCount", i);
                var garrisonFollowerCount = packet.Translator.ReadUInt32("GarrisonFollowerCount", i);
                var garrisonMissionCount = packet.Translator.ReadUInt32("GarrisonMissionCount", i);
                var garrisonMissionRewardsCount = packet.Translator.ReadUInt32("GarrisonMissionRewardsCount", i);
                var garrisonMissionOvermaxRewardsCount = packet.Translator.ReadUInt32("GarrisonMissionOvermaxRewardsCount", i);
                var areaBonusCount = packet.Translator.ReadUInt32("GarrisonMissionAreaBonusCount", i);
                var talentsCount = packet.Translator.ReadUInt32("Talents", i);
                var canStartMissionCount = packet.Translator.ReadUInt32("CanStartMission", i);
                var archivedMissionsCount = packet.Translator.ReadUInt32("ArchivedMissionsCount", i);

                packet.Translator.ReadInt32("NumFollowerActivationsRemaining", i);
                packet.Translator.ReadUInt32("NumMissionsStartedToday", i);

                for (int j = 0; j < garrisonBuildingInfoCount; j++)
                    ReadGarrisonBuildingInfo(packet, "BuildingInfo", i, j);

                for (int j = 0; j < garrisonPlotInfoCount; j++)
                    V6_0_2_19033.Parsers.GarrisonHandler.ReadGarrisonPlotInfo(packet, "PlotInfo", i, j);

                for (int j = 0; j < garrisonFollowerCount; j++)
                    ReadGarrisonFollower(packet, "Follower", i, j);

                for (int j = 0; j < garrisonMissionCount; j++)
                    ReadGarrisonMission(packet, "Mission", i, j);

                for (int j = 0; j < garrisonMissionRewardsCount; j++)
                    ReadGarrisonMissionOvermaxRewards(packet, "MissionRewards", i, j);

                for (int j = 0; j < garrisonMissionOvermaxRewardsCount; j++)
                    ReadGarrisonMissionOvermaxRewards(packet, "MissionOvermaxRewards", i, j);

                for (int j = 0; j < areaBonusCount; j++)
                    V6_0_2_19033.Parsers.GarrisonHandler.ReadGarrisonMissionAreaBonus(packet, "MissionAreaBonus", i, j);

                for (int j = 0; j < talentsCount; j++)
                    ReadGarrisonTalents(packet, "Talents", i, j);

                for (int j = 0; j < archivedMissionsCount; j++)
                    packet.Translator.ReadInt32("ArchivedMissions", i, j);

                packet.Translator.ResetBitReader();

                for (int j = 0; j < canStartMissionCount; j++)
                    packet.Translator.ReadBit("CanStartMission", i, j);
            }
        }
    }
}
