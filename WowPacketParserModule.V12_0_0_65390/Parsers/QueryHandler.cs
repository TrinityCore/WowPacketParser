using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using ConditionalTextType = WowPacketParserModule.V10_0_0_46181.Parsers.QuestHandler.ConditionalTextType;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class QueryHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            packet.ReadInt32("Entry");

            var hasData = packet.ReadBit("Has Data");
            if (!hasData)
                return; // nothing to do

            var id = packet.ReadEntry("Quest ID");

            var quest = new QuestTemplate
            {
                ID = (uint)id.Key
            };

            quest.QuestType = packet.ReadInt32E<QuestType>("QuestType");
            quest.QuestPackageID = (uint)packet.ReadInt32("QuestPackageID");
            quest.ContentTuningID = packet.ReadInt32("ContentTuningID");
            quest.QuestSortID = (QuestSort)packet.ReadInt32("QuestSortID");
            quest.QuestInfoID = packet.ReadInt32E<QuestInfo>("QuestInfoID");
            quest.SuggestedGroupNum = (uint)packet.ReadInt32("SuggestedGroupNum");
            quest.RewardNextQuest = (uint)packet.ReadInt32("RewardNextQuest");
            quest.RewardXPDifficulty = (uint)packet.ReadInt32("RewardXPDifficulty");

            quest.RewardXPMultiplier = packet.ReadSingle("RewardXPMultiplier");

            quest.RewardMoney = packet.ReadInt32("RewardMoney");
            quest.RewardMoneyDifficulty = (uint)packet.ReadInt32("RewardMoneyDifficulty");

            quest.RewardMoneyMultiplier = packet.ReadSingle("RewardMoneyMultiplier");

            quest.RewardBonusMoney = (uint)packet.ReadInt32("RewardBonusMoney");

            var rewardDisplaySpellCount = packet.ReadUInt32("RewardDisplaySpellCount");

            quest.RewardSpellWod = (uint)packet.ReadInt32("RewardSpell");
            quest.RewardHonorWod = (uint)packet.ReadInt32("RewardHonor");

            quest.RewardKillHonor = packet.ReadSingle("RewardKillHonor");

            quest.RewardFavor = packet.ReadInt32("RewardFavor");

            quest.RewardArtifactXPDifficulty = (uint)packet.ReadInt32("RewardArtifactXPDifficulty");
            quest.RewardArtifactXPMultiplier = packet.ReadSingle("RewardArtifactXPMultiplier");
            quest.RewardArtifactCategoryID = (uint)packet.ReadInt32("RewardArtifactCategoryID");

            quest.StartItem = (uint)packet.ReadInt32("StartItem");
            quest.Flags = packet.ReadUInt32E<QuestFlags>("Flags");
            quest.FlagsEx = packet.ReadUInt32E<QuestFlagsEx>("FlagsEx");
            quest.FlagsEx2 = packet.ReadUInt32E<QuestFlagsEx2>("FlagsEx2");
            quest.FlagsEx3 = packet.ReadUInt32E<QuestFlagsEx3>("FlagsEx3");

            quest.RewardItem = new uint?[4];
            quest.RewardAmount = new uint?[4];
            quest.ItemDrop = new uint?[4];
            quest.ItemDropQuantity = new uint?[4];
            for (var i = 0; i < 4; ++i)
            {
                quest.RewardItem[i] = (uint)packet.ReadInt32("RewardItems", i);
                quest.RewardAmount[i] = (uint)packet.ReadInt32("RewardAmount", i);
                quest.ItemDrop[i] = (uint)packet.ReadInt32("ItemDrop", i);
                quest.ItemDropQuantity[i] = (uint)packet.ReadInt32("ItemDropQuantity", i);
            }

            quest.RewardChoiceItemID = new uint?[6];
            quest.RewardChoiceItemQuantity = new uint?[6];
            quest.RewardChoiceItemDisplayID = new uint?[6];
            for (var i = 0; i < 6; ++i)
            {
                quest.RewardChoiceItemID[i] = (uint)packet.ReadInt32("RewardChoiceItemID", i);
                quest.RewardChoiceItemQuantity[i] = (uint)packet.ReadInt32("RewardChoiceItemQuantity", i);
                quest.RewardChoiceItemDisplayID[i] = (uint)packet.ReadInt32("RewardChoiceItemDisplayID", i);
            }

            quest.POIContinent = (uint)packet.ReadInt32("POIContinent");
            quest.POIx = packet.ReadSingle("POIx");
            quest.POIy = packet.ReadSingle("POIy");
            quest.POIPriorityWod = packet.ReadInt32("POIPriority");

            quest.RewardTitle = (uint)packet.ReadInt32("RewardTitle");
            quest.RewardArenaPoints = (uint)packet.ReadInt32("RewardArenaPoints");
            quest.RewardSkillLineID = (uint)packet.ReadInt32("RewardSkillLineID");
            quest.RewardNumSkillUps = (uint)packet.ReadInt32("RewardNumSkillUps");

            quest.QuestGiverPortrait = (uint)packet.ReadInt32("PortraitGiver");
            quest.PortraitGiverMount = (uint)packet.ReadInt32("PortraitGiverMount");
            quest.PortraitGiverModelSceneID = packet.ReadInt32("PortraitGiverModelSceneID");
            quest.QuestTurnInPortrait = (uint)packet.ReadInt32("PortraitTurnIn");

            quest.RewardFactionID = new uint?[5];
            quest.RewardFactionOverride = new int?[5];
            quest.RewardFactionValue = new int?[5];
            quest.RewardFactionCapIn = new int?[5];
            for (var i = 0; i < 5; ++i)
            {
                quest.RewardFactionID[i] = (uint)packet.ReadInt32("RewardFactionID", i);
                quest.RewardFactionValue[i] = packet.ReadInt32("RewardFactionValue", i);
                quest.RewardFactionOverride[i] = packet.ReadInt32("RewardFactionOverride", i);
                quest.RewardFactionCapIn[i] = packet.ReadInt32("RewardFactionCapIn", i);
            }

            quest.RewardFactionFlags = (uint)packet.ReadInt32("RewardFactionFlags");

            quest.RewardCurrencyID = new uint?[4];
            quest.RewardCurrencyCount = new uint?[4];
            for (var i = 0; i < 4; ++i)
            {
                quest.RewardCurrencyID[i] = (uint)packet.ReadInt32("RewardCurrencyID", i);
                quest.RewardCurrencyCount[i] = (uint)packet.ReadInt32("RewardCurrencyQty", i);
            }

            quest.SoundAccept = (uint)packet.ReadInt32("AcceptedSoundKitID");
            quest.SoundTurnIn = (uint)packet.ReadInt32("CompleteSoundKitID");

            quest.AreaGroupID = (uint)packet.ReadInt32("AreaGroupID");
            quest.TimeAllowed = packet.ReadInt64("TimeAllowed");

            var objectiveCount = packet.ReadUInt32("ObjectiveCount");
            quest.AllowableRacesWod = packet.ReadUInt32("RaceMask", 0);
            quest.AllowableRacesWod |= (ulong)packet.ReadUInt32("RaceMask", 1) << 32;
            var treasurePickerCount = packet.ReadUInt32();
            var nonDisplayableTreasurePickerCount = packet.ReadUInt32();
            quest.Expansion = packet.ReadInt32("Expansion");
            quest.ManagedWorldStateID = packet.ReadInt32("ManagedWorldStateID");
            quest.QuestSessionBonus = packet.ReadInt32("QuestSessionBonus");

            packet.ReadInt32("QuestGiverCreatureID");
            var conditionalQuestDescriptionCount = packet.ReadUInt32();
            var conditionalQuestCompletionLogCount = packet.ReadUInt32();

            var rewardHouseRoomCount = packet.ReadUInt32();
            var rewardHouseDecorCount = packet.ReadUInt32();

            for (uint i = 0; i < rewardDisplaySpellCount; ++i)
                V10_0_0_46181.Parsers.QueryHandler.ReadQuestCompleteDisplaySpell(packet, (uint)id.Key, i, i, "RewardDisplaySpell");

            for (uint i = 0; i < objectiveCount; ++i)
            {
                var objectiveId = packet.ReadEntry("Id", i);

                var questInfoObjective = new QuestObjective
                {
                    ID = (uint)objectiveId.Key,
                    QuestID = (uint)id.Key
                };

                questInfoObjective.Type = packet.ReadUInt32E<QuestRequirementType>("QuestRequirementType", i);
                questInfoObjective.StorageIndex = packet.ReadSByte("StorageIndex", i);
                questInfoObjective.Order = i;
                questInfoObjective.ObjectID = packet.ReadInt32("ObjectID", i);
                questInfoObjective.Amount = packet.ReadInt32("Amount", i);
                questInfoObjective.ConditionalAmount = packet.ReadInt32("ConditionalAmount", i);

                questInfoObjective.Flags = packet.ReadUInt32("Flags", i);
                questInfoObjective.Flags2 = packet.ReadUInt32("Flags2", i);
                questInfoObjective.ProgressBarWeight = packet.ReadSingle("ProgressBarWeight", i);

                var visualEffectsCount = packet.ReadInt32("VisualEffects", i);
                questInfoObjective.ParentObjectiveID = packet.ReadInt32("ParentObjectiveID", i);

                for (var j = 0; j < visualEffectsCount; ++j)
                {
                    QuestVisualEffect questVisualEffect = new QuestVisualEffect
                    {
                        ID = questInfoObjective.ID,
                        Index = (uint)j,
                        VisualEffect = packet.ReadInt32("VisualEffectId", i, j)
                    };

                    Storage.QuestVisualEffects.Add(questVisualEffect, packet.TimeSpan);
                }

                packet.ResetBitReader();

                var descriptionLength = packet.ReadBits(8);
                questInfoObjective.Visible = packet.ReadBit("Visible", i);

                questInfoObjective.Description = packet.ReadWoWString("Description", descriptionLength, i);

                if (ClientLocale.PacketLocale != LocaleConstant.enUS && questInfoObjective.Description != string.Empty)
                {
                    QuestObjectivesLocale localesQuestObjectives = new QuestObjectivesLocale
                    {
                        ID = (uint)objectiveId.Key,
                        QuestId = (uint)id.Key,
                        StorageIndex = questInfoObjective.StorageIndex,
                        Description = questInfoObjective.Description
                    };

                    Storage.LocalesQuestObjectives.Add(localesQuestObjectives, packet.TimeSpan);
                }

                Storage.QuestObjectives.Add((uint)questInfoObjective.ID, questInfoObjective, packet.TimeSpan);
            }

            for (var i = 0; i < treasurePickerCount; ++i)
            {
                Storage.QuestTreasurePickersStorage.Add(new QuestTreasurePickers
                {
                    QuestID = quest.ID,
                    TreasurePickerID = packet.ReadInt32("TreasurePickerID", i),
                    OrderIndex = i
                });
            }

            for (var i = 0; i < nonDisplayableTreasurePickerCount; ++i)
            {
                var treasurePickerID = packet.ReadInt32("NonDisplayableTreasurePickerID", i);
                //QuestTreasurePickers pickers = new()
                //{
                //    QuestID = quest.ID,
                //    TreasurePickerID = treasurePickerID,
                //    OrderIndex = (int)i
                //};
                //Storage.QuestTreasurePickersStorage.Add(pickers);
            }

            for (var i = 0; i < conditionalQuestDescriptionCount; i++)
                V10_0_0_46181.Parsers.QuestHandler.ReadConditionalQuestText(packet, id.Key, i, ConditionalTextType.Description, i, "ConditionalDescriptionText");

            for (var i = 0; i < conditionalQuestCompletionLogCount; i++)
                V10_0_0_46181.Parsers.QuestHandler.ReadConditionalQuestText(packet, id.Key, i, ConditionalTextType.CompletionLog, i, "ConditionalCompletionLogText");

            for (var i = 0; i < rewardHouseRoomCount; ++i)
            {
                Storage.QuestRewardHouseRoomStorage.Add(new QuestRewardHouseRoom
                {
                    QuestID = quest.ID,
                    HouseRoomID = packet.ReadInt32("RewardHouseRoomID", i),
                    OrderIndex = i
                });
            }

            for (var i = 0; i < rewardHouseDecorCount; ++i)
            {
                Storage.QuestRewardHouseDecorStorage.Add(new QuestRewardHouseDecor
                {
                    QuestID = quest.ID,
                    HouseDecorID = packet.ReadInt32("RewardHouseDecorID", i),
                    OrderIndex = i
                });
            }

            packet.ResetBitReader();

            var logTitleLen = packet.ReadBits(9);
            var logDescriptionLen = packet.ReadBits(12);
            var questDescriptionLen = packet.ReadBits(12);
            var areaDescriptionLen = packet.ReadBits(9);
            var questGiverTextWindowLen = packet.ReadBits(10);
            var questGiverTargetNameLen = packet.ReadBits(8);
            var questTurnTextWindowLen = packet.ReadBits(10);
            var questTurnTargetNameLen = packet.ReadBits(8);
            var questCompletionLogLen = packet.ReadBits(11);
            packet.ReadBit("ReadyForTranslation");
            packet.ReadBit("ResetByScheduler");

            quest.LogTitle = packet.ReadWoWString("LogTitle", logTitleLen);
            quest.LogDescription = packet.ReadWoWString("LogDescription", logDescriptionLen);
            quest.QuestDescription = packet.ReadWoWString("QuestDescription", questDescriptionLen);
            quest.AreaDescription = packet.ReadWoWString("AreaDescription", areaDescriptionLen);
            quest.QuestGiverTextWindow = packet.ReadWoWString("PortraitGiverText", questGiverTextWindowLen);
            quest.QuestGiverTargetName = packet.ReadWoWString("PortraitGiverName", questGiverTargetNameLen);
            quest.QuestTurnTextWindow = packet.ReadWoWString("PortraitTurnInText", questTurnTextWindowLen);
            quest.QuestTurnTargetName = packet.ReadWoWString("PortraitTurnInName", questTurnTargetNameLen);
            quest.QuestCompletionLog = packet.ReadWoWString("QuestCompletionLog", questCompletionLogLen);

            ObjectName objectName = new ObjectName
            {
                ObjectType = StoreNameType.Quest,
                ID = (int?)quest.ID,
                Name = quest.LogTitle
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                LocalesQuest localesQuest = new LocalesQuest
                {
                    ID = (uint)id.Key,
                    LogTitle = quest.LogTitle,
                    LogDescription = quest.LogDescription,
                    QuestDescription = quest.QuestDescription,
                    AreaDescription = quest.AreaDescription,
                    PortraitGiverText = quest.QuestGiverTextWindow,
                    PortraitGiverName = quest.QuestGiverTargetName,
                    PortraitTurnInText = quest.QuestTurnTextWindow,
                    PortraitTurnInName = quest.QuestTurnTargetName,
                    QuestCompletionLog = quest.QuestCompletionLog
                };

                Storage.LocalesQuests.Add(localesQuest, packet.TimeSpan);
            }

            Storage.QuestTemplates.Add(quest, packet.TimeSpan);
        }
    }
}
