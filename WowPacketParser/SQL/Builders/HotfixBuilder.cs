using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class HotfixBuilder
    {
        [BuilderMethod(TargetSQLDatabase.Hotfixes)]
        public static string Hotfixes()
        {
            var stringBuilder = new StringBuilder();
            string sql = string.Empty;

            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.hotfixes))
            {
                if (ClientLocale.PacketLocale == LocaleConstant.enUS)
                {
                    if (!Storage.AchievementHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementCategoryHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementCategoryHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementCategoryHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AdventureJournalHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureJournalHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureJournalHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AdventureMapPoiHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureMapPoiHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureMapPoiHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AnimationDataHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AnimationDataHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AnimationDataHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AnimKitHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AnimKitHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AnimKitHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaGroupMemberHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaGroupMemberHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaGroupMemberHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTableHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTableHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTableHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTableHotfixes1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTableHotfixes1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTableHotfixes1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTriggerHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTriggerHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTriggerHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTriggerHotfixes1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTriggerHotfixes1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTriggerHotfixes1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArmorLocationHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArmorLocationHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArmorLocationHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactAppearanceHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactAppearanceHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactAppearanceHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactAppearanceSetHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactAppearanceSetHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactAppearanceSetHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactCategoryHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactCategoryHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactCategoryHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactPowerHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactPowerHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactPowerHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactPowerLinkHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactPowerLinkHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactPowerLinkHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactPowerPickerHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactPowerPickerHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactPowerPickerHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactPowerRankHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactPowerRankHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactPowerRankHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactQuestXpHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactQuestXpHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactQuestXpHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactTierHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactTierHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactTierHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactUnlockHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactUnlockHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactUnlockHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AuctionHouseHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AuctionHouseHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AuctionHouseHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEmpoweredItemHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEmpoweredItemHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEmpoweredItemHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEssenceHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEssenceHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEssenceHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEssencePowerHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEssencePowerHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEssencePowerHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteItemHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteItemHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteItemHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteItemMilestonePowerHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteItemMilestonePowerHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteItemMilestonePowerHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteKnowledgeMultiplierHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteKnowledgeMultiplierHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteKnowledgeMultiplierHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteLevelInfoHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteLevelInfoHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteLevelInfoHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeritePowerHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeritePowerHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeritePowerHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeritePowerSetMemberHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeritePowerSetMemberHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeritePowerSetMemberHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteTierUnlockHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteTierUnlockHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteTierUnlockHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteTierUnlockSetHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteTierUnlockSetHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteTierUnlockSetHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteUnlockMappingHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteUnlockMappingHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteUnlockMappingHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BankBagSlotPricesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BankBagSlotPricesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BankBagSlotPricesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BannedAddonsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BannedAddonsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BannedAddonsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BarberShopStyleHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BarberShopStyleHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BarberShopStyleHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlemasterListHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlemasterListHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlemasterListHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetAbilityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetAbilityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetAbilityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetBreedQualityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetBreedQualityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetBreedQualityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetBreedStateHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetBreedStateHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetBreedStateHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixes1002.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixes1002, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixes1002, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesStateHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesStateHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesStateHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextDurationHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextDurationHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextDurationHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CfgRegionsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CfgRegionsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CfgRegionsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChallengeModeItemBonusOverrideHotfixes1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChallengeModeItemBonusOverrideHotfixes1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChallengeModeItemBonusOverrideHotfixes1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharTitlesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharTitlesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharTitlesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharacterLoadoutHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharacterLoadoutHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharacterLoadoutHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharacterLoadoutItemHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharacterLoadoutItemHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharacterLoadoutItemHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChatChannelsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChatChannelsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChatChannelsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassUiDisplayHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassUiDisplayHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassUiDisplayHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixes1017.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixes1017, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixes1017, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesXPowerTypesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesXPowerTypesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesXPowerTypesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixes1010.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixes1010, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixes1010, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationDisplayInfoHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationDisplayInfoHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationDisplayInfoHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationElementHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationElementHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationElementHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationElementHotfixes1017.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationElementHotfixes1017, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationElementHotfixes1017, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationOptionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationOptionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationOptionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixes1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixes1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixes1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqChoiceHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqChoiceHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqChoiceHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrModelHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrModelHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrModelHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRaceXChrModelHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRaceXChrModelHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRaceXChrModelHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRacesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRacesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRacesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrSpecializationHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrSpecializationHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrSpecializationHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CinematicCameraHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CinematicCameraHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CinematicCameraHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CinematicSequencesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CinematicSequencesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CinematicSequencesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ConditionalChrModelHotfixes1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConditionalChrModelHotfixes1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConditionalChrModelHotfixes1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ConditionalContentTuningHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConditionalContentTuningHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConditionalContentTuningHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ContentTuningHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ContentTuningHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ContentTuningHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ContentTuningHotfixes1010.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ContentTuningHotfixes1010, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ContentTuningHotfixes1010, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ContentTuningXExpectedHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ContentTuningXExpectedHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ContentTuningXExpectedHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ContentTuningXLabelHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ContentTuningXLabelHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ContentTuningXLabelHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ConversationLineHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConversationLineHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConversationLineHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CorruptionEffectsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CorruptionEffectsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CorruptionEffectsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureDisplayInfoHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureDisplayInfoHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureDisplayInfoHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureDisplayInfoExtraHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureDisplayInfoExtraHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureDisplayInfoExtraHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureFamilyHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureFamilyHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureFamilyHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureModelDataHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureModelDataHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureModelDataHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureModelDataHotfixes1017.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureModelDataHotfixes1017, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureModelDataHotfixes1017, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureTypeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureTypeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureTypeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaHotfixes1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaHotfixes1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaHotfixes1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaTreeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaTreeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaTreeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaTreeHotfixes1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaTreeHotfixes1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaTreeHotfixes1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyContainerHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyContainerHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyContainerHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyContainerHotfixes1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyContainerHotfixes1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyContainerHotfixes1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixes1002.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixes1002, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixes1002, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurveHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurveHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurveHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurvePointHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurvePointHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurvePointHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurvePointHotfixes1010.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurvePointHotfixes1010, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurvePointHotfixes1010, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DestructibleModelDataHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DestructibleModelDataHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DestructibleModelDataHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixes1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixes1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixes1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DungeonEncounterHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DungeonEncounterHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DungeonEncounterHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DurabilityCostsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DurabilityCostsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DurabilityCostsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DurabilityQualityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DurabilityQualityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DurabilityQualityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesTextHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesTextHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesTextHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesTextSoundHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesTextSoundHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesTextSoundHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ExpectedStatHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ExpectedStatHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ExpectedStatHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ExpectedStatModHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ExpectedStatModHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ExpectedStatModHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionTemplateHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionTemplateHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionTemplateHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipRepReactionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipRepReactionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipRepReactionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipReputationHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipReputationHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipReputationHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectArtKitHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectArtKitHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectArtKitHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectDisplayInfoHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectDisplayInfoHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectDisplayInfoHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectDisplayInfoHotfixes1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectDisplayInfoHotfixes1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectDisplayInfoHotfixes1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrAbilityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrAbilityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrAbilityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrBuildingHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrBuildingHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrBuildingHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrBuildingHotfixes1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrBuildingHotfixes1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrBuildingHotfixes1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrBuildingPlotInstHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrBuildingPlotInstHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrBuildingPlotInstHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrClassSpecHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrClassSpecHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrClassSpecHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrFollowerHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrFollowerHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrFollowerHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrFollowerHotfixes1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrFollowerHotfixes1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrFollowerHotfixes1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrFollowerXAbilityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrFollowerXAbilityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrFollowerXAbilityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrMissionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrMissionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrMissionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrMissionHotfixes1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrMissionHotfixes1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrMissionHotfixes1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrPlotHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrPlotHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrPlotHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrPlotBuildingHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrPlotBuildingHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrPlotBuildingHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrPlotInstanceHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrPlotInstanceHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrPlotInstanceHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrSiteLevelHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrSiteLevelHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrSiteLevelHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrSiteLevelPlotInstHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrSiteLevelPlotInstHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrSiteLevelPlotInstHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrTalentTreeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrTalentTreeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrTalentTreeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrTalentTreeHotfixes1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrTalentTreeHotfixes1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrTalentTreeHotfixes1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GemPropertiesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GemPropertiesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GemPropertiesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlobalCurveHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlobalCurveHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlobalCurveHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphBindableSpellHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphBindableSpellHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphBindableSpellHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphPropertiesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphPropertiesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphPropertiesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphRequiredSpecHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphRequiredSpecHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphRequiredSpecHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GossipNPCOptionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GossipNPCOptionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GossipNPCOptionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GossipNPCOptionHotfixes1002.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GossipNPCOptionHotfixes1002, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GossipNPCOptionHotfixes1002, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildColorBackgroundHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildColorBackgroundHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildColorBackgroundHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildColorBorderHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildColorBorderHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildColorBorderHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildColorEmblemHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildColorEmblemHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildColorEmblemHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildPerkSpellsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildPerkSpellsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildPerkSpellsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.HeirloomHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.HeirloomHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.HeirloomHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.HolidaysHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.HolidaysHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.HolidaysHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceArmorHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceArmorHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceArmorHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceQualityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceQualityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceQualityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceShieldHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceShieldHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceShieldHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceWeaponHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceWeaponHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceWeaponHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemAppearanceHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemAppearanceHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemAppearanceHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemArmorQualityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemArmorQualityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemArmorQualityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemArmorShieldHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemArmorShieldHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemArmorShieldHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemArmorTotalHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemArmorTotalHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemArmorTotalHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBagFamilyHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBagFamilyHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBagFamilyHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusListGroupEntryHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusListGroupEntryHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusListGroupEntryHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusListLevelDeltaHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusListLevelDeltaHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusListLevelDeltaHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusTreeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusTreeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusTreeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusTreeNodeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusTreeNodeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusTreeNodeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusTreeNodeHotfixes1010.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusTreeNodeHotfixes1010, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusTreeNodeHotfixes1010, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemChildEquipmentHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemChildEquipmentHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemChildEquipmentHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemClassHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemClassHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemClassHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemContextPickerEntryHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemContextPickerEntryHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemContextPickerEntryHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemCurrencyCostHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemCurrencyCostHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemCurrencyCostHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageAmmoHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageAmmoHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageAmmoHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageOneHandHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageOneHandHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageOneHandHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageOneHandCasterHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageOneHandCasterHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageOneHandCasterHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageTwoHandHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageTwoHandHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageTwoHandHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageTwoHandCasterHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageTwoHandCasterHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageTwoHandCasterHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDisenchantLootHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDisenchantLootHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDisenchantLootHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemEffectHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemEffectHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemEffectHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemExtendedCostHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemExtendedCostHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemExtendedCostHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLevelSelectorHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLevelSelectorHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLevelSelectorHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLevelSelectorQualityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLevelSelectorQualityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLevelSelectorQualityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLevelSelectorQualitySetHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLevelSelectorQualitySetHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLevelSelectorQualitySetHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLimitCategoryHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLimitCategoryHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLimitCategoryHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLimitCategoryConditionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLimitCategoryConditionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLimitCategoryConditionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemModifiedAppearanceHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemModifiedAppearanceHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemModifiedAppearanceHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemModifiedAppearanceExtraHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemModifiedAppearanceExtraHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemModifiedAppearanceExtraHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemNameDescriptionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemNameDescriptionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemNameDescriptionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemPriceBaseHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemPriceBaseHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemPriceBaseHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSearchNameHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSearchNameHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSearchNameHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSetHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSetHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSetHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSetSpellHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSetSpellHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSetSpellHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSparseHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSparseHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSparseHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSpecHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSpecHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSpecHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSpecOverrideHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSpecOverrideHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSpecOverrideHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemXBonusTreeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemXBonusTreeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemXBonusTreeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemXItemEffectHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemXItemEffectHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemXItemEffectHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterSectionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterSectionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterSectionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalInstanceHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalInstanceHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalInstanceHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalInstanceHotfixes1002.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalInstanceHotfixes1002, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalInstanceHotfixes1002, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalTierHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalTierHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalTierHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.KeychainHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.KeychainHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.KeychainHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.KeystoneAffixHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.KeystoneAffixHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.KeystoneAffixHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguageWordsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguageWordsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguageWordsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguagesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguagesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguagesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguagesHotfixes1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguagesHotfixes1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguagesHotfixes1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LfgDungeonsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LfgDungeonsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LfgDungeonsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LightHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LightHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LightHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LiquidTypeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LiquidTypeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LiquidTypeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LockHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LockHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LockHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MailTemplateHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MailTemplateHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MailTemplateHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapChallengeModeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapChallengeModeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapChallengeModeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyXConditionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyXConditionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyXConditionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MawPowerHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MawPowerHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MawPowerHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ModifierTreeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ModifierTreeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ModifierTreeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountCapabilityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountCapabilityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountCapabilityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountTypeXCapabilityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountTypeXCapabilityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountTypeXCapabilityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountXDisplayHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountXDisplayHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountXDisplayHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MovieHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MovieHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MovieHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MovieHotfixes1010.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MovieHotfixes1010, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MovieHotfixes1010, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MythicPlusSeasonHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MythicPlusSeasonHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MythicPlusSeasonHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NameGenHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NameGenHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NameGenHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NamesProfanityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NamesProfanityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NamesProfanityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NamesReservedHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NamesReservedHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NamesReservedHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NamesReservedLocaleHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NamesReservedLocaleHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NamesReservedLocaleHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NumTalentsAtLevelHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NumTalentsAtLevelHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NumTalentsAtLevelHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.OverrideSpellDataHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.OverrideSpellDataHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.OverrideSpellDataHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ParagonReputationHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ParagonReputationHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ParagonReputationHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PhaseHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PhaseHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PhaseHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PhaseXPhaseGroupHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PhaseXPhaseGroupHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PhaseXPhaseGroupHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PlayerConditionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PowerDisplayHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PowerDisplayHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PowerDisplayHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PowerTypeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PowerTypeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PowerTypeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PrestigeLevelInfoHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PrestigeLevelInfoHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PrestigeLevelInfoHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpDifficultyHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpDifficultyHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpDifficultyHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpItemHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpItemHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpItemHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpSeasonHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpSeasonHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpSeasonHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentHotfixes1002.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentHotfixes1002, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentHotfixes1002, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentCategoryHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentCategoryHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentCategoryHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentSlotUnlockHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentSlotUnlockHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentSlotUnlockHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTierHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTierHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTierHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestFactionRewardHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestFactionRewardHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestFactionRewardHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestInfoHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestInfoHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestInfoHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestInfoHotfixes1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestInfoHotfixes1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestInfoHotfixes1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestLineXQuestHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestLineXQuestHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestLineXQuestHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestMoneyRewardHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestMoneyRewardHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestMoneyRewardHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestPackageItemHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestPackageItemHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestPackageItemHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestSortHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestSortHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestSortHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestV2Hotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestV2Hotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestV2Hotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestXpHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestXpHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestXpHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RandPropPointsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RandPropPointsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RandPropPointsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RewardPackHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RewardPackHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RewardPackHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RewardPackXCurrencyTypeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RewardPackXCurrencyTypeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RewardPackXCurrencyTypeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RewardPackXItemHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RewardPackXItemHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RewardPackXItemHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioStepHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioStepHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioStepHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptGlobalTextHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptGlobalTextHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptGlobalTextHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptPackageHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptPackageHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptPackageHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptTextHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptTextHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptTextHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineAbilityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineAbilityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineAbilityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineXTraitTreeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineXTraitTreeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineXTraitTreeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillRaceClassInfoHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillRaceClassInfoHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillRaceClassInfoHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SoulbindConduitRankHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SoulbindConduitRankHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SoulbindConduitRankHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SoundKitHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SoundKitHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SoundKitHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpecializationSpellsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpecializationSpellsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpecializationSpellsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpecSetMemberHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpecSetMemberHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpecSetMemberHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellAuraOptionsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellAuraOptionsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellAuraOptionsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellAuraRestrictionsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellAuraRestrictionsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellAuraRestrictionsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCastTimesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCastTimesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCastTimesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCastingRequirementsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCastingRequirementsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCastingRequirementsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoriesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoriesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoriesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoryHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoryHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoryHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoryHotfixes1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoryHotfixes1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoryHotfixes1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellClassOptionsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellClassOptionsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellClassOptionsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCooldownsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCooldownsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCooldownsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellDurationHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellDurationHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellDurationHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellEffectHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellEffectHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellEffectHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellEquippedItemsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellEquippedItemsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellEquippedItemsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellFocusObjectHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellFocusObjectHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellFocusObjectHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellInterruptsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellInterruptsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellInterruptsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellKeyboundOverrideHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellKeyboundOverrideHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellKeyboundOverrideHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentConditionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentConditionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentConditionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellLabelHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellLabelHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellLabelHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellLearnSpellHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellLearnSpellHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellLearnSpellHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellLevelsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellLevelsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellLevelsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellMiscHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellMiscHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellMiscHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellNameHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellNameHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellNameHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellPowerHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellPowerHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellPowerHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellPowerDifficultyHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellPowerDifficultyHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellPowerDifficultyHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellProcsPerMinuteHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellProcsPerMinuteHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellProcsPerMinuteHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellProcsPerMinuteModHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellProcsPerMinuteModHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellProcsPerMinuteModHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellRadiusHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellRadiusHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellRadiusHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellRangeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellRangeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellRangeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellReagentsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellReagentsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellReagentsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellReagentsHotfixes1002.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellReagentsHotfixes1002, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellReagentsHotfixes1002, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellReagentsCurrencyHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellReagentsCurrencyHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellReagentsCurrencyHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellScalingHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellScalingHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellScalingHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftFormHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftFormHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftFormHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellTargetRestrictionsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellTargetRestrictionsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellTargetRestrictionsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellTotemsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellTotemsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellTotemsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualEffectNameHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualEffectNameHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualEffectNameHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualMissileHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualMissileHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualMissileHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualKitHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualKitHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualKitHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellXSpellVisualHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellXSpellVisualHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellXSpellVisualHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SummonPropertiesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SummonPropertiesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SummonPropertiesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TactKeyHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TactKeyHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TactKeyHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiNodesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiNodesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiNodesHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiNodesHotfixes1017.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiNodesHotfixes1017, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiNodesHotfixes1017, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiPathHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiPathHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiPathHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiPathNodeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiPathNodeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiPathNodeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiPathNodeHotfixes1017.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiPathNodeHotfixes1017, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiPathNodeHotfixes1017, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TotemCategoryHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TotemCategoryHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TotemCategoryHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ToyHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ToyHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ToyHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogHolidayHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogHolidayHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogHolidayHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCondHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCondHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCondHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCostHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCostHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCostHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCurrencyHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCurrencyHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCurrencyHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCurrencySourceHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCurrencySourceHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCurrencySourceHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitDefinitionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitDefinitionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitDefinitionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitDefinitionEffectPointsHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitDefinitionEffectPointsHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitDefinitionEffectPointsHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitEdgeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitEdgeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitEdgeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeEntryHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeEntryHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeEntryHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeEntryXTraitCondHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeEntryXTraitCondHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeEntryXTraitCondHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeEntryXTraitCostHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeEntryXTraitCostHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeEntryXTraitCostHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeGroupHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeGroupHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeGroupHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeGroupXTraitCondHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeGroupXTraitCondHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeGroupXTraitCondHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeGroupXTraitCostHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeGroupXTraitCostHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeGroupXTraitCostHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeGroupXTraitNodeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeGroupXTraitNodeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeGroupXTraitNodeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeXTraitCondHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeXTraitCondHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeXTraitCondHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeXTraitCostHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeXTraitCostHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeXTraitCostHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeXTraitCostHotfixes1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeXTraitCostHotfixes1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeXTraitCostHotfixes1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeXTraitNodeEntryHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeXTraitNodeEntryHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeXTraitNodeEntryHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeLoadoutHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeLoadoutHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeLoadoutHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeLoadoutEntryHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeLoadoutEntryHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeLoadoutEntryHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeXTraitCostHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeXTraitCostHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeXTraitCostHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeXTraitCurrencyHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeXTraitCurrencyHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeXTraitCurrencyHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogIllusionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogIllusionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogIllusionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetGroupHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetGroupHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetGroupHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetItemHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetItemHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetItemHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransportAnimationHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransportAnimationHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransportAnimationHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransportRotationHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransportRotationHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransportRotationHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixes1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixes1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixes1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapAssignmentHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapAssignmentHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapAssignmentHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapLinkHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapLinkHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapLinkHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapXMapArtHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapXMapArtHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapXMapArtHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiSplashScreenHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiSplashScreenHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiSplashScreenHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitConditionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitConditionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitConditionHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitPowerBarHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitPowerBarHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitPowerBarHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VehicleHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VehicleHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VehicleHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VehicleHotfixes1010.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VehicleHotfixes1010, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VehicleHotfixes1010, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VehicleSeatHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VehicleSeatHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VehicleSeatHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WmoAreaTableHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WmoAreaTableHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WmoAreaTableHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WorldEffectHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WorldEffectHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WorldEffectHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WorldMapOverlayHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WorldMapOverlayHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WorldMapOverlayHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WorldStateExpressionHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WorldStateExpressionHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WorldStateExpressionHotfixes1000, hotfixes, StoreNameType.None);
                    }
                }
                else
                {
                    if (!Storage.AchievementHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementCategoryHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementCategoryHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementCategoryHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AdventureJournalHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureJournalHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureJournalHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AdventureMapPoiHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureMapPoiHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureMapPoiHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTableHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTableHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTableHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTableHotfixesLocale1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTableHotfixesLocale1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTableHotfixesLocale1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactAppearanceHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactAppearanceHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactAppearanceHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactAppearanceSetHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactAppearanceSetHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactAppearanceSetHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AuctionHouseHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AuctionHouseHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AuctionHouseHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEssenceHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEssenceHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEssenceHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEssencePowerHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEssencePowerHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEssencePowerHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BarberShopStyleHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BarberShopStyleHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BarberShopStyleHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixesLocale1002.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixesLocale1002, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixesLocale1002, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlemasterListHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlemasterListHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlemasterListHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharTitlesHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharTitlesHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharTitlesHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChatChannelsHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChatChannelsHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChatChannelsHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixesLocale1017.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixesLocale1017, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixesLocale1017, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixesLocale1010.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixesLocale1010, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixesLocale1010, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationOptionHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationOptionHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationOptionHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixesLocale1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixesLocale1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixesLocale1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRacesHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRacesHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRacesHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrSpecializationHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrSpecializationHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrSpecializationHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureFamilyHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureFamilyHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureFamilyHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureTypeHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureTypeHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureTypeHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaTreeHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaTreeHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaTreeHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaTreeHotfixesLocale1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaTreeHotfixesLocale1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaTreeHotfixesLocale1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyContainerHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyContainerHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyContainerHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyContainerHotfixesLocale1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyContainerHotfixesLocale1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyContainerHotfixesLocale1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixesLocale1002.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixesLocale1002, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixesLocale1002, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixesLocale1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixesLocale1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixesLocale1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DungeonEncounterHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DungeonEncounterHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DungeonEncounterHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipRepReactionHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipRepReactionHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipRepReactionHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipReputationHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipReputationHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipReputationHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectsHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectsHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectsHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrAbilityHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrAbilityHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrAbilityHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrBuildingHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrBuildingHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrBuildingHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrBuildingHotfixesLocale1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrBuildingHotfixesLocale1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrBuildingHotfixesLocale1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrClassSpecHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrClassSpecHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrClassSpecHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrFollowerHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrFollowerHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrFollowerHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrFollowerHotfixesLocale1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrFollowerHotfixesLocale1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrFollowerHotfixesLocale1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrMissionHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrMissionHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrMissionHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrMissionHotfixesLocale1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrMissionHotfixesLocale1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrMissionHotfixesLocale1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrTalentTreeHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrTalentTreeHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrTalentTreeHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrTalentTreeHotfixesLocale1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrTalentTreeHotfixesLocale1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrTalentTreeHotfixesLocale1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.HeirloomHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.HeirloomHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.HeirloomHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBagFamilyHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBagFamilyHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBagFamilyHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemClassHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemClassHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemClassHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLimitCategoryHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLimitCategoryHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLimitCategoryHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemNameDescriptionHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemNameDescriptionHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemNameDescriptionHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSearchNameHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSearchNameHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSearchNameHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSetHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSetHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSetHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSparseHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSparseHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSparseHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterSectionHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterSectionHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterSectionHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalInstanceHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalInstanceHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalInstanceHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalInstanceHotfixesLocale1002.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalInstanceHotfixesLocale1002, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalInstanceHotfixesLocale1002, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalTierHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalTierHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalTierHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.KeystoneAffixHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.KeystoneAffixHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.KeystoneAffixHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguagesHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguagesHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguagesHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguagesHotfixesLocale1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguagesHotfixesLocale1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguagesHotfixesLocale1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LfgDungeonsHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LfgDungeonsHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LfgDungeonsHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MailTemplateHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MailTemplateHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MailTemplateHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapChallengeModeHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapChallengeModeHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapChallengeModeHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyXConditionHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyXConditionHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyXConditionHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PlayerConditionHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PrestigeLevelInfoHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PrestigeLevelInfoHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PrestigeLevelInfoHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentHotfixesLocale1002.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentHotfixesLocale1002, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentHotfixesLocale1002, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTierHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTierHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTierHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestInfoHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestInfoHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestInfoHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestInfoHotfixesLocale1007.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestInfoHotfixesLocale1007, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestInfoHotfixesLocale1007, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestSortHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestSortHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestSortHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioStepHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioStepHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioStepHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineAbilityHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineAbilityHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineAbilityHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpecializationSpellsHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpecializationSpellsHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpecializationSpellsHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoryHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoryHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoryHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoryHotfixesLocale1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoryHotfixesLocale1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoryHotfixesLocale1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellFocusObjectHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellFocusObjectHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellFocusObjectHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellNameHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellNameHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellNameHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellRangeHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellRangeHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellRangeHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftFormHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftFormHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftFormHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiNodesHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiNodesHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiNodesHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiNodesHotfixesLocale1017.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiNodesHotfixesLocale1017, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiNodesHotfixesLocale1017, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TotemCategoryHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TotemCategoryHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TotemCategoryHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ToyHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ToyHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ToyHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCurrencySourceHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCurrencySourceHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCurrencySourceHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitDefinitionHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitDefinitionHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitDefinitionHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetGroupHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetGroupHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetGroupHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixesLocale1015.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixesLocale1015, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixesLocale1015, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiSplashScreenHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiSplashScreenHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiSplashScreenHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitPowerBarHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitPowerBarHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitPowerBarHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WmoAreaTableHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WmoAreaTableHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WmoAreaTableHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }
                }

                // TODO: add way to identify clean DBCache and packet is ALL hotfixes not just few
                /*if (ClientLocale.PacketLocale == LocaleConstant.enUS && !Settings.SkipOnlyVerifiedBuildUpdateRows)
                {
                    sql += $"DELETE FROM `achievement` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `achievement_category` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `adventure_journal` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `adventure_map_poi` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `animation_data` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `anim_kit` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `area_group_member` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `area_table` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `area_trigger` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `armor_location` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_appearance` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_appearance_set` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_category` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_power` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_power_link` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_power_picker` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_power_rank` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_quest_xp` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_tier` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_unlock` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `auction_house` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_empowered_item` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_essence` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_essence_power` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_item` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_item_milestone_power` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_knowledge_multiplier` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_level_info` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_power` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_power_set_member` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_tier_unlock` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_tier_unlock_set` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_unlock_mapping` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `bank_bag_slot_prices` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `banned_addons` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `barber_shop_style` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battle_pet_breed_quality` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battle_pet_breed_state` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battle_pet_species` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battle_pet_species_state` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battlemaster_list` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `broadcast_text_duration` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `cfg_regions` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `char_titles` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `character_loadout` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `character_loadout_item` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chat_channels` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_class_ui_display` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_classes` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_classes_x_power_types` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_customization_choice` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_customization_display_info` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_customization_element` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_customization_option` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_customization_req` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_customization_req_choice` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_model` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_race_x_chr_model` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_races` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_specialization` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `cinematic_camera` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `cinematic_sequences` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `content_tuning` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `content_tuning_x_expected` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `conversation_line` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `corruption_effects` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `creature_display_info` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `creature_display_info_extra` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `creature_family` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `creature_model_data` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `creature_type` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `criteria` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `criteria_tree` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `currency_container` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `currency_types` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `curve` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `curve_point` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `destructible_model_data` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `difficulty` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `dungeon_encounter` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `durability_costs` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `durability_quality` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `emotes` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `emotes_text` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `emotes_text_sound` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `expected_stat` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `expected_stat_mod` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `faction` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `faction_template` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `friendship_rep_reaction` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `friendship_reputation` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `gameobject_art_kit` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `gameobject_display_info` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `gameobjects` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_ability` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_building` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_building_plot_inst` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_class_spec` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_follower` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_follower_x_ability` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_mission` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_plot` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_plot_building` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_plot_instance` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_site_level` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_site_level_plot_inst` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_talent_tree` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `gem_properties` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `global_curve` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `glyph_bindable_spell` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `glyph_properties` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `glyph_required_spec` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `guild_color_background` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `guild_color_border` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `guild_color_emblem` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `guild_perk_spells` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `heirloom` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `holidays` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `import_price_armor` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `import_price_quality` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `import_price_shield` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `import_price_weapon` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_appearance` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_armor_quality` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_armor_shield` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_armor_total` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_bag_family` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_bonus` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_bonus_list_level_delta` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_bonus_tree_node` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_child_equipment` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_class` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_currency_cost` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_damage_ammo` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_damage_one_hand` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_damage_one_hand_caster` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_damage_two_hand` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_damage_two_hand_caster` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_disenchant_loot` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_effect` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_extended_cost` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_level_selector` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_level_selector_quality` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_level_selector_quality_set` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_limit_category` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_limit_category_condition` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_modified_appearance` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_modified_appearance_extra` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_name_description` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_price_base` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_search_name` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_set` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_set_spell` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_sparse` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_spec` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_spec_override` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_x_bonus_tree` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_x_item_effect` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `journal_encounter` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `journal_encounter_section` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `journal_instance` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `journal_tier` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `keystone_affix` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `language_words` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `languages` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `lfg_dungeons` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `light` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `liquid_type` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `lock` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mail_template` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `map` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `map_challenge_mode` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `map_difficulty` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `map_difficulty_x_condition` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `maw_power` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `modifier_tree` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mount` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mount_capability` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mount_type_x_capability` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mount_x_display` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `movie` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `name_gen` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `names_profanity` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `names_reserved` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `names_reserved_locale` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `num_talents_at_level` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `override_spell_data` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `paragon_reputation` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `phase` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `phase_x_phase_group` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `power_display` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `power_type` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `prestige_level_info` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `pvp_difficulty` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `pvp_item` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `pvp_talent` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `pvp_talent_category` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `pvp_talent_slot_unlock` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `pvp_tier` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `quest_faction_reward` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `quest_info` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `quest_line_x_quest` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `quest_money_reward` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `quest_package_item` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `quest_sort` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `quest_v2` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `quest_xp` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `rand_prop_points` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `reward_pack` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `reward_pack_x_currency_type` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `reward_pack_x_item` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `scenario` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `scenario_step` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `scene_script` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `scene_script_global_text` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `scene_script_package` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `scene_script_text` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `skill_line` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `skill_line_ability` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `skill_race_class_info` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `soulbind_conduit_rank` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `sound_kit` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `specialization_spells` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spec_set_member` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_aura_options` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_aura_restrictions` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_cast_times` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_casting_requirements` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_categories` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_category` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_class_options` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_cooldowns` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_duration` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_effect` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_equipped_items` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_focus_object` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_interrupts` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_item_enchantment` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_item_enchantment_condition` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_label` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_learn_spell` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_levels` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_misc` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_name` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_power` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_power_difficulty` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_procs_per_minute` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_procs_per_minute_mod` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_radius` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_range` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_reagents` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_reagents_currency` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_scaling` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_shapeshift` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_shapeshift_form` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_target_restrictions` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_totems` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_visual` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_visual_effect_name` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_visual_missile` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_visual_kit` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_x_spell_visual` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `summon_properties` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `talent` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `taxi_nodes` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `taxi_path` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `taxi_path_node` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `totem_category` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `toy` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_holiday` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_illusion` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_set` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_set_group` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_set_item` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transport_animation` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transport_rotation` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `ui_map` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `ui_map_assignment` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `ui_map_link` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `ui_map_x_map_art` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `ui_splash_screen` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `unit_condition` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `unit_power_bar` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `vehicle` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `vehicle_seat` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `wmo_area_table` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `world_effect` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `world_map_overlay` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `world_state_expression` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                }
                else if (!Settings.SkipOnlyVerifiedBuildUpdateRows)
                {
                    sql += $"DELETE FROM `achievement_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `achievement_category_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `adventure_journal_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `adventure_map_poi_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `area_table_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_appearance_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `artifact_appearance_set_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `auction_house_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_essence_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `azerite_essence_power_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `barber_shop_style_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battle_pet_species_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battlemaster_list_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `char_titles_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chat_channels_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_classes_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_customization_choice_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_customization_option_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_customization_req_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_races_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_specialization_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `creature_family_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `creature_type_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `criteria_tree_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `currency_container_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `currency_types_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `difficulty_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `dungeon_encounter_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `faction_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `friendship_rep_reaction_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `friendship_reputation_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `gameobjects_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_ability_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_building_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_class_spec_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_follower_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_mission_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `garr_talent_tree_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `heirloom_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_bag_family_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_class_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_limit_category_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_name_description_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_search_name_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_set_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_sparse_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `journal_encounter_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `journal_encounter_section_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `journal_instance_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `journal_tier_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `keystone_affix_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `languages_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `lfg_dungeons_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mail_template_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `map_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `map_challenge_mode_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `map_difficulty_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `map_difficulty_x_condition_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mount_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `prestige_level_info_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `pvp_talent_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `pvp_tier_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `quest_info_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `quest_sort_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `scenario_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `scenario_step_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `skill_line_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `skill_line_ability_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `specialization_spells_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_category_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_focus_object_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_item_enchantment_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_name_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_range_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_shapeshift_form_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `talent_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `taxi_nodes_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `totem_category_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `toy_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_set_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_set_group_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `ui_map_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `ui_splash_screen_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `unit_power_bar_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `wmo_area_table_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                }*/

                return sql;
            }
            else
            {
                if (ClientLocale.PacketLocale == LocaleConstant.enUS)
                {
                    // ReSharper disable once LoopCanBePartlyConvertedToQuery
                    foreach (DB2Hash hashValue in Enum.GetValues(typeof(DB2Hash)))
                        if (HotfixSettings.Instance.ShouldLog(hashValue))
                            HotfixStoreMgr.GetStore(hashValue)?.Serialize(stringBuilder, null);
                }
                else
                {
                    var emptyStringBuilder = new StringBuilder();

                    foreach (DB2Hash hashValue in Enum.GetValues(typeof(DB2Hash)))
                    {
                        if (!HotfixSettings.Instance.ShouldLog(hashValue))
                            continue;

                        var localeBuilder = new StringBuilder();
                        HotfixStoreMgr.GetStore(hashValue)?.Serialize(stringBuilder, localeBuilder);
                        emptyStringBuilder.Append(localeBuilder);
                    }
                    return emptyStringBuilder.ToString();
                }

                return stringBuilder.ToString();
            }
        }

        [BuilderMethod(true, TargetSQLDatabase.Hotfixes)]
        public static string HotfixData()
        {
            if (Storage.HotfixDatas.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.hotfix_data))
                return string.Empty;

            string sql = string.Empty;
            var templatesDb = SQLDatabase.Get(Storage.HotfixDatas, Settings.HotfixesDatabase);

            sql += SQLUtil.Compare(Storage.HotfixDatas, templatesDb, StoreNameType.None);

            // only add delete query if anything is updated, otherwise we delete valid data
            // TODO: add way to identify clean DBCache and packet is ALL hotfixes not just few
            //if (!Settings.SkipOnlyVerifiedBuildUpdateRows)
            //    sql += $"DELETE FROM `hotfix_data` WHERE `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt};" + Environment.NewLine;

            return sql;
        }

        [BuilderMethod(true, TargetSQLDatabase.Hotfixes)]
        public static string HotfixBlob()
        {
            if (Storage.HotfixBlobs.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.hotfix_blob))
                return string.Empty;

            string sql = string.Empty;
            var templatesDb = SQLDatabase.Get(Storage.HotfixBlobs, Settings.HotfixesDatabase);

            sql += SQLUtil.Compare(Storage.HotfixBlobs, templatesDb, StoreNameType.None);

            // only add delete query if anything is updated, otherwise we delete valid data
            // TODO: add way to identify clean DBCache and packet is ALL hotfixes not just few
            //if (!Settings.SkipOnlyVerifiedBuildUpdateRows)
            //    sql += $"DELETE FROM `hotfix_blob` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;

            return sql;
        }

        // Special Hotfix Builders
        [BuilderMethod(true, TargetSQLDatabase.Hotfixes)]
        public static string BroadcastText()
        {
            if (Storage.BroadcastTexts.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.broadcast_text))
                return string.Empty;

            foreach (var broadcastText in Storage.BroadcastTexts)
                broadcastText.Item1.ConvertToDBStruct();

            // pass empty list, because we want to select the whole db table (faster than select only needed columns)
            var templatesDb = SQLDatabase.Get(new RowList<Store.Objects.BroadcastText>(), Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.BroadcastTexts, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true, TargetSQLDatabase.Hotfixes)]
        public static string BroadcastTextLocales()
        {
            if (Storage.BroadcastTextLocales.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.broadcast_text_locale))
                return string.Empty;

            // pass empty list, because we want to select the whole db table (faster than select only needed columns)
            var templatesDb = SQLDatabase.Get(new RowList<Store.Objects.BroadcastTextLocale>(), Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.BroadcastTextLocales, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(TargetSQLDatabase.Hotfixes)]
        public static string HotfixOptionalData()
        {
            if (Storage.HotfixOptionalDatas.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.hotfix_optional_data))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(new RowList<Store.Objects.HotfixOptionalData>(), Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.HotfixOptionalDatas, templatesDb, StoreNameType.None);
        }
    }
}
