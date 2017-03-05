using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class QuestHandler
    {
        private static void ReadQuestRewards(Packet packet)
        {
            packet.Translator.ReadInt32("ChoiceItemCount");

            for (var i = 0; i < 6; ++i)
            {
                packet.Translator.ReadInt32("ItemID", i);
                packet.Translator.ReadInt32("Quantity", i);
            }

            packet.Translator.ReadInt32("ItemCount");

            for (var i = 0; i < 4; ++i)
            {
                packet.Translator.ReadInt32("ItemID", i);
                packet.Translator.ReadInt32("ItemQty", i);
            }

            packet.Translator.ReadInt32("Money");
            packet.Translator.ReadInt32("Xp");
            packet.Translator.ReadInt32("Title");
            packet.Translator.ReadInt32("Talents");
            packet.Translator.ReadInt32("FactionFlags");

            for (var i = 0; i < 5; ++i)
            {
                packet.Translator.ReadInt32("FactionID", i);
                packet.Translator.ReadInt32("FactionValue", i);
                packet.Translator.ReadInt32("FactionOverride", i);
            }

            packet.Translator.ReadInt32("SpellCompletionDisplayID");
            packet.Translator.ReadInt32("SpellCompletionID");

            for (var i = 0; i < 4; ++i)
            {
                packet.Translator.ReadInt32("CurrencyID", i);
                packet.Translator.ReadInt32("CurrencyQty", i);
            }

            packet.Translator.ReadInt32("SkillLineID");
            packet.Translator.ReadInt32("FactiNumSkillUpsonFlags");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("bit44");
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

            var bits13 = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("QuestTitle", bits13, indexes);
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_INFO)]
        public static void HandleQuestQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Entry");
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_QUERY_QUEST)]
        public static void HandleQuestGiverQueryQuest(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("QuestGiverGUID");
            packet.Translator.ReadUInt32("QuestID");
            packet.Translator.ReadBit("RespondToGiver");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_COMPLETE_QUEST)]
        public static void HandleQuestGiverCompleteQuest(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("QuestGiverGUID");
            packet.Translator.ReadUInt32("QuestID");
            packet.Translator.ReadBit("FromScript");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD)]
        public static void HandleQuestChooseReward(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("QuestGiverGUID");
            packet.Translator.ReadInt32("QuestID");
            packet.Translator.ReadInt32("ItemChoiceID");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_ACCEPT_QUEST)]
        public static void HandleQuestgiverAcceptQuest(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("QuestGiverGUID");
            packet.Translator.ReadUInt32<QuestId>("QuestID");
            packet.Translator.ReadBit("StartCheat");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_REQUEST_REWARD)]
        public static void HandleQuestRequestReward(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("QuestGiverGUID");
            packet.Translator.ReadUInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPOIQuery(Packet packet)
        {
            packet.Translator.ReadUInt32("MissingQuestCount");

            for (var i = 0; i < 50; i++)
                packet.Translator.ReadInt32<QuestId>("MissingQuestPOIs", i);
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            packet.Translator.ReadInt32("NumPOIs");
            int int4 = packet.Translator.ReadInt32("QuestPOIData");

            for (int i = 0; i < int4; ++i)
            {
                int questId = packet.Translator.ReadInt32("QuestID", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173))
                    packet.Translator.ReadUInt32("NumBlobs", i);

                int int2 = packet.Translator.ReadInt32("QuestPOIBlobData", i);

                for (int j = 0; j < int2; ++j)
                {
                    QuestPOI questPoi = new QuestPOI
                    {
                        QuestID = questId,
                        ID = j,
                        BlobIndex = packet.Translator.ReadInt32("BlobIndex", i, j),
                        ObjectiveIndex = packet.Translator.ReadInt32("ObjectiveIndex", i, j),
                        QuestObjectiveID = packet.Translator.ReadInt32("QuestObjectiveID", i, j),
                        QuestObjectID = packet.Translator.ReadInt32("QuestObjectID", i, j),
                        MapID = packet.Translator.ReadInt32("MapID", i, j),
                        WorldMapAreaId = packet.Translator.ReadInt32("WorldMapAreaID", i, j),
                        Floor = packet.Translator.ReadInt32("Floor", i, j),
                        Priority = packet.Translator.ReadInt32("Priority", i, j),
                        Flags = packet.Translator.ReadInt32("Flags", i, j),
                        WorldEffectID = packet.Translator.ReadInt32("WorldEffectID", i, j),
                        PlayerConditionID = packet.Translator.ReadInt32("PlayerConditionID", i, j)
                    };

                    if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173))
                        packet.Translator.ReadInt32("NumPoints", i, j);

                    questPoi.WoDUnk1 = packet.Translator.ReadInt32("WoDUnk1", i, j);

                    int int13 = packet.Translator.ReadInt32("QuestPOIBlobPoint", i, j);
                    for (int k = 0; k < int13; ++k)
                    {
                        QuestPOIPoint questPoiPoint = new QuestPOIPoint
                        {
                            QuestID = questId,
                            Idx1 = j,
                            Idx2 = k,
                            X = packet.Translator.ReadInt32("X", i, j, k),
                            Y = packet.Translator.ReadInt32("Y", i, j, k)
                        };
                        Storage.QuestPOIPoints.Add(questPoiPoint, packet.TimeSpan);
                    }

                    Storage.QuestPOIs.Add(questPoi, packet.TimeSpan);
                }
            }
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
            quest.RewardDisplaySpell = packet.Translator.ReadUInt32("RewardDisplaySpell");
            quest.RewardSpellWod = packet.Translator.ReadUInt32("RewardSpell");
            quest.RewardHonorWod = packet.Translator.ReadUInt32("RewardHonor");

            quest.RewardKillHonor = packet.Translator.ReadSingle("RewardKillHonor");

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
            quest.RewardTalents = packet.Translator.ReadUInt32("RewardTalents");
            quest.RewardArenaPoints = packet.Translator.ReadUInt32("RewardArenaPoints");
            quest.RewardSkillLineID = packet.Translator.ReadUInt32("RewardSkillLineID");
            quest.RewardNumSkillUps = packet.Translator.ReadUInt32("RewardNumSkillUps");
            quest.QuestGiverPortrait = packet.Translator.ReadUInt32("PortraitGiver");
            quest.QuestTurnInPortrait = packet.Translator.ReadUInt32("PortraitTurnIn");

            quest.RewardFactionID = new uint?[5];
            quest.RewardFactionValue = new int?[5];
            quest.RewardFactionOverride = new int?[5];
            for (int i = 0; i < 5; ++i)
            {
                quest.RewardFactionID[i] = packet.Translator.ReadUInt32("RewardFactionID", i);
                quest.RewardFactionValue[i] = packet.Translator.ReadInt32("RewardFactionValue", i);
                quest.RewardFactionOverride[i] = packet.Translator.ReadInt32("RewardFactionOverride", i);
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
                questInfoObjective.ProgressBarWeight = packet.Translator.ReadSingle("ProgressBarWeight", i);

                int int280 = packet.Translator.ReadInt32("VisualEffects", i);
                for (int j = 0; j < int280; ++j)
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

            packet.Translator.ResetBitReader();

            uint bits26 = packet.Translator.ReadBits(9);
            uint bits154 = packet.Translator.ReadBits(12);
            uint bits904 = packet.Translator.ReadBits(12);
            uint bits1654 = packet.Translator.ReadBits(9);
            uint bits1789 = packet.Translator.ReadBits(10);
            uint bits2045 = packet.Translator.ReadBits(8);
            uint bits2109 = packet.Translator.ReadBits(10);
            uint bits2365 = packet.Translator.ReadBits(8);
            uint bits2429 = packet.Translator.ReadBits(11);

            quest.LogTitle = packet.Translator.ReadWoWString("LogTitle", bits26);
            quest.LogDescription = packet.Translator.ReadWoWString("LogDescription", bits154);
            quest.QuestDescription = packet.Translator.ReadWoWString("QuestDescription", bits904);
            quest.AreaDescription = packet.Translator.ReadWoWString("AreaDescription", bits1654);
            quest.QuestGiverTextWindow = packet.Translator.ReadWoWString("PortraitGiverText", bits1789);
            quest.QuestGiverTargetName = packet.Translator.ReadWoWString("PortraitGiverName", bits2045);
            quest.QuestTurnTextWindow = packet.Translator.ReadWoWString("PortraitTurnInText", bits2109);
            quest.QuestTurnTargetName = packet.Translator.ReadWoWString("PortraitTurnInName", bits2365);
            quest.QuestCompletionLog = packet.Translator.ReadWoWString("QuestCompletionLog", bits2429);

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

        [Parser(Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS)]
        public static void HandleQueryQuestCompletionNpcs(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
                packet.Translator.ReadInt32<QuestId>("QuestID", i);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS)]
        public static void HandleQuestgiverDetails(Packet packet)
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
            int int5860 = packet.Translator.ReadInt32("LearnSpellsCount");

            ReadQuestRewards(packet);

            int int2584 = packet.Translator.ReadInt32("DescEmotesCount");
            int int5876 = packet.Translator.ReadInt32("ObjectivesCount");

            for (int i = 0; i < int5860; i++)
                packet.Translator.ReadInt32("LearnSpells", i);

            questDetails.Emote = new uint?[] {0, 0, 0, 0};
            questDetails.EmoteDelay = new uint?[] {0, 0, 0, 0};
            for (int i = 0; i < int2584; i++)
            {
                questDetails.Emote[i] = (uint)packet.Translator.ReadInt32("Type", i);
                questDetails.EmoteDelay[i] = packet.Translator.ReadUInt32("Delay", i);
            }

            for (int i = 0; i < int5876; i++)
            {
                packet.Translator.ReadInt32("ObjectID", i);
                packet.Translator.ReadInt32("ObjectID", i);
                packet.Translator.ReadInt32("Amount", i);
                packet.Translator.ReadByte("Type", i);
            }

            packet.Translator.ResetBitReader();

            uint bits516 = packet.Translator.ReadBits(9);
            uint bits1606 = packet.Translator.ReadBits(12);
            uint bits715 = packet.Translator.ReadBits(12);
            uint bits260 = packet.Translator.ReadBits(10);
            uint bits650 = packet.Translator.ReadBits(8);
            uint bits4 = packet.Translator.ReadBits(10);
            uint bits1532 = packet.Translator.ReadBits(8);

            packet.Translator.ReadBit("DisplayPopup");
            packet.Translator.ReadBit("StartCheat");
            packet.Translator.ReadBit("AutoLaunched");

            packet.Translator.ReadWoWString("QuestTitle", bits516);
            packet.Translator.ReadWoWString("DescriptionText", bits1606);
            packet.Translator.ReadWoWString("LogDescription", bits715);
            packet.Translator.ReadWoWString("PortraitGiverText", bits260);
            packet.Translator.ReadWoWString("PortraitGiverName", bits650);
            packet.Translator.ReadWoWString("PortraitTurnInText", bits4);
            packet.Translator.ReadWoWString("PortraitTurnInName", bits1532);

            Storage.QuestDetails.Add(questDetails, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_STATUS_QUERY)]
        public static void HandleQuestgiverStatusQuery(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("QuestGiverGUID");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS)]
        public static void HandleQuestgiverStatus(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("QuestGiverGUID");
            packet.Translator.ReadInt32E<QuestGiverStatus4x>("StatusFlags");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatusMultiple(Packet packet)
        {
            var int16 = packet.Translator.ReadInt32("QuestGiverStatusCount");
            for (var i = 0; i < int16; ++i)
            {
                packet.Translator.ReadPackedGuid128("Guid");
                packet.Translator.ReadInt32E<QuestGiverStatus4x>("Status");
            }
        }

        [Parser(Opcode.SMSG_QUEST_COMPLETION_NPC_RESPONSE)]
        public static void HandleQuestCompletionNPCResponse(Packet packet)
        {
            var int1 = packet.Translator.ReadInt32("QuestCompletionNPCsCount");

            // QuestCompletionNPC
            for (var i = 0; i < int1; ++i)
            {
                packet.Translator.ReadInt32("Quest Id", i);

                var int4 = packet.Translator.ReadInt32("NpcCount", i);
                for (var j = 0; j < int4; ++j)
                    packet.Translator.ReadInt32("Npc", i, j);
            }
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE)]
        public static void HandleQuestCompleted(Packet packet)
        {
            packet.Translator.ReadUInt32("QuestId");
            packet.Translator.ReadUInt32("SkillLineIDReward");
            packet.Translator.ReadUInt32("MoneyReward");
            packet.Translator.ReadUInt32("NumSkillUpsReward");
            packet.Translator.ReadUInt32("XpReward");
            packet.Translator.ReadUInt32("TalentReward");

            ItemHandler.ReadItemInstance(packet);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("UseQuestReward");
            packet.Translator.ReadBit("LaunchGossip");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE)]
        public static void HandleQuestOfferReward(Packet packet)
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

            ReadQuestRewards(packet);

            int int252 = packet.Translator.ReadInt32("EmotesCount");

            // QuestDescEmote
            questOfferReward.Emote = new uint?[] {0, 0, 0, 0};
            questOfferReward.EmoteDelay = new uint?[] {0, 0, 0, 0};
            for (int i = 0; i < int252; i++)
            {
                questOfferReward.Emote[i] = (uint)packet.Translator.ReadInt32("Type");
                questOfferReward.EmoteDelay[i] = packet.Translator.ReadUInt32("Delay");
            }

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("AutoLaunched");

            packet.Translator.ReadInt32("QuestPackageID");
            packet.Translator.ReadInt32("PortraitTurnIn");
            packet.Translator.ReadInt32("PortraitGiver");

            packet.Translator.ResetBitReader();

            uint bits1139 = packet.Translator.ReadBits(9);
            uint bits69 = packet.Translator.ReadBits(12);
            uint bits883 = packet.Translator.ReadBits(10);
            uint bits819 = packet.Translator.ReadBits(8);
            uint bits1268 = packet.Translator.ReadBits(10);
            uint bits4 = packet.Translator.ReadBits(8);

            packet.Translator.ReadWoWString("QuestTitle", bits1139);
            questOfferReward.RewardText = packet.Translator.ReadWoWString("RewardText", bits69);
            packet.Translator.ReadWoWString("PortraitGiverText", bits883);
            packet.Translator.ReadWoWString("PortraitGiverName", bits819);
            packet.Translator.ReadWoWString("PortraitTurnInText", bits1268);
            packet.Translator.ReadWoWString("PortraitTurnInName", bits4);

            Storage.QuestOfferRewards.Add(questOfferReward, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_CREDIT)]
        public static void HandleQuestUpdateAddCredit(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("VictimGUID");

            packet.Translator.ReadInt32("QuestID");
            packet.Translator.ReadInt32("ObjectID");

            packet.Translator.ReadInt16("Count");
            packet.Translator.ReadInt16("Required");

            packet.Translator.ReadByte("ObjectiveType");
        }

        [Parser(Opcode.SMSG_CLEAR_QUEST_COMPLETED_BITS)]
        public static void HandleClearQuestCompletedBits(Packet packet)
        {
            var int4 = packet.Translator.ReadUInt32("Count");
            for (int i = 0; i < int4; i++)
                packet.Translator.ReadInt32("Qbits", i);
        }

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

            uint int520 = packet.Translator.ReadUInt32("GossipTextCount");
            for (int i = 0; i < int520; i++)
                ReadGossipText(packet, i);

            packet.Translator.ResetBitReader();

            uint bits16 = packet.Translator.ReadBits(11);
            questGreeting.Greeting = packet.Translator.ReadWoWString("Greeting", bits16);

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

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS)]
        public static void HandleQuestRequestItems(Packet packet)
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
            int int44 = packet.Translator.ReadInt32("QuestObjectiveCollectCount");
            int int60 = packet.Translator.ReadInt32("QuestCurrencyCount");
            packet.Translator.ReadInt32("StatusFlags");

            for (int i = 0; i < int44; i++)
            {
                packet.Translator.ReadInt32("ObjectID", i);
                packet.Translator.ReadInt32("Amount", i);
            }

            for (int i = 0; i < int60; i++)
            {
                packet.Translator.ReadInt32("CurrencyID", i);
                packet.Translator.ReadInt32("Amount", i);
            }

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("AutoLaunched");

            packet.Translator.ResetBitReader();

            uint bits3016 = packet.Translator.ReadBits(9);
            uint bits16 = packet.Translator.ReadBits(12);

            packet.Translator.ReadWoWString("QuestTitle", bits3016);
            questRequestItems.CompletionText = packet.Translator.ReadWoWString("CompletionText", bits16);

            Storage.QuestRequestItems.Add(questRequestItems, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE)]
        [Parser(Opcode.CMSG_QUEST_CLOSE_AUTOACCEPT_QUEST)]
        public static void HandleQuestForceRemoved(Packet packet)
        {
            packet.Translator.ReadInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_CREDIT_SIMPLE)]
        public static void HandleQuestUpdateAddCreditSimple(Packet packet)
        {
            packet.Translator.ReadInt32<QuestId>("QuestID");
            packet.Translator.ReadInt32("ObjectID");
            packet.Translator.ReadByte("ObjectiveType");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_HELLO)]
        public static void HandleQuestGiverHello(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("QuestGiver GUID");
        }

        [Parser(Opcode.CMSG_QUEST_PUSH_RESULT)]
        public static void HandleCliQuestPushResult(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("SenderGUID");
            packet.Translator.ReadInt32("QuestID");
            packet.Translator.ReadByteE<QuestPartyResult>("Result");
        }

        [Parser(Opcode.SMSG_QUEST_PUSH_RESULT)]
        public static void HandleQuestPushResult(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("SenderGUID");
            packet.Translator.ReadByteE<QuestPartyResult>("Result");
        }

        [Parser(Opcode.SMSG_SET_QUEST_COMPLETED_BIT)]
        [Parser(Opcode.SMSG_CLEAR_QUEST_COMPLETED_BIT)]
        public static void HandleSetQuestCompletedBit(Packet packet)
        {
            packet.Translator.ReadInt32("Bit");
            packet.Translator.ReadInt32("QuestID");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_INVALID_QUEST)]
        public static void HandleQuestGiverInvalidQuest(Packet packet)
        {
            packet.Translator.ReadUInt32E<QuestReasonTypeWoD>("Reason");
            packet.Translator.ReadBit("SendErrorMessage");

            var len = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("ReasonText", len);
        }

        [Parser(Opcode.CMSG_CHOICE_RESPONSE)]
        public static void HandleChoiceResponse(Packet packet)
        {
            packet.Translator.ReadInt32("ChoiceID");
            packet.Translator.ReadInt32("ResponseID");
        }

        [Parser(Opcode.SMSG_DISPLAY_PLAYER_CHOICE)]
        public static void HandleDisplayPlayerChoice(Packet packet)
        {
            packet.Translator.ReadInt32("ChoiceID");
            var int5 = packet.Translator.ReadInt32("PlayerChoiceResponseCount");
            packet.Translator.ReadPackedGuid128("Guid");

            for (int i = 0; i < int5; i++)
            {
                packet.Translator.ReadInt32("ResponseID", i);
                packet.Translator.ReadInt32("ChoiceArtFileID", i);

                packet.Translator.ResetBitReader();

                var bits4 = packet.Translator.ReadBits(9);
                var bits404 = packet.Translator.ReadBits(11);
                var bit2112 = packet.Translator.ReadBit("HasPlayerChoiceResponseReward", i);

                packet.Translator.ReadWoWString("Answer", bits4);
                packet.Translator.ReadWoWString("Description", bits404);

                if (bit2112)
                {
                    packet.Translator.ReadInt32("TitleID", i);
                    packet.Translator.ReadInt32("PackageID", i);
                    packet.Translator.ReadInt32("SkillLineID", i);
                    packet.Translator.ReadInt32("SkillPointCount", i);
                    packet.Translator.ReadInt32("ArenaPointCount", i);
                    packet.Translator.ReadInt32("HonorPointCount", i);
                    packet.Translator.ReadInt64("Money", i);
                    packet.Translator.ReadInt32("Xp", i);

                    var int36 = packet.Translator.ReadInt32("ItemsCount", i);
                    var int52 = packet.Translator.ReadInt32("CurrenciesCount", i);
                    var int68 = packet.Translator.ReadInt32("FactionsCount", i);
                    var int84 = packet.Translator.ReadInt32("ItemChoicesCount", i);

                    for (int j = 0; j < int36; j++) // @To-Do: need verification
                    {
                        packet.Translator.ReadInt32("Id", i, j);
                        packet.Translator.ReadInt32("DisplayID", i, j);
                        packet.Translator.ReadInt32("Quantity", i, j);

                        packet.Translator.ResetBitReader();

                        var bit32 = packet.Translator.ReadBit("HasBit32", i, j);
                        var bit56 = packet.Translator.ReadBit("HasBit56", i, j);

                        if (bit32)
                        {
                            // sub_5ED78D
                            packet.Translator.ReadByte("", i, j);

                            var int1 = packet.Translator.ReadUInt32("", i, j);
                            for (int k = 0; k < int1; k++)
                                packet.Translator.ReadUInt32("", i, j, k);
                        }

                        if (bit56)
                        {
                            // sub_5ECDA0
                            var int4 = packet.Translator.ReadInt32("", i, j);
                            packet.Translator.ReadWoWString("", int4, i, j);
                        }

                        packet.Translator.ReadInt32("", i, j);
                    }

                    for (int j = 0; j < int52; j++)
                    {
                        packet.Translator.ReadInt32("", i, j);
                        packet.Translator.ReadInt32("", i, j);
                        packet.Translator.ReadInt32("", i, j);

                        packet.Translator.ResetBitReader();

                        var bit32 = packet.Translator.ReadBit("", i, j);
                        var bit56 = packet.Translator.ReadBit("", i, j);

                        if (bit32)
                        {
                            // sub_5ED78D
                            packet.Translator.ReadByte("", i, j);

                            var int1 = packet.Translator.ReadUInt32("", i, j);
                            for (int k = 0; k < int1; k++)
                                packet.Translator.ReadUInt32("", i, j, k);
                        }

                        if (bit56)
                        {
                            // sub_5ECDA0
                            var int4 = packet.Translator.ReadInt32("", i, j);
                            packet.Translator.ReadWoWString("", int4, i, j);
                        }

                        packet.Translator.ReadInt32("", i, j);
                    }

                    for (int j = 0; j < int68; j++)
                    {
                        packet.Translator.ReadInt32("", i, j);
                        packet.Translator.ReadInt32("", i, j);
                        packet.Translator.ReadInt32("", i, j);

                        packet.Translator.ResetBitReader();

                        var bit32 = packet.Translator.ReadBit("", i, j);
                        var bit56 = packet.Translator.ReadBit("", i, j);

                        if (bit32)
                        {
                            // sub_5ED78D
                            packet.Translator.ReadByte("", i, j);

                            var int1 = packet.Translator.ReadUInt32("", i, j);
                            for (int k = 0; k < int1; k++)
                                packet.Translator.ReadUInt32("", i, j, k);
                        }

                        if (bit56)
                        {
                            // sub_5ECDA0
                            var int4 = packet.Translator.ReadInt32("", i, j);
                            packet.Translator.ReadWoWString("", int4, i, j);
                        }

                        packet.Translator.ReadInt32("", i, j);
                    }

                    for (int j = 0; j < int84; j++)
                    {
                        packet.Translator.ReadInt32("", i, j);
                        packet.Translator.ReadInt32("", i, j);
                        packet.Translator.ReadInt32("", i, j);

                        packet.Translator.ResetBitReader();

                        var bit32 = packet.Translator.ReadBit("", i, j);
                        var bit56 = packet.Translator.ReadBit("", i, j);

                        if (bit32)
                        {
                            // sub_5ED78D
                            packet.Translator.ReadByte("", i, j);

                            var int1 = packet.Translator.ReadUInt32("", i, j);
                            for (int k = 0; k < int1; k++)
                                packet.Translator.ReadUInt32("", i, j, k);
                        }

                        if (bit56)
                        {
                            // sub_5ECDA0
                            var int4 = packet.Translator.ReadInt32("", i, j);
                            packet.Translator.ReadWoWString("", int4, i, j);
                        }

                        packet.Translator.ReadInt32("", i, j);
                    }
                }
            }

            packet.Translator.ResetBitReader();

            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Question", len);
        }

        [Parser(Opcode.SMSG_DAILY_QUESTS_RESET)]
        public static void HandleDailyQuestsReset(Packet packet)
        {
            packet.Translator.ReadInt32("Count");
        }

        [Parser(Opcode.SMSG_QUEST_CONFIRM_ACCEPT)]
        public static void HandleQuestConfirmAccept(Packet packet)
        {
            packet.Translator.ReadInt32("QuestID");
            packet.Translator.ReadPackedGuid128("InitiatedBy");
            var len = packet.Translator.ReadBits(10);
            packet.Translator.ReadWoWString("QuestTitle", len);
        }
    }
}
