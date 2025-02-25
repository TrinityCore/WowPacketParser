using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class QuestHandler
    {
        public static void ReadQuestRewards(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ChoiceItemCount", idx);

            for (var i = 0; i < 6; ++i)
            {
                packet.ReadInt32("ItemID", idx, i);
                packet.ReadInt32("Quantity", idx, i);
            }

            packet.ReadInt32("ItemCount", idx);

            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("ItemID", idx, i);
                packet.ReadInt32("ItemQty", idx, i);
            }

            packet.ReadInt32("Money", idx);
            packet.ReadInt32("XP", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23826))
                packet.ReadInt64("ArtifactXP", idx);
            else
                packet.ReadInt32("ArtifactXP", idx);
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
            packet.ReadInt32("RewardID", idx);

            packet.ResetBitReader();

            packet.ReadBit("IsBoostSpell", idx);
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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
                quest.QuestMaxScalingLevel = packet.ReadInt32("QuestMaxScalingLevel");
            quest.QuestPackageID = packet.ReadUInt32("QuestPackageID");
            quest.MinLevel = packet.ReadInt32("QuestMinLevel");
            quest.QuestSortID = (QuestSort)packet.ReadUInt32("QuestSortID");
            quest.QuestInfoID = packet.ReadInt32E<QuestInfo>("QuestInfoID");
            quest.SuggestedGroupNum = packet.ReadUInt32("SuggestedGroupNum");
            quest.RewardNextQuest = packet.ReadUInt32("RewardNextQuest");
            quest.RewardXPDifficulty = packet.ReadUInt32("RewardXPDifficulty");

            quest.RewardXPMultiplier = packet.ReadSingle("RewardXPMultiplier");

            quest.RewardMoney = packet.ReadInt32("RewardMoney");
            quest.RewardMoneyDifficulty = packet.ReadUInt32("RewardMoneyDifficulty");

            quest.RewardMoneyMultiplier = packet.ReadSingle("RewardMoneyMultiplier");

            quest.RewardBonusMoney = packet.ReadUInt32("RewardBonusMoney");

            quest.RewardDisplaySpellLegion = new uint?[3];
            for (int i = 0; i < 3; ++i)
                quest.RewardDisplaySpellLegion[i] = packet.ReadUInt32("RewardDisplaySpell", i);

            quest.RewardSpellWod = packet.ReadUInt32("RewardSpell");
            quest.RewardHonorWod = packet.ReadUInt32("RewardHonor");

            quest.RewardKillHonor = packet.ReadSingle("RewardKillHonor");

            quest.RewardArtifactXPDifficulty = packet.ReadUInt32("RewardArtifactXPDifficulty");
            quest.RewardArtifactXPMultiplier = packet.ReadSingle("RewardArtifactXPMultiplier");
            quest.RewardArtifactCategoryID = packet.ReadUInt32("RewardArtifactCategoryID");

            quest.StartItem = packet.ReadUInt32("StartItem");
            quest.Flags = packet.ReadUInt32E<QuestFlags>("Flags");
            quest.FlagsEx = packet.ReadUInt32E<QuestFlagsEx>("FlagsEx");

            quest.RewardItem = new uint?[4];
            quest.RewardAmount = new uint?[4];
            quest.ItemDrop = new uint?[4];
            quest.ItemDropQuantity = new uint?[4];
            for (int i = 0; i < 4; ++i)
            {
                quest.RewardItem[i] = packet.ReadUInt32("RewardItems", i);
                quest.RewardAmount[i] = packet.ReadUInt32("RewardAmount", i);
                quest.ItemDrop[i] = packet.ReadUInt32("ItemDrop", i);
                quest.ItemDropQuantity[i] = packet.ReadUInt32("ItemDropQuantity", i);
            }

            quest.RewardChoiceItemID = new uint?[6];
            quest.RewardChoiceItemQuantity = new uint?[6];
            quest.RewardChoiceItemDisplayID = new uint?[6];
            for (int i = 0; i < 6; ++i) // CliQuestInfoChoiceItem
            {
                quest.RewardChoiceItemID[i] = packet.ReadUInt32("RewardChoiceItemID", i);
                quest.RewardChoiceItemQuantity[i] = packet.ReadUInt32("RewardChoiceItemQuantity", i);
                quest.RewardChoiceItemDisplayID[i] = packet.ReadUInt32("RewardChoiceItemDisplayID", i);
            }

            quest.POIContinent = packet.ReadUInt32("POIContinent");

            quest.POIx = packet.ReadSingle("POIx");
            quest.POIy = packet.ReadSingle("POIy");

            quest.POIPriorityWod = packet.ReadInt32("POIPriority");
            quest.RewardTitle = packet.ReadUInt32("RewardTitle");
            quest.RewardArenaPoints = packet.ReadUInt32("RewardArenaPoints");
            quest.RewardSkillLineID = packet.ReadUInt32("RewardSkillLineID");
            quest.RewardNumSkillUps = packet.ReadUInt32("RewardNumSkillUps");
            quest.QuestGiverPortrait = packet.ReadUInt32("PortraitGiver");
            quest.QuestTurnInPortrait = packet.ReadUInt32("PortraitTurnIn");

            quest.RewardFactionID = new uint?[5];
            quest.RewardFactionOverride = new int?[5];
            quest.RewardFactionValue = new int?[5];
            quest.RewardFactionCapIn = new int?[5];
            for (int i = 0; i < 5; ++i)
            {
                quest.RewardFactionID[i] = packet.ReadUInt32("RewardFactionID", i);
                quest.RewardFactionValue[i] = packet.ReadInt32("RewardFactionValue", i);
                quest.RewardFactionOverride[i] = packet.ReadInt32("RewardFactionOverride", i);
                quest.RewardFactionCapIn[i] = packet.ReadInt32("RewardFactionCapIn", i);
            }

            quest.RewardFactionFlags = packet.ReadUInt32("RewardFactionFlags");

            quest.RewardCurrencyID = new uint?[4];
            quest.RewardCurrencyCount = new uint?[4];
            for (int i = 0; i < 4; ++i)
            {
                quest.RewardCurrencyID[i] = packet.ReadUInt32("RewardCurrencyID");
                quest.RewardCurrencyCount[i] = packet.ReadUInt32("RewardCurrencyQty");
            }

            quest.SoundAccept = packet.ReadUInt32("AcceptedSoundKitID");
            quest.SoundTurnIn = packet.ReadUInt32("CompleteSoundKitID");
            quest.AreaGroupID = packet.ReadUInt32("AreaGroupID");
            quest.TimeAllowed = packet.ReadInt32("TimeAllowed");
            uint cliQuestInfoObjective = packet.ReadUInt32("CliQuestInfoObjective");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
                quest.AllowableRacesWod = packet.ReadUInt64("AllowableRaces");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_0_3_22248) && ClientVersion.RemovedInVersion(ClientVersionBuild.V7_3_5_25848))
                quest.AllowableRacesWod = (uint)packet.ReadInt32("AllowableRaces");
            quest.QuestRewardID = packet.ReadInt32("QuestRewardID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23826))
                quest.Expansion = packet.ReadInt32("Expansion");

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

            for (uint i = 0; i < cliQuestInfoObjective; ++i)
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
                questInfoObjective.Flags = packet.ReadUInt32("Flags", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_1_0_22900))
                    questInfoObjective.Flags2 = packet.ReadUInt32("Flags2", i);
                questInfoObjective.ProgressBarWeight = packet.ReadSingle("ProgressBarWeight", i);

                int visualEffectsCount = packet.ReadInt32("VisualEffects", i);
                for (uint j = 0; j < visualEffectsCount; ++j)
                {
                    QuestVisualEffect questVisualEffect = new QuestVisualEffect
                    {
                        ID = questInfoObjective.ID,
                        Index = j,
                        VisualEffect = packet.ReadInt32("VisualEffectId", i, j)
                    };

                    Storage.QuestVisualEffects.Add(questVisualEffect, packet.TimeSpan);
                }

                packet.ResetBitReader();

                uint bits6 = packet.ReadBits(8);
                questInfoObjective.Description = packet.ReadWoWString("Description", bits6, i);

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
                    LogTitle            = quest.LogTitle,
                    LogDescription      = quest.LogDescription,
                    QuestDescription    = quest.QuestDescription,
                    AreaDescription     = quest.AreaDescription,
                    PortraitGiverText   = quest.QuestGiverTextWindow,
                    PortraitGiverName   = quest.QuestGiverTargetName,
                    PortraitTurnInText  = quest.QuestTurnTextWindow,
                    PortraitTurnInName  = quest.QuestTurnTargetName,
                    QuestCompletionLog  = quest.QuestCompletionLog
                };

                Storage.LocalesQuests.Add(localesQuest, packet.TimeSpan);
            }

            Storage.QuestTemplates.Add(quest, packet.TimeSpan);
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

            int emotesCount = packet.ReadInt32("EmotesCount");

            // QuestDescEmote
            questOfferReward.Emote = new int?[] { 0, 0, 0, 0 };
            questOfferReward.EmoteDelay = new uint?[] { 0, 0, 0, 0 };
            for (int i = 0; i < emotesCount; i++)
            {
                questOfferReward.Emote[i] = packet.ReadInt32("Type");
                questOfferReward.EmoteDelay[i] = packet.ReadUInt32("Delay");
            }

            packet.ResetBitReader();

            packet.ReadBit("AutoLaunched");

            ReadQuestRewards(packet, "QuestRewards");

            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitGiver");
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

            if (ClientLocale.PacketLocale != LocaleConstant.enUS && questOfferReward.RewardText != string.Empty)
            {
                QuestOfferRewardLocale localesQuestOfferReward = new QuestOfferRewardLocale
                {
                    ID = (uint)id,
                    RewardText = questOfferReward.RewardText
                };

                Storage.LocalesQuestOfferRewards.Add(localesQuestOfferReward, packet.TimeSpan);
            }
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
            packet.ReadInt32("PortraitTurnIn");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("SuggestedPartyMembers");
            int learnSpellsCount = packet.ReadInt32("LearnSpellsCount");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_2_0_23826))
                ReadQuestRewards(packet);

            int descEmotesCount = packet.ReadInt32("DescEmotesCount");
            int objectivesCount = packet.ReadInt32("ObjectivesCount");
            packet.ReadInt32("QuestStartItemID");

            for (int i = 0; i < learnSpellsCount; i++)
                packet.ReadInt32("LearnSpells", i);

            questDetails.Emote = new uint?[] { 0, 0, 0, 0 };
            questDetails.EmoteDelay = new uint?[] { 0, 0, 0, 0 };
            for (int i = 0; i < descEmotesCount; i++)
            {
                questDetails.Emote[i] = (uint)packet.ReadInt32("Type", i);
                questDetails.EmoteDelay[i] = packet.ReadUInt32("Delay", i);
            }

            for (int i = 0; i < objectivesCount; i++)
            {
                packet.ReadInt32("ObjectID", i);
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
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_2_0_23826))
            {
                packet.ReadBit("CanIgnoreQuest");
                packet.ReadBit("IsQuestIgnored");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23826))
                ReadQuestRewards(packet);

            packet.ReadWoWString("QuestTitle", questTitleLen);
            packet.ReadWoWString("DescriptionText", descriptionTextLen);
            packet.ReadWoWString("LogDescription", logDescriptionLen);
            packet.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            Storage.QuestDetails.Add(questDetails, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS)]
        public static void HandleQuestGiverRequestItems(Packet packet)
        {
            var requestItems = packet.Holder.QuestGiverRequestItems = new();
            var questgiverGUID = packet.ReadPackedGuid128("QuestGiverGUID");
            requestItems.QuestGiver = questgiverGUID;
            requestItems.QuestGiverEntry = (uint)packet.ReadInt32("QuestGiverCreatureID");

            int id = packet.ReadInt32("QuestID");
            requestItems.QuestId = (uint)id;
            int delay = requestItems.EmoteDelay = packet.ReadInt32("EmoteDelay");
            int emote = requestItems.EmoteType = packet.ReadInt32("EmoteType");

            CoreParsers.QuestHandler.AddQuestEnder(questgiverGUID, (uint)id);

            for (int i = 0; i < 2; i++)
            {
                uint flags = (uint)packet.ReadInt32("QuestFlags", i);
                if (i == 0)
                    requestItems.QuestFlags = flags;
                else if (i == 1)
                    requestItems.QuestFlags2 = flags;
            }

            requestItems.SuggestedPartyMembers = packet.ReadInt32("SuggestPartyMembers");
            requestItems.MoneyToGet = packet.ReadInt32("MoneyToGet");
            int collectCount = packet.ReadInt32("CollectCount");
            int currencyCount = packet.ReadInt32("CurrencyCount");
            requestItems.CollectCount = (uint)collectCount;
            requestItems.CurrencyCount = (uint)currencyCount;
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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23826))
            {
                requestItems.CanIgnoreQuest = packet.ReadBit("CanIgnoreQuest");
                requestItems.IsQuestIgnored = packet.ReadBit("IsQuestIgnored");
            }

            packet.ResetBitReader();

            uint questTitleLen = packet.ReadBits(9);
            uint completionTextLen = packet.ReadBits(12);

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

        [Parser(Opcode.SMSG_WORLD_QUEST_UPDATE_RESPONSE)]
        public static void HandleWorldQuestUpdateResponse(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (int i = 0; i < count; i++)
            {
                packet.ReadTime("LastUpdate", i);
                packet.ReadUInt32<QuestId>("QuestID", i);
                packet.ReadUInt32("Timer", i);
                packet.ReadInt32("VariableID", i);
                packet.ReadInt32("Value", i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_WORLD_QUEST_UPDATE)]
        public static void HandleQuestZero(Packet packet) { }

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

        [Parser(Opcode.SMSG_QUEST_GIVER_INVALID_QUEST, ClientVersionBuild.V7_2_0_23826)]
        public static void HandleQuestGiverInvalidQuest(Packet packet)
        {
            packet.ReadUInt32E<QuestReasonTypeWoD>("Reason");
            packet.ReadInt32("ContributionRewardID");

            packet.ResetBitReader();

            packet.ReadBit("SendErrorMessage");

            var len = packet.ReadBits(9);
            packet.ReadWoWString("ReasonText", len);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE)]
        public static void HandlQuestGiverQuestComplete(Packet packet)
        {
            var questComplete = packet.Holder.QuestGiverQuestComplete = new();
            questComplete.QuestId = (uint) packet.ReadInt32("QuestId");
            packet.ReadInt32("XpReward");
            packet.ReadInt64("MoneyReward");
            packet.ReadInt32("SkillLineIDReward");
            packet.ReadInt32("NumSkillUpsReward");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_2_0_23706))
                Substructures.ItemHandler.ReadItemInstance(packet, "ItemReward");

            packet.ResetBitReader();

            packet.ReadBit("UseQuestReward");
            packet.ReadBit("LaunchGossip");
            packet.ReadBit("LaunchQuest");
            packet.ReadBit("HideChatMessage");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23706))
                Substructures.ItemHandler.ReadItemInstance(packet, "ItemReward");
        }

        [Parser(Opcode.SMSG_DISPLAY_PLAYER_CHOICE)]
        public static void HandleDisplayPlayerChoice(Packet packet)
        {
            var choiceId = packet.ReadInt32("ChoiceID");
            var responseCount = packet.ReadUInt32();
            packet.ReadPackedGuid128("SenderGUID");
            var uiTextureKitId = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
                uiTextureKitId = packet.ReadInt32("UiTextureKitID");
            packet.ResetBitReader();
            var questionLength = packet.ReadBits(8);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_5_24330))
                packet.ReadBit("CloseChoiceFrame");

            var hideWarboardHeader = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
                hideWarboardHeader = packet.ReadBit("HideWarboardHeader");

            for (var i = 0u; i < responseCount; ++i)
                ReadPlayerChoiceResponse(packet, choiceId, i, "PlayerChoiceResponse", i);

            var question = packet.ReadWoWString("Question", questionLength);

            Storage.PlayerChoices.Add(new PlayerChoiceTemplate
            {
                ChoiceId = choiceId,
                UiTextureKitId = uiTextureKitId,
                Question = question,
                HideWarboardHeader = hideWarboardHeader
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
            packet.ResetBitReader();
            var responseId = packet.ReadInt32("ResponseID", indexes);
            var choiceArtFileId = packet.ReadInt32("ChoiceArtFileID", indexes);
            var answerLength = packet.ReadBits(9);
            var headerLength = packet.ReadBits(9);
            var descriptionLength = packet.ReadBits(11);
            var confirmationTextLength = packet.ReadBits(7);
            var hasReward = packet.ReadBit();
            if (hasReward)
                V6_0_2_19033.Parsers.QuestHandler.ReadPlayerChoiceResponseReward(packet, choiceId, responseId, "PlayerChoiceResponseReward", indexes);

            var answer = packet.ReadWoWString("Answer", answerLength, indexes);
            var header = packet.ReadWoWString("Header", headerLength, indexes);
            var description = packet.ReadWoWString("Description", descriptionLength, indexes);
            var confirmation = packet.ReadWoWString("ConfirmationText", confirmationTextLength, indexes);

            Storage.PlayerChoiceResponses.Add(new PlayerChoiceResponseTemplate
            {
                ChoiceId = choiceId,
                ResponseId = responseId,
                Index = index,
                ChoiceArtFileId = choiceArtFileId,
                Header = header,
                Answer = answer,
                Description = description,
                Confirmation = confirmation
            }, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                Storage.PlayerChoiceResponseLocales.Add(new PlayerChoiceResponseLocaleTemplate
                {
                    ChoiceId = choiceId,
                    ResponseId = responseId,
                    Locale = ClientLocale.PacketLocaleString,
                    Header = header,
                    Answer = answer,
                    Description = description,
                    Confirmation = confirmation
                }, packet.TimeSpan);
            }
        }

        [Parser(Opcode.CMSG_QUERY_TREASURE_PICKER)]
        public static void HandleQueryQuestRewards(Packet packet)
        {
            packet.ReadInt32("QuestId");
            packet.ReadInt32("QuestTimer");
        }

        [Parser(Opcode.SMSG_TREASURE_PICKER_RESPONSE)]
        public static void HandleTreasurePickerResponse(Packet packet)
        {
            packet.ReadInt32("QuestId");
            packet.ReadInt32("QuestTimer");

            var itemCount = packet.ReadInt32("ItemCount");
            int currencyCount = packet.ReadInt32("CurrencyCount");

            packet.ReadInt64("MoneyReward");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23706))
            {
                for (int i = 0; i < currencyCount; i++)
                {
                    packet.ReadInt32("CurrencyID", i);
                    packet.ReadInt32("Amount", i);
                }

                for (var i = 0; i < itemCount; ++i)
                {
                    Substructures.ItemHandler.ReadItemInstance(packet, i);
                    packet.ReadInt32("Quantity", i);
                }
            }
            else
            {
                for (var i = 0; i < itemCount; ++i)
                {
                    packet.ReadInt32<ItemId>("ItemId");
                    packet.ReadInt32("Quantity");
                    packet.ReadByte("Context");
                }

                for (int i = 0; i < currencyCount; i++)
                {
                    packet.ReadInt32("CurrencyID", i);
                    packet.ReadInt32("Amount", i);
                }
            }
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            packet.ReadInt32("NumPOIs");
            int questPOIData = packet.ReadInt32("QuestPOIData");

            for (int i = 0; i < questPOIData; ++i)
            {
                int questId = packet.ReadInt32("QuestID", i);

                int questPOIBlobData = packet.ReadInt32("QuestPOIBlobData", i);

                for (int j = 0; j < questPOIBlobData; ++j)
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
                        WorldMapAreaId = packet.ReadInt32("WorldMapAreaID", i, j),
                        Floor = packet.ReadInt32("Floor", i, j),
                        Priority = packet.ReadInt32("Priority", i, j),
                        Flags = packet.ReadInt32("Flags", i, j),
                        WorldEffectID = packet.ReadInt32("WorldEffectID", i, j),
                        PlayerConditionID = packet.ReadInt32("PlayerConditionID", i, j),
                        SpawnTrackingID = packet.ReadInt32("SpawnTrackingID", i, j),
                    };

                    int questPOIBlobPoint = packet.ReadInt32("QuestPOIBlobPoint", i, j);
                    for (int k = 0; k < questPOIBlobPoint; ++k)
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

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
                    {
                        packet.ResetBitReader();
                        questPoi.AlwaysAllowMergingBlobs = packet.ReadBit("AlwaysAllowMergingBlobs", i, j);
                    }

                    Storage.QuestPOIs.Add(questPoi, packet.TimeSpan);

                    CoreParsers.QuestHandler.AddSpawnTrackingData(questPoi, packet.TimeSpan);
                }
            }
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_PVP_CREDIT)]
        public static void HandleQuestUpdateAddPvPCredit(Packet packet)
        {
            packet.ReadInt32<QuestId>("Quest ID");
            packet.ReadUInt16("Count");
        }

        [Parser(Opcode.SMSG_QUEST_POI_UPDATE_RESPONSE)]
        public static void HandleQuestPOIUpdateResponse(Packet packet)
        {
            var count = packet.ReadUInt32();

            for (var i = 0; i < count; i++)
            {
                var spawnTrackingId = packet.ReadInt32("SpawnTrackingID", i);
                packet.ReadInt32("ObjectID", i);

                packet.ResetBitReader();

                packet.ReadBit("Visible", i);

                SpawnTrackingTemplate spawnTrackingTemplate = new SpawnTrackingTemplate
                {
                    SpawnTrackingId = (uint)spawnTrackingId,
                    MapId = CoreParsers.MovementHandler.CurrentMapId
                };

                Storage.SpawnTrackingTemplates.Add(spawnTrackingTemplate, packet.TimeSpan);
            }
        }
    }
}
