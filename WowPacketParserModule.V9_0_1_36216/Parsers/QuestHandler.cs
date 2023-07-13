using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class QuestHandler
    {
        public static ItemInstance ReadRewardItem(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBitsE<LootItemType>("LootItemType", 2, idx);
            var itemInstance = Substructures.ItemHandler.ReadItemInstance(packet, idx);

            packet.ReadInt32("Quantity", idx);
            return itemInstance;
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

        [Parser(Opcode.CMSG_REQUEST_COVENANT_CALLINGS)]
        [Parser(Opcode.SMSG_CLEAR_TREASURE_PICKER_CACHE)]
        public static void HandleEmpty(Packet packet)
        {
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
            for (int i = 0; i < 6; ++i)
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

            if(ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
                quest.PortraitGiverModelSceneID = packet.ReadInt32("PortraitGiverModelSceneID");

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
                quest.RewardCurrencyID[i] = (uint)packet.ReadInt32("RewardCurrencyID", i);
                quest.RewardCurrencyCount[i] = (uint)packet.ReadInt32("RewardCurrencyQty", i);
            }

            quest.SoundAccept = (uint)packet.ReadInt32("AcceptedSoundKitID");
            quest.SoundTurnIn = (uint)packet.ReadInt32("CompleteSoundKitID");
            quest.AreaGroupID = (uint)packet.ReadInt32("AreaGroupID");
            quest.TimeAllowed = packet.ReadInt32("TimeAllowed");
            uint objectiveCount = packet.ReadUInt32("ObjectiveCount");
            quest.AllowableRacesWod = packet.ReadUInt64("AllowableRaces");
            quest.QuestRewardID = packet.ReadInt32("TreasurePickerID");
            quest.Expansion = packet.ReadInt32("Expansion");
            quest.ManagedWorldStateID = packet.ReadInt32("ManagedWorldStateID");
            quest.QuestSessionBonus = packet.ReadInt32("QuestSessionBonus");

            for (uint i = 0; i < rewardDisplaySpellCount; ++i)
            {
                QuestRewardDisplaySpell questRewardDisplaySpell = new QuestRewardDisplaySpell
                {
                    QuestID = (uint)id.Key,
                    Idx = i,
                    SpellID = (uint)packet.ReadInt32<SpellId>("SpellID", i, "RewardDisplaySpell"),
                    PlayerConditionID = (uint)packet.ReadInt32("PlayerConditionID", i, "RewardDisplaySpell")
            };

                Storage.QuestRewardDisplaySpells.Add(questRewardDisplaySpell, packet.TimeSpan);
            }

            packet.ResetBitReader();

            uint logTitleLen = packet.ReadBits("logTitleLen", 9);
            uint logDescriptionLen = packet.ReadBits("logDescriptionLen", 12);
            uint questDescriptionLen = packet.ReadBits("questDescriptionLen", 12);
            uint areaDescriptionLen = packet.ReadBits("areaDescriptionLen", 9);
            uint questGiverTextWindowLen = packet.ReadBits("questGiverTextWindowLen", 10);
            uint questGiverTargetNameLen = packet.ReadBits("questGiverTargetNameLen", 8);
            uint questTurnTextWindowLen = packet.ReadBits("questTurnTextWindowLen", 10);
            uint questTurnTargetNameLen = packet.ReadBits("questTurnTargetNameLen", 8);
            uint questCompletionLogLen = packet.ReadBits("questCompletionLogLen", 11);
            packet.ReadBit("ReadyForTranslation");

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
                        NavigationPlayerConditionID = packet.ReadInt32("NavigationPlayerConditionID", i, j),
                        SpawnTrackingID = packet.ReadInt32("SpawnTrackingID", i, j)
                    };

                    var questPOIBlobPoint = packet.ReadUInt32("QuestPOIBlobPoint", i, j);
                    for (var k = 0; k < questPOIBlobPoint; ++k)
                    {
                        QuestPOIPoint questPoiPoint = new QuestPOIPoint
                        {
                            QuestID = questId,
                            Idx1 = j,
                            Idx2 = k,
                            X = packet.ReadInt16("X", i, j, k),
                            Y = packet.ReadInt16("Y", i, j, k),
                            Z = packet.ReadInt16("Z", i, j, k)
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
            packet.ReadBit("Unused");

            ReadQuestRewards(packet, "QuestRewards");

            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitGiver");
            packet.ReadInt32("PortraitGiverMount");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
                packet.ReadInt32("PortraitGiverModelSceneID");
            packet.ReadInt32("PortraitTurnIn");

            packet.ResetBitReader();

            uint questTitleLen = packet.ReadBits(9);
            uint rewardTextLen = packet.ReadBits(12);
            uint portraitGiverTextLen = packet.ReadBits(10);
            uint portraitGiverNameLen = packet.ReadBits(8);
            uint portraitTurnInTextLen = packet.ReadBits(10);
            uint portraitTurnInNameLen = packet.ReadBits(8);

            packet.ReadWoWString("QuestTitle", questTitleLen);
            questOfferReward.RewardText = packet.ReadWoWString("RewardText", rewardTextLen);
            packet.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            Storage.QuestOfferRewards.Add(questOfferReward, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD)]
        public static void HandleQuestChooseReward(Packet packet)
        {
            var chooseReward = packet.Holder.ClientQuestGiverChooseReward = new();
            chooseReward.QuestGiver = packet.ReadPackedGuid128("QuestGiverGUID");
            chooseReward.QuestId = (uint)packet.ReadInt32("QuestID");
            chooseReward.Item = (uint)ReadRewardItem(packet, "ItemChoice").ItemID;
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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
                packet.ReadInt32("PortraitGiverModelSceneID");
            packet.ReadInt32("PortraitTurnIn");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("SuggestedPartyMembers");
            var learnSpellsCount = packet.ReadUInt32("LearnSpellsCount");

            var descEmotesCount = packet.ReadUInt32("DescEmotesCount");
            var objectivesCount = packet.ReadUInt32("ObjectivesCount");
            packet.ReadInt32("QuestStartItemID");
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

            packet.ReadBit("AutoLaunched");
            packet.ReadBit("Unused");
            packet.ReadBit("StartCheat");
            packet.ReadBit("DisplayPopup");

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

        [Parser(Opcode.CMSG_QUEST_GIVER_CLOSE_QUEST)]
        public static void HandleQuestGiverCloseQuest(Packet packet)
        {
            packet.ReadInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.SMSG_DISPLAY_PLAYER_CHOICE)]
        public static void HandleDisplayPlayerChoice(Packet packet)
        {
            var choiceId = packet.ReadInt32("ChoiceID");
            var responseCount = packet.ReadUInt32();
            packet.ReadPackedGuid128("SenderGUID");
            var uiTextureKitId = packet.ReadInt32("UiTextureKitID");
            var soundKitId = packet.ReadUInt32("SoundKitID");
            uint? closeSoundKitId = null;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                closeSoundKitId = packet.ReadUInt32("CloseUISoundKitID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
                packet.ReadByte("NumRerolls");
            long? duration = null;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                duration = packet.ReadInt64("Duration");
            packet.ResetBitReader();
            var questionLength = packet.ReadBits(8);
            var pendingChoiceTextLength = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                pendingChoiceTextLength = packet.ReadBits(8);
            packet.ReadBit("CloseChoiceFrame");
            var hideWarboardHeader = packet.ReadBit("HideWarboardHeader");
            var keepOpenAfterChoice = packet.ReadBit("KeepOpenAfterChoice");

            for (var i = 0u; i < responseCount; ++i)
                ReadPlayerChoiceResponse(packet, choiceId, i, "PlayerChoiceResponse", i);

            var question = packet.ReadWoWString("Question", questionLength);
            var pendingChoiceText = "";
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                pendingChoiceText = packet.ReadWoWString("PendingChoiceText", pendingChoiceTextLength);

            Storage.PlayerChoices.Add(new PlayerChoiceTemplate
            {
                ChoiceId = choiceId,
                UiTextureKitId = uiTextureKitId,
                SoundKitId = soundKitId,
                CloseSoundKitId = closeSoundKitId,
                Duration = duration,
                Question = question,
                PendingChoiceText = pendingChoiceText,
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

        public static void ReadPlayerChoiceResponseMawPower(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            packet.ReadInt32("Unused901_1", indexes);
            packet.ReadInt32("TypeArtFileID", indexes);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_2_0_42423))
            {
                packet.ReadInt32("Rarity", indexes);
                packet.ReadUInt32("RarityColor", indexes);
            }
            packet.ReadInt32("Unused901_2", indexes);
            packet.ReadInt32("SpellID", indexes);
            packet.ReadInt32("MaxStacks", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
            {
                var hasRarity = packet.ReadBit();
                var hasRarityColor = packet.ReadBit();

                if (hasRarity)
                    packet.ReadInt32("Rarity", indexes);

                if (hasRarityColor)
                    packet.ReadUInt32("RarityColor", indexes);
            }
        }

        public static void ReadPlayerChoiceResponse(Packet packet, int choiceId, uint index, params object[] indexes)
        {
            var responseId = packet.ReadInt32("ResponseID", indexes);
            var responseIdentifier = packet.ReadInt16("ResponseIdentifier", indexes);
            var choiceArtFileId = packet.ReadInt32("ChoiceArtFileID", indexes);
            var flags = packet.ReadInt32("Flags", indexes);
            var widgetSetId = packet.ReadUInt32("WidgetSetID", indexes);
            var uiTextureAtlasElementID = packet.ReadUInt32("UiTextureAtlasElementID", indexes);
            var soundKitId = packet.ReadUInt32("SoundKitID", indexes);
            var groupID = packet.ReadByte("GroupID", indexes);
            var uiTextureKitID = packet.ReadUInt32("UiTextureKitID", indexes);

            packet.ResetBitReader();
            var answerLength = packet.ReadBits(9);
            var headerLength = packet.ReadBits(9);
            var subHeaderLength = packet.ReadBits(7);
            var buttonTooltipLength = packet.ReadBits(9);
            var descriptionLength = packet.ReadBits(11);
            var confirmationTextLength = packet.ReadBits(7);
            var hasRewardQuestID = packet.ReadBit();
            var hasReward = packet.ReadBit();
            var hasMawPower = packet.ReadBit();
            if (hasReward)
                V6_0_2_19033.Parsers.QuestHandler.ReadPlayerChoiceResponseReward(packet, choiceId, responseId, "PlayerChoiceResponseReward", indexes);

            var answer = packet.ReadWoWString("Answer", answerLength, indexes);
            var header = packet.ReadWoWString("Header", headerLength, indexes);
            var subheader = packet.ReadWoWString("SubHeader", subHeaderLength, indexes);
            var buttonTooltip = packet.ReadWoWString("ButtonTooltip", buttonTooltipLength, indexes);
            var description = packet.ReadWoWString("Description", descriptionLength, indexes);
            var confirmation = packet.ReadWoWString("ConfirmationText", confirmationTextLength, indexes);

            var rewardQuestID = 0u;
            if (hasRewardQuestID)
                rewardQuestID = packet.ReadUInt32("RewardQuestID", indexes);

            if (hasMawPower)
                ReadPlayerChoiceResponseMawPower(packet, indexes);

            Storage.PlayerChoiceResponses.Add(new PlayerChoiceResponseTemplate
            {
                ChoiceId = choiceId,
                ResponseIdentifier = responseIdentifier,
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
                RewardQuestID = rewardQuestID,
                UiTextureKitID = uiTextureKitID
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

        [Parser(Opcode.CMSG_CHOICE_RESPONSE, ClientVersionBuild.V9_1_0_39185)]
        public static void HandleChoiceResponse(Packet packet)
        {
            packet.ReadInt32("ChoiceID");
            packet.ReadInt32("ResponseID");
            packet.ReadBit("IsReroll");
        }

        [Parser(Opcode.SMSG_WORLD_QUEST_UPDATE_RESPONSE)]
        public static void HandleWorldQuestUpdateResponse(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (int i = 0; i < count; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
                    packet.ReadTime64("LastUpdate", i);
                else
                    packet.ReadTime("LastUpdate", i);

                packet.ReadUInt32<QuestId>("QuestID", i);
                packet.ReadUInt32("Timer", i);
                packet.ReadInt32("VariableID", i);
                packet.ReadInt32("Value", i);
            }
        }

        [Parser(Opcode.SMSG_QUEST_PUSH_RESULT)]
        public static void HandleQuestPushResult(Packet packet)
        {
            packet.ReadPackedGuid128("SenderGUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
            {
                packet.ReadByteE<QuestPushReason915>("Result");

                var questTitleLength = packet.ReadBits(9);
                packet.ReadWoWString("QuestTitle", questTitleLength);
            }
            else
                packet.ReadByteE<QuestPushReason>("Result");
        }
    }
}
