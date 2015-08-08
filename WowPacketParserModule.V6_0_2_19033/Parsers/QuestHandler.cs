using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class QuestHandler
    {
        private static void ReadQuestRewards(Packet packet)
        {
            packet.ReadInt32("ChoiceItemCount");

            for (var i = 0; i < 6; ++i)
            {
                packet.ReadInt32("ItemID", i);
                packet.ReadInt32("Quantity", i);
            }

            packet.ReadInt32("ItemCount");

            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("ItemID", i);
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

        public static void ReadGossipText(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("QuestID", indexes);
            packet.ReadUInt32("QuestType", indexes);
            packet.ReadUInt32("QuestLevel", indexes);

            for (int i = 0; i < 2; i++)
                packet.ReadUInt32("QuestFlags", indexes, i);

            packet.ResetBitReader();

            packet.ReadBit("Repeatable", indexes);

            var bits13 = packet.ReadBits(9);
            packet.ReadWoWString("QuestTitle", bits13, indexes);
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
            packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadUInt32("QuestID");
            packet.ReadBit("FromScript");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD)]
        public static void HandleQuestChooseReward(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadInt32("QuestID");
            packet.ReadInt32("ItemChoiceID");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_ACCEPT_QUEST)]
        public static void HandleQuestgiverAcceptQuest(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadUInt32<QuestId>("QuestID");
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
            var int4 = packet.ReadInt32("QuestPOIData");

            for (var i = 0; i < int4; ++i)
            {
                var questId = packet.ReadInt32("QuestID", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173))
                    packet.ReadUInt32("NumBlobs", i);

                var int2 = packet.ReadInt32("QuestPOIBlobData", i);

                for (var j = 0; j < int2; ++j)
                {
                    var questPoi = new QuestPOIWoD
                    {
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

                    questPoi.WoDUnk1 = packet.ReadInt32("WoDUnk1", i, j);

                    var int13 = packet.ReadInt32("QuestPOIBlobPoint", i, j);
                    for (var k = 0; k < int13; ++k)
                    {
                        var questPoiPoint = new QuestPOIPointWoD
                        {
                            X = packet.ReadInt32("X", i, j, k),
                            Y = packet.ReadInt32("Y", i, j, k)
                        };
                        Storage.QuestPOIPointWoDs.Add(Tuple.Create(questId, j, k), questPoiPoint);
                    }

                    Storage.QuestPOIWoDs.Add(Tuple.Create(questId, j), questPoi, packet.TimeSpan);
                }
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            packet.ReadInt32("Entry");

            var hasData = packet.ReadBit("Has Data");
            if (!hasData)
                return; // nothing to do

            var quest = new QuestTemplateWod();

            var id = packet.ReadEntry("Quest ID");
            quest.QuestType = packet.ReadInt32E<QuestMethod>("QuestType");
            quest.QuestLevel = packet.ReadInt32("QuestLevel");
            quest.QuestPackageID = packet.ReadUInt32("QuestPackageID");
            quest.MinLevel = packet.ReadInt32("QuestMinLevel");
            quest.QuestSortID = (QuestSort)packet.ReadUInt32("QuestSortID");
            quest.QuestInfoID = packet.ReadInt32E<QuestType>("QuestInfoID");
            quest.SuggestedGroupNum = packet.ReadUInt32("SuggestedGroupNum");
            quest.RewardNextQuest = packet.ReadUInt32("RewardNextQuest");
            quest.RewardXPDifficulty = packet.ReadUInt32("RewardXPDifficulty");

            quest.RewardXPMultiplier = packet.ReadSingle("RewardXPMultiplier");

            quest.RewardMoney = packet.ReadInt32("RewardMoney");
            quest.RewardMoneyDifficulty = packet.ReadUInt32("RewardMoneyDifficulty");

            quest.RewardMoneyMultiplier = packet.ReadSingle("RewardMoneyMultiplier");

            quest.RewardBonusMoney = packet.ReadUInt32("RewardBonusMoney");
            quest.RewardDisplaySpell = packet.ReadUInt32("RewardDisplaySpell");
            quest.RewardSpell = packet.ReadUInt32("RewardSpell");
            quest.RewardHonor = packet.ReadUInt32("RewardHonor");

            quest.RewardKillHonor = packet.ReadSingle("RewardKillHonor");

            quest.StartItem = packet.ReadUInt32("StartItem");
            quest.Flags = packet.ReadUInt32E<QuestFlags>("Flags");
            quest.FlagsEx = packet.ReadUInt32("FlagsEx");

            quest.RewardItems = new uint[4];
            quest.RewardAmount = new uint[4];
            quest.ItemDrop = new uint[4];
            quest.ItemDropQuantity = new uint[4];
            for (var i = 0; i < 4; ++i)
            {
                quest.RewardItems[i] = packet.ReadUInt32("RewardItems", i);
                quest.RewardAmount[i] = packet.ReadUInt32("RewardAmount", i);
                quest.ItemDrop[i] = packet.ReadUInt32("ItemDrop", i);
                quest.ItemDropQuantity[i] = packet.ReadUInt32("ItemDropQuantity", i);
            }

            quest.RewardChoiceItemID = new uint[6];
            quest.RewardChoiceItemQuantity = new uint[6];
            quest.RewardChoiceItemDisplayID = new uint[6];
            for (var i = 0; i < 6; ++i) // CliQuestInfoChoiceItem
            {
                quest.RewardChoiceItemID[i] = packet.ReadUInt32("RewardChoiceItemID", i);
                quest.RewardChoiceItemQuantity[i] = packet.ReadUInt32("RewardChoiceItemQuantity", i);
                quest.RewardChoiceItemDisplayID[i] = packet.ReadUInt32("RewardChoiceItemDisplayID", i);
            }

            quest.POIContinent = packet.ReadUInt32("POIContinent");

            quest.POIx = packet.ReadSingle("POIx");
            quest.POIy = packet.ReadSingle("POIy");

            quest.POIPriority = packet.ReadInt32("POIPriority");
            quest.RewardTitle = packet.ReadUInt32("RewardTitle");
            quest.RewardTalents = packet.ReadUInt32("RewardTalents");
            quest.RewardArenaPoints = packet.ReadUInt32("RewardArenaPoints");
            quest.RewardSkillLineID = packet.ReadUInt32("RewardSkillLineID");
            quest.RewardNumSkillUps = packet.ReadUInt32("RewardNumSkillUps");
            quest.PortraitGiver = packet.ReadUInt32("PortraitGiver");
            quest.PortraitTurnIn = packet.ReadUInt32("PortraitTurnIn");

            quest.RewardFactionID = new uint[5];
            quest.RewardFactionValue = new int[5];
            quest.RewardFactionOverride = new int[5];
            for (var i = 0; i < 5; ++i)
            {
                quest.RewardFactionID[i] = packet.ReadUInt32("RewardFactionID", i);
                quest.RewardFactionValue[i] = packet.ReadInt32("RewardFactionValue", i);
                quest.RewardFactionOverride[i] = packet.ReadInt32("RewardFactionOverride", i);
            }

            quest.RewardFactionFlags = packet.ReadUInt32("RewardFactionFlags");

            quest.RewardCurrencyID = new uint[4];
            quest.RewardCurrencyQty = new uint[4];
            for (var i = 0; i < 4; ++i)
            {
                quest.RewardCurrencyID[i] = packet.ReadUInt32("RewardCurrencyID");
                quest.RewardCurrencyQty[i] = packet.ReadUInt32("RewardCurrencyQty");
            }

            quest.AcceptedSoundKitID = packet.ReadUInt32("AcceptedSoundKitID");
            quest.CompleteSoundKitID = packet.ReadUInt32("CompleteSoundKitID");
            quest.AreaGroupID = packet.ReadUInt32("AreaGroupID");
            quest.TimeAllowed = packet.ReadUInt32("TimeAllowed");
            var int2946 = packet.ReadUInt32("CliQuestInfoObjective");
            quest.AllowableRaces = packet.ReadInt32("AllowableRaces");

            for (var i = 0; i < int2946; ++i)
            {
                var questInfoObjective = new QuestInfoObjective
                {
                    QuestId = (uint) id.Key
                };

                var objectiveId = packet.ReadEntry("Id", i);
                questInfoObjective.Type = packet.ReadByteE<QuestRequirementType>("Quest Requirement Type", i);
                questInfoObjective.StorageIndex = packet.ReadSByte("StorageIndex", i);
                questInfoObjective.ObjectID = packet.ReadInt32("ObjectID", i);
                questInfoObjective.Amount = packet.ReadInt32("Amount", i);
                questInfoObjective.Flags = packet.ReadUInt32("Flags", i);
                questInfoObjective.UnkFloat = packet.ReadSingle("Float5", i);

                var int280 = packet.ReadInt32("VisualEffects", i);
                questInfoObjective.VisualEffectIds = new List<QuestVisualEffect>(int280);
                for (var j = 0; j < int280; ++j)
                {
                    var questVisualEffect = new QuestVisualEffect
                    {
                        Index = (uint) j,
                        VisualEffect = packet.ReadInt32("VisualEffectId", i, j)
                    };

                    questInfoObjective.VisualEffectIds.Add(questVisualEffect);
                }

                packet.ResetBitReader();

                var bits6 = packet.ReadBits(8);
                questInfoObjective.Description = packet.ReadWoWString("Description", bits6, i);

                if (BinaryPacketReader.GetLocale() != LocaleConstant.enUS && questInfoObjective.Description != string.Empty)
                {
                    var localesQuestObjectives = new LocalesQuestObjectives
                    {
                        QuestId = (uint)id.Key,
                        StorageIndex = questInfoObjective.StorageIndex,
                        Description = questInfoObjective.Description
                    };

                    Storage.LocalesQuestObjectives.Add(Tuple.Create((uint)objectiveId.Key, BinaryPacketReader.GetClientLocale()), localesQuestObjectives, packet.TimeSpan);
                }

                Storage.QuestObjectives.Add((uint)objectiveId.Key, questInfoObjective, packet.TimeSpan);
            }

            packet.ResetBitReader();

            var bits26 = packet.ReadBits(9);
            var bits154 = packet.ReadBits(12);
            var bits904 = packet.ReadBits(12);
            var bits1654 = packet.ReadBits(9);
            var bits1789 = packet.ReadBits(10);
            var bits2045 = packet.ReadBits(8);
            var bits2109 = packet.ReadBits(10);
            var bits2365 = packet.ReadBits(8);
            var bits2429 = packet.ReadBits(11);

            quest.LogTitle = packet.ReadWoWString("LogTitle", bits26);
            quest.LogDescription = packet.ReadWoWString("LogDescription", bits154);
            quest.QuestDescription = packet.ReadWoWString("QuestDescription", bits904);
            quest.AreaDescription = packet.ReadWoWString("AreaDescription", bits1654);
            quest.PortraitGiverText = packet.ReadWoWString("PortraitGiverText", bits1789);
            quest.PortraitGiverName = packet.ReadWoWString("PortraitGiverName", bits2045);
            quest.PortraitTurnInText = packet.ReadWoWString("PortraitTurnInText", bits2109);
            quest.PortraitTurnInName = packet.ReadWoWString("PortraitTurnInName", bits2365);
            quest.QuestCompletionLog = packet.ReadWoWString("QuestCompletionLog", bits2429);

            if (BinaryPacketReader.GetLocale() != LocaleConstant.enUS)
            {
                var localesQuest = new LocalesQuest
                {
                    LogTitle            = quest.LogTitle,
                    LogDescription      = quest.LogDescription,
                    QuestDescription    = quest.QuestDescription,
                    AreaDescription     = quest.AreaDescription,
                    PortraitGiverText   = quest.PortraitGiverText,
                    PortraitGiverName   = quest.PortraitGiverName,
                    PortraitTurnInText  = quest.PortraitTurnInText,
                    PortraitTurnInName  = quest.PortraitTurnInName,
                    QuestCompletionLog  = quest.QuestCompletionLog
                };

                Storage.LocalesQuests.Add(Tuple.Create((uint)id.Key, BinaryPacketReader.GetClientLocale()), localesQuest, packet.TimeSpan);
            }

            Storage.QuestTemplatesWod.Add((uint)id.Key, quest, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS)]
        public static void HandleQueryQuestCompletionNPCs(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
                packet.ReadInt32<QuestId>("QuestID", i);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS)]
        public static void HandleQuestgiverDetails(Packet packet)
        {
            var questDetails = new QuestDetails();

            packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadPackedGuid128("InformUnit");

            var id = packet.ReadInt32("QuestID");
            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitGiver");
            packet.ReadInt32("SuggestedPartyMembers");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("PortraitTurnIn");
            var int5860 = packet.ReadInt32("LearnSpellsCount");

            ReadQuestRewards(packet);

            var int2584 = packet.ReadInt32("DescEmotesCount");
            var int5876 = packet.ReadInt32("ObjectivesCount");

            for (int i = 0; i < int5860; i++)
                packet.ReadInt32("LearnSpells", i);

            questDetails.Emote = new uint[4];
            questDetails.EmoteDelay = new uint[4];
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

            var bits516 = packet.ReadBits(9);
            var bits1606 = packet.ReadBits(12);
            var bits715 = packet.ReadBits(12);
            var bits260 = packet.ReadBits(10);
            var bits650 = packet.ReadBits(8);
            var bits4 = packet.ReadBits(10);
            var bits1532 = packet.ReadBits(8);

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

            Storage.QuestDetails.Add((uint)id, questDetails, packet.TimeSpan);
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
            packet.ReadInt32E<QuestGiverStatus4x>("StatusFlags");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatusMultiple(Packet packet)
        {
            var int16 = packet.ReadInt32("QuestGiverStatusCount");
            for (var i = 0; i < int16; ++i)
            {
                packet.ReadPackedGuid128("Guid");
                packet.ReadInt32E<QuestGiverStatus4x>("Status");
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
            packet.ReadUInt32("QuestId");
            packet.ReadUInt32("SkillLineIDReward");
            packet.ReadUInt32("MoneyReward");
            packet.ReadUInt32("NumSkillUpsReward");
            packet.ReadUInt32("XpReward");
            packet.ReadUInt32("TalentReward");

            ItemHandler.ReadItemInstance(packet);

            packet.ResetBitReader();

            packet.ReadBit("UseQuestReward");
            packet.ReadBit("LaunchGossip");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE)]
        public static void HandleQuestOfferReward(Packet packet)
        {
            var questOfferReward = new QuestOfferReward();

            packet.ReadPackedGuid128("QuestGiverGUID");

            packet.ReadInt32("QuestGiverCreatureID");
            var id = packet.ReadInt32("QuestID");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("SuggestedPartyMembers");

            ReadQuestRewards(packet);

            var int252 = packet.ReadInt32("EmotesCount");

            // QuestDescEmote
            questOfferReward.Emote = new uint[4];
            questOfferReward.EmoteDelay = new uint[4];
            for (int i = 0; i < int252; i++)
            {
                questOfferReward.Emote[i] = (uint)packet.ReadInt32("Type");
                questOfferReward.EmoteDelay[i] = packet.ReadUInt32("Delay");
            }

            packet.ResetBitReader();

            packet.ReadBit("AutoLaunched");

            packet.ReadInt32("PortraitTurnIn");
            packet.ReadInt32("PortraitGiver");
            packet.ReadInt32("QuestPackageID");

            packet.ResetBitReader();

            var bits1139 = packet.ReadBits(9);
            var bits69 = packet.ReadBits(12);
            var bits883 = packet.ReadBits(10);
            var bits819 = packet.ReadBits(8);
            var bits1268 = packet.ReadBits(10);
            var bits4 = packet.ReadBits(8);

            packet.ReadWoWString("QuestTitle", bits1139);
            questOfferReward.RewardText = packet.ReadWoWString("RewardText", bits69);
            packet.ReadWoWString("PortraitGiverText", bits883);
            packet.ReadWoWString("PortraitGiverName", bits819);
            packet.ReadWoWString("PortraitTurnInText", bits1268);
            packet.ReadWoWString("PortraitTurnInName", bits4);

            Storage.QuestOfferRewards.Add((uint)id, questOfferReward, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_CREDIT)]
        public static void HandleQuestUpdateAddCredit(Packet packet)
        {
            packet.ReadPackedGuid128("VictimGUID");

            packet.ReadInt32("QuestID");
            packet.ReadInt32("ObjectID");

            packet.ReadInt16("Count");
            packet.ReadInt16("Required");

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
            var guid = packet.ReadPackedGuid128("QuestGiverGUID");

            var questGreeting = new QuestGreeting
            {
                GreetEmoteDelay = packet.ReadUInt32("GreetEmoteDelay"),
                GreetEmoteType = packet.ReadUInt32("GreetEmoteType")
            };

            var int520 = packet.ReadUInt32("GossipTextCount");
            for (int i = 0; i < int520; i++)
                ReadGossipText(packet, i);

            packet.ResetBitReader();

            var bits16 = packet.ReadBits(11);
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

            Storage.QuestGreetings.Add(guid.GetEntry(), questGreeting, packet.TimeSpan);

        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS)]
        public static void HandleQuestRequestItems(Packet packet)
        {
            var questRequestItems = new QuestRequestItems();

            packet.ReadPackedGuid128("QuestGiverGUID");

            packet.ReadInt32("QuestGiverCreatureID");
            var id = packet.ReadInt32("QuestID");
            questRequestItems.CompEmoteDelay = packet.ReadInt32("CompEmoteDelay");
            questRequestItems.CompEmoteType = packet.ReadInt32("CompEmoteType");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("SuggestPartyMembers");
            packet.ReadInt32("MoneyToGet");
            var int44 = packet.ReadInt32("QuestObjectiveCollectCount");
            var int60 = packet.ReadInt32("QuestCurrencyCount");
            packet.ReadInt32("StatusFlags");

            for (int i = 0; i < int44; i++)
            {
                packet.ReadInt32("ObjectID", i);
                packet.ReadInt32("Amount", i);
            }

            for (int i = 0; i < int60; i++)
            {
                packet.ReadInt32("CurrencyID", i);
                packet.ReadInt32("Amount", i);
            }

            packet.ResetBitReader();

            packet.ReadBit("AutoLaunched");

            packet.ResetBitReader();

            var bits3016 = packet.ReadBits(9);
            var bits16 = packet.ReadBits(12);

            packet.ReadWoWString("QuestTitle", bits3016);
            questRequestItems.CompletionText = packet.ReadWoWString("CompletionText", bits16);

            Storage.QuestRequestItems.Add((uint)id, questRequestItems, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE)]
        [Parser(Opcode.CMSG_QUEST_CLOSE_AUTOACCEPT_QUEST)]
        public static void HandleQuestForceRemoved(Packet packet)
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
            packet.ReadInt32("ChoiceID");
            var int5 = packet.ReadInt32("PlayerChoiceResponseCount");
            packet.ReadPackedGuid128("Guid");

            for (int i = 0; i < int5; i++)
            {
                packet.ReadInt32("ResponseID", i);
                packet.ReadInt32("ChoiceArtFileID", i);

                packet.ResetBitReader();

                var bits4 = packet.ReadBits(9);
                var bits404 = packet.ReadBits(11);
                var bit2112 = packet.ReadBit("HasPlayerChoiceResponseReward", i);

                packet.ReadWoWString("Answer", bits4);
                packet.ReadWoWString("Description", bits404);

                if (bit2112)
                {
                    packet.ReadInt32("TitleID", i);
                    packet.ReadInt32("PackageID", i);
                    packet.ReadInt32("SkillLineID", i);
                    packet.ReadInt32("SkillPointCount", i);
                    packet.ReadInt32("ArenaPointCount", i);
                    packet.ReadInt32("HonorPointCount", i);
                    packet.ReadInt64("Money", i);
                    packet.ReadInt32("Xp", i);

                    var int36 = packet.ReadInt32("ItemsCount", i);
                    var int52 = packet.ReadInt32("CurrenciesCount", i);
                    var int68 = packet.ReadInt32("FactionsCount", i);
                    var int84 = packet.ReadInt32("ItemChoicesCount", i);

                    for (int j = 0; j < int36; j++) // @To-Do: need verification
                    {
                        packet.ReadInt32("Id", i, j);
                        packet.ReadInt32("DisplayID", i, j);
                        packet.ReadInt32("Quantity", i, j);

                        packet.ResetBitReader();

                        var bit32 = packet.ReadBit("HasBit32", i, j);
                        var bit56 = packet.ReadBit("HasBit56", i, j);

                        if (bit32)
                        {
                            // sub_5ED78D
                            packet.ReadByte("", i, j);

                            var int1 = packet.ReadUInt32("", i, j);
                            for (int k = 0; k < int1; k++)
                                packet.ReadUInt32("", i, j, k);
                        }

                        if (bit56)
                        {
                            // sub_5ECDA0
                            var int4 = packet.ReadInt32("", i, j);
                            packet.ReadWoWString("", int4, i, j);
                        }

                        packet.ReadInt32("", i, j);
                    }

                    for (int j = 0; j < int52; j++)
                    {
                        packet.ReadInt32("", i, j);
                        packet.ReadInt32("", i, j);
                        packet.ReadInt32("", i, j);

                        packet.ResetBitReader();

                        var bit32 = packet.ReadBit("", i, j);
                        var bit56 = packet.ReadBit("", i, j);

                        if (bit32)
                        {
                            // sub_5ED78D
                            packet.ReadByte("", i, j);

                            var int1 = packet.ReadUInt32("", i, j);
                            for (int k = 0; k < int1; k++)
                                packet.ReadUInt32("", i, j, k);
                        }

                        if (bit56)
                        {
                            // sub_5ECDA0
                            var int4 = packet.ReadInt32("", i, j);
                            packet.ReadWoWString("", int4, i, j);
                        }

                        packet.ReadInt32("", i, j);
                    }

                    for (int j = 0; j < int68; j++)
                    {
                        packet.ReadInt32("", i, j);
                        packet.ReadInt32("", i, j);
                        packet.ReadInt32("", i, j);

                        packet.ResetBitReader();

                        var bit32 = packet.ReadBit("", i, j);
                        var bit56 = packet.ReadBit("", i, j);

                        if (bit32)
                        {
                            // sub_5ED78D
                            packet.ReadByte("", i, j);

                            var int1 = packet.ReadUInt32("", i, j);
                            for (int k = 0; k < int1; k++)
                                packet.ReadUInt32("", i, j, k);
                        }

                        if (bit56)
                        {
                            // sub_5ECDA0
                            var int4 = packet.ReadInt32("", i, j);
                            packet.ReadWoWString("", int4, i, j);
                        }

                        packet.ReadInt32("", i, j);
                    }

                    for (int j = 0; j < int84; j++)
                    {
                        packet.ReadInt32("", i, j);
                        packet.ReadInt32("", i, j);
                        packet.ReadInt32("", i, j);

                        packet.ResetBitReader();

                        var bit32 = packet.ReadBit("", i, j);
                        var bit56 = packet.ReadBit("", i, j);

                        if (bit32)
                        {
                            // sub_5ED78D
                            packet.ReadByte("", i, j);

                            var int1 = packet.ReadUInt32("", i, j);
                            for (int k = 0; k < int1; k++)
                                packet.ReadUInt32("", i, j, k);
                        }

                        if (bit56)
                        {
                            // sub_5ECDA0
                            var int4 = packet.ReadInt32("", i, j);
                            packet.ReadWoWString("", int4, i, j);
                        }

                        packet.ReadInt32("", i, j);
                    }
                }
            }

            packet.ResetBitReader();

            var len = packet.ReadBits(8);
            packet.ReadWoWString("Question", len);
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
