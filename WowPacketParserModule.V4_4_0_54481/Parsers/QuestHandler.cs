using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class QuestHandler
    {
        public enum ConditionalTextType
        {
            Description = 0,
            CompletionLog = 1,
            OfferReward = 2,
            RequestItems = 3
        }

        public static void ReadConditionalQuestText(Packet packet, int questId, int idx, ConditionalTextType type, params object[] indexes)
        {
            var playerConditionId = packet.ReadInt32("PlayerConditionID", indexes);
            var questgiverCreatureId = packet.ReadInt32("QuestGiverCreatureID", indexes);

            packet.ResetBitReader();
            var textLength = packet.ReadBits(12);
            var text = packet.ReadWoWString("Text", textLength, indexes);

            QuestTextConditional questTextConditional = new QuestTextConditional
            {
                QuestId = questId,
                PlayerConditionId = playerConditionId,
                QuestgiverCreatureId = questgiverCreatureId,
                Text = text,
                OrderIndex = idx
            };

            switch (type)
            {
                case ConditionalTextType.Description:
                    Storage.QuestDescriptionConditional.Add(new QuestDescriptionConditional(questTextConditional), packet.TimeSpan);
                    break;
                case ConditionalTextType.CompletionLog:
                    Storage.QuestCompletionLogConditional.Add(new QuestCompletionLogConditional(questTextConditional), packet.TimeSpan);
                    break;
                case ConditionalTextType.OfferReward:
                    Storage.QuestOfferRewardConditional.Add(new QuestOfferRewardConditional(questTextConditional), packet.TimeSpan);
                    break;
                case ConditionalTextType.RequestItems:
                    Storage.QuestRequestItemsConditional.Add(new QuestRequestItemsConditional(questTextConditional), packet.TimeSpan);
                    break;
                default:
                    break;
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
            packet.ReadBit(); // Unused
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

        public static QuestOfferReward ReadQuestGiverOfferRewardData(Packet packet, params object[] indexes)
        {
            var questgiverGUID = packet.ReadPackedGuid128("QuestGiverGUID");

            packet.ReadInt32("QuestGiverCreatureID");
            int id = packet.ReadInt32("QuestID");

            QuestOfferReward questOfferReward = new QuestOfferReward
            {
                ID = (uint)id
            };

            CoreParsers.QuestHandler.AddQuestEnder(questgiverGUID, (uint)id);

            for (int i = 0; i < 3; i++)
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

            return questOfferReward;
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_STATUS_QUERY)]
        public static void HandleQuestgiverStatusQuery(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS)]
        public static void HandleQuestgiverStatus(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadUInt64E<QuestGiverStatus4x>("Status");
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

            for (uint i = 0; i < 3; ++i)
            {
                QuestRewardDisplaySpell questRewardDisplaySpell = new QuestRewardDisplaySpell
                {
                    QuestID = (uint)id.Key,
                    Idx = i,
                    SpellID = (uint)packet.ReadInt32<SpellId>("SpellID", i, "RewardDisplaySpell"),
                };

                if (questRewardDisplaySpell.SpellID != 0)
                    Storage.QuestRewardDisplaySpells.Add(questRewardDisplaySpell, packet.TimeSpan);
            }

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
            quest.TimeAllowed = packet.ReadInt64("TimeAllowed");
            uint objectiveCount = packet.ReadUInt32("ObjectiveCount");
            quest.AllowableRacesWod = packet.ReadUInt64("AllowableRaces");
            quest.QuestRewardID = packet.ReadInt32("TreasurePickerID");
            quest.Expansion = packet.ReadInt32("Expansion");
            packet.ReadInt32("QuestGiverCreatureID");

            var conditionalQuestDescriptionCount = packet.ReadUInt32();
            var conditionalQuestCompletionLogCount = packet.ReadUInt32();

            packet.ResetBitReader();

            uint logTitleLen = packet.ReadBits(9);
            uint logDescriptionLen = packet.ReadBits(12);
            uint questDescriptionLen = packet.ReadBits(12);
            uint areaDescriptionLen = packet.ReadBits(9);
            uint questGiverTextWindowLen = packet.ReadBits(10);
            uint questGiverTargetNameLen = packet.ReadBits(8);
            uint questTurnTextWindowLen = packet.ReadBits(10);
            uint questTurnTargetNameLen = packet.ReadBits(8);
            uint questCompletionLogLen = packet.ReadBits(11);

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

            for (int i = 0; i < conditionalQuestDescriptionCount; i++)
                ReadConditionalQuestText(packet, id.Key, i, ConditionalTextType.Description, i, "ConditionalDescriptionText");

            for (int i = 0; i < conditionalQuestCompletionLogCount; i++)
                ReadConditionalQuestText(packet, id.Key, i, ConditionalTextType.CompletionLog, i, "ConditionalCompletionLogText");

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

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatusMultiple(Packet packet)
        {
            var int16 = packet.ReadInt32("QuestGiverStatusCount");
            for (var i = 0; i < int16; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadUInt64E<QuestGiverStatus4x>("Status", i);
            }
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPOIQuery(Packet packet)
        {
            packet.ReadUInt32("MissingQuestCount");

            for (var i = 0; i < 25; i++)
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

        [Parser(Opcode.SMSG_DISPLAY_PLAYER_CHOICE)]
        public static void HandleDisplayPlayerChoice(Packet packet)
        {
            var choiceId = packet.ReadInt32("ChoiceID");
            var responseCount = packet.ReadUInt32();
            packet.ReadPackedGuid128("SenderGUID");
            var uiTextureKitId = packet.ReadInt32("UiTextureKitID");
            var soundKitId = packet.ReadUInt32("SoundKitID");
            var closeSoundKitId = packet.ReadUInt32("CloseUISoundKitID");
            packet.ReadByte("NumRerolls");
            var duration = packet.ReadInt64("Duration");
            packet.ResetBitReader();
            var questionLength = packet.ReadBits(8);
            var pendingChoiceTextLength = packet.ReadBits(8);
            packet.ReadBit("CloseChoiceFrame");
            var hideWarboardHeader = packet.ReadBit("HideWarboardHeader");
            var keepOpenAfterChoice = packet.ReadBit("KeepOpenAfterChoice");

            for (var i = 0u; i < responseCount; ++i)
                ReadPlayerChoiceResponse(packet, choiceId, i, "PlayerChoiceResponse", i);

            var question = packet.ReadWoWString("Question", questionLength);
            var pendingChoiceText = packet.ReadWoWString("PendingChoiceText", pendingChoiceTextLength);

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

        [Parser(Opcode.SMSG_QUEST_COMPLETION_NPC_RESPONSE)]
        public static void HandleQuestCompletionNPCResponse(Packet packet)
        {
            var int1 = packet.ReadInt32("QuestCompletionNPCsCount");

            // QuestCompletionNPC
            for (var i = 0; i < int1; ++i)
            {
                packet.ReadInt32("Quest Id", i);

                var int4 = packet.ReadInt32("NpcCount", i);
                for (var j = 0; j < int4; ++j)
                    packet.ReadInt32("Npc", i, j);
            }
        }

        [Parser(Opcode.SMSG_QUEST_CONFIRM_ACCEPT)]
        public static void HandleQuestConfirmAccept(Packet packet)
        {
            packet.ReadInt32("QuestID");
            packet.ReadPackedGuid128("InitiatedBy");
            var len = packet.ReadBits(10);
            packet.ReadWoWString("QuestTitle", len);
        }

        [Parser(Opcode.SMSG_QUEST_FORCE_REMOVED)]
        public static void HandleQuestForceRemoved(Packet packet)
        {
            packet.ReadInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_INVALID_QUEST)]
        public static void HandleQuestGiverInvalidQuest(Packet packet)
        {
            packet.ReadUInt32E<QuestReasonTypeWoD>("Reason");
            packet.ReadInt32("ContributionRewardID");

            packet.ResetBitReader();

            packet.ReadBit("SendErrorMessage");

            var len = packet.ReadBits(9);
            packet.ReadWoWString("ReasonText", len);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE)]
        public static void QuestGiverOfferReward(Packet packet)
        {
            var questOfferReward = ReadQuestGiverOfferRewardData(packet, "QuestGiverOfferRewardData");

            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitGiver");
            packet.ReadInt32("PortraitGiverMount");
            packet.ReadInt32("PortraitGiverModelSceneID");
            packet.ReadInt32("PortraitTurnIn");
            packet.ReadInt32("QuestGiverCreatureID");
            var conditionalRewardTextCount = packet.ReadUInt32();

            packet.ResetBitReader();

            uint questTitleLen = packet.ReadBits(9);
            uint rewardTextLen = packet.ReadBits(12);
            uint portraitGiverTextLen = packet.ReadBits(10);
            uint portraitGiverNameLen = packet.ReadBits(8);
            uint portraitTurnInTextLen = packet.ReadBits(10);
            uint portraitTurnInNameLen = packet.ReadBits(8);

            for (int i = 0; i < conditionalRewardTextCount; i++)
                ReadConditionalQuestText(packet, (int)questOfferReward.ID, i, ConditionalTextType.OfferReward, i, "ConditionalRewardText");

            packet.ReadWoWString("QuestTitle", questTitleLen);
            questOfferReward.RewardText = packet.ReadWoWString("RewardText", rewardTextLen);
            packet.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            Storage.QuestOfferRewards.Add(questOfferReward, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS && questOfferReward.RewardText != string.Empty)
            {
                QuestOfferRewardLocale localesQuestOfferReward = new QuestOfferRewardLocale
                {
                    ID = questOfferReward.ID,
                    RewardText = questOfferReward.RewardText
                };

                Storage.LocalesQuestOfferRewards.Add(localesQuestOfferReward, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE)]
        public static void HandlQuestGiverQuestComplete(Packet packet)
        {
            var questComplete = packet.Holder.QuestGiverQuestComplete = new();
            questComplete.QuestId = (uint)packet.ReadInt32("QuestId");
            packet.ReadInt32("XpReward");
            packet.ReadInt64("MoneyReward");
            packet.ReadInt32("SkillLineIDReward");
            packet.ReadInt32("NumSkillUpsReward");

            packet.ResetBitReader();

            packet.ReadBit("UseQuestReward");
            packet.ReadBit("LaunchGossip");
            packet.ReadBit("LaunchQuest");
            packet.ReadBit("HideChatMessage");

            Substructures.ItemHandler.ReadItemInstance(packet, "ItemReward");
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
            packet.ReadInt32("PortraitGiverModelSceneID");
            packet.ReadInt32("PortraitTurnIn");

            for (int i = 0; i < 3; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("SuggestedPartyMembers");
            var learnSpellsCount = packet.ReadUInt32("LearnSpellsCount");

            var descEmotesCount = packet.ReadUInt32("DescEmotesCount");
            var objectivesCount = packet.ReadUInt32("ObjectivesCount");
            packet.ReadInt32("QuestStartItemID");
            packet.ReadInt32("QuestSessionBonus");
            packet.ReadInt32("QuestGiverCreatureID");
            var conditionalDescriptionTextCount = packet.ReadUInt32();

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

            for (int i = 0; i < conditionalDescriptionTextCount; i++)
                ReadConditionalQuestText(packet, id, i, ConditionalTextType.Description, i, "ConditionalDescriptionText");

            Storage.QuestDetails.Add(questDetails, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_FAILED)]
        public static void HandleQuestFailed(Packet packet)
        {
            var questFailed = packet.Holder.QuestFailed = new();
            questFailed.QuestId = packet.ReadUInt32<QuestId>("Quest ID");
            packet.ReadUInt32("Reason");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_LIST_MESSAGE)]
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
            uint greetingLen = packet.ReadBits(11);

            for (int i = 0; i < questsCount; i++)
                NpcHandler.ReadGossipQuestTextData(packet, i, "GossipQuests");

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

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS)]
        public static void HandleQuestGiverRequestItems(Packet packet)
        {
            var requestItems = packet.Holder.QuestGiverRequestItems = new();
            var questgiverGUID = packet.ReadPackedGuid128("QuestGiverGUID");
            requestItems.QuestGiver = questgiverGUID;
            requestItems.QuestGiverEntry = (uint)packet.ReadInt32("QuestGiverCreatureID");

            int id = packet.ReadInt32("QuestID");
            int delay = requestItems.EmoteDelay = packet.ReadInt32("EmoteDelay");
            int emote = requestItems.EmoteType = packet.ReadInt32("EmoteType");
            requestItems.QuestId = (uint)id;

            CoreParsers.QuestHandler.AddQuestEnder(questgiverGUID, (uint)id);

            for (int i = 0; i < 3; i++)
            {
                var flags = packet.ReadInt32("QuestFlags", i);
                if (i == 0)
                    requestItems.QuestFlags = (uint)flags;
                else if (i == 1)
                    requestItems.QuestFlags2 = (uint)flags;
            }

            requestItems.SuggestedPartyMembers = packet.ReadInt32("SuggestPartyMembers");
            requestItems.MoneyToGet = packet.ReadInt32("MoneyToGet");

            var collectCount = requestItems.CollectCount = packet.ReadUInt32("CollectCount");
            var currencyCount = requestItems.CurrencyCount = packet.ReadUInt32("CurrencyCount");
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
            packet.ReadInt32("QuestGiverCreatureID"); // questgiver entry?
            var conditionalCompletionTextCount = packet.ReadUInt32();
            var questTitleLen = packet.ReadBits(9);
            var completionTextLen = packet.ReadBits(12);

            for (int i = 0; i < conditionalCompletionTextCount; i++)
                ReadConditionalQuestText(packet, id, i, ConditionalTextType.RequestItems, i, "ConditionalCompletionText");

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

        [Parser(Opcode.SMSG_QUEST_PUSH_RESULT)]
        public static void HandleQuestPushResult(Packet packet)
        {
            packet.ReadPackedGuid128("SenderGUID");
            packet.ReadByteE<QuestPushReason915>("Result");

            var questTitleLength = packet.ReadBits(9);
            packet.ReadWoWString("QuestTitle", questTitleLength);
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_CREDIT)]
        public static void HandleQuestUpdateAddCredit(Packet packet)
        {
            var addCredit = packet.Holder.QuestAddKillCredit = new();
            addCredit.Victim = packet.ReadPackedGuid128("VictimGUID");

            addCredit.QuestId = (uint)packet.ReadInt32("QuestID");
            addCredit.KillCredit = (uint)packet.ReadInt32("ObjectID");

            addCredit.Count = packet.ReadUInt16("Count");
            addCredit.RequiredCount = packet.ReadUInt16("Required");

            packet.ReadByte("ObjectiveType");
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_CREDIT_SIMPLE)]
        public static void HandleQuestUpdateAddCreditSimple(Packet packet)
        {
            packet.ReadInt32<QuestId>("QuestID");
            packet.ReadInt32("ObjectID");
            packet.ReadByte("ObjectiveType");
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_PVP_CREDIT)]
        public static void HandleQuestUpdateAddPvPCredit(Packet packet)
        {
            packet.ReadInt32<QuestId>("Quest ID");
            packet.ReadUInt16("Count");
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE)]
        public static void HandleQuestUpdateComplete(Packet packet)
        {
            var questComplete = packet.Holder.QuestComplete = new();
            questComplete.QuestId = (uint)packet.ReadInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_FAILED_TIMER)]
        public static void HandleQuestUpdateFailedTimer(Packet packet)
        {
            var questFailed = packet.Holder.QuestFailed = new();
            questFailed.QuestId = (uint)packet.ReadInt32<QuestId>("QuestID");
            questFailed.TimerFail = true;
        }

        [Parser(Opcode.SMSG_WORLD_QUEST_UPDATE_RESPONSE)]
        public static void HandleWorldQuestUpdateResponse(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (int i = 0; i < count; i++)
            {
                packet.ReadTime64("LastUpdate", i);
                packet.ReadUInt32<QuestId>("QuestID", i);
                packet.ReadUInt32("Timer", i);
                packet.ReadInt32("VariableID", i);
                packet.ReadInt32("Value", i);
            }
        }

        [Parser(Opcode.SMSG_DAILY_QUESTS_RESET)]
        [Parser(Opcode.CMSG_QUEST_GIVER_STATUS_MULTIPLE_QUERY)]
        [Parser(Opcode.SMSG_QUEST_LOG_FULL)]
        public static void HandleQuestZeroLengthPackets(Packet packet)
        {
        }
    }
}
