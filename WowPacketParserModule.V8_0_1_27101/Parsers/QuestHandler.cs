using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class QuestHandler
    {
        public static void ReadRewardItem(Packet packet, params object[] idx)
        {
            Substructures.ItemHandler.ReadItemInstance(packet, idx);

            packet.ReadInt32("Quantity", idx);
        }

        public static void ReadQuestRewards(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ChoiceItemCount", idx);
            packet.ReadInt32("ItemCount", idx);

            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("ID", idx, i);
                packet.ReadInt32("Quantity", idx, i);
            }

            packet.ReadInt32("RewardMoney", idx);
            packet.ReadInt32("XP", idx);
            packet.ReadUInt64("ArtifactXP", idx);
            packet.ReadInt32("ArtifactCategoryID", idx);
            packet.ReadInt32("Honor", idx);
            packet.ReadInt32("Title", idx);
            packet.ReadInt32("FactionFlags", idx);

            for (var i = 0; i < 5; ++i)
            {
                packet.ReadInt32("FactionID", idx, i);
                packet.ReadInt32("FactionValue", idx, i);
                packet.ReadInt32("FactionOverride", idx, i);
                packet.ReadInt32("FactionCapIn", idx, i);
            }

            for (var i = 0; i < 3; ++i)
                packet.ReadInt32("SpellCompletionDisplayID", idx, i);

            packet.ReadInt32("SpellCompletionID", idx);

            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("CurrencyID", idx, i);
                packet.ReadInt32("CurrencyQty", idx, i);
            }

            packet.ReadInt32("SkillLineID", idx);
            packet.ReadInt32("NumSkillUps", idx);
            packet.ReadInt32("TreasurePickerID", idx);

            for (var i = 0; i < 6; ++i)
                ReadRewardItem(packet, "QuestRewards", "ItemChoiceData", i);

            packet.ResetBitReader();
            packet.ReadBit("IsBoostSpell", idx);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS)]
        public static void HandleQuestGiverQuestDetails(Packet packet)
        {
            var questgiverGUID = packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadPackedGuid128("InformUnit");

            int id = packet.ReadInt32("QuestID");
            QuestDetails questDetails = new QuestDetails
            {
                ID = (uint)id
            };

            CoreParsers.QuestHandler.AddQuestStarter(questgiverGUID, (uint)id);

            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitGiver");
            packet.ReadInt32("PortraitGiverMount");
            packet.ReadInt32("PortraitTurnIn");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("SuggestedPartyMembers");
            var learnSpellsCount = packet.ReadUInt32("LearnSpellsCount");

            var descEmotesCount = packet.ReadUInt32("DescEmotesCount");
            var objectivesCount = packet.ReadUInt32("ObjectivesCount");
            packet.ReadInt32("QuestStartItemID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_5_31921))
                packet.ReadInt32("QuestSessionBonus");

            for (var i = 0; i < learnSpellsCount; i++)
                packet.ReadInt32("LearnSpells", i);

            questDetails.Emote = new uint?[] { 0, 0, 0, 0 };
            questDetails.EmoteDelay = new uint?[] { 0, 0, 0, 0 };
            for (var i = 0; i < descEmotesCount; i++)
            {
                questDetails.Emote[i] = (uint)packet.ReadInt32("Type", i);
                questDetails.EmoteDelay[i] = packet.ReadUInt32("Delay", i);
            }

            for (var i = 0; i < objectivesCount; i++)
            {
                packet.ReadInt32("ObjectiveID", i);
                packet.ReadInt32("ObjectID", i);
                packet.ReadInt32("Amount", i);
                packet.ReadByte("Type", i);
            }

            packet.ResetBitReader();

            uint questTitleLen = packet.ReadBits(9);
            uint descriptionTextLen = packet.ReadBits(12);
            uint logDescriptionLen = packet.ReadBits(12);
            uint portraitGiverTextLen = packet.ReadBits(10);
            uint portraitGiverNameLen = packet.ReadBits(8);
            uint portraitTurnInTextLen = packet.ReadBits(10);
            uint portraitTurnInNameLen = packet.ReadBits(8);

            packet.ReadBit("DisplayPopup");
            packet.ReadBit("StartCheat");
            packet.ReadBit("AutoLaunched");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_5_31921))
                packet.ReadBit(); // Unused by client

            ReadQuestRewards(packet, "QuestRewards");

            packet.ReadWoWString("QuestTitle", questTitleLen);
            packet.ReadWoWString("DescriptionText", descriptionTextLen);
            packet.ReadWoWString("LogDescription", logDescriptionLen);
            packet.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            Storage.QuestDetails.Add(questDetails, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            packet.ReadInt32("Entry");

            Bit hasData = packet.ReadBit("Has Data");
            if (!hasData)
                return; // nothing to do

            var id = packet.ReadEntry("Quest ID");

            QuestTemplate quest = new QuestTemplate
            {
                ID = (uint)id.Key
            };

            quest.QuestType = packet.ReadInt32E<QuestType>("QuestType");
            quest.QuestLevel = packet.ReadInt32("QuestLevel");
            quest.QuestScalingFactionGroup = packet.ReadInt32("QuestScalingFactionGroup");
            quest.QuestMaxScalingLevel = packet.ReadInt32("QuestMaxScalingLevel");
            quest.QuestPackageID = (uint)packet.ReadInt32("QuestPackageID");
            quest.MinLevel = packet.ReadInt32("QuestMinLevel");
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

            quest.RewardDisplaySpellLegion = new uint?[3];
            for (int i = 0; i < 3; ++i)
                quest.RewardDisplaySpellLegion[i] = (uint)packet.ReadInt32("RewardDisplaySpell", i);

            quest.RewardSpellWod = (uint)packet.ReadInt32("RewardSpell");
            quest.RewardHonorWod = (uint)packet.ReadInt32("RewardHonor");

            quest.RewardKillHonor = packet.ReadSingle("RewardKillHonor");

            quest.RewardArtifactXPDifficulty = (uint)packet.ReadInt32("RewardArtifactXPDifficulty");
            quest.RewardArtifactXPMultiplier = packet.ReadSingle("RewardArtifactXPMultiplier");
            quest.RewardArtifactCategoryID = (uint)packet.ReadInt32("RewardArtifactCategoryID");

            quest.StartItem = (uint)packet.ReadInt32("StartItem");
            quest.Flags = packet.ReadInt32E<QuestFlags>("Flags");
            quest.FlagsEx = packet.ReadInt32E<QuestFlagsEx>("FlagsEx");
            quest.FlagsEx2 = packet.ReadInt32E<QuestFlagsEx2>("FlagsEx2");

            quest.RewardItem = new uint?[4];
            quest.RewardAmount = new uint?[4];
            quest.ItemDrop = new uint?[4];
            quest.ItemDropQuantity = new uint?[4];
            for (int i = 0; i < 4; ++i)
            {
                quest.RewardItem[i] = (uint)packet.ReadInt32("RewardItems", i);
                quest.RewardAmount[i] = (uint)packet.ReadInt32("RewardAmount", i);
                quest.ItemDrop[i] = (uint)packet.ReadInt32("ItemDrop", i);
                quest.ItemDropQuantity[i] = (uint)packet.ReadInt32("ItemDropQuantity", i);
            }

            quest.RewardChoiceItemID = new uint?[6];
            quest.RewardChoiceItemQuantity = new uint?[6];
            quest.RewardChoiceItemDisplayID = new uint?[6];
            for (int i = 0; i < 6; ++i) // CliQuestInfoChoiceItem
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
            quest.QuestTurnInPortrait = (uint)packet.ReadInt32("PortraitTurnIn");

            quest.RewardFactionID = new uint?[5];
            quest.RewardFactionOverride = new int?[5];
            quest.RewardFactionValue = new int?[5];
            quest.RewardFactionCapIn = new int?[5];
            for (int i = 0; i < 5; ++i)
            {
                quest.RewardFactionID[i] = (uint)packet.ReadInt32("RewardFactionID", i);
                quest.RewardFactionValue[i] = packet.ReadInt32("RewardFactionValue", i);
                quest.RewardFactionOverride[i] = packet.ReadInt32("RewardFactionOverride", i);
                quest.RewardFactionCapIn[i] = packet.ReadInt32("RewardFactionCapIn", i);
            }

            quest.RewardFactionFlags = (uint)packet.ReadInt32("RewardFactionFlags");

            quest.RewardCurrencyID = new uint?[4];
            quest.RewardCurrencyCount = new uint?[4];
            for (int i = 0; i < 4; ++i)
            {
                quest.RewardCurrencyID[i] = (uint)packet.ReadInt32("RewardCurrencyID");
                quest.RewardCurrencyCount[i] = (uint)packet.ReadInt32("RewardCurrencyQty");
            }

            quest.SoundAccept = (uint)packet.ReadInt32("AcceptedSoundKitID");
            quest.SoundTurnIn = (uint)packet.ReadInt32("CompleteSoundKitID");
            quest.AreaGroupID = (uint)packet.ReadInt32("AreaGroupID");
            quest.TimeAllowed = (uint)packet.ReadInt32("TimeAllowed");
            uint objectiveCount = packet.ReadUInt32("ObjectiveCount");
            quest.AllowableRacesWod = packet.ReadUInt64("AllowableRaces");
            quest.QuestRewardID = packet.ReadInt32("TreasurePickerID");
            quest.Expansion = packet.ReadInt32("Expansion");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_5_29683))
                quest.ManagedWorldStateID = packet.ReadInt32("ManagedWorldStateID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_5_31921))
                quest.QuestSessionBonus = packet.ReadInt32("QuestSessionBonus");

            packet.ResetBitReader();

            uint logTitleLen = 0;
            uint logDescriptionLen = 0;
            uint questDescriptionLen = 0;
            uint areaDescriptionLen = 0;
            uint questGiverTextWindowLen = 0;
            uint questGiverTargetNameLen = 0;
            uint questTurnTextWindowLen = 0;
            uint questTurnTargetNameLen = 0;
            uint questCompletionLogLen = 0;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724) && ClientVersion.RemovedInVersion(ClientVersionBuild.V8_1_5_29683))
            {
                logTitleLen = packet.ReadBits(10);
                logDescriptionLen = packet.ReadBits(12);
                questDescriptionLen = packet.ReadBits(12);
                areaDescriptionLen = packet.ReadBits(9);
                questGiverTextWindowLen = packet.ReadBits(11);
                questGiverTargetNameLen = packet.ReadBits(9);
                questTurnTextWindowLen = packet.ReadBits(11);
                questTurnTargetNameLen = packet.ReadBits(9);
                questCompletionLogLen = packet.ReadBits(12);
            }
            else
            {
                logTitleLen = packet.ReadBits(9);
                logDescriptionLen = packet.ReadBits(12);
                questDescriptionLen = packet.ReadBits(12);
                areaDescriptionLen = packet.ReadBits(9);
                questGiverTextWindowLen = packet.ReadBits(10);
                questGiverTargetNameLen = packet.ReadBits(8);
                questTurnTextWindowLen = packet.ReadBits(10);
                questTurnTargetNameLen = packet.ReadBits(8);
                questCompletionLogLen = packet.ReadBits(11);
            }

            for (uint i = 0; i < objectiveCount; ++i)
            {
                var objectiveId = packet.ReadEntry("Id", i);

                QuestObjective questInfoObjective = new QuestObjective
                {
                    ID = (uint)objectiveId.Key,
                    QuestID = (uint)id.Key
                };
                questInfoObjective.Type = packet.ReadByteE<QuestRequirementType>("Quest Requirement Type", i);
                questInfoObjective.StorageIndex = packet.ReadSByte("StorageIndex", i);
                questInfoObjective.Order = i;
                questInfoObjective.ObjectID = packet.ReadInt32("ObjectID", i);
                questInfoObjective.Amount = packet.ReadInt32("Amount", i);
                questInfoObjective.Flags = (uint)packet.ReadInt32("Flags", i);
                questInfoObjective.Flags2 = packet.ReadUInt32("Flags2", i);
                questInfoObjective.ProgressBarWeight = packet.ReadSingle("ProgressBarWeight", i);

                var visualEffectsCount = packet.ReadUInt32("VisualEffects", i);
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

                uint descriptionLength = packet.ReadBits(8);
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

                Storage.QuestObjectives.Add(questInfoObjective, packet.TimeSpan);
            }

            quest.LogTitle = packet.ReadWoWString("LogTitle", logTitleLen);
            quest.LogDescription = packet.ReadWoWString("LogDescription", logDescriptionLen);
            quest.QuestDescription = packet.ReadWoWString("QuestDescription", questDescriptionLen);
            quest.AreaDescription = packet.ReadWoWString("AreaDescription", areaDescriptionLen);
            quest.QuestGiverTextWindow = packet.ReadWoWString("PortraitGiverText", questGiverTextWindowLen);
            quest.QuestGiverTargetName = packet.ReadWoWString("PortraitGiverName", questGiverTargetNameLen);
            quest.QuestTurnTextWindow = packet.ReadWoWString("PortraitTurnInText", questTurnTextWindowLen);
            quest.QuestTurnTargetName = packet.ReadWoWString("PortraitTurnInName", questTurnTargetNameLen);
            quest.QuestCompletionLog = packet.ReadWoWString("QuestCompletionLog", questCompletionLogLen);

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

        public static void ReadTreasurePickItem(Packet packet, params object[] indexes)
        {
            Substructures.ItemHandler.ReadItemInstance(packet, indexes);
            packet.ReadUInt32("Quantity", indexes);
        }

        public static void ReadTreasurePickCurrency(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("CurrencyID", indexes);
            packet.ReadUInt32("Amount", indexes);
        }

        [Parser(Opcode.SMSG_TREASURE_PICKER_RESPONSE)]
        public static void HandleTreasurePickerResponse(Packet packet)
        {
            packet.ReadUInt32("QuestId");
            packet.ReadUInt32("TreasurePickerID");

            var itemCount = packet.ReadUInt32("ItemCount");
            var currencyCount = packet.ReadUInt32("CurrencyCount");

            packet.ReadUInt64("MoneyReward");
            var bonusCount = packet.ReadUInt32("BonusCount");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_0_30898))
                packet.ReadInt32("Flags");

            for (int i = 0; i < currencyCount; i++)
                ReadTreasurePickCurrency(packet, i);

            for (var i = 0; i < itemCount; ++i)
                ReadTreasurePickItem(packet, i);

            for (var i = 0; i < bonusCount; ++i)
            {
                var bonusItemCount = packet.ReadUInt32("BonusItemCount", i);
                var bonusCurrencyCount = packet.ReadUInt32("BonusCurrencyCount", i);
                packet.ReadUInt64("BonusMoney", i);

                for (var z = 0; z < bonusCurrencyCount; ++z)
                    ReadTreasurePickCurrency(packet, i, z);

                packet.ReadBit("UnkBit", i);

                for (int z = 0; z < bonusItemCount; ++z)
                    ReadTreasurePickItem(packet, i, z);
            }
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPOIQuery(Packet packet)
        {
            packet.ReadUInt32("MissingQuestCount");

            int max = 100;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_7_48676))
                max = 175;
            else if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_5_31921))
                max = 125;

            for (var i = 0; i < max; i++)
                packet.ReadInt32<QuestId>("MissingQuestPOIs", i);
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            packet.ReadUInt32("NumPOIs");
            var questPOIData = packet.ReadUInt32("QuestPOIData");

            for (var i = 0; i < questPOIData; ++i)
            {
                int questId = packet.ReadInt32("QuestID", i);

                var questPOIBlobData = packet.ReadUInt32("QuestPOIBlobData", i);

                for (var j = 0; j < questPOIBlobData; ++j)
                {
                    QuestPOI questPoi = new QuestPOI
                    {
                        QuestID = questId,
                        ID = j,
                        BlobIndex = packet.ReadInt32("BlobIndex", i, j),
                        ObjectiveIndex = packet.ReadInt32("ObjectiveIndex", i, j),
                        QuestObjectiveID = packet.ReadInt32("QuestObjectiveID", i, j),
                        QuestObjectID = packet.ReadInt32("QuestObjectID", i, j),
                        MapID = packet.ReadInt32("MapID", i, j),
                        UiMapID = packet.ReadInt32("UiMapID", i, j),
                        Priority = packet.ReadInt32("Priority", i, j),
                        Flags = packet.ReadInt32("Flags", i, j),
                        WorldEffectID = packet.ReadInt32("WorldEffectID", i, j),
                        PlayerConditionID = packet.ReadInt32("PlayerConditionID", i, j),
                        SpawnTrackingID = packet.ReadInt32("SpawnTrackingID", i, j),
                    };

                    var questPOIBlobPoint = packet.ReadUInt32("QuestPOIBlobPoint", i, j);
                    for (var k = 0; k < questPOIBlobPoint; ++k)
                    {
                        QuestPOIPoint questPoiPoint = new QuestPOIPoint
                        {
                            QuestID = questId,
                            Idx1 = j,
                            Idx2 = k,
                            X = packet.ReadInt32("X", i, j, k),
                            Y = packet.ReadInt32("Y", i, j, k)
                        };
                        Storage.QuestPOIPoints.Add(questPoiPoint, packet.TimeSpan);
                    }

                    packet.ResetBitReader();
                    questPoi.AlwaysAllowMergingBlobs = packet.ReadBit("AlwaysAllowMergingBlobs", i, j);

                    Storage.QuestPOIs.Add(questPoi, packet.TimeSpan);
                }
            }
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE)]
        public static void QuestGiverOfferReward(Packet packet)
        {
            var questgiverGUID = packet.ReadPackedGuid128("QuestGiverGUID");

            packet.ReadInt32("QuestGiverCreatureID");
            int id = packet.ReadInt32("QuestID");

            QuestOfferReward questOfferReward = new QuestOfferReward
            {
                ID = (uint)id
            };

            CoreParsers.QuestHandler.AddQuestEnder(questgiverGUID, (uint)id);

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("SuggestedPartyMembers");

            var emotesCount = packet.ReadUInt32("EmotesCount");

            // QuestDescEmote
            questOfferReward.Emote = new int?[] { 0, 0, 0, 0 };
            questOfferReward.EmoteDelay = new uint?[] { 0, 0, 0, 0 };
            for (var i = 0; i < emotesCount; i++)
            {
                questOfferReward.Emote[i] = packet.ReadInt32("Type");
                questOfferReward.EmoteDelay[i] = packet.ReadUInt32("Delay");
            }

            packet.ResetBitReader();

            packet.ReadBit("AutoLaunched");

            ReadQuestRewards(packet, "QuestRewards");

            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitGiver");
            packet.ReadInt32("PortraitGiverMount");
            packet.ReadInt32("PortraitTurnIn");

            packet.ResetBitReader();
            uint questTitleLen = 0;
            uint rewardTextLen = 0;
            uint portraitGiverTextLen = 0;
            uint portraitGiverNameLen = 0;
            uint portraitTurnInTextLen = 0;
            uint portraitTurnInNameLen = 0;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724) && ClientVersion.RemovedInVersion(ClientVersionBuild.V8_1_5_29683))
            {
                questTitleLen = packet.ReadBits(10);
                rewardTextLen = packet.ReadBits(12);
                portraitGiverTextLen = packet.ReadBits(11);
                portraitGiverNameLen = packet.ReadBits(9);
                portraitTurnInTextLen = packet.ReadBits(11);
                portraitTurnInNameLen = packet.ReadBits(9);
            }
            else
            {
                questTitleLen = packet.ReadBits(9);
                rewardTextLen = packet.ReadBits(12);
                portraitGiverTextLen = packet.ReadBits(10);
                portraitGiverNameLen = packet.ReadBits(8);
                portraitTurnInTextLen = packet.ReadBits(10);
                portraitTurnInNameLen = packet.ReadBits(8);
            }

            packet.ReadWoWString("QuestTitle", questTitleLen);
            questOfferReward.RewardText = packet.ReadWoWString("RewardText", rewardTextLen);
            packet.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            Storage.QuestOfferRewards.Add(questOfferReward, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_DISPLAY_PLAYER_CHOICE)]
        public static void HandleDisplayPlayerChoice(Packet packet)
        {
            var choiceId = packet.ReadInt32("ChoiceID");
            var responseCount = packet.ReadUInt32();
            packet.ReadPackedGuid128("SenderGUID");
            var uiTextureKitId = packet.ReadInt32("UiTextureKitID");
            var soundKitId = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                soundKitId = packet.ReadUInt32("SoundKitID");
            packet.ResetBitReader();
            var questionLength = packet.ReadBits(8);
            packet.ReadBit("CloseChoiceFrame");
            var hideWarboardHeader = packet.ReadBit("HideWarboardHeader");
            var keepOpenAfterChoice = packet.ReadBit("KeepOpenAfterChoice");

            for (var i = 0u; i < responseCount; ++i)
                ReadPlayerChoiceResponse(packet, choiceId, i, "PlayerChoiceResponse", i);

            var question = packet.ReadWoWString("Question", questionLength);

            Storage.PlayerChoices.Add(new PlayerChoiceTemplate
            {
                ChoiceId = choiceId,
                UiTextureKitId = uiTextureKitId,
                SoundKitId = soundKitId,
                Question = question,
                HideWarboardHeader = hideWarboardHeader,
                KeepOpenAfterChoice = keepOpenAfterChoice
            }, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                Storage.PlayerChoiceLocales.Add(new PlayerChoiceLocaleTemplate
                {
                    ChoiceId = choiceId,
                    Locale = ClientLocale.PacketLocaleString,
                    Question = question
                }, packet.TimeSpan);
            }
        }

        public static void ReadPlayerChoiceResponse(Packet packet, int choiceId, uint index, params object[] indexes)
        {
            var responseId = packet.ReadInt32("ResponseID", indexes);
            var choiceArtFileId = packet.ReadInt32("ChoiceArtFileID", indexes);
            var flags = packet.ReadInt32("Flags", indexes);
            var widgetSetId = packet.ReadUInt32("WidgetSetID", indexes);
            var uiTextureAtlasElementID = 0u;
            var soundKitId = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_5_29683))
            {
                uiTextureAtlasElementID = packet.ReadUInt32("UiTextureAtlasElementID", indexes);
                soundKitId = packet.ReadUInt32("SoundKitID", indexes);
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_1_5_29683))
                    packet.ReadUInt32("Unk801", indexes);
            }
            var groupID = packet.ReadByte("GroupID", indexes);
            packet.ResetBitReader();
            var answerLength = packet.ReadBits(9);
            var headerLength = packet.ReadBits(9);
            var subHeaderLength = 0u;
            var buttonTooltipLength = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
            {
                subHeaderLength = packet.ReadBits(7);
                buttonTooltipLength = packet.ReadBits(9);
            }
            var descriptionLength = packet.ReadBits(11);
            var confirmationTextLength = packet.ReadBits(7);
            var hasRewardQuestID = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_5_29683))
                hasRewardQuestID = packet.ReadBit();
            var hasReward = packet.ReadBit();
            if (hasReward)
                V6_0_2_19033.Parsers.QuestHandler.ReadPlayerChoiceResponseReward(packet, choiceId, responseId, "PlayerChoiceResponseReward", indexes);

            var answer = packet.ReadWoWString("Answer", answerLength, indexes);
            var header = packet.ReadWoWString("Header", headerLength, indexes);
            var subheader = packet.ReadWoWString("SubHeader", subHeaderLength, indexes);
            var buttonTooltip = packet.ReadWoWString("ButtonTooltip", buttonTooltipLength, indexes);
            var description = packet.ReadWoWString("Description", descriptionLength, indexes);
            var confirmation = packet.ReadWoWString("ConfirmationText", confirmationTextLength, indexes);

            var rewardQuestID = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_5_29683))
            {
                if (hasRewardQuestID)
                    rewardQuestID = packet.ReadUInt32("RewardQuestID", indexes);
            }

            Storage.PlayerChoiceResponses.Add(new PlayerChoiceResponseTemplate
            {
                ChoiceId = choiceId,
                ResponseId = responseId,
                Index = index,
                ChoiceArtFileId = choiceArtFileId,
                Flags = flags,
                WidgetSetId = widgetSetId,
                UiTextureAtlasElementID = uiTextureAtlasElementID,
                SoundKitId = soundKitId,
                GroupId = groupID,
                Header = header,
                Subheader = subheader,
                ButtonTooltip = buttonTooltip,
                Answer = answer,
                Description = description,
                Confirmation = confirmation,
                RewardQuestID = rewardQuestID
            }, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                Storage.PlayerChoiceResponseLocales.Add(new PlayerChoiceResponseLocaleTemplate
                {
                    ChoiceId = choiceId,
                    ResponseId = responseId,
                    Locale = ClientLocale.PacketLocaleString,
                    Header = header,
                    Subheader = subheader,
                    ButtonTooltip = buttonTooltip,
                    Description = description,
                    Answer = answer,
                    Confirmation = confirmation
                }, packet.TimeSpan);
            }
        }

        [Parser(Opcode.CMSG_CLOSE_QUEST_CHOICE)]
        public static void HandleQuestEmpty(Packet packet) { }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleQuestGiverRequestItems(Packet packet)
        {
            var questgiverGUID = packet.ReadPackedGuid128("QuestGiverGUID");
            var requestItems = packet.Holder.QuestGiverRequestItems = new();
            requestItems.QuestGiver = questgiverGUID;
            requestItems.QuestGiverEntry = (uint)packet.ReadInt32("QuestGiverCreatureID");

            int id = packet.ReadInt32("QuestID");
            int delay = requestItems.EmoteDelay = packet.ReadInt32("EmoteDelay");
            int emote = requestItems.EmoteType = packet.ReadInt32("EmoteType");
            requestItems.QuestId = (uint)id;

            CoreParsers.QuestHandler.AddQuestEnder(questgiverGUID, (uint)id);

            for (int i = 0; i < 2; i++)
            {
                var flags = packet.ReadInt32("QuestFlags", i);
                if (i == 0)
                    requestItems.QuestFlags = (uint)flags;
                else if (i == 1)
                    requestItems.QuestFlags2 = (uint)flags;
            }

            requestItems.SuggestedPartyMembers = packet.ReadInt32("SuggestPartyMembers");
            requestItems.MoneyToGet = packet.ReadInt32("MoneyToGet");
            uint collectCount = requestItems.CollectCount = packet.ReadUInt32("CollectCount");
            uint currencyCount = requestItems.CurrencyCount = packet.ReadUInt32("CurrencyCount");
            QuestStatusFlags statusFlags = packet.ReadInt32E<QuestStatusFlags>("StatusFlags");
            requestItems.StatusFlags = (PacketQuestStatusFlags)statusFlags;
            bool isComplete = (statusFlags & (QuestStatusFlags.Complete)) == QuestStatusFlags.Complete;
            bool noRequestOnComplete = (statusFlags & QuestStatusFlags.NoRequestOnComplete) != 0;

            for (int i = 0; i < collectCount; i++)
            {
                var objectId = packet.ReadInt32("ObjectID", i);
                var amount = packet.ReadInt32("Amount", i);
                var flags = packet.ReadUInt32("Flags", i);
                requestItems.Collect.Add(new QuestCollect()
                {
                    Id = objectId,
                    Count = amount,
                    Flags = flags
                });
            }

            for (int i = 0; i < currencyCount; i++)
            {
                var currencyId = packet.ReadInt32("CurrencyID", i);
                var amount = packet.ReadInt32("Amount", i);
                requestItems.Currencies.Add(new Currency()
                {
                    Id = (uint)currencyId,
                    Count = (uint)amount
                });
            }

            packet.ResetBitReader();

            requestItems.AutoLaunched = packet.ReadBit("AutoLaunched");

            packet.ResetBitReader();

            uint questTitleLen = 0;
            uint completionTextLen = 0;

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_1_5_29683))
            {
                questTitleLen = packet.ReadBits(10);
            }
            else
            {
                questTitleLen = packet.ReadBits(9);
            }
            completionTextLen = packet.ReadBits(12);

            requestItems.QuestTitle = packet.ReadWoWString("QuestTitle", questTitleLen);
            string completionText = requestItems.CompletionText = packet.ReadWoWString("CompletionText", completionTextLen);

            CoreParsers.QuestHandler.QuestRequestItemHelper(id, completionText, delay, emote, isComplete, packet, noRequestOnComplete);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS && completionText != string.Empty)
            {
                QuestRequestItemsLocale localesQuestRequestItems = new QuestRequestItemsLocale
                {
                    ID = (uint)id,
                    CompletionText = completionText
                };
                Storage.LocalesQuestRequestItems.Add(localesQuestRequestItems, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_LIST_MESSAGE, ClientVersionBuild.V8_1_0_28724, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleQuestgiverQuestList(Packet packet)
        {
            WowGuid guid = packet.ReadPackedGuid128("QuestGiverGUID");

            QuestGreeting questGreeting = new QuestGreeting
            {
                ID = guid.GetEntry(),
                GreetEmoteDelay = packet.ReadUInt32("GreetEmoteDelay"),
                GreetEmoteType = packet.ReadUInt32("GreetEmoteType")
            };

            uint questsCount = packet.ReadUInt32("GossipQuestsCount");
            packet.ResetBitReader();
            uint greetingLen = packet.ReadBits(12);

            for (int i = 0; i < questsCount; i++)
                V7_0_3_22248.Parsers.NpcHandler.ReadGossipQuestTextData(packet, i, "GossipQuests");

            questGreeting.Greeting = packet.ReadWoWString("Greeting", greetingLen);

            switch (guid.GetObjectType())
            {
                case ObjectType.Unit:
                    questGreeting.Type = 0;
                    break;
                case ObjectType.GameObject:
                    questGreeting.Type = 1;
                    break;
            }

            Storage.QuestGreetings.Add(questGreeting, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS && questGreeting.Greeting != string.Empty)
            {
                QuestGreetingLocale localesQuestGreeting = new QuestGreetingLocale
                {
                    ID = questGreeting.ID,
                    Type = questGreeting.Type,
                    Greeting = questGreeting.Greeting
                };
                Storage.LocalesQuestGreeting.Add(localesQuestGreeting, packet.TimeSpan);
            }
        }

        [Parser(Opcode.CMSG_UI_MAP_QUEST_LINES_REQUEST)]
        public static void HandleUiMapQuestLinesRequest(Packet packet)
        {
            packet.ReadInt32("UiMapID");
        }

        [Parser(Opcode.SMSG_UI_MAP_QUEST_LINES_RESPONSE)]
        public static void HandleUiMapQuestLinesResponse(Packet packet)
        {
            packet.ReadInt32("UiMapID");
            var count = packet.ReadUInt32();

            for (int i = 0; i < count; i++)
                packet.ReadUInt32("QuestLineXQuestID", i);
        }
    }
}
