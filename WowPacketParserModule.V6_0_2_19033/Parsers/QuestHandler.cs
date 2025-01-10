using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class QuestHandler
    {
        private static void ReadQuestRewards(Packet packet)
        {
            packet.ReadInt32("ChoiceItemCount");

            for (var i = 0; i < 6; ++i)
            {
                packet.ReadInt32<ItemId>("ItemID", i);
                packet.ReadInt32("Quantity", i);
            }

            packet.ReadInt32("ItemCount");

            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32<ItemId>("ItemID", i);
                packet.ReadInt32("ItemQty", i);
            }

            packet.ReadInt32("Money");
            packet.ReadInt32("Xp");
            packet.ReadInt32("Title");
            packet.ReadInt32("Talents");
            packet.ReadInt32("FactionFlags");

            for (var i = 0; i < 5; ++i)
            {
                packet.ReadInt32("FactionID", i);
                packet.ReadInt32("FactionValue", i);
                packet.ReadInt32("FactionOverride", i);
            }

            packet.ReadInt32("SpellCompletionDisplayID");
            packet.ReadInt32("SpellCompletionID");

            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("CurrencyID", i);
                packet.ReadInt32("CurrencyQty", i);
            }

            packet.ReadInt32("SkillLineID");
            packet.ReadInt32("FactiNumSkillUpsonFlags");

            packet.ResetBitReader();

            packet.ReadBit("bit44");
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_INFO)]
        public static void HandleQuestQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_QUERY_QUEST)]
        public static void HandleQuestGiverQueryQuest(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadUInt32("QuestID");
            packet.ReadBit("RespondToGiver");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_COMPLETE_QUEST)]
        public static void HandleQuestGiverCompleteQuest(Packet packet)
        {
            var questGiverCompleteQuest = packet.Holder.QuestGiverCompleteQuestRequest = new();
            questGiverCompleteQuest.QuestGiver = packet.ReadPackedGuid128("QuestGiverGUID");
            questGiverCompleteQuest.QuestId = packet.ReadUInt32("QuestID");
            packet.ReadBit("FromScript");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD)]
        public static void HandleQuestChooseReward(Packet packet)
        {
            var chooseReward = packet.Holder.ClientQuestGiverChooseReward = new();
            chooseReward.QuestGiver = packet.ReadPackedGuid128("QuestGiverGUID");
            chooseReward.QuestId = (uint)packet.ReadInt32("QuestID");
            chooseReward.Item = packet.ReadUInt32("ItemChoiceID");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_ACCEPT_QUEST)]
        public static void HandleQuestgiverAcceptQuest(Packet packet)
        {
            var questGiverAcceptQuest = packet.Holder.QuestGiverAcceptQuest = new();
            questGiverAcceptQuest.QuestGiver = packet.ReadPackedGuid128("QuestGiverGUID");
            questGiverAcceptQuest.QuestId = packet.ReadUInt32<QuestId>("QuestID");
            packet.ReadBit("StartCheat");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_REQUEST_REWARD)]
        public static void HandleQuestRequestReward(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadUInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPOIQuery(Packet packet)
        {
            packet.ReadUInt32("MissingQuestCount");

            for (var i = 0; i < 50; i++)
                packet.ReadInt32<QuestId>("MissingQuestPOIs", i);
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            packet.ReadInt32("NumPOIs");
            int int4 = packet.ReadInt32("QuestPOIData");

            for (int i = 0; i < int4; ++i)
            {
                int questId = packet.ReadInt32("QuestID", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173))
                    packet.ReadUInt32("NumBlobs", i);

                int int2 = packet.ReadInt32("QuestPOIBlobData", i);

                for (int j = 0; j < int2; ++j)
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
                        PlayerConditionID = packet.ReadInt32("PlayerConditionID", i, j)
                    };

                    if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173))
                        packet.ReadInt32("NumPoints", i, j);

                    questPoi.SpawnTrackingID = packet.ReadInt32("SpawnTrackingID", i, j);

                    int int13 = packet.ReadInt32("QuestPOIBlobPoint", i, j);
                    for (int k = 0; k < int13; ++k)
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

                    Storage.QuestPOIs.Add(questPoi, packet.TimeSpan);

                    CoreParsers.QuestHandler.AddSpawnTrackingData(questPoi, packet.TimeSpan);
                }
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            packet.ReadUInt32("QuestID");

            Bit hasData = packet.ReadBit("Allow");
            if (!hasData)
                return; // nothing to do

            var id = packet.ReadEntry("Quest ID");

            QuestTemplate quest = new QuestTemplate
            {
                ID = (uint)id.Key
            };

            quest.QuestType = packet.ReadInt32E<QuestType>("QuestType");
            quest.QuestLevel = packet.ReadInt32("QuestLevel");
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
            quest.RewardDisplaySpell = packet.ReadUInt32("RewardDisplaySpell");
            quest.RewardSpellWod = packet.ReadUInt32("RewardSpell");
            quest.RewardHonorWod = packet.ReadUInt32("RewardHonor");

            quest.RewardKillHonor = packet.ReadSingle("RewardKillHonor");

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
            quest.RewardTalents = packet.ReadUInt32("RewardTalents");
            quest.RewardArenaPoints = packet.ReadUInt32("RewardArenaPoints");
            quest.RewardSkillLineID = packet.ReadUInt32("RewardSkillLineID");
            quest.RewardNumSkillUps = packet.ReadUInt32("RewardNumSkillUps");
            quest.QuestGiverPortrait = packet.ReadUInt32("PortraitGiver");
            quest.QuestTurnInPortrait = packet.ReadUInt32("PortraitTurnIn");

            quest.RewardFactionID = new uint?[5];
            quest.RewardFactionValue = new int?[5];
            quest.RewardFactionOverride = new int?[5];
            for (int i = 0; i < 5; ++i)
            {
                quest.RewardFactionID[i] = packet.ReadUInt32("RewardFactionID", i);
                quest.RewardFactionValue[i] = packet.ReadInt32("RewardFactionValue", i);
                quest.RewardFactionOverride[i] = packet.ReadInt32("RewardFactionOverride", i);
            }

            quest.RewardFactionFlags = packet.ReadUInt32("RewardFactionFlags");

            quest.RewardCurrencyID = new uint?[4];
            quest.RewardCurrencyCount = new uint?[4];
            for (int i = 0; i < 4; ++i)
            {
                quest.RewardCurrencyID[i] = packet.ReadUInt32("RewardCurrencyID");
                quest.RewardCurrencyCount[i] = packet.ReadUInt32("RewardCurrencyQty");
            }

            quest.SoundAccept = packet.ReadUInt32<SoundId>("AcceptedSoundKitID");
            quest.SoundTurnIn = packet.ReadUInt32<SoundId>("CompleteSoundKitID");
            quest.AreaGroupID = packet.ReadUInt32("AreaGroupID");
            quest.TimeAllowed = packet.ReadInt32("TimeAllowed");
            uint int2946 = packet.ReadUInt32("CliQuestInfoObjective");
            quest.AllowableRacesWod = (uint)packet.ReadInt32("AllowableRaces");

            for (int i = 0; i < int2946; ++i)
            {
                var objectiveId = packet.ReadEntry("Id", i);

                QuestObjective questInfoObjective = new QuestObjective
                {
                    ID = (uint)objectiveId.Key,
                    QuestID = (uint)id.Key
                };
                questInfoObjective.Type = packet.ReadByteE<QuestRequirementType>("Quest Requirement Type", i);
                questInfoObjective.StorageIndex = packet.ReadSByte("StorageIndex", i);
                questInfoObjective.ObjectID = packet.ReadInt32("ObjectID", i);
                questInfoObjective.Amount = packet.ReadInt32("Amount", i);
                questInfoObjective.Flags = packet.ReadUInt32("Flags", i);
                questInfoObjective.ProgressBarWeight = packet.ReadSingle("ProgressBarWeight", i);

                int int280 = packet.ReadInt32("VisualEffects", i);
                for (int j = 0; j < int280; ++j)
                {
                    QuestVisualEffect questVisualEffect = new QuestVisualEffect
                    {
                        ID = questInfoObjective.ID,
                        Index = (uint) j,
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

            packet.ResetBitReader();

            uint bits26 = packet.ReadBits(9);
            uint bits154 = packet.ReadBits(12);
            uint bits904 = packet.ReadBits(12);
            uint bits1654 = packet.ReadBits(9);
            uint bits1789 = packet.ReadBits(10);
            uint bits2045 = packet.ReadBits(8);
            uint bits2109 = packet.ReadBits(10);
            uint bits2365 = packet.ReadBits(8);
            uint bits2429 = packet.ReadBits(11);

            quest.LogTitle = packet.ReadWoWString("LogTitle", bits26);
            quest.LogDescription = packet.ReadWoWString("LogDescription", bits154);
            quest.QuestDescription = packet.ReadWoWString("QuestDescription", bits904);
            quest.AreaDescription = packet.ReadWoWString("AreaDescription", bits1654);
            quest.QuestGiverTextWindow = packet.ReadWoWString("PortraitGiverText", bits1789);
            quest.QuestGiverTargetName = packet.ReadWoWString("PortraitGiverName", bits2045);
            quest.QuestTurnTextWindow = packet.ReadWoWString("PortraitTurnInText", bits2109);
            quest.QuestTurnTargetName = packet.ReadWoWString("PortraitTurnInName", bits2365);
            quest.QuestCompletionLog = packet.ReadWoWString("QuestCompletionLog", bits2429);

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

        [Parser(Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS)]
        public static void HandleQueryQuestCompletionNpcs(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
                packet.ReadInt32<QuestId>("QuestID", i);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS)]
        public static void HandleQuestgiverDetails(Packet packet)
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
            packet.ReadInt32("SuggestedPartyMembers");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("PortraitTurnIn");
            int int5860 = packet.ReadInt32("LearnSpellsCount");

            ReadQuestRewards(packet);

            int int2584 = packet.ReadInt32("DescEmotesCount");
            int int5876 = packet.ReadInt32("ObjectivesCount");

            for (int i = 0; i < int5860; i++)
                packet.ReadInt32("LearnSpells", i);

            questDetails.Emote = new uint?[] {0, 0, 0, 0};
            questDetails.EmoteDelay = new uint?[] {0, 0, 0, 0};
            for (int i = 0; i < int2584; i++)
            {
                questDetails.Emote[i] = (uint)packet.ReadInt32("Type", i);
                questDetails.EmoteDelay[i] = packet.ReadUInt32("Delay", i);
            }

            for (int i = 0; i < int5876; i++)
            {
                packet.ReadInt32("ObjectID", i);
                packet.ReadInt32("ObjectID", i);
                packet.ReadInt32("Amount", i);
                packet.ReadByte("Type", i);
            }

            packet.ResetBitReader();

            uint bits516 = packet.ReadBits(9);
            uint bits1606 = packet.ReadBits(12);
            uint bits715 = packet.ReadBits(12);
            uint bits260 = packet.ReadBits(10);
            uint bits650 = packet.ReadBits(8);
            uint bits4 = packet.ReadBits(10);
            uint bits1532 = packet.ReadBits(8);

            packet.ReadBit("DisplayPopup");
            packet.ReadBit("StartCheat");
            packet.ReadBit("AutoLaunched");

            packet.ReadWoWString("QuestTitle", bits516);
            packet.ReadWoWString("DescriptionText", bits1606);
            packet.ReadWoWString("LogDescription", bits715);
            packet.ReadWoWString("PortraitGiverText", bits260);
            packet.ReadWoWString("PortraitGiverName", bits650);
            packet.ReadWoWString("PortraitTurnInText", bits4);
            packet.ReadWoWString("PortraitTurnInName", bits1532);

            Storage.QuestDetails.Add(questDetails, packet.TimeSpan);
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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_5_50232))
                packet.ReadUInt64E<QuestGiverStatus4x>("Status");
            else
                packet.ReadUInt32E<QuestGiverStatus4x>("Status");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatusMultiple(Packet packet)
        {
            var int16 = packet.ReadInt32("QuestGiverStatusCount");
            for (var i = 0; i < int16; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_5_50232))
                    packet.ReadUInt64E<QuestGiverStatus4x>("Status", i);
                else
                    packet.ReadUInt32E<QuestGiverStatus4x>("Status", i);
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

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE)]
        public static void HandleQuestCompleted(Packet packet)
        {
            var questComplete = packet.Holder.QuestGiverQuestComplete = new();
            questComplete.QuestId = packet.ReadUInt32("QuestId");
            packet.ReadUInt32("SkillLineIDReward");
            packet.ReadUInt32("MoneyReward");
            packet.ReadUInt32("NumSkillUpsReward");
            packet.ReadUInt32("XpReward");
            packet.ReadUInt32("TalentReward");

            Substructures.ItemHandler.ReadItemInstance(packet);

            packet.ResetBitReader();

            packet.ReadBit("UseQuestReward");
            packet.ReadBit("LaunchGossip");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE)]
        public static void HandleQuestOfferReward(Packet packet)
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

            ReadQuestRewards(packet);

            int int252 = packet.ReadInt32("EmotesCount");

            // QuestDescEmote
            questOfferReward.Emote = new int?[] {0, 0, 0, 0};
            questOfferReward.EmoteDelay = new uint?[] {0, 0, 0, 0};
            for (int i = 0; i < int252; i++)
            {
                questOfferReward.Emote[i] = packet.ReadInt32("Type");
                questOfferReward.EmoteDelay[i] = packet.ReadUInt32("Delay");
            }

            packet.ResetBitReader();

            packet.ReadBit("AutoLaunched");

            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitTurnIn");
            packet.ReadInt32("PortraitGiver");

            packet.ResetBitReader();

            uint bits1139 = packet.ReadBits(9);
            uint bits69 = packet.ReadBits(12);
            uint bits883 = packet.ReadBits(10);
            uint bits819 = packet.ReadBits(8);
            uint bits1268 = packet.ReadBits(10);
            uint bits4 = packet.ReadBits(8);

            packet.ReadWoWString("QuestTitle", bits1139);
            questOfferReward.RewardText = packet.ReadWoWString("RewardText", bits69);
            packet.ReadWoWString("PortraitGiverText", bits883);
            packet.ReadWoWString("PortraitGiverName", bits819);
            packet.ReadWoWString("PortraitTurnInText", bits1268);
            packet.ReadWoWString("PortraitTurnInName", bits4);

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

        [Parser(Opcode.SMSG_CLEAR_QUEST_COMPLETED_BITS)]
        public static void HandleClearQuestCompletedBits(Packet packet)
        {
            var int4 = packet.ReadUInt32("Count");
            for (int i = 0; i < int4; i++)
                packet.ReadInt32("Qbits", i);
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
            for (int i = 0; i < questsCount; i++)
                NpcHandler.ReadGossipQuestTextData(packet, i, "GossipQuests");

            packet.ResetBitReader();

            uint bits16 = packet.ReadBits(11);
            questGreeting.Greeting = packet.ReadWoWString("Greeting", bits16);

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
        public static void HandleQuestRequestItems(Packet packet)
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
                var flags = packet.ReadInt32("QuestFlags", i);
                if (i == 0)
                    requestItems.QuestFlags = (uint)flags;
                else if (i == 1)
                    requestItems.QuestFlags2 = (uint)flags;
            }

            requestItems.SuggestedPartyMembers = packet.ReadInt32("SuggestPartyMembers");
            requestItems.MoneyToGet = packet.ReadInt32("MoneyToGet");
            int collectCount = packet.ReadInt32("QuestObjectiveCollectCount");
            int currencyCount = packet.ReadInt32("QuestCurrencyCount");
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
                requestItems.Collect.Add(new QuestCollect()
                {
                    Id = objectId,
                    Count = amount
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

            uint bits3016 = packet.ReadBits(9);
            uint bits16 = packet.ReadBits(12);

            requestItems.QuestTitle = packet.ReadWoWString("QuestTitle", bits3016);
            string completionText = requestItems.CompletionText = packet.ReadWoWString("CompletionText", bits16);

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

        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE)]
        public static void HandleQuestUpdateComplete(Packet packet)
        {
            var questComplete = packet.Holder.QuestComplete = new();
            questComplete.QuestId = (uint)packet.ReadInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.CMSG_QUEST_CLOSE_AUTOACCEPT_QUEST)]
        public static void HandleQuestCloseAutoAcceptQuest(Packet packet)
        {
            packet.ReadInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_CREDIT_SIMPLE)]
        public static void HandleQuestUpdateAddCreditSimple(Packet packet)
        {
            packet.ReadInt32<QuestId>("QuestID");
            packet.ReadInt32("ObjectID");
            packet.ReadByte("ObjectiveType");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_HELLO)]
        public static void HandleQuestGiverHello(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiver GUID");
        }

        [Parser(Opcode.CMSG_QUEST_PUSH_RESULT)]
        public static void HandleCliQuestPushResult(Packet packet)
        {
            packet.ReadPackedGuid128("SenderGUID");
            packet.ReadInt32("QuestID");
            packet.ReadByteE<QuestPartyResult>("Result");
        }

        [Parser(Opcode.SMSG_QUEST_PUSH_RESULT)]
        public static void HandleQuestPushResult(Packet packet)
        {
            packet.ReadPackedGuid128("SenderGUID");
            packet.ReadByteE<QuestPartyResult>("Result");
        }

        [Parser(Opcode.SMSG_SET_QUEST_COMPLETED_BIT)]
        [Parser(Opcode.SMSG_CLEAR_QUEST_COMPLETED_BIT)]
        public static void HandleSetQuestCompletedBit(Packet packet)
        {
            packet.ReadInt32("Bit");
            packet.ReadInt32("QuestID");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_INVALID_QUEST)]
        public static void HandleQuestGiverInvalidQuest(Packet packet)
        {
            packet.ReadUInt32E<QuestReasonTypeWoD>("Reason");
            packet.ReadBit("SendErrorMessage");

            var len = packet.ReadBits(9);
            packet.ReadWoWString("ReasonText", len);
        }

        [Parser(Opcode.CMSG_CHOICE_RESPONSE)]
        public static void HandleChoiceResponse(Packet packet)
        {
            packet.ReadInt32("ChoiceID");
            packet.ReadInt32("ResponseID");
        }

        [Parser(Opcode.SMSG_DISPLAY_PLAYER_CHOICE)]
        public static void HandleDisplayPlayerChoice(Packet packet)
        {
            var choiceId = packet.ReadInt32("ChoiceID");
            var responseCount = packet.ReadUInt32();
            packet.ReadPackedGuid128("NpcGUID");
            for (var i = 0u; i < responseCount; ++i)
                ReadPlayerChoiceResponse(packet, choiceId, i, "PlayerChoiceResponse", i);

            packet.ResetBitReader();

            var questionLength = packet.ReadBits(8);
            var question = packet.ReadWoWString("Question", questionLength);

            Storage.PlayerChoices.Add(new PlayerChoiceTemplate
            {
                ChoiceId = choiceId,
                Question = question
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
            var descriptionLength = packet.ReadBits(11);
            var hasReward = packet.ReadBit();

            var answer = packet.ReadWoWString("Answer", answerLength, indexes);
            var description = packet.ReadWoWString("Description", descriptionLength, indexes);

            if (hasReward)
                ReadPlayerChoiceResponseReward(packet, choiceId, responseId, "PlayerChoiceResponseReward", indexes);

            Storage.PlayerChoiceResponses.Add(new PlayerChoiceResponseTemplate
            {
                ChoiceId = choiceId,
                ResponseId = responseId,
                Index = index,
                ChoiceArtFileId = choiceArtFileId,
                Answer = answer,
                Description = description
            }, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                Storage.PlayerChoiceResponseLocales.Add(new PlayerChoiceResponseLocaleTemplate
                {
                    ChoiceId = choiceId,
                    ResponseId = responseId,
                    Locale = ClientLocale.PacketLocaleString,
                    Answer = answer,
                    Description = description
                }, packet.TimeSpan);
            }
        }

        public static void ReadPlayerChoiceResponseReward(Packet packet, int choiceId, int responseId, params object[] indexes)
        {
            packet.ResetBitReader();
            var titleID = packet.ReadInt32("TitleID", indexes);
            var packageID = packet.ReadInt32("PackageID", indexes);
            var skillLineID = packet.ReadInt32("SkillLineID", indexes);
            var skillPointCount = packet.ReadUInt32("SkillPointCount", indexes);
            var arenaPointCount = packet.ReadUInt32("ArenaPointCount", indexes);
            var honorPointCount = packet.ReadUInt32("HonorPointCount", indexes);
            var money = packet.ReadUInt64("Money", indexes);
            var xp = packet.ReadUInt32("Xp", indexes);

            Storage.PlayerChoiceResponseRewards.Add(new PlayerChoiceResponseRewardTemplate
            {
                ChoiceId = choiceId,
                ResponseId = responseId,
                TitleId = titleID,
                PackageId = packageID,
                SkillLineId = skillLineID,
                SkillPointCount = skillPointCount,
                ArenaPointCount = arenaPointCount,
                HonorPointCount = honorPointCount,
                Money = money,
                Xp = xp
            }, packet.TimeSpan);

            var itemCount = packet.ReadUInt32();
            var currencyCount = packet.ReadUInt32();
            var factionCount = packet.ReadUInt32();
            var itemChoiceCount = packet.ReadUInt32();

            for (var i = 0u; i < itemCount; ++i)
                ReadPlayerChoiceResponseRewardItem(packet, choiceId, responseId, i, "Item", i);

            for (var i = 0u; i < currencyCount; ++i)
                ReadPlayerChoiceResponseRewardCurrency(packet, choiceId, responseId, i, "Currency", i);

            for (var i = 0u; i < factionCount; ++i)
                ReadPlayerChoiceResponseRewardFactions(packet, choiceId, responseId, i, "Faction", i);

            for (var i = 0u; i < itemChoiceCount; ++i)
                ReadPlayerChoiceResponseRewardItemChoice(packet, choiceId, responseId, i, "ItemChoice", i);
        }

        public static void ReadPlayerChoiceResponseRewardItemChoice(Packet packet, int choiceId, int responseId, uint index, params object[] indexes)
        {
            ItemInstance instance = Substructures.ItemHandler.ReadItemInstance(packet, indexes);
            var quantity = packet.ReadInt32("Quantity", indexes);

            // these fields are unused in client, last checked in 8.2.5
        }

        public static void ReadPlayerChoiceResponseRewardItem(Packet packet, int choiceId, int responseId, uint index, params object[] indexes)
        {
            ItemInstance instance = Substructures.ItemHandler.ReadItemInstance(packet, indexes);
            var quantity = packet.ReadInt32("Quantity", indexes);

            string bonusListIds = "";
            if (instance.BonusListIDs != null)
            {
                for (var i = 0; i < instance.BonusListIDs.Length; i++)
                    bonusListIds += instance.BonusListIDs[i] + " ";
                bonusListIds = bonusListIds.TrimEnd(' ');
            }

            Storage.PlayerChoiceResponseRewardItems.Add(new PlayerChoiceResponseRewardItemTemplate
            {
                ChoiceId = choiceId,
                ResponseId = responseId,
                Index = index,
                ItemId = instance.ItemID,
                BonusListIDs = bonusListIds,
                Quantity = quantity
            }, packet.TimeSpan);
        }

        public static void ReadPlayerChoiceResponseRewardCurrency(Packet packet, int choiceId, int responseId, uint index, params object[] indexes)
        {
            ItemInstance instance = Substructures.ItemHandler.ReadItemInstance(packet, indexes);
            var quantity = packet.ReadInt32("Quantity", indexes);

            Storage.PlayerChoiceResponseRewardCurrencies.Add(new PlayerChoiceResponseRewardCurrencyTemplate
            {
                ChoiceId = choiceId,
                ResponseId = responseId,
                Index = index,
                CurrencyId = instance.ItemID,
                Quantity = quantity
            }, packet.TimeSpan);
        }

        public static void ReadPlayerChoiceResponseRewardFactions(Packet packet, int choiceId, int responseId, uint index, params object[] indexes)
        {
            ItemInstance instance = Substructures.ItemHandler.ReadItemInstance(packet, indexes);
            var quantity = packet.ReadInt32("Quantity", indexes);

            Storage.PlayerChoiceResponseRewardFactions.Add(new PlayerChoiceResponseRewardFactionTemplate
            {
                ChoiceId = choiceId,
                ResponseId = responseId,
                Index = index,
                FactionId = instance.ItemID,
                Quantity = quantity
            }, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_DAILY_QUESTS_RESET)]
        public static void HandleDailyQuestsReset(Packet packet)
        {
            packet.ReadInt32("Count");
        }

        [Parser(Opcode.SMSG_QUEST_CONFIRM_ACCEPT)]
        public static void HandleQuestConfirmAccept(Packet packet)
        {
            packet.ReadInt32("QuestID");
            packet.ReadPackedGuid128("InitiatedBy");
            var len = packet.ReadBits(10);
            packet.ReadWoWString("QuestTitle", len);
        }
    }
}
