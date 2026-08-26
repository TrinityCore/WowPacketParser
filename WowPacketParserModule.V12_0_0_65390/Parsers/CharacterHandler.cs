using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.V10_0_0_46181.Parsers;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class CharacterHandler
    {
        public static void ReadClassUnlockData(Packet packet, params object[] idx)
        {
            packet.ReadSByteE<Class>("RaceID", idx);
            packet.ReadUInt32("AchievementID", idx);

            packet.ResetBitReader();
            packet.ReadBit("HasExpansion", idx);
            packet.ReadBit("HasUnlockedAchievement", idx);
            packet.ReadBit("HasEntitlement", idx);
        }

        public static void ReadRaceUnlockData(Packet packet, params object[] idx)
        {
            packet.ReadSByteE<Race>("RaceID", idx);

            var classCount = packet.ReadUInt32();
            for (var i = 0u; i < classCount; ++i)
                ReadClassUnlockData(packet, idx, "ClassUnlocks");

            packet.ResetBitReader();
            packet.ReadBit("HasExpansion", idx);
            packet.ReadBit("HasUnlockedAchievement", idx);
            packet.ReadBit("HasHeritageArmorUnlockAchievement", idx);
            packet.ReadBit("HasEntitlement", idx);
            packet.ReadBit("HideRaceOnClient", idx);
            packet.ReadBit("FactionBalanceDisabled", idx);
            packet.ReadBit("DoesNotHaveAvailableClasses", idx);
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleEnumCharactersResult(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadBit("Realmless");
            packet.ReadBit("IsDeletedCharacters");
            packet.ReadBit("IsNewPlayerRestrictionSkipped");
            packet.ReadBit("IsNewPlayerRestricted");
            packet.ReadBit("IsNewPlayer");
            packet.ReadBit("IsTrialAccountRestricted");
            packet.ReadBit("IsAccountLapsedPlayer");
            var hasDisabledClassesMask = packet.ReadBit("HasDisabledClassesMask");
            packet.ReadBit("ForceCharacterListSort");

            var charsCount = packet.ReadUInt32("CharactersCount");
            var regionwideCharsCount = packet.ReadUInt32("RegionwideCharactersCount");
            packet.ReadInt32("MaxCharacterLevel");
            var raceUnlockCount = packet.ReadUInt32("RaceUnlockCount");
            var unlockedConditionalAppearanceCount = packet.ReadUInt32("UnlockedConditionalAppearanceCount");
            var raceLimitDisablesCount = packet.ReadUInt32("RaceLimitDisablesCount");
            var warbandGroupsCount = packet.ReadUInt32("WarbandGroupsCount");

            if (hasDisabledClassesMask)
                packet.ReadUInt32("DisabledClassesMask");

            for (var i = 0u; i < charsCount; ++i)
                V11_0_0_55666.Parsers.CharacterHandler.ReadCharacterListEntry(packet, i, "Characters");

            for (var i = 0u; i < regionwideCharsCount; ++i)
                V11_0_0_55666.Parsers.CharacterHandler.ReadRegionwideCharacterListEntry(packet, i, "RegionwideCharacters");

            for (var i = 0u; i < raceUnlockCount; ++i)
                ReadRaceUnlockData(packet, i, "RaceUnlockData");

            for (var i = 0u; i < unlockedConditionalAppearanceCount; ++i)
                V8_0_1_27101.Parsers.CharacterHandler.ReadUnlockedConditionalAppearance(packet, "UnlockedConditionalAppearances", i);

            for (var i = 0u; i < raceLimitDisablesCount; i++)
                V11_0_0_55666.Parsers.CharacterHandler.ReadRaceLimitDisableInfo(packet, "RaceLimitDisableInfo", i);

            for (var i = 0u; i < warbandGroupsCount; ++i)
                V11_0_0_55666.Parsers.CharacterHandler.ReadWarbandGroup(packet, i, "WarbandGroups");
        }

        [Parser(Opcode.SMSG_INSPECT_RESULT, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleInspectResult(Packet packet)
        {
            V9_0_1_36216.Parsers.CharacterHandler.ReadPlayerModelDisplayInfo(packet, "DisplayInfo");
            var glyphCount = packet.ReadUInt32("GlyphsCount");
            var talentCount = packet.ReadUInt32("TalentsCount");
            var pvpTalentCount = packet.ReadUInt32("PvpTalentsCount");
            V10_0_0_46181.Parsers.CharacterHandler.ReadClassicTalentInfoUpdate(packet, "TalentInfo");
            packet.ReadInt32("ItemLevel");

            for (var i = 0u; i < 9; i++)
                V10_0_0_46181.Parsers.CharacterHandler.ReadPVPBracketData(packet, i, "PVPBracketData");

            packet.ReadByte("LifetimeMaxRank");
            packet.ReadUInt16("TodayHK");
            packet.ReadUInt16("YesterdayHK");
            packet.ReadUInt32("LifetimeHK");
            packet.ReadUInt32("HonorLevel");
            packet.ReadInt32("Level", "TraitInspectData");
            packet.ReadInt32("ChrSpecializationID", "TraitInspectData");
            TraitHandler.ReadTraitConfig(packet, "TraitInspectData", "Traits");

            for (int i = 0; i < glyphCount; i++)
                packet.ReadUInt16("Glyphs", i);

            for (int i = 0; i < talentCount; i++)
                packet.ReadUInt16("Talents", i);

            for (int i = 0; i < pvpTalentCount; i++)
                packet.ReadUInt16("PvpTalents", i);

            packet.ResetBitReader();
            var hasGuildData = packet.ReadBit("HasGuildData");
            var hasAzeriteLevel = packet.ReadBit("HasAzeriteLevel");

            if (hasGuildData)
            {
                packet.ReadPackedGuid128("GuildGUID");
                packet.ReadInt32("NumGuildMembers");
                packet.ReadInt32("GuildAchievementPoints");
            }

            if (hasAzeriteLevel)
                packet.ReadInt32("AzeriteLevel");
        }
    }
}
