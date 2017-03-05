using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class QuestHandler
    {
        public static void ReadQuestRewards(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("ChoiceItemCount", idx);

            for (var i = 0; i < 6; ++i)
            {
                packet.Translator.ReadInt32("ItemID", idx, i);
                packet.Translator.ReadInt32("Quantity", idx, i);
            }

            packet.Translator.ReadInt32("ItemCount", idx);

            for (var i = 0; i < 4; ++i)
            {
                packet.Translator.ReadInt32("ItemID", idx, i);
                packet.Translator.ReadInt32("ItemQty", idx, i);
            }

            packet.Translator.ReadInt32("Money", idx);
            packet.Translator.ReadInt32("XP", idx);
            packet.Translator.ReadInt32("ArtifactXP", idx);
            packet.Translator.ReadInt32("ArtifactCategoryID", idx);
            packet.Translator.ReadInt32("Honor", idx);
            packet.Translator.ReadInt32("Title", idx);
            packet.Translator.ReadInt32("FactionFlags", idx);

            for (var i = 0; i < 5; ++i)
            {
                packet.Translator.ReadInt32("FactionID", idx, i);
                packet.Translator.ReadInt32("FactionValue", idx, i);
                packet.Translator.ReadInt32("FactionOverride", idx, i);
                packet.Translator.ReadInt32("FactionCapIn", idx, i);
            }

            for (var i = 0; i < 3; ++i)
                packet.Translator.ReadInt32("SpellCompletionDisplayID", idx, i);

            packet.Translator.ReadInt32("SpellCompletionID", idx);

            for (var i = 0; i < 4; ++i)
            {
                packet.Translator.ReadInt32("CurrencyID", idx, i);
                packet.Translator.ReadInt32("CurrencyQty", idx, i);
            }

            packet.Translator.ReadInt32("SkillLineID", idx);
            packet.Translator.ReadInt32("NumSkillUps", idx);
            packet.Translator.ReadInt32("RewardID", idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("IsBoostSpell", idx);
        }

        public static void ReadGossipText(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadUInt32("QuestID", indexes);
            packet.Translator.ReadUInt32("QuestType", indexes);
            packet.Translator.ReadUInt32("QuestLevel", indexes);

            for (int i = 0; i < 2; i++)
                packet.Translator.ReadUInt32("QuestFlags", indexes, i);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Repeatable", indexes);
            packet.Translator.ReadBit("IsQuestIgnored", indexes);

            var guestTitleLen = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("QuestTitle", guestTitleLen, indexes);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            packet.Translator.ReadInt32("Entry");

            Bit hasData = packet.Translator.ReadBit("Has Data");
            if (!hasData)
                return; // nothing to do

            var id = packet.Translator.ReadEntry("Quest ID");

            QuestTemplate quest = new QuestTemplate
            {
                ID = (uint)id.Key
            };

            quest.QuestType = packet.Translator.ReadInt32E<QuestType>("QuestType");
            quest.QuestLevel = packet.Translator.ReadInt32("QuestLevel");
            quest.QuestPackageID = packet.Translator.ReadUInt32("QuestPackageID");
            quest.MinLevel = packet.Translator.ReadInt32("QuestMinLevel");
            quest.QuestSortID = (QuestSort)packet.Translator.ReadUInt32("QuestSortID");
            quest.QuestInfoID = packet.Translator.ReadInt32E<QuestInfo>("QuestInfoID");
            quest.SuggestedGroupNum = packet.Translator.ReadUInt32("SuggestedGroupNum");
            quest.RewardNextQuest = packet.Translator.ReadUInt32("RewardNextQuest");
            quest.RewardXPDifficulty = packet.Translator.ReadUInt32("RewardXPDifficulty");

            quest.RewardXPMultiplier = packet.Translator.ReadSingle("RewardXPMultiplier");

            quest.RewardMoney = packet.Translator.ReadInt32("RewardMoney");
            quest.RewardMoneyDifficulty = packet.Translator.ReadUInt32("RewardMoneyDifficulty");

            quest.RewardMoneyMultiplier = packet.Translator.ReadSingle("RewardMoneyMultiplier");

            quest.RewardBonusMoney = packet.Translator.ReadUInt32("RewardBonusMoney");

            quest.RewardDisplaySpellLegion = new uint?[3];
            for (int i = 0; i < 3; ++i)
                quest.RewardDisplaySpellLegion[i] = packet.Translator.ReadUInt32("RewardDisplaySpell", i);

            quest.RewardSpellWod = packet.Translator.ReadUInt32("RewardSpell");
            quest.RewardHonorWod = packet.Translator.ReadUInt32("RewardHonor");

            quest.RewardKillHonor = packet.Translator.ReadSingle("RewardKillHonor");

            quest.RewardArtifactXPDifficulty = packet.Translator.ReadUInt32("RewardArtifactXPDifficulty");
            quest.RewardArtifactXPMultiplier = packet.Translator.ReadSingle("RewardArtifactXPMultiplier");
            quest.RewardArtifactCategoryID = packet.Translator.ReadUInt32("RewardArtifactCategoryID");

            quest.StartItem = packet.Translator.ReadUInt32("StartItem");
            quest.Flags = packet.Translator.ReadUInt32E<QuestFlags>("Flags");
            quest.FlagsEx = packet.Translator.ReadUInt32E<QuestFlags2>("FlagsEx");

            quest.RewardItem = new uint?[4];
            quest.RewardAmount = new uint?[4];
            quest.ItemDrop = new uint?[4];
            quest.ItemDropQuantity = new uint?[4];
            for (int i = 0; i < 4; ++i)
            {
                quest.RewardItem[i] = packet.Translator.ReadUInt32("RewardItems", i);
                quest.RewardAmount[i] = packet.Translator.ReadUInt32("RewardAmount", i);
                quest.ItemDrop[i] = packet.Translator.ReadUInt32("ItemDrop", i);
                quest.ItemDropQuantity[i] = packet.Translator.ReadUInt32("ItemDropQuantity", i);
            }

            quest.RewardChoiceItemID = new uint?[6];
            quest.RewardChoiceItemQuantity = new uint?[6];
            quest.RewardChoiceItemDisplayID = new uint?[6];
            for (int i = 0; i < 6; ++i) // CliQuestInfoChoiceItem
            {
                quest.RewardChoiceItemID[i] = packet.Translator.ReadUInt32("RewardChoiceItemID", i);
                quest.RewardChoiceItemQuantity[i] = packet.Translator.ReadUInt32("RewardChoiceItemQuantity", i);
                quest.RewardChoiceItemDisplayID[i] = packet.Translator.ReadUInt32("RewardChoiceItemDisplayID", i);
            }

            quest.POIContinent = packet.Translator.ReadUInt32("POIContinent");

            quest.POIx = packet.Translator.ReadSingle("POIx");
            quest.POIy = packet.Translator.ReadSingle("POIy");

            quest.POIPriorityWod = packet.Translator.ReadInt32("POIPriority");
            quest.RewardTitle = packet.Translator.ReadUInt32("RewardTitle");
            quest.RewardArenaPoints = packet.Translator.ReadUInt32("RewardArenaPoints");
            quest.RewardSkillLineID = packet.Translator.ReadUInt32("RewardSkillLineID");
            quest.RewardNumSkillUps = packet.Translator.ReadUInt32("RewardNumSkillUps");
            quest.QuestGiverPortrait = packet.Translator.ReadUInt32("PortraitGiver");
            quest.QuestTurnInPortrait = packet.Translator.ReadUInt32("PortraitTurnIn");

            quest.RewardFactionID = new uint?[5];
            quest.RewardFactionOverride = new int?[5];
            quest.RewardFactionValue = new int?[5];
            quest.RewardFactionCapIn = new int?[5];
            for (int i = 0; i < 5; ++i)
            {
                quest.RewardFactionID[i] = packet.Translator.ReadUInt32("RewardFactionID", i);
                quest.RewardFactionValue[i] = packet.Translator.ReadInt32("RewardFactionValue", i);
                quest.RewardFactionOverride[i] = packet.Translator.ReadInt32("RewardFactionOverride", i);
                quest.RewardFactionCapIn[i] = packet.Translator.ReadInt32("RewardFactionCapIn", i);
            }

            quest.RewardFactionFlags = packet.Translator.ReadUInt32("RewardFactionFlags");

            quest.RewardCurrencyID = new uint?[4];
            quest.RewardCurrencyCount = new uint?[4];
            for (int i = 0; i < 4; ++i)
            {
                quest.RewardCurrencyID[i] = packet.Translator.ReadUInt32("RewardCurrencyID");
                quest.RewardCurrencyCount[i] = packet.Translator.ReadUInt32("RewardCurrencyQty");
            }

            quest.SoundAccept = packet.Translator.ReadUInt32("AcceptedSoundKitID");
            quest.SoundTurnIn = packet.Translator.ReadUInt32("CompleteSoundKitID");
            quest.AreaGroupID = packet.Translator.ReadUInt32("AreaGroupID");
            quest.TimeAllowed = packet.Translator.ReadUInt32("TimeAllowed");
            uint int2946 = packet.Translator.ReadUInt32("CliQuestInfoObjective");
            quest.AllowableRacesWod = packet.Translator.ReadInt32("AllowableRaces");
            quest.QuestRewardID = packet.Translator.ReadInt32("QuestRewardID");

            packet.Translator.ResetBitReader();

            uint logTitleLen = packet.Translator.ReadBits(9);
            uint logDescriptionLen = packet.Translator.ReadBits(12);
            uint questDescriptionLen = packet.Translator.ReadBits(12);
            uint areaDescriptionLen = packet.Translator.ReadBits(9);
            uint questGiverTextWindowLen = packet.Translator.ReadBits(10);
            uint questGiverTargetNameLen = packet.Translator.ReadBits(8);
            uint questTurnTextWindowLen = packet.Translator.ReadBits(10);
            uint questTurnTargetNameLen = packet.Translator.ReadBits(8);
            uint questCompletionLogLen = packet.Translator.ReadBits(11);

            for (int i = 0; i < int2946; ++i)
            {
                var objectiveId = packet.Translator.ReadEntry("Id", i);

                QuestObjective questInfoObjective = new QuestObjective
                {
                    ID = (uint)objectiveId.Key,
                    QuestID = (uint)id.Key
                };
                questInfoObjective.Type = packet.Translator.ReadByteE<QuestRequirementType>("Quest Requirement Type", i);
                questInfoObjective.StorageIndex = packet.Translator.ReadSByte("StorageIndex", i);
                questInfoObjective.ObjectID = packet.Translator.ReadInt32("ObjectID", i);
                questInfoObjective.Amount = packet.Translator.ReadInt32("Amount", i);
                questInfoObjective.Flags = packet.Translator.ReadUInt32("Flags", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_1_0_22900))
                    questInfoObjective.Flags2 = packet.Translator.ReadUInt32("Flags2", i);
                questInfoObjective.ProgressBarWeight = packet.Translator.ReadSingle("ProgressBarWeight", i);

                int visualEffectsCount = packet.Translator.ReadInt32("VisualEffects", i);
                for (int j = 0; j < visualEffectsCount; ++j)
                {
                    QuestVisualEffect questVisualEffect = new QuestVisualEffect
                    {
                        ID = questInfoObjective.ID,
                        Index = (uint) j,
                        VisualEffect = packet.Translator.ReadInt32("VisualEffectId", i, j)
                    };

                    Storage.QuestVisualEffects.Add(questVisualEffect, packet.TimeSpan);
                }

                packet.Translator.ResetBitReader();

                uint bits6 = packet.Translator.ReadBits(8);
                questInfoObjective.Description = packet.Translator.ReadWoWString("Description", bits6, i);

                if (BinaryPacketReader.GetLocale() != LocaleConstant.enUS && questInfoObjective.Description != string.Empty)
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

            quest.LogTitle = packet.Translator.ReadWoWString("LogTitle", logTitleLen);
            quest.LogDescription = packet.Translator.ReadWoWString("LogDescription", logDescriptionLen);
            quest.QuestDescription = packet.Translator.ReadWoWString("QuestDescription", questDescriptionLen);
            quest.AreaDescription = packet.Translator.ReadWoWString("AreaDescription", areaDescriptionLen);
            quest.QuestGiverTextWindow = packet.Translator.ReadWoWString("PortraitGiverText", questGiverTextWindowLen);
            quest.QuestGiverTargetName = packet.Translator.ReadWoWString("PortraitGiverName", questGiverTargetNameLen);
            quest.QuestTurnTextWindow = packet.Translator.ReadWoWString("PortraitTurnInText", questTurnTextWindowLen);
            quest.QuestTurnTargetName = packet.Translator.ReadWoWString("PortraitTurnInName", questTurnTargetNameLen);
            quest.QuestCompletionLog = packet.Translator.ReadWoWString("QuestCompletionLog", questCompletionLogLen);

            if (BinaryPacketReader.GetLocale() != LocaleConstant.enUS)
            {
                LocalesQuest localesQuest = new LocalesQuest
                {
                    ID = (uint)id.Key,
                    Locale = BinaryPacketReader.GetClientLocale(),
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
            packet.Translator.ReadPackedGuid128("QuestGiverGUID");

            packet.Translator.ReadInt32("QuestGiverCreatureID");
            int id = packet.Translator.ReadInt32("QuestID");

            QuestOfferReward questOfferReward = new QuestOfferReward
            {
                ID = (uint)id
            };

            for (int i = 0; i < 2; i++)
                packet.Translator.ReadInt32("QuestFlags", i);

            packet.Translator.ReadInt32("SuggestedPartyMembers");

            int emotesCount = packet.Translator.ReadInt32("EmotesCount");

            // QuestDescEmote
            questOfferReward.Emote = new uint?[] { 0, 0, 0, 0 };
            questOfferReward.EmoteDelay = new uint?[] { 0, 0, 0, 0 };
            for (int i = 0; i < emotesCount; i++)
            {
                questOfferReward.Emote[i] = (uint)packet.Translator.ReadInt32("Type");
                questOfferReward.EmoteDelay[i] = packet.Translator.ReadUInt32("Delay");
            }

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("AutoLaunched");

            ReadQuestRewards(packet, "QuestRewards");

            packet.Translator.ReadInt32("PortraitTurnIn");
            packet.Translator.ReadInt32("PortraitGiver");
            packet.Translator.ReadInt32("QuestPackageID");

            packet.Translator.ResetBitReader();

            uint questTitleLen = packet.Translator.ReadBits(9);
            uint rewardTextLen = packet.Translator.ReadBits(12);
            uint portraitGiverTextLen = packet.Translator.ReadBits(10);
            uint portraitGiverNameLen = packet.Translator.ReadBits(8);
            uint portraitTurnInTextLen = packet.Translator.ReadBits(10);
            uint portraitTurnInNameLen = packet.Translator.ReadBits(8);

            packet.Translator.ReadWoWString("QuestTitle", questTitleLen);
            questOfferReward.RewardText = packet.Translator.ReadWoWString("RewardText", rewardTextLen);
            packet.Translator.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.Translator.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.Translator.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.Translator.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            Storage.QuestOfferRewards.Add(questOfferReward, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS)]
        public static void HandleQuestGiverQuestDetails(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("QuestGiverGUID");
            packet.Translator.ReadPackedGuid128("InformUnit");

            int id = packet.Translator.ReadInt32("QuestID");
            QuestDetails questDetails = new QuestDetails
            {
                ID = (uint)id
            };

            packet.Translator.ReadInt32("QuestPackageID");
            packet.Translator.ReadInt32("PortraitGiver");
            packet.Translator.ReadInt32("SuggestedPartyMembers");

            for (int i = 0; i < 2; i++)
                packet.Translator.ReadInt32("QuestFlags", i);

            packet.Translator.ReadInt32("PortraitTurnIn");
            int learnSpellsCount = packet.Translator.ReadInt32("LearnSpellsCount");

            ReadQuestRewards(packet);

            int descEmotesCount = packet.Translator.ReadInt32("DescEmotesCount");
            int objectivesCount = packet.Translator.ReadInt32("ObjectivesCount");
            packet.Translator.ReadInt32("QuestStartItemID");

            for (int i = 0; i < learnSpellsCount; i++)
                packet.Translator.ReadInt32("LearnSpells", i);

            questDetails.Emote = new uint?[] { 0, 0, 0, 0 };
            questDetails.EmoteDelay = new uint?[] { 0, 0, 0, 0 };
            for (int i = 0; i < descEmotesCount; i++)
            {
                questDetails.Emote[i] = (uint)packet.Translator.ReadInt32("Type", i);
                questDetails.EmoteDelay[i] = packet.Translator.ReadUInt32("Delay", i);
            }

            for (int i = 0; i < objectivesCount; i++)
            {
                packet.Translator.ReadInt32("ObjectID", i);
                packet.Translator.ReadInt32("ObjectID", i);
                packet.Translator.ReadInt32("Amount", i);
                packet.Translator.ReadByte("Type", i);
            }

            packet.Translator.ResetBitReader();

            uint questTitleLen = packet.Translator.ReadBits(9);
            uint descriptionTextLen = packet.Translator.ReadBits(12);
            uint logDescriptionLen = packet.Translator.ReadBits(12);
            uint portraitGiverTextLen = packet.Translator.ReadBits(10);
            uint portraitGiverNameLen = packet.Translator.ReadBits(8);
            uint portraitTurnInTextLen = packet.Translator.ReadBits(10);
            uint portraitTurnInNameLen = packet.Translator.ReadBits(8);

            packet.Translator.ReadBit("DisplayPopup");
            packet.Translator.ReadBit("StartCheat");
            packet.Translator.ReadBit("AutoLaunched");
            packet.Translator.ReadBit("CanIgnoreQuest");
            packet.Translator.ReadBit("IsQuestIgnored");

            packet.Translator.ReadWoWString("QuestTitle", questTitleLen);
            packet.Translator.ReadWoWString("DescriptionText", descriptionTextLen);
            packet.Translator.ReadWoWString("LogDescription", logDescriptionLen);
            packet.Translator.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.Translator.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.Translator.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.Translator.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            Storage.QuestDetails.Add(questDetails, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS)]
        public static void HandleQuestGiverRequestItems(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("QuestGiverGUID");
            packet.Translator.ReadInt32("QuestGiverCreatureID");

            int id = packet.Translator.ReadInt32("QuestID");
            QuestRequestItems questRequestItems = new QuestRequestItems
            {
                ID = (uint)id
            };

            questRequestItems.EmoteOnCompleteDelay = (uint)packet.Translator.ReadInt32("CompEmoteDelay");
            questRequestItems.EmoteOnComplete = (uint)packet.Translator.ReadInt32("CompEmoteType");

            for (int i = 0; i < 2; i++)
                packet.Translator.ReadInt32("QuestFlags", i);

            packet.Translator.ReadInt32("SuggestPartyMembers");
            packet.Translator.ReadInt32("MoneyToGet");
            int collectCount = packet.Translator.ReadInt32("CollectCount");
            int currencyCount = packet.Translator.ReadInt32("CurrencyCount");
            packet.Translator.ReadInt32("StatusFlags");

            for (int i = 0; i < collectCount; i++)
            {
                packet.Translator.ReadInt32("ObjectID", i);
                packet.Translator.ReadInt32("Amount", i);
                packet.Translator.ReadUInt32("Flags", i);
            }

            for (int i = 0; i < currencyCount; i++)
            {
                packet.Translator.ReadInt32("CurrencyID", i);
                packet.Translator.ReadInt32("Amount", i);
            }

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("AutoLaunched");
            packet.Translator.ReadBit("CanIgnoreQuest");
            packet.Translator.ReadBit("IsQuestIgnored");

            packet.Translator.ResetBitReader();

            uint questTitleLen = packet.Translator.ReadBits(9);
            uint completionTextLen = packet.Translator.ReadBits(12);

            packet.Translator.ReadWoWString("QuestTitle", questTitleLen);
            questRequestItems.CompletionText = packet.Translator.ReadWoWString("CompletionText", completionTextLen);

            Storage.QuestRequestItems.Add(questRequestItems, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_WORLD_QUEST_UPDATE)]
        public static void HandleWorldQuestUpdate(Packet packet)
        {
            var count = packet.Translator.ReadInt32("Count");

            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadTime("LastUpdate", i);
                packet.Translator.ReadUInt32<QuestId>("QuestID", i);
                packet.Translator.ReadUInt32("Timer", i);
                packet.Translator.ReadInt32("VariableID", i);
                packet.Translator.ReadInt32("Value", i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_WORLD_QUEST_UPDATE)]
        public static void HandleQuestZero(Packet packet) { }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_LIST_MESSAGE)]
        public static void HandleQuestgiverQuestList(Packet packet)
        {
            WowGuid guid = packet.Translator.ReadPackedGuid128("QuestGiverGUID");

            QuestGreeting questGreeting = new QuestGreeting
            {
                ID = guid.GetEntry(),
                GreetEmoteDelay = packet.Translator.ReadUInt32("GreetEmoteDelay"),
                GreetEmoteType = packet.Translator.ReadUInt32("GreetEmoteType")
            };

            uint gossipTextCount = packet.Translator.ReadUInt32("GossipTextCount");
            packet.Translator.ResetBitReader();
            uint greetingLen = packet.Translator.ReadBits(11);

            for (int i = 0; i < gossipTextCount; i++)
                ReadGossipText(packet, i);

            questGreeting.Greeting = packet.Translator.ReadWoWString("Greeting", greetingLen);

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
        }
    }
}
