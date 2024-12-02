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
                    // Dragonflight Hotfix Builder
                    if (!Storage.AchievementHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementHotfixes1026.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixes1026, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixes1026, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementHotfixes1027.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixes1027, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixes1027, hotfixes, StoreNameType.None);
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

                    if (!Storage.AdventureJournalHotfixes1026.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureJournalHotfixes1026, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureJournalHotfixes1026, hotfixes, StoreNameType.None);
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

                    if (!Storage.AreaTriggerHotfixes1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTriggerHotfixes1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTriggerHotfixes1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.CfgCategoriesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CfgCategoriesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CfgCategoriesHotfixes1000, hotfixes, StoreNameType.None);
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

                    if (!Storage.CharBaseInfoHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharBaseInfoHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharBaseInfoHotfixes1000, hotfixes, StoreNameType.None);
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

                    if (!Storage.ChrCustomizationDisplayInfoHotfixes1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationDisplayInfoHotfixes1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationDisplayInfoHotfixes1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.ChrCustomizationElementHotfixes1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationElementHotfixes1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationElementHotfixes1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.ConditionalChrModelHotfixes1025.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConditionalChrModelHotfixes1025, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConditionalChrModelHotfixes1025, hotfixes, StoreNameType.None);
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

                    if (!Storage.ContentTuningHotfixes1027.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ContentTuningHotfixes1027, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ContentTuningHotfixes1027, hotfixes, StoreNameType.None);
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

                    if (!Storage.ConversationLineHotfixes1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConversationLineHotfixes1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConversationLineHotfixes1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.CurvePointHotfixes1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurvePointHotfixes1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurvePointHotfixes1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.ItemModifiedAppearanceHotfixes1027.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemModifiedAppearanceHotfixes1027, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemModifiedAppearanceHotfixes1027, hotfixes, StoreNameType.None);
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

                    if (!Storage.MountHotfixes1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountHotfixes1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountHotfixes1020, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountCapabilityHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountCapabilityHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountCapabilityHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountCapabilityHotfixes1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountCapabilityHotfixes1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountCapabilityHotfixes1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.MovieHotfixes1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MovieHotfixes1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MovieHotfixes1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.PhaseHotfixes1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PhaseHotfixes1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PhaseHotfixes1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.PlayerConditionHotfixes1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixes1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixes1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.PvpStatHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpStatHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpStatHotfixes1000, hotfixes, StoreNameType.None);
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

                    if (!Storage.ServerMessagesHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ServerMessagesHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ServerMessagesHotfixes1000, hotfixes, StoreNameType.None);
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

                    if (!Storage.SpellEmpowerHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellEmpowerHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellEmpowerHotfixes1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellEmpowerStageHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellEmpowerStageHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellEmpowerStageHotfixes1000, hotfixes, StoreNameType.None);
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

                    if (!Storage.SpellShapeshiftFormHotfixes1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftFormHotfixes1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftFormHotfixes1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.TactKeyHotfixes.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TactKeyHotfixes, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TactKeyHotfixes, hotfixes, StoreNameType.None);
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

                    if (!Storage.UiMapHotfixes1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixes1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixes1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.VignetteHotfixes1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VignetteHotfixes1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VignetteHotfixes1000, hotfixes, StoreNameType.None);
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

                    // The War Within Hotfix Builder
                    if (!Storage.AchievementHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementCategoryHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementCategoryHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementCategoryHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AdventureJournalHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureJournalHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureJournalHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AdventureMapPoiHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureMapPoiHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureMapPoiHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AnimationDataHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AnimationDataHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AnimationDataHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AnimKitHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AnimKitHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AnimKitHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaGroupMemberHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaGroupMemberHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaGroupMemberHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTableHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTableHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTableHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTriggerHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTriggerHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTriggerHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTriggerActionSetHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTriggerActionSetHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTriggerActionSetHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArmorLocationHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArmorLocationHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArmorLocationHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactAppearanceHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactAppearanceHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactAppearanceHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactAppearanceSetHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactAppearanceSetHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactAppearanceSetHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactCategoryHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactCategoryHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactCategoryHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactPowerHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactPowerHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactPowerHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactPowerLinkHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactPowerLinkHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactPowerLinkHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactPowerPickerHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactPowerPickerHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactPowerPickerHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactPowerRankHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactPowerRankHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactPowerRankHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactQuestXpHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactQuestXpHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactQuestXpHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactTierHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactTierHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactTierHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactUnlockHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactUnlockHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactUnlockHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AuctionHouseHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AuctionHouseHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AuctionHouseHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEmpoweredItemHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEmpoweredItemHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEmpoweredItemHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEssenceHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEssenceHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEssenceHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEssencePowerHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEssencePowerHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEssencePowerHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteItemHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteItemHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteItemHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteItemMilestonePowerHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteItemMilestonePowerHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteItemMilestonePowerHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteKnowledgeMultiplierHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteKnowledgeMultiplierHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteKnowledgeMultiplierHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteLevelInfoHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteLevelInfoHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteLevelInfoHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeritePowerHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeritePowerHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeritePowerHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeritePowerSetMemberHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeritePowerSetMemberHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeritePowerSetMemberHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteTierUnlockHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteTierUnlockHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteTierUnlockHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteTierUnlockSetHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteTierUnlockSetHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteTierUnlockSetHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteUnlockMappingHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteUnlockMappingHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteUnlockMappingHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BankBagSlotPricesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BankBagSlotPricesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BankBagSlotPricesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BannedAddonsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BannedAddonsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BannedAddonsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BarberShopStyleHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BarberShopStyleHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BarberShopStyleHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlemasterListHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlemasterListHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlemasterListHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlemasterListXMapHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlemasterListXMapHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlemasterListXMapHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetAbilityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetAbilityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetAbilityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetBreedQualityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetBreedQualityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetBreedQualityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetBreedStateHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetBreedStateHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetBreedStateHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesStateHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesStateHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesStateHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextDurationHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextDurationHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextDurationHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextDurationHotfixes1105.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextDurationHotfixes1105, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextDurationHotfixes1105, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CfgCategoriesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CfgCategoriesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CfgCategoriesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CfgRegionsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CfgRegionsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CfgRegionsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChallengeModeItemBonusOverrideHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChallengeModeItemBonusOverrideHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChallengeModeItemBonusOverrideHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharBaseInfoHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharBaseInfoHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharBaseInfoHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharTitlesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharTitlesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharTitlesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharacterLoadoutHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharacterLoadoutHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharacterLoadoutHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharacterLoadoutItemHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharacterLoadoutItemHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharacterLoadoutItemHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChatChannelsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChatChannelsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChatChannelsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassUiDisplayHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassUiDisplayHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassUiDisplayHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesXPowerTypesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesXPowerTypesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesXPowerTypesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationDisplayInfoHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationDisplayInfoHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationDisplayInfoHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationElementHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationElementHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationElementHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationOptionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationOptionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationOptionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationOptionHotfixes1105.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationOptionHotfixes1105, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationOptionHotfixes1105, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqChoiceHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqChoiceHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqChoiceHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrModelHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrModelHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrModelHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRaceXChrModelHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRaceXChrModelHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRaceXChrModelHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRacesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRacesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRacesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrSpecializationHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrSpecializationHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrSpecializationHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CinematicCameraHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CinematicCameraHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CinematicCameraHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CinematicSequencesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CinematicSequencesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CinematicSequencesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ConditionalChrModelHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConditionalChrModelHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConditionalChrModelHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ConditionalContentTuningHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConditionalContentTuningHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConditionalContentTuningHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ContentTuningHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ContentTuningHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ContentTuningHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ContentTuningXExpectedHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ContentTuningXExpectedHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ContentTuningXExpectedHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ContentTuningXLabelHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ContentTuningXLabelHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ContentTuningXLabelHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ConversationLineHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConversationLineHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConversationLineHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CorruptionEffectsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CorruptionEffectsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CorruptionEffectsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureDisplayInfoHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureDisplayInfoHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureDisplayInfoHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureDisplayInfoExtraHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureDisplayInfoExtraHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureDisplayInfoExtraHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureFamilyHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureFamilyHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureFamilyHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureLabelHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureLabelHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureLabelHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureModelDataHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureModelDataHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureModelDataHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureTypeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureTypeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureTypeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaTreeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaTreeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaTreeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyContainerHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyContainerHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyContainerHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurveHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurveHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurveHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurvePointHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurvePointHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurvePointHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DestructibleModelDataHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DestructibleModelDataHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DestructibleModelDataHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixes1105.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixes1105, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixes1105, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DungeonEncounterHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DungeonEncounterHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DungeonEncounterHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DurabilityCostsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DurabilityCostsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DurabilityCostsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DurabilityQualityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DurabilityQualityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DurabilityQualityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesTextHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesTextHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesTextHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesTextSoundHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesTextSoundHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesTextSoundHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ExpectedStatHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ExpectedStatHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ExpectedStatHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ExpectedStatModHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ExpectedStatModHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ExpectedStatModHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionTemplateHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionTemplateHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionTemplateHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FlightCapabilityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FlightCapabilityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FlightCapabilityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipRepReactionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipRepReactionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipRepReactionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipRepReactionHotfixes1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipRepReactionHotfixes1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipRepReactionHotfixes1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipReputationHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipReputationHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipReputationHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectArtKitHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectArtKitHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectArtKitHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectDisplayInfoHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectDisplayInfoHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectDisplayInfoHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectLabelHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectLabelHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectLabelHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrAbilityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrAbilityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrAbilityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrBuildingHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrBuildingHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrBuildingHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrBuildingPlotInstHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrBuildingPlotInstHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrBuildingPlotInstHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrClassSpecHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrClassSpecHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrClassSpecHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrFollowerHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrFollowerHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrFollowerHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrFollowerXAbilityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrFollowerXAbilityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrFollowerXAbilityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrMissionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrMissionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrMissionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrMissionHotfixes1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrMissionHotfixes1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrMissionHotfixes1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrPlotHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrPlotHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrPlotHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrPlotHotfixes1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrPlotHotfixes1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrPlotHotfixes1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrPlotBuildingHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrPlotBuildingHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrPlotBuildingHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrPlotInstanceHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrPlotInstanceHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrPlotInstanceHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrSiteLevelHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrSiteLevelHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrSiteLevelHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrSiteLevelPlotInstHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrSiteLevelPlotInstHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrSiteLevelPlotInstHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrTalentTreeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrTalentTreeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrTalentTreeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GemPropertiesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GemPropertiesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GemPropertiesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlobalCurveHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlobalCurveHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlobalCurveHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphBindableSpellHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphBindableSpellHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphBindableSpellHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphPropertiesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphPropertiesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphPropertiesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphRequiredSpecHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphRequiredSpecHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphRequiredSpecHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GossipNPCOptionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GossipNPCOptionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GossipNPCOptionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildColorBackgroundHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildColorBackgroundHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildColorBackgroundHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildColorBorderHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildColorBorderHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildColorBorderHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildColorEmblemHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildColorEmblemHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildColorEmblemHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildPerkSpellsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildPerkSpellsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildPerkSpellsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.HeirloomHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.HeirloomHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.HeirloomHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.HolidaysHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.HolidaysHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.HolidaysHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceArmorHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceArmorHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceArmorHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceQualityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceQualityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceQualityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceShieldHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceShieldHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceShieldHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceWeaponHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceWeaponHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceWeaponHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemAppearanceHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemAppearanceHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemAppearanceHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemArmorQualityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemArmorQualityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemArmorQualityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemArmorShieldHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemArmorShieldHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemArmorShieldHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemArmorTotalHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemArmorTotalHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemArmorTotalHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBagFamilyHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBagFamilyHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBagFamilyHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusListGroupEntryHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusListGroupEntryHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusListGroupEntryHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusListLevelDeltaHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusListLevelDeltaHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusListLevelDeltaHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusTreeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusTreeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusTreeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusTreeNodeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusTreeNodeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusTreeNodeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusTreeNodeHotfixes1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusTreeNodeHotfixes1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusTreeNodeHotfixes1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemChildEquipmentHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemChildEquipmentHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemChildEquipmentHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemClassHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemClassHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemClassHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemContextPickerEntryHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemContextPickerEntryHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemContextPickerEntryHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemCurrencyCostHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemCurrencyCostHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemCurrencyCostHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageAmmoHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageAmmoHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageAmmoHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageOneHandHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageOneHandHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageOneHandHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageOneHandCasterHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageOneHandCasterHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageOneHandCasterHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageTwoHandHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageTwoHandHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageTwoHandHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageTwoHandCasterHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageTwoHandCasterHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageTwoHandCasterHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDisenchantLootHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDisenchantLootHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDisenchantLootHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemEffectHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemEffectHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemEffectHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemExtendedCostHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemExtendedCostHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemExtendedCostHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLevelSelectorHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLevelSelectorHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLevelSelectorHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLevelSelectorQualityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLevelSelectorQualityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLevelSelectorQualityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLevelSelectorQualitySetHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLevelSelectorQualitySetHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLevelSelectorQualitySetHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLimitCategoryHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLimitCategoryHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLimitCategoryHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLimitCategoryConditionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLimitCategoryConditionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLimitCategoryConditionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemModifiedAppearanceHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemModifiedAppearanceHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemModifiedAppearanceHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemModifiedAppearanceExtraHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemModifiedAppearanceExtraHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemModifiedAppearanceExtraHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemNameDescriptionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemNameDescriptionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemNameDescriptionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemPriceBaseHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemPriceBaseHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemPriceBaseHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSearchNameHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSearchNameHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSearchNameHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSetHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSetHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSetHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSetSpellHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSetSpellHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSetSpellHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSparseHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSparseHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSparseHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSpecHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSpecHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSpecHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSpecOverrideHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSpecOverrideHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSpecOverrideHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemXBonusTreeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemXBonusTreeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemXBonusTreeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemXItemEffectHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemXItemEffectHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemXItemEffectHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterSectionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterSectionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterSectionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalInstanceHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalInstanceHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalInstanceHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalTierHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalTierHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalTierHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.KeychainHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.KeychainHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.KeychainHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.KeystoneAffixHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.KeystoneAffixHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.KeystoneAffixHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguageWordsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguageWordsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguageWordsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguagesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguagesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguagesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LfgDungeonsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LfgDungeonsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LfgDungeonsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LfgDungeonsHotfixes1105.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LfgDungeonsHotfixes1105, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LfgDungeonsHotfixes1105, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LightHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LightHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LightHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LiquidTypeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LiquidTypeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LiquidTypeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LocationHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LocationHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LocationHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LockHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LockHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LockHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MailTemplateHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MailTemplateHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MailTemplateHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapChallengeModeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapChallengeModeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapChallengeModeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyHotfixes1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyHotfixes1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyHotfixes1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyXConditionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyXConditionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyXConditionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MawPowerHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MawPowerHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MawPowerHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ModifierTreeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ModifierTreeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ModifierTreeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountCapabilityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountCapabilityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountCapabilityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountEquipmentHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountEquipmentHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountEquipmentHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountTypeXCapabilityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountTypeXCapabilityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountTypeXCapabilityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountXDisplayHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountXDisplayHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountXDisplayHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MovieHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MovieHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MovieHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MythicPlusSeasonHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MythicPlusSeasonHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MythicPlusSeasonHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NameGenHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NameGenHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NameGenHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NamesProfanityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NamesProfanityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NamesProfanityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NamesReservedHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NamesReservedHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NamesReservedHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NamesReservedLocaleHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NamesReservedLocaleHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NamesReservedLocaleHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NumTalentsAtLevelHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NumTalentsAtLevelHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NumTalentsAtLevelHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.OverrideSpellDataHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.OverrideSpellDataHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.OverrideSpellDataHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ParagonReputationHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ParagonReputationHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ParagonReputationHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PathHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PathHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PathHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PathNodeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PathNodeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PathNodeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PathPropertyHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PathPropertyHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PathPropertyHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PhaseHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PhaseHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PhaseHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PhaseXPhaseGroupHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PhaseXPhaseGroupHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PhaseXPhaseGroupHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PlayerConditionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PowerDisplayHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PowerDisplayHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PowerDisplayHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PowerTypeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PowerTypeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PowerTypeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PrestigeLevelInfoHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PrestigeLevelInfoHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PrestigeLevelInfoHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpDifficultyHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpDifficultyHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpDifficultyHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpItemHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpItemHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpItemHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpSeasonHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpSeasonHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpSeasonHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpStatHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpStatHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpStatHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentCategoryHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentCategoryHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentCategoryHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentSlotUnlockHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentSlotUnlockHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentSlotUnlockHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTierHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTierHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTierHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestFactionRewardHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestFactionRewardHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestFactionRewardHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestInfoHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestInfoHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestInfoHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestLineXQuestHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestLineXQuestHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestLineXQuestHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestMoneyRewardHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestMoneyRewardHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestMoneyRewardHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestPackageItemHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestPackageItemHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestPackageItemHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestSortHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestSortHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestSortHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestSortHotfixes1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestSortHotfixes1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestSortHotfixes1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestV2Hotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestV2Hotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestV2Hotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestXpHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestXpHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestXpHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RandPropPointsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RandPropPointsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RandPropPointsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RewardPackHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RewardPackHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RewardPackHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RewardPackXCurrencyTypeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RewardPackXCurrencyTypeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RewardPackXCurrencyTypeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RewardPackXItemHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RewardPackXItemHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RewardPackXItemHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioStepHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioStepHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioStepHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptGlobalTextHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptGlobalTextHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptGlobalTextHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptPackageHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptPackageHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptPackageHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptTextHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptTextHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptTextHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ServerMessagesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ServerMessagesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ServerMessagesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineAbilityHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineAbilityHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineAbilityHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineXTraitTreeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineXTraitTreeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineXTraitTreeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillRaceClassInfoHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillRaceClassInfoHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillRaceClassInfoHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SoulbindConduitRankHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SoulbindConduitRankHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SoulbindConduitRankHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SoundKitHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SoundKitHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SoundKitHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpecializationSpellsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpecializationSpellsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpecializationSpellsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpecSetMemberHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpecSetMemberHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpecSetMemberHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellAuraOptionsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellAuraOptionsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellAuraOptionsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellAuraRestrictionsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellAuraRestrictionsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellAuraRestrictionsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCastTimesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCastTimesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCastTimesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCastingRequirementsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCastingRequirementsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCastingRequirementsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoriesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoriesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoriesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoryHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoryHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoryHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellClassOptionsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellClassOptionsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellClassOptionsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCooldownsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCooldownsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCooldownsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellDurationHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellDurationHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellDurationHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellEffectHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellEffectHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellEffectHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellEmpowerHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellEmpowerHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellEmpowerHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellEmpowerStageHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellEmpowerStageHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellEmpowerStageHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellEquippedItemsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellEquippedItemsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellEquippedItemsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellFocusObjectHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellFocusObjectHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellFocusObjectHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellInterruptsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellInterruptsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellInterruptsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellKeyboundOverrideHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellKeyboundOverrideHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellKeyboundOverrideHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentConditionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentConditionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentConditionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellLabelHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellLabelHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellLabelHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellLearnSpellHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellLearnSpellHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellLearnSpellHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellLevelsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellLevelsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellLevelsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellMiscHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellMiscHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellMiscHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellMiscHotfixes1105.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellMiscHotfixes1105, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellMiscHotfixes1105, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellNameHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellNameHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellNameHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellPowerHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellPowerHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellPowerHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellPowerDifficultyHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellPowerDifficultyHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellPowerDifficultyHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellProcsPerMinuteHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellProcsPerMinuteHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellProcsPerMinuteHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellProcsPerMinuteModHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellProcsPerMinuteModHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellProcsPerMinuteModHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellRadiusHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellRadiusHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellRadiusHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellRangeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellRangeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellRangeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellReagentsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellReagentsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellReagentsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellReagentsCurrencyHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellReagentsCurrencyHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellReagentsCurrencyHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellScalingHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellScalingHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellScalingHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftFormHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftFormHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftFormHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftFormHotfixes1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftFormHotfixes1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftFormHotfixes1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellTargetRestrictionsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellTargetRestrictionsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellTargetRestrictionsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellTotemsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellTotemsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellTotemsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualEffectNameHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualEffectNameHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualEffectNameHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualMissileHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualMissileHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualMissileHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualKitHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualKitHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualKitHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellXSpellVisualHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellXSpellVisualHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellXSpellVisualHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SummonPropertiesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SummonPropertiesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SummonPropertiesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TactKeyHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TactKeyHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TactKeyHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiNodesHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiNodesHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiNodesHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiPathHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiPathHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiPathHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiPathNodeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiPathNodeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiPathNodeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TotemCategoryHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TotemCategoryHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TotemCategoryHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ToyHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ToyHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ToyHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogHolidayHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogHolidayHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogHolidayHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCondHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCondHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCondHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCondHotfixes1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCondHotfixes1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCondHotfixes1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCostHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCostHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCostHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCurrencyHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCurrencyHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCurrencyHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCurrencySourceHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCurrencySourceHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCurrencySourceHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitDefinitionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitDefinitionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitDefinitionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitDefinitionEffectPointsHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitDefinitionEffectPointsHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitDefinitionEffectPointsHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitEdgeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitEdgeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitEdgeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeEntryHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeEntryHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeEntryHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeEntryXTraitCondHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeEntryXTraitCondHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeEntryXTraitCondHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeEntryXTraitCostHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeEntryXTraitCostHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeEntryXTraitCostHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeGroupHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeGroupHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeGroupHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeGroupXTraitCondHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeGroupXTraitCondHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeGroupXTraitCondHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeGroupXTraitCostHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeGroupXTraitCostHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeGroupXTraitCostHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeGroupXTraitNodeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeGroupXTraitNodeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeGroupXTraitNodeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeXTraitCondHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeXTraitCondHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeXTraitCondHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeXTraitCostHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeXTraitCostHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeXTraitCostHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeXTraitNodeEntryHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeXTraitNodeEntryHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeXTraitNodeEntryHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitSubTreeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitSubTreeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitSubTreeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeLoadoutHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeLoadoutHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeLoadoutHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeLoadoutEntryHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeLoadoutEntryHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeLoadoutEntryHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeXTraitCostHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeXTraitCostHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeXTraitCostHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeXTraitCurrencyHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeXTraitCurrencyHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeXTraitCurrencyHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogIllusionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogIllusionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogIllusionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetGroupHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetGroupHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetGroupHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetItemHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetItemHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetItemHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransportAnimationHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransportAnimationHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransportAnimationHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransportRotationHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransportRotationHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransportRotationHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixes1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixes1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixes1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapAssignmentHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapAssignmentHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapAssignmentHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapLinkHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapLinkHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapLinkHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapXMapArtHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapXMapArtHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapXMapArtHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiSplashScreenHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiSplashScreenHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiSplashScreenHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitConditionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitConditionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitConditionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitPowerBarHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitPowerBarHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitPowerBarHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VehicleHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VehicleHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VehicleHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VehicleSeatHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VehicleSeatHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VehicleSeatHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VignetteHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VignetteHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VignetteHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WmoAreaTableHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WmoAreaTableHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WmoAreaTableHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WorldEffectHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WorldEffectHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WorldEffectHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WorldMapOverlayHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WorldMapOverlayHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WorldMapOverlayHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WorldStateExpressionHotfixes1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WorldStateExpressionHotfixes1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WorldStateExpressionHotfixes1100, hotfixes, StoreNameType.None);
                    }

                    // WotLK Classic Hotfix Builder
                    if (!Storage.AchievementHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementCategoryHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementCategoryHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementCategoryHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AdventureJournalHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureJournalHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureJournalHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AdventureMapPOIHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureMapPOIHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureMapPOIHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AnimationDataHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AnimationDataHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AnimationDataHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AnimKitHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AnimKitHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AnimKitHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaGroupMemberHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaGroupMemberHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaGroupMemberHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTableHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTableHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTableHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTableHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTableHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTableHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTriggerHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTriggerHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTriggerHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArmorLocationHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArmorLocationHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArmorLocationHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactAppearanceHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactAppearanceHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactAppearanceHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactAppearanceSetHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactAppearanceSetHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactAppearanceSetHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactCategoryHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactCategoryHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactCategoryHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactPowerHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactPowerHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactPowerHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactPowerLinkHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactPowerLinkHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactPowerLinkHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactPowerPickerHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactPowerPickerHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactPowerPickerHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactPowerRankHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactPowerRankHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactPowerRankHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactQuestXpHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactQuestXpHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactQuestXpHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactTierHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactTierHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactTierHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactUnlockHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactUnlockHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactUnlockHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AuctionHouseHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AuctionHouseHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AuctionHouseHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEmpoweredItemHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEmpoweredItemHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEmpoweredItemHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEssenceHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEssenceHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEssenceHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEssencePowerHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEssencePowerHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEssencePowerHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteItemHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteItemHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteItemHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteItemMilestonePowerHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteItemMilestonePowerHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteItemMilestonePowerHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteKnowledgeMultiplierHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteKnowledgeMultiplierHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteKnowledgeMultiplierHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteLevelInfoHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteLevelInfoHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteLevelInfoHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeritePowerHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeritePowerHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeritePowerHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeritePowerSetMemberHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeritePowerSetMemberHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeritePowerSetMemberHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteTierUnlockHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteTierUnlockHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteTierUnlockHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteTierUnlockSetHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteTierUnlockSetHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteTierUnlockSetHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BankBagSlotPricesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BankBagSlotPricesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BankBagSlotPricesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BannedAddonsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BannedAddonsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BannedAddonsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BarberShopStyleHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BarberShopStyleHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BarberShopStyleHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetAbilityHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetAbilityHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetAbilityHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetBreedQualityHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetBreedQualityHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetBreedQualityHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetBreedStateHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetBreedStateHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetBreedStateHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesStateHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesStateHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesStateHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlemasterListHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlemasterListHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlemasterListHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CfgCategoriesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CfgCategoriesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CfgCategoriesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CfgRegionsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CfgRegionsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CfgRegionsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharTitlesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharTitlesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharTitlesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharacterLoadoutHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharacterLoadoutHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharacterLoadoutHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharacterLoadoutItemHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharacterLoadoutItemHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharacterLoadoutItemHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChatChannelsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChatChannelsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChatChannelsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChatChannelsHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChatChannelsHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChatChannelsHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassUiDisplayHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassUiDisplayHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassUiDisplayHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesXPowerTypesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesXPowerTypesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesXPowerTypesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixes342.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixes342, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixes342, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationDisplayInfoHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationDisplayInfoHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationDisplayInfoHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationElementHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationElementHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationElementHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationElementHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationElementHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationElementHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationElementHotfixes342.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationElementHotfixes342, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationElementHotfixes342, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationElementHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationElementHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationElementHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationOptionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationOptionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationOptionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqChoiceHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqChoiceHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqChoiceHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrModelHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrModelHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrModelHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrModelHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrModelHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrModelHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRaceXChrModelHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRaceXChrModelHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRaceXChrModelHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRaceXChrModelHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRaceXChrModelHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRaceXChrModelHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRacesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRacesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRacesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrSpecializationHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrSpecializationHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrSpecializationHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CinematicCameraHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CinematicCameraHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CinematicCameraHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CinematicSequencesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CinematicSequencesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CinematicSequencesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ConditionalChrModelHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConditionalChrModelHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConditionalChrModelHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ConditionalContentTuningHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConditionalContentTuningHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConditionalContentTuningHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ContentTuningHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ContentTuningHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ContentTuningHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ConversationLineHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConversationLineHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConversationLineHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureDisplayInfoHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureDisplayInfoHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureDisplayInfoHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureDisplayInfoHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureDisplayInfoHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureDisplayInfoHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureDisplayInfoExtraHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureDisplayInfoExtraHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureDisplayInfoExtraHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureFamilyHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureFamilyHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureFamilyHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureModelDataHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureModelDataHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureModelDataHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureTypeHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureTypeHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureTypeHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaTreeHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaTreeHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaTreeHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaTreeHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaTreeHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaTreeHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyContainerHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyContainerHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyContainerHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurveHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurveHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurveHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurveHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurveHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurveHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurvePointHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurvePointHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurvePointHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurvePointHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurvePointHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurvePointHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurvePointHotfixes342.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurvePointHotfixes342, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurvePointHotfixes342, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DestructibleModelDataHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DestructibleModelDataHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DestructibleModelDataHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DungeonEncounterHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DungeonEncounterHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DungeonEncounterHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DungeonEncounterHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DungeonEncounterHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DungeonEncounterHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DungeonEncounterHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DungeonEncounterHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DungeonEncounterHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DurabilityCostsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DurabilityCostsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DurabilityCostsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DurabilityQualityHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DurabilityQualityHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DurabilityQualityHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesTextHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesTextHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesTextHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesTextSoundHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesTextSoundHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesTextSoundHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ExpectedStatHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ExpectedStatHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ExpectedStatHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ExpectedStatModHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ExpectedStatModHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ExpectedStatModHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionTemplateHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionTemplateHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionTemplateHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionTemplateHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionTemplateHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionTemplateHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipRepReactionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipRepReactionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipRepReactionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipReputationHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipReputationHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipReputationHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipReputationHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipReputationHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipReputationHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectArtKitHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectArtKitHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectArtKitHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectDisplayInfoHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectDisplayInfoHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectDisplayInfoHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrAbilityHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrAbilityHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrAbilityHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrBuildingHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrBuildingHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrBuildingHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrBuildingPlotInstHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrBuildingPlotInstHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrBuildingPlotInstHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrClassSpecHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrClassSpecHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrClassSpecHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrFollowerHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrFollowerHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrFollowerHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrFollowerXAbilityHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrFollowerXAbilityHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrFollowerXAbilityHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrMissionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrMissionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrMissionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrPlotHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrPlotHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrPlotHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrPlotBuildingHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrPlotBuildingHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrPlotBuildingHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrPlotInstanceHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrPlotInstanceHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrPlotInstanceHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrSiteLevelHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrSiteLevelHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrSiteLevelHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrSiteLevelPlotInstHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrSiteLevelPlotInstHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrSiteLevelPlotInstHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrTalentTreeHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrTalentTreeHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrTalentTreeHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GemPropertiesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GemPropertiesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GemPropertiesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphBindableSpellHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphBindableSpellHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphBindableSpellHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphSlotHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphSlotHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphSlotHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphPropertiesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphPropertiesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphPropertiesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphRequiredSpecHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphRequiredSpecHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphRequiredSpecHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GossipNPCOptionHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GossipNPCOptionHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GossipNPCOptionHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildColorBackgroundHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildColorBackgroundHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildColorBackgroundHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildColorBorderHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildColorBorderHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildColorBorderHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildColorEmblemHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildColorEmblemHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildColorEmblemHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildPerkSpellsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildPerkSpellsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildPerkSpellsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.HeirloomHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.HeirloomHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.HeirloomHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.HolidaysHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.HolidaysHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.HolidaysHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceArmorHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceArmorHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceArmorHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceQualityHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceQualityHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceQualityHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceShieldHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceShieldHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceShieldHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceWeaponHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceWeaponHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceWeaponHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemAppearanceHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemAppearanceHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemAppearanceHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemArmorQualityHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemArmorQualityHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemArmorQualityHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemArmorShieldHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemArmorShieldHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemArmorShieldHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemArmorTotalHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemArmorTotalHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemArmorTotalHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBagFamilyHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBagFamilyHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBagFamilyHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusListLevelDeltaHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusListLevelDeltaHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusListLevelDeltaHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusTreeNodeHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusTreeNodeHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusTreeNodeHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemChildEquipmentHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemChildEquipmentHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemChildEquipmentHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemClassHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemClassHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemClassHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemContextPickerEntryHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemContextPickerEntryHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemContextPickerEntryHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemCurrencyCostHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemCurrencyCostHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemCurrencyCostHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageAmmoHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageAmmoHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageAmmoHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageOneHandHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageOneHandHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageOneHandHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageOneHandCasterHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageOneHandCasterHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageOneHandCasterHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageTwoHandHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageTwoHandHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageTwoHandHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageTwoHandCasterHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageTwoHandCasterHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageTwoHandCasterHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDisenchantLootHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDisenchantLootHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDisenchantLootHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemEffectHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemEffectHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemEffectHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemExtendedCostHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemExtendedCostHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemExtendedCostHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemExtendedCostHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemExtendedCostHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemExtendedCostHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLevelSelectorHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLevelSelectorHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLevelSelectorHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLevelSelectorQualityHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLevelSelectorQualityHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLevelSelectorQualityHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLevelSelectorQualitySetHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLevelSelectorQualitySetHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLevelSelectorQualitySetHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLimitCategoryHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLimitCategoryHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLimitCategoryHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLimitCategoryConditionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLimitCategoryConditionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLimitCategoryConditionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemModifiedAppearanceHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemModifiedAppearanceHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemModifiedAppearanceHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemModifiedAppearanceExtraHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemModifiedAppearanceExtraHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemModifiedAppearanceExtraHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemNameDescriptionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemNameDescriptionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemNameDescriptionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemPriceBaseHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemPriceBaseHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemPriceBaseHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemRandomPropertiesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemRandomPropertiesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemRandomPropertiesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemRandomSuffixHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemRandomSuffixHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemRandomSuffixHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSearchNameHotfixes342.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSearchNameHotfixes342, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSearchNameHotfixes342, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSetHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSetHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSetHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSetSpellHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSetSpellHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSetSpellHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSparseHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSparseHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSparseHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSparseHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSparseHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSparseHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSpecHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSpecHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSpecHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSpecOverrideHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSpecOverrideHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSpecOverrideHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemXBonusTreeHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemXBonusTreeHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemXBonusTreeHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterSectionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterSectionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterSectionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalInstanceHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalInstanceHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalInstanceHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalInstanceHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalInstanceHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalInstanceHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalTierHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalTierHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalTierHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.KeychainHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.KeychainHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.KeychainHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.KeystoneAffixHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.KeystoneAffixHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.KeystoneAffixHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguageWordsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguageWordsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguageWordsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguagesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguagesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguagesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguagesHotfixes342.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguagesHotfixes342, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguagesHotfixes342, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LfgDungeonsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LfgDungeonsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LfgDungeonsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LfgDungeonsHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LfgDungeonsHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LfgDungeonsHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LightHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LightHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LightHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LiquidTypeHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LiquidTypeHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LiquidTypeHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LockHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LockHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LockHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MailTemplateHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MailTemplateHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MailTemplateHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapChallengeModeHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapChallengeModeHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapChallengeModeHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyXConditionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyXConditionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyXConditionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ModifierTreeHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ModifierTreeHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ModifierTreeHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ModifierTreeHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ModifierTreeHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ModifierTreeHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountCapabilityHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountCapabilityHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountCapabilityHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountTypeXCapabilityHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountTypeXCapabilityHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountTypeXCapabilityHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountXDisplayHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountXDisplayHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountXDisplayHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MovieHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MovieHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MovieHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MythicPlusSeasonHotfixes342.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MythicPlusSeasonHotfixes342, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MythicPlusSeasonHotfixes342, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NameGenHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NameGenHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NameGenHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NamesProfanityHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NamesProfanityHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NamesProfanityHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NamesReservedHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NamesReservedHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NamesReservedHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NamesReservedLocaleHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NamesReservedLocaleHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NamesReservedLocaleHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NumTalentsAtLevelHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NumTalentsAtLevelHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NumTalentsAtLevelHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.OverrideSpellDataHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.OverrideSpellDataHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.OverrideSpellDataHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ParagonReputationHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ParagonReputationHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ParagonReputationHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PhaseHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PhaseHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PhaseHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PhaseXPhaseGroupHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PhaseXPhaseGroupHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PhaseXPhaseGroupHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PlayerConditionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PowerDisplayHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PowerDisplayHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PowerDisplayHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PowerTypeHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PowerTypeHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PowerTypeHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PowerTypeHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PowerTypeHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PowerTypeHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PrestigeLevelInfoHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PrestigeLevelInfoHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PrestigeLevelInfoHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpDifficultyHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpDifficultyHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpDifficultyHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpItemHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpItemHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpItemHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpSeasonHotfixes342.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpSeasonHotfixes342, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpSeasonHotfixes342, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentCategoryHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentCategoryHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentCategoryHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentSlotUnlockHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentSlotUnlockHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentSlotUnlockHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTierHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTierHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTierHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestFactionRewardHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestFactionRewardHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestFactionRewardHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestInfoHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestInfoHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestInfoHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestLineXQuestHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestLineXQuestHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestLineXQuestHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestMoneyRewardHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestMoneyRewardHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestMoneyRewardHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestPackageItemHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestPackageItemHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestPackageItemHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestSortHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestSortHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestSortHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestV2Hotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestV2Hotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestV2Hotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestXpHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestXpHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestXpHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RandPropPointsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RandPropPointsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RandPropPointsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RewardPackHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RewardPackHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RewardPackHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RewardPackXCurrencyTypeHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RewardPackXCurrencyTypeHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RewardPackXCurrencyTypeHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RewardPackXItemHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RewardPackXItemHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RewardPackXItemHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioStepHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioStepHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioStepHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScalingStatDistributionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScalingStatDistributionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScalingStatDistributionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScalingStatDistributionHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScalingStatDistributionHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScalingStatDistributionHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScalingStatValuesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScalingStatValuesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScalingStatValuesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptGlobalTextHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptGlobalTextHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptGlobalTextHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptPackageHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptPackageHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptPackageHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptTextHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptTextHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptTextHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ServerMessagesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ServerMessagesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ServerMessagesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineAbilityHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineAbilityHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineAbilityHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineXTraitTreeHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineXTraitTreeHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineXTraitTreeHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillRaceClassInfoHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillRaceClassInfoHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillRaceClassInfoHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SoundKitHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SoundKitHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SoundKitHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SoundKitHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SoundKitHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SoundKitHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpecializationSpellsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpecializationSpellsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpecializationSpellsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpecSetMemberHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpecSetMemberHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpecSetMemberHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellAuraOptionsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellAuraOptionsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellAuraOptionsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellAuraRestrictionsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellAuraRestrictionsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellAuraRestrictionsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCastTimesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCastTimesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCastTimesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCastingRequirementsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCastingRequirementsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCastingRequirementsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCastingRequirementsHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCastingRequirementsHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCastingRequirementsHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoriesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoriesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoriesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoryHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoryHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoryHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoryHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoryHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoryHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellClassOptionsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellClassOptionsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellClassOptionsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCooldownsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCooldownsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCooldownsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellDurationHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellDurationHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellDurationHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellEffectHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellEffectHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellEffectHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellEquippedItemsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellEquippedItemsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellEquippedItemsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellFocusObjectHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellFocusObjectHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellFocusObjectHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellInterruptsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellInterruptsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellInterruptsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentConditionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentConditionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentConditionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellKeyboundOverrideHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellKeyboundOverrideHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellKeyboundOverrideHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellLabelHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellLabelHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellLabelHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellLearnSpellHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellLearnSpellHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellLearnSpellHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellLevelsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellLevelsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellLevelsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellMiscHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellMiscHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellMiscHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellMiscHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellMiscHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellMiscHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellNameHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellNameHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellNameHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellPowerHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellPowerHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellPowerHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellPowerDifficultyHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellPowerDifficultyHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellPowerDifficultyHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellProcsPerMinuteHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellProcsPerMinuteHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellProcsPerMinuteHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellProcsPerMinuteModHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellProcsPerMinuteModHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellProcsPerMinuteModHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellRadiusHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellRadiusHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellRadiusHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellRangeHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellRangeHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellRangeHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellReagentsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellReagentsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellReagentsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellReagentsCurrencyHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellReagentsCurrencyHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellReagentsCurrencyHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellScalingHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellScalingHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellScalingHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftFormHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftFormHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftFormHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellTargetRestrictionsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellTargetRestrictionsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellTargetRestrictionsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellTotemsHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellTotemsHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellTotemsHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualEffectNameHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualEffectNameHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualEffectNameHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualMissileHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualMissileHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualMissileHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualKitHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualKitHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualKitHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellXSpellVisualHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellXSpellVisualHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellXSpellVisualHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SummonPropertiesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SummonPropertiesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SummonPropertiesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentTabHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentTabHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentTabHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiNodesHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiNodesHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiNodesHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiNodesHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiNodesHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiNodesHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiPathHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiPathHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiPathHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiPathNodeHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiPathNodeHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiPathNodeHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiPathNodeHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiPathNodeHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiPathNodeHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TotemCategoryHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TotemCategoryHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TotemCategoryHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ToyHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ToyHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ToyHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogHolidayHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogHolidayHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogHolidayHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCondHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCondHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCondHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCostHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCostHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCostHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCurrencyHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCurrencyHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCurrencyHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCurrencySourceHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCurrencySourceHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCurrencySourceHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitDefinitionHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitDefinitionHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitDefinitionHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitDefinitionEffectPointsHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitDefinitionEffectPointsHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitDefinitionEffectPointsHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitEdgeHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitEdgeHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitEdgeHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeEntryHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeEntryHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeEntryHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeEntryXTraitCondHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeEntryXTraitCondHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeEntryXTraitCondHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeEntryXTraitCostHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeEntryXTraitCostHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeEntryXTraitCostHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeGroupHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeGroupHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeGroupHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeGroupXTraitCondHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeGroupXTraitCondHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeGroupXTraitCondHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeGroupXTraitCostHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeGroupXTraitCostHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeGroupXTraitCostHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeGroupXTraitNodeHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeGroupXTraitNodeHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeGroupXTraitNodeHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeXTraitCondHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeXTraitCondHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeXTraitCondHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeXTraitCostHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeXTraitCostHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeXTraitCostHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitNodeXTraitNodeEntryHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitNodeXTraitNodeEntryHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitNodeXTraitNodeEntryHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeLoadoutHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeLoadoutHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeLoadoutHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeLoadoutEntryHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeLoadoutEntryHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeLoadoutEntryHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeXTraitCostHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeXTraitCostHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeXTraitCostHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitTreeXTraitCurrencyHotfixes341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitTreeXTraitCurrencyHotfixes341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitTreeXTraitCurrencyHotfixes341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetGroupHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetGroupHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetGroupHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetItemHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetItemHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetItemHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransportAnimationHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransportAnimationHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransportAnimationHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransportRotationHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransportRotationHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransportRotationHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixes343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixes343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixes343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapAssignmentHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapAssignmentHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapAssignmentHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapLinkHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapLinkHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapLinkHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapXMapArtHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapXMapArtHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapXMapArtHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitConditionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitConditionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitConditionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitPowerBarHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitPowerBarHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitPowerBarHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VehicleHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VehicleHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VehicleHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VehicleHotfixes342.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VehicleHotfixes342, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VehicleHotfixes342, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VehicleSeatHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VehicleSeatHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VehicleSeatHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VehicleSeatHotfixes342.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VehicleSeatHotfixes342, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VehicleSeatHotfixes342, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WmoAreaTableHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WmoAreaTableHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WmoAreaTableHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WorldEffectHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WorldEffectHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WorldEffectHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WorldMapOverlayHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WorldMapOverlayHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WorldMapOverlayHotfixes340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WorldStateExpressionHotfixes340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WorldStateExpressionHotfixes340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WorldStateExpressionHotfixes340, hotfixes, StoreNameType.None);
                    }

                    // Cata Classic HotfixBuilder
                    if (!Storage.AchievementHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementCategoryHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementCategoryHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementCategoryHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AnimationDataHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AnimationDataHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AnimationDataHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AnimationDataHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AnimationDataHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AnimationDataHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AnimKitHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AnimKitHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AnimKitHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaGroupMemberHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaGroupMemberHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaGroupMemberHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTableHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTableHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTableHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTriggerHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTriggerHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTriggerHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTriggerHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTriggerHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTriggerHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTriggerActionSetHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTriggerActionSetHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTriggerActionSetHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArmorLocationHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArmorLocationHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArmorLocationHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AuctionHouseHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AuctionHouseHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AuctionHouseHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BankBagSlotPricesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BankBagSlotPricesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BankBagSlotPricesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BannedAddonsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BannedAddonsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BannedAddonsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BarberShopStyleHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BarberShopStyleHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BarberShopStyleHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetAbilityHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetAbilityHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetAbilityHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetBreedQualityHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetBreedQualityHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetBreedQualityHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetBreedStateHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetBreedStateHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetBreedStateHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesStateHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesStateHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesStateHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlemasterListHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlemasterListHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlemasterListHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlemasterListHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlemasterListHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlemasterListHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlemasterListXMapHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlemasterListXMapHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlemasterListXMapHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CfgCategoriesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CfgCategoriesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CfgCategoriesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CfgRegionsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CfgRegionsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CfgRegionsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharTitlesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharTitlesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharTitlesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharacterLoadoutHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharacterLoadoutHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharacterLoadoutHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharacterLoadoutHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharacterLoadoutHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharacterLoadoutHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharacterLoadoutItemHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharacterLoadoutItemHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharacterLoadoutItemHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChatChannelsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChatChannelsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChatChannelsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassUiDisplayHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassUiDisplayHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassUiDisplayHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesXPowerTypesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesXPowerTypesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesXPowerTypesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationDisplayInfoHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationDisplayInfoHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationDisplayInfoHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationElementHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationElementHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationElementHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationOptionHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationOptionHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationOptionHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqChoiceHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqChoiceHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqChoiceHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrModelHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrModelHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrModelHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRaceXChrModelHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRaceXChrModelHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRaceXChrModelHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRacesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRacesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRacesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CinematicCameraHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CinematicCameraHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CinematicCameraHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CinematicSequencesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CinematicSequencesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CinematicSequencesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ConditionalChrModelHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConditionalChrModelHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConditionalChrModelHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ConditionalContentTuningHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConditionalContentTuningHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConditionalContentTuningHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ContentTuningHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ContentTuningHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ContentTuningHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ConversationLineHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ConversationLineHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ConversationLineHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureDisplayInfoHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureDisplayInfoHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureDisplayInfoHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureDisplayInfoHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureDisplayInfoHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureDisplayInfoHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureDisplayInfoExtraHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureDisplayInfoExtraHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureDisplayInfoExtraHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureFamilyHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureFamilyHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureFamilyHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureModelDataHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureModelDataHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureModelDataHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureModelDataHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureModelDataHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureModelDataHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureTypeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureTypeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureTypeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaTreeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaTreeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaTreeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyContainerHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyContainerHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyContainerHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurveHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurveHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurveHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurvePointHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurvePointHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurvePointHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DestructibleModelDataHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DestructibleModelDataHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DestructibleModelDataHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DungeonEncounterHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DungeonEncounterHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DungeonEncounterHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DurabilityCostsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DurabilityCostsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DurabilityCostsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DurabilityQualityHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DurabilityQualityHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DurabilityQualityHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesTextHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesTextHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesTextHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.EmotesTextSoundHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.EmotesTextSoundHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.EmotesTextSoundHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ExpectedStatHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ExpectedStatHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ExpectedStatHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ExpectedStatModHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ExpectedStatModHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ExpectedStatModHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionTemplateHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionTemplateHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionTemplateHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionTemplateHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionTemplateHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionTemplateHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipRepReactionHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipRepReactionHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipRepReactionHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipReputationHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipReputationHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipReputationHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectArtKitHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectArtKitHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectArtKitHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectDisplayInfoHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectDisplayInfoHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectDisplayInfoHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectDisplayInfoHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectDisplayInfoHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectDisplayInfoHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectsHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectsHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectsHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GemPropertiesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GemPropertiesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GemPropertiesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphBindableSpellHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphBindableSpellHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphBindableSpellHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphSlotHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphSlotHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphSlotHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphPropertiesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphPropertiesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphPropertiesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GlyphRequiredSpecHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GlyphRequiredSpecHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GlyphRequiredSpecHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GossipNPCOptionHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GossipNPCOptionHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GossipNPCOptionHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildColorBackgroundHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildColorBackgroundHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildColorBackgroundHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildColorBorderHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildColorBorderHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildColorBorderHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildColorEmblemHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildColorEmblemHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildColorEmblemHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GuildPerkSpellsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GuildPerkSpellsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GuildPerkSpellsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.HeirloomHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.HeirloomHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.HeirloomHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.HolidaysHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.HolidaysHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.HolidaysHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceArmorHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceArmorHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceArmorHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceQualityHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceQualityHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceQualityHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceShieldHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceShieldHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceShieldHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ImportPriceWeaponHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ImportPriceWeaponHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ImportPriceWeaponHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemAppearanceHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemAppearanceHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemAppearanceHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemAppearanceHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemAppearanceHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemAppearanceHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemArmorQualityHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemArmorQualityHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemArmorQualityHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemArmorShieldHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemArmorShieldHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemArmorShieldHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemArmorTotalHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemArmorTotalHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemArmorTotalHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBagFamilyHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBagFamilyHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBagFamilyHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusListLevelDeltaHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusListLevelDeltaHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusListLevelDeltaHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBonusTreeNodeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBonusTreeNodeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBonusTreeNodeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemChildEquipmentHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemChildEquipmentHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemChildEquipmentHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemClassHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemClassHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemClassHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemContextPickerEntryHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemContextPickerEntryHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemContextPickerEntryHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemCurrencyCostHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemCurrencyCostHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemCurrencyCostHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageAmmoHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageAmmoHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageAmmoHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageOneHandHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageOneHandHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageOneHandHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageOneHandCasterHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageOneHandCasterHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageOneHandCasterHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageTwoHandHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageTwoHandHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageTwoHandHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDamageTwoHandCasterHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDamageTwoHandCasterHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDamageTwoHandCasterHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemDisenchantLootHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemDisenchantLootHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemDisenchantLootHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemEffectHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemEffectHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemEffectHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemExtendedCostHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemExtendedCostHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemExtendedCostHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLevelSelectorHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLevelSelectorHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLevelSelectorHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLevelSelectorQualityHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLevelSelectorQualityHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLevelSelectorQualityHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLevelSelectorQualitySetHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLevelSelectorQualitySetHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLevelSelectorQualitySetHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLimitCategoryHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLimitCategoryHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLimitCategoryHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLimitCategoryConditionHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLimitCategoryConditionHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLimitCategoryConditionHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemModifiedAppearanceHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemModifiedAppearanceHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemModifiedAppearanceHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemModifiedAppearanceExtraHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemModifiedAppearanceExtraHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemModifiedAppearanceExtraHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemNameDescriptionHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemNameDescriptionHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemNameDescriptionHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemPriceBaseHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemPriceBaseHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemPriceBaseHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemReforgeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemReforgeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemReforgeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSearchNameHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSearchNameHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSearchNameHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSearchNameHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSearchNameHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSearchNameHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSetHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSetHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSetHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSetSpellHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSetSpellHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSetSpellHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSparseHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSparseHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSparseHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSparseHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSparseHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSparseHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemXBonusTreeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemXBonusTreeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemXBonusTreeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterSectionHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterSectionHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterSectionHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalInstanceHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalInstanceHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalInstanceHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalTierHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalTierHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalTierHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.KeychainHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.KeychainHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.KeychainHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.KeystoneAffixHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.KeystoneAffixHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.KeystoneAffixHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguageWordsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguageWordsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguageWordsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguagesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguagesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguagesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LfgDungeonsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LfgDungeonsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LfgDungeonsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LightHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LightHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LightHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LiquidTypeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LiquidTypeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LiquidTypeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LocationHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LocationHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LocationHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LockHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LockHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LockHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MailTemplateHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MailTemplateHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MailTemplateHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapChallengeModeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapChallengeModeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapChallengeModeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyXConditionHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyXConditionHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyXConditionHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ModifierTreeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ModifierTreeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ModifierTreeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountCapabilityHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountCapabilityHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountCapabilityHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountCapabilityHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountCapabilityHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountCapabilityHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountTypeXCapabilityHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountTypeXCapabilityHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountTypeXCapabilityHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountXDisplayHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountXDisplayHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountXDisplayHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountXDisplayHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountXDisplayHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountXDisplayHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MovieHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MovieHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MovieHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MythicPlusSeasonHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MythicPlusSeasonHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MythicPlusSeasonHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MythicPlusSeasonHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MythicPlusSeasonHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MythicPlusSeasonHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NameGenHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NameGenHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NameGenHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NamesProfanityHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NamesProfanityHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NamesProfanityHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NamesReservedHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NamesReservedHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NamesReservedHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NamesReservedLocaleHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NamesReservedLocaleHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NamesReservedLocaleHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.NumTalentsAtLevelHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.NumTalentsAtLevelHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.NumTalentsAtLevelHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.OverrideSpellDataHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.OverrideSpellDataHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.OverrideSpellDataHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ParagonReputationHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ParagonReputationHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ParagonReputationHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PathHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PathHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PathHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PathNodeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PathNodeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PathNodeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PathPropertyHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PathPropertyHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PathPropertyHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PhaseHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PhaseHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PhaseHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PhaseXPhaseGroupHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PhaseXPhaseGroupHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PhaseXPhaseGroupHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PlayerConditionHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PlayerConditionHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PowerDisplayHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PowerDisplayHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PowerDisplayHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PowerDisplayHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PowerDisplayHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PowerDisplayHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PowerTypeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PowerTypeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PowerTypeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PowerTypeHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PowerTypeHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PowerTypeHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PrestigeLevelInfoHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PrestigeLevelInfoHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PrestigeLevelInfoHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpDifficultyHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpDifficultyHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpDifficultyHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpItemHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpItemHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpItemHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpSeasonHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpSeasonHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpSeasonHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTierHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTierHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTierHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestFactionRewardHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestFactionRewardHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestFactionRewardHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestInfoHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestInfoHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestInfoHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestLineXQuestHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestLineXQuestHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestLineXQuestHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestMoneyRewardHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestMoneyRewardHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestMoneyRewardHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestPackageItemHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestPackageItemHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestPackageItemHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestSortHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestSortHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestSortHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestV2Hotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestV2Hotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestV2Hotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestXpHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestXpHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestXpHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RandPropPointsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RandPropPointsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RandPropPointsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RewardPackHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RewardPackHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RewardPackHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RewardPackXCurrencyTypeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RewardPackXCurrencyTypeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RewardPackXCurrencyTypeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.RewardPackXItemHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.RewardPackXItemHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.RewardPackXItemHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioStepHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioStepHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioStepHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptGlobalTextHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptGlobalTextHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptGlobalTextHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptPackageHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptPackageHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptPackageHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SceneScriptTextHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SceneScriptTextHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SceneScriptTextHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ServerMessagesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ServerMessagesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ServerMessagesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineAbilityHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineAbilityHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineAbilityHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineAbilityHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineAbilityHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineAbilityHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillRaceClassInfoHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillRaceClassInfoHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillRaceClassInfoHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillRaceClassInfoHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillRaceClassInfoHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillRaceClassInfoHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SoundKitHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SoundKitHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SoundKitHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellAuraOptionsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellAuraOptionsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellAuraOptionsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellAuraRestrictionsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellAuraRestrictionsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellAuraRestrictionsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCastTimesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCastTimesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCastTimesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCastingRequirementsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCastingRequirementsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCastingRequirementsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoriesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoriesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoriesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoryHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoryHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoryHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellClassOptionsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellClassOptionsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellClassOptionsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCooldownsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCooldownsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCooldownsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellDurationHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellDurationHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellDurationHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellEffectHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellEffectHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellEffectHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellEquippedItemsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellEquippedItemsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellEquippedItemsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellFocusObjectHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellFocusObjectHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellFocusObjectHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellInterruptsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellInterruptsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellInterruptsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentConditionHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentConditionHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentConditionHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellKeyboundOverrideHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellKeyboundOverrideHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellKeyboundOverrideHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellLabelHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellLabelHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellLabelHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellLearnSpellHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellLearnSpellHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellLearnSpellHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellLevelsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellLevelsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellLevelsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellMiscHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellMiscHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellMiscHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellMiscHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellMiscHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellMiscHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellNameHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellNameHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellNameHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellPowerHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellPowerHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellPowerHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellPowerDifficultyHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellPowerDifficultyHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellPowerDifficultyHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellProcsPerMinuteHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellProcsPerMinuteHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellProcsPerMinuteHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellProcsPerMinuteModHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellProcsPerMinuteModHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellProcsPerMinuteModHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellRadiusHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellRadiusHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellRadiusHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellRangeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellRangeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellRangeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellReagentsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellReagentsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellReagentsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellReagentsCurrencyHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellReagentsCurrencyHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellReagentsCurrencyHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellScalingHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellScalingHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellScalingHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftFormHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftFormHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftFormHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellTargetRestrictionsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellTargetRestrictionsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellTargetRestrictionsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellTotemsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellTotemsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellTotemsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualEffectNameHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualEffectNameHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualEffectNameHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualEffectNameHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualEffectNameHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualEffectNameHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualMissileHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualMissileHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualMissileHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualMissileHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualMissileHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualMissileHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellVisualKitHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellVisualKitHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellVisualKitHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellXSpellVisualHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellXSpellVisualHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellXSpellVisualHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SummonPropertiesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SummonPropertiesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SummonPropertiesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TactKeyHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TactKeyHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TactKeyHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentTabHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentTabHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentTabHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentTreePrimarySpellsHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentTreePrimarySpellsHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentTreePrimarySpellsHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiNodesHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiNodesHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiNodesHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiPathHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiPathHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiPathHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiPathNodeHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiPathNodeHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiPathNodeHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TotemCategoryHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TotemCategoryHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TotemCategoryHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ToyHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ToyHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ToyHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogHolidayHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogHolidayHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogHolidayHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetGroupHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetGroupHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetGroupHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetItemHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetItemHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetItemHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransportAnimationHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransportAnimationHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransportAnimationHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransportRotationHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransportRotationHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransportRotationHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapAssignmentHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapAssignmentHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapAssignmentHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapLinkHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapLinkHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapLinkHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapXMapArtHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapXMapArtHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapXMapArtHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitConditionHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitConditionHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitConditionHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitConditionHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitConditionHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitConditionHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitPowerBarHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitPowerBarHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitPowerBarHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VehicleHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VehicleHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VehicleHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VehicleSeatHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VehicleSeatHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VehicleSeatHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VehicleSeatHotfixes441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VehicleSeatHotfixes441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VehicleSeatHotfixes441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VignetteHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VignetteHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VignetteHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WmoAreaTableHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WmoAreaTableHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WmoAreaTableHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WorldEffectHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WorldEffectHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WorldEffectHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WorldMapOverlayHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WorldMapOverlayHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WorldMapOverlayHotfixes440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WorldStateExpressionHotfixes440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WorldStateExpressionHotfixes440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WorldStateExpressionHotfixes440, hotfixes, StoreNameType.None);
                    }
                }
                else
                {
                    // Dragonflight Hotfix Builder
                    if (!Storage.AchievementHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementHotfixesLocale1026.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixesLocale1026, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixesLocale1026, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementHotfixesLocale1027.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixesLocale1027, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixesLocale1027, hotfixes, StoreNameType.None);
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

                    if (!Storage.AdventureJournalHotfixesLocale1026.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureJournalHotfixesLocale1026, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureJournalHotfixesLocale1026, hotfixes, StoreNameType.None);
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

                    if (!Storage.MountHotfixesLocale1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountHotfixesLocale1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountHotfixesLocale1020, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PlayerConditionHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PlayerConditionHotfixesLocale1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixesLocale1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixesLocale1020, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PrestigeLevelInfoHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PrestigeLevelInfoHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PrestigeLevelInfoHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpStatHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpStatHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpStatHotfixesLocale1000, hotfixes, StoreNameType.None);
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

                    if (!Storage.ServerMessagesHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ServerMessagesHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ServerMessagesHotfixesLocale1000, hotfixes, StoreNameType.None);
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

                    if (!Storage.SpellShapeshiftFormHotfixesLocale1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftFormHotfixesLocale1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftFormHotfixesLocale1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.UiMapHotfixesLocale1020.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixesLocale1020, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixesLocale1020, hotfixes, StoreNameType.None);
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

                    if (!Storage.VignetteHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VignetteHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VignetteHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WmoAreaTableHotfixesLocale1000.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WmoAreaTableHotfixesLocale1000, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WmoAreaTableHotfixesLocale1000, hotfixes, StoreNameType.None);
                    }

                    // The War Within Hotfix Builder
                    if (!Storage.AchievementHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementCategoryHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementCategoryHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementCategoryHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AdventureJournalHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureJournalHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureJournalHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AdventureMapPoiHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureMapPoiHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureMapPoiHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTableHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTableHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTableHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactAppearanceHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactAppearanceHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactAppearanceHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactAppearanceSetHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactAppearanceSetHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactAppearanceSetHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AuctionHouseHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AuctionHouseHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AuctionHouseHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEssenceHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEssenceHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEssenceHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEssencePowerHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEssencePowerHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEssencePowerHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BarberShopStyleHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BarberShopStyleHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BarberShopStyleHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlemasterListHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlemasterListHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlemasterListHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetAbilityHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetAbilityHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetAbilityHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharTitlesHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharTitlesHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharTitlesHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChatChannelsHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChatChannelsHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChatChannelsHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationOptionHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationOptionHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationOptionHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationOptionHotfixesLocale1105.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationOptionHotfixesLocale1105, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationOptionHotfixesLocale1105, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRacesHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRacesHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRacesHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrSpecializationHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrSpecializationHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrSpecializationHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureFamilyHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureFamilyHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureFamilyHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureTypeHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureTypeHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureTypeHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaTreeHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaTreeHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaTreeHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyContainerHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyContainerHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyContainerHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixesLocale1105.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixesLocale1105, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixesLocale1105, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DungeonEncounterHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DungeonEncounterHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DungeonEncounterHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipRepReactionHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipRepReactionHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipRepReactionHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipRepReactionHotfixesLocale1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipRepReactionHotfixesLocale1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipRepReactionHotfixesLocale1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipReputationHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipReputationHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipReputationHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectsHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectsHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectsHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrAbilityHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrAbilityHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrAbilityHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrBuildingHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrBuildingHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrBuildingHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrClassSpecHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrClassSpecHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrClassSpecHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrFollowerHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrFollowerHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrFollowerHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrMissionHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrMissionHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrMissionHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrMissionHotfixesLocale1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrMissionHotfixesLocale1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrMissionHotfixesLocale1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrTalentTreeHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrTalentTreeHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrTalentTreeHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.HeirloomHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.HeirloomHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.HeirloomHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBagFamilyHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBagFamilyHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBagFamilyHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemClassHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemClassHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemClassHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLimitCategoryHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLimitCategoryHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLimitCategoryHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemNameDescriptionHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemNameDescriptionHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemNameDescriptionHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSearchNameHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSearchNameHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSearchNameHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSetHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSetHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSetHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSparseHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSparseHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSparseHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterSectionHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterSectionHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterSectionHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalInstanceHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalInstanceHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalInstanceHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalTierHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalTierHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalTierHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.KeystoneAffixHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.KeystoneAffixHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.KeystoneAffixHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguagesHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguagesHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguagesHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LfgDungeonsHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LfgDungeonsHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LfgDungeonsHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LfgDungeonsHotfixesLocale1105.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LfgDungeonsHotfixesLocale1105, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LfgDungeonsHotfixesLocale1105, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MailTemplateHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MailTemplateHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MailTemplateHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapChallengeModeHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapChallengeModeHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapChallengeModeHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyHotfixesLocale1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyHotfixesLocale1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyHotfixesLocale1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyXConditionHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyXConditionHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyXConditionHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PlayerConditionHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PrestigeLevelInfoHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PrestigeLevelInfoHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PrestigeLevelInfoHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpStatHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpStatHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpStatHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTierHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTierHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTierHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestInfoHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestInfoHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestInfoHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestSortHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestSortHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestSortHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestSortHotfixesLocale1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestSortHotfixesLocale1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestSortHotfixesLocale1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioStepHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioStepHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioStepHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ServerMessagesHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ServerMessagesHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ServerMessagesHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineAbilityHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineAbilityHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineAbilityHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpecializationSpellsHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpecializationSpellsHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpecializationSpellsHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoryHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoryHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoryHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellFocusObjectHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellFocusObjectHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellFocusObjectHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellNameHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellNameHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellNameHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellRangeHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellRangeHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellRangeHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftFormHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftFormHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftFormHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftFormHotfixesLocale1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftFormHotfixesLocale1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftFormHotfixesLocale1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiNodesHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiNodesHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiNodesHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TotemCategoryHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TotemCategoryHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TotemCategoryHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ToyHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ToyHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ToyHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCurrencySourceHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCurrencySourceHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCurrencySourceHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitDefinitionHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitDefinitionHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitDefinitionHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitSubTreeHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitSubTreeHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitSubTreeHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetGroupHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetGroupHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetGroupHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixesLocale1102.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixesLocale1102, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixesLocale1102, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiSplashScreenHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiSplashScreenHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiSplashScreenHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitPowerBarHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitPowerBarHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitPowerBarHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VignetteHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VignetteHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VignetteHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WmoAreaTableHotfixesLocale1100.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WmoAreaTableHotfixesLocale1100, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WmoAreaTableHotfixesLocale1100, hotfixes, StoreNameType.None);
                    }

                    // Wotlk Classic Hotfix Builder
                    if (!Storage.AchievementHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementHotfixesLocale343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixesLocale343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixesLocale343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementCategoryHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementCategoryHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementCategoryHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AdventureJournalHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureJournalHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureJournalHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AdventureMapPOIHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AdventureMapPOIHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AdventureMapPOIHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTableHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTableHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTableHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTableHotfixesLocale343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTableHotfixesLocale343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTableHotfixesLocale343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTriggerHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTriggerHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTriggerHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactAppearanceHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactAppearanceHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactAppearanceHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ArtifactAppearanceSetHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ArtifactAppearanceSetHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ArtifactAppearanceSetHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AuctionHouseHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AuctionHouseHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AuctionHouseHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEssenceHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEssenceHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEssenceHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AzeriteEssencePowerHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AzeriteEssencePowerHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AzeriteEssencePowerHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BarberShopStyleHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BarberShopStyleHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BarberShopStyleHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetAbilityHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetAbilityHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetAbilityHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixesLocale341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixesLocale341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixesLocale341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlemasterListHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlemasterListHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlemasterListHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CfgCategoriesHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CfgCategoriesHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CfgCategoriesHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharTitlesHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharTitlesHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharTitlesHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChatChannelsHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChatChannelsHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChatChannelsHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChatChannelsHotfixesLocale343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChatChannelsHotfixesLocale343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChatChannelsHotfixesLocale343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixesLocale343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixesLocale343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixesLocale343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixesLocale341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixesLocale341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixesLocale341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixesLocale342.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixesLocale342, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixesLocale342, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationOptionHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationOptionHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationOptionHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixesLocale341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixesLocale341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixesLocale341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixesLocale343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixesLocale343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixesLocale343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRacesHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRacesHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRacesHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrSpecializationHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrSpecializationHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrSpecializationHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureFamilyHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureFamilyHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureFamilyHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureTypeHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureTypeHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureTypeHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaTreeHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaTreeHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaTreeHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaTreeHotfixesLocale343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaTreeHotfixesLocale343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaTreeHotfixesLocale343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyContainerHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyContainerHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyContainerHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixesLocale343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixesLocale343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixesLocale343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DungeonEncounterHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DungeonEncounterHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DungeonEncounterHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DungeonEncounterHotfixesLocale341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DungeonEncounterHotfixesLocale341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DungeonEncounterHotfixesLocale341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DungeonEncounterHotfixesLocale343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DungeonEncounterHotfixesLocale343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DungeonEncounterHotfixesLocale343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionHotfixesLocale341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionHotfixesLocale341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionHotfixesLocale341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipRepReactionHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipRepReactionHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipRepReactionHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipReputationHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipReputationHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipReputationHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipReputationHotfixesLocale341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipReputationHotfixesLocale341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipReputationHotfixesLocale341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectsHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectsHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectsHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrAbilityHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrAbilityHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrAbilityHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrBuildingHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrBuildingHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrBuildingHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrClassSpecHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrClassSpecHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrClassSpecHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrFollowerHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrFollowerHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrFollowerHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrMissionHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrMissionHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrMissionHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GarrTalentTreeHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GarrTalentTreeHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GarrTalentTreeHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.HeirloomHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.HeirloomHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.HeirloomHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBagFamilyHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBagFamilyHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBagFamilyHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemClassHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemClassHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemClassHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLimitCategoryHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLimitCategoryHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLimitCategoryHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemNameDescriptionHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemNameDescriptionHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemNameDescriptionHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemRandomPropertiesHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemRandomPropertiesHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemRandomPropertiesHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemRandomSuffixHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemRandomSuffixHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemRandomSuffixHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSearchNameHotfixesLocale342.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSearchNameHotfixesLocale342, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSearchNameHotfixesLocale342, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSetHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSetHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSetHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSparseHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSparseHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSparseHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSparseHotfixesLocale341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSparseHotfixesLocale341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSparseHotfixesLocale341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterSectionHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterSectionHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterSectionHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalInstanceHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalInstanceHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalInstanceHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalInstanceHotfixesLocale341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalInstanceHotfixesLocale341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalInstanceHotfixesLocale341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalTierHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalTierHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalTierHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.KeystoneAffixHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.KeystoneAffixHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.KeystoneAffixHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguagesHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguagesHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguagesHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguagesHotfixesLocale342.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguagesHotfixesLocale342, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguagesHotfixesLocale342, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LfgDungeonsHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LfgDungeonsHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LfgDungeonsHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LfgDungeonsHotfixesLocale341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LfgDungeonsHotfixesLocale341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LfgDungeonsHotfixesLocale341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MailTemplateHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MailTemplateHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MailTemplateHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapChallengeModeHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapChallengeModeHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapChallengeModeHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyXConditionHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyXConditionHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyXConditionHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PlayerConditionHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PrestigeLevelInfoHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PrestigeLevelInfoHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PrestigeLevelInfoHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTalentHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTalentHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTalentHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTierHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTierHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTierHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestInfoHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestInfoHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestInfoHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestSortHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestSortHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestSortHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioStepHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioStepHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioStepHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ServerMessagesHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ServerMessagesHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ServerMessagesHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineAbilityHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineAbilityHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineAbilityHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpecializationSpellsHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpecializationSpellsHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpecializationSpellsHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoryHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoryHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoryHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoryHotfixesLocale343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoryHotfixesLocale343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoryHotfixesLocale343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellFocusObjectHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellFocusObjectHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellFocusObjectHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellNameHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellNameHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellNameHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellRangeHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellRangeHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellRangeHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftFormHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftFormHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftFormHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentTabHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentTabHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentTabHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiNodesHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiNodesHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiNodesHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiNodesHotfixesLocale343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiNodesHotfixesLocale343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiNodesHotfixesLocale343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TotemCategoryHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TotemCategoryHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TotemCategoryHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ToyHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ToyHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ToyHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitCurrencySourceHotfixesLocale341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitCurrencySourceHotfixesLocale341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitCurrencySourceHotfixesLocale341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TraitDefinitionHotfixesLocale341.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TraitDefinitionHotfixesLocale341, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TraitDefinitionHotfixesLocale341, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetGroupHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetGroupHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetGroupHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixesLocale343.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixesLocale343, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixesLocale343, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitPowerBarHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitPowerBarHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitPowerBarHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WmoAreaTableHotfixesLocale340.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WmoAreaTableHotfixesLocale340, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WmoAreaTableHotfixesLocale340, hotfixes, StoreNameType.None);
                    }

                    // Cata Classic Hotfix Builder
                    if (!Storage.AchievementHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AchievementCategoryHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AchievementCategoryHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AchievementCategoryHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTableHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTableHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTableHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTriggerHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTriggerHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTriggerHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AreaTriggerHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AreaTriggerHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AreaTriggerHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.AuctionHouseHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.AuctionHouseHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.AuctionHouseHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BarberShopStyleHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BarberShopStyleHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BarberShopStyleHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetAbilityHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetAbilityHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetAbilityHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlePetSpeciesHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlePetSpeciesHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlePetSpeciesHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlemasterListHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlemasterListHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlemasterListHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BattlemasterListHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BattlemasterListHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BattlemasterListHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.BroadcastTextHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.BroadcastTextHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.BroadcastTextHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CfgCategoriesHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CfgCategoriesHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CfgCategoriesHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CharTitlesHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CharTitlesHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CharTitlesHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChatChannelsHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChatChannelsHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChatChannelsHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrClassesHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrClassesHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrClassesHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationChoiceHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationChoiceHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationChoiceHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationOptionHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationOptionHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationOptionHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrCustomizationReqHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrCustomizationReqHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrCustomizationReqHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ChrRacesHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ChrRacesHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ChrRacesHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureFamilyHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureFamilyHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureFamilyHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CreatureTypeHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CreatureTypeHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CreatureTypeHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CriteriaTreeHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CriteriaTreeHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CriteriaTreeHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyContainerHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyContainerHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyContainerHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.CurrencyTypesHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.CurrencyTypesHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.CurrencyTypesHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DifficultyHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DifficultyHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DifficultyHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.DungeonEncounterHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.DungeonEncounterHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.DungeonEncounterHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FactionHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FactionHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FactionHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipRepReactionHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipRepReactionHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipRepReactionHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.FriendshipReputationHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.FriendshipReputationHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.FriendshipReputationHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectsHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectsHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectsHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.GameobjectsHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.GameobjectsHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.GameobjectsHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.HeirloomHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.HeirloomHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.HeirloomHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemBagFamilyHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemBagFamilyHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemBagFamilyHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemClassHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemClassHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemClassHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemLimitCategoryHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemLimitCategoryHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemLimitCategoryHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemNameDescriptionHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemNameDescriptionHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemNameDescriptionHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSearchNameHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSearchNameHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSearchNameHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSearchNameHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSearchNameHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSearchNameHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSetHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSetHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSetHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSparseHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSparseHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSparseHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ItemSparseHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ItemSparseHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ItemSparseHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalEncounterSectionHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalEncounterSectionHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalEncounterSectionHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalInstanceHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalInstanceHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalInstanceHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.JournalTierHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.JournalTierHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.JournalTierHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.KeystoneAffixHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.KeystoneAffixHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.KeystoneAffixHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LanguagesHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LanguagesHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LanguagesHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.LfgDungeonsHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.LfgDungeonsHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.LfgDungeonsHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MailTemplateHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MailTemplateHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MailTemplateHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapChallengeModeHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapChallengeModeHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapChallengeModeHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MapDifficultyXConditionHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MapDifficultyXConditionHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MapDifficultyXConditionHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.MountHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.MountHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.MountHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PlayerConditionHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PlayerConditionHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PlayerConditionHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PlayerConditionHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PrestigeLevelInfoHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PrestigeLevelInfoHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PrestigeLevelInfoHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.PvpTierHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.PvpTierHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.PvpTierHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestInfoHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestInfoHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestInfoHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.QuestSortHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.QuestSortHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.QuestSortHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ScenarioStepHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ScenarioStepHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ScenarioStepHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ServerMessagesHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ServerMessagesHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ServerMessagesHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineAbilityHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineAbilityHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineAbilityHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SkillLineAbilityHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SkillLineAbilityHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SkillLineAbilityHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellCategoryHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellCategoryHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellCategoryHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellFocusObjectHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellFocusObjectHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellFocusObjectHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellItemEnchantmentHotfixesLocale441.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellItemEnchantmentHotfixesLocale441, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellItemEnchantmentHotfixesLocale441, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellNameHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellNameHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellNameHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellRangeHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellRangeHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellRangeHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.SpellShapeshiftFormHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.SpellShapeshiftFormHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.SpellShapeshiftFormHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TalentTabHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TalentTabHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TalentTabHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TaxiNodesHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TaxiNodesHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TaxiNodesHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TotemCategoryHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TotemCategoryHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TotemCategoryHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.ToyHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.ToyHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.ToyHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.TransmogSetGroupHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.TransmogSetGroupHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.TransmogSetGroupHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UiMapHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UiMapHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UiMapHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.UnitPowerBarHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.UnitPowerBarHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.UnitPowerBarHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.VignetteHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.VignetteHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.VignetteHotfixesLocale440, hotfixes, StoreNameType.None);
                    }

                    if (!Storage.WmoAreaTableHotfixesLocale440.IsEmpty())
                    {
                        var hotfixes = SQLDatabase.Get(Storage.WmoAreaTableHotfixesLocale440, Settings.HotfixesDatabase);

                        sql += SQLUtil.Compare(Storage.WmoAreaTableHotfixesLocale440, hotfixes, StoreNameType.None);
                    }
                }

                // TODO: add way to identify clean DBCache and packet is ALL hotfixes not just few
                /*if (ClientLocale.PacketLocale == LocaleConstant.enUS && !Settings.SkipOnlyVerifiedBuildUpdateRows)
                {
                    sql += $"DELETE FROM `achievement` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `achievement_category` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `animation_data` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `anim_kit` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `area_group_member` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `area_table` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `area_trigger` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `area_trigger_action_set` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `armor_location` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `auction_house` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `bank_bag_slot_prices` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `banned_addons` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `barber_shop_style` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battle_pet_ability` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battle_pet_breed_quality` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battle_pet_breed_state` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battle_pet_species` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battle_pet_species_state` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battlemaster_list` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battlemaster_list_x_map` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `cfg_categories` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
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
                    sql += $"DELETE FROM `cinematic_camera` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `cinematic_sequences` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `conditional_chr_model` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `conditional_content_tuning` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `content_tuning` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `conversation_line` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
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
                    sql += $"DELETE FROM `gem_properties` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `glyph_bindable_spell` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `glyph_slot` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `glyph_properties` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `glyph_required_spec` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `gossip_npc_option` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
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
                    sql += $"DELETE FROM `item_context_picker_entry` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
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
                    sql += $"DELETE FROM `item_reforge` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_search_name` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_set` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_set_spell` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_sparse` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `item_x_bonus_tree` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `journal_encounter` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `journal_encounter_section` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `journal_instance` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `journal_tier` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `keychain` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `keystone_affix` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `language_words` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `languages` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `lfg_dungeons` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `light` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `liquid_type` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `location` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `lock` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mail_template` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `map` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `map_challenge_mode` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `map_difficulty` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `map_difficulty_x_condition` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `modifier_tree` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mount` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mount_capability` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mount_type_x_capability` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mount_x_display` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `movie` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `mythic_plus_season` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `name_gen` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `names_profanity` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `names_reserved` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `names_reserved_locale` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `num_talents_at_level` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `override_spell_data` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `paragon_reputation` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `path` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `path_node` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `path_property` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `phase` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `phase_x_phase_group` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `power_display` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `power_type` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `prestige_level_info` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `pvp_difficulty` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `pvp_item` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `pvp_season` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
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
                    sql += $"DELETE FROM `server_messages` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `skill_line` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `skill_line_ability` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `skill_race_class_info` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `sound_kit` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
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
                    sql += $"DELETE FROM `spell_keybound_override` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
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
                    sql += $"DELETE FROM `talent_tab` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `talent_tree_primary_spells` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `taxi_nodes` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `taxi_path` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `taxi_path_node` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `totem_category` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `toy` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_holiday` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_set` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_set_group` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_set_item` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transport_animation` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transport_rotation` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `ui_map` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `ui_map_assignment` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `ui_map_link` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `ui_map_x_map_art` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `unit_condition` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `unit_power_bar` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `vehicle` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `vehicle_seat` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `vignette` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `wmo_area_table` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `world_effect` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `world_map_overlay` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `world_state_expression` WHERE (`VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                }
                else if (!Settings.SkipOnlyVerifiedBuildUpdateRows)
                {
                    sql += $"DELETE FROM `achievement_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `achievement_category_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `area_table_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `area_trigger_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `auction_house_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `barber_shop_style_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battle_pet_ability_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battle_pet_species_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `battlemaster_list_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `cfg_categories_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `char_titles_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chat_channels_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_classes_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_customization_choice_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_customization_option_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_customization_req_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `chr_races_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
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
                    sql += $"DELETE FROM `pvp_tier_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `quest_info_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `quest_sort_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `scenario_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `scenario_step_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `server_messages_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `skill_line_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `skill_line_ability_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_category_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_focus_object_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_item_enchantment_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_name_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_range_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `spell_shapeshift_form_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `talent_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `talent_tab_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `taxi_nodes_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `totem_category_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `toy_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_set_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `transmog_set_group_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `ui_map_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `unit_power_bar_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
                    sql += $"DELETE FROM `vignette_locale` WHERE (`locale`= '{ClientLocale.PacketLocaleString}' AND `VerifiedBuild`>0 AND `VerifiedBuild`<{ClientVersion.BuildInt});" + Environment.NewLine;
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
