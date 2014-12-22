using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class QuestHandler
    {
        private static void ReadQuestRewards(ref Packet packet)
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

        [Parser(Opcode.CMSG_QUEST_QUERY)]
        public static void HandleQuestQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_QUESTGIVER_QUERY_QUEST)]
        public static void HandleQuestGiverQueryQuest(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadUInt32("QuestID");
            packet.ReadBit("RespondToGiver");
        }

        [Parser(Opcode.CMSG_QUESTGIVER_COMPLETE_QUEST)]
        public static void HandleQuestGiverCompleteQuest(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadUInt32("QuestID");
            packet.ReadBit("FromScript");
        }

        [Parser(Opcode.CMSG_QUEST_NPC_QUERY)]
        public static void HandleQuestNpcQuery(Packet packet)
        {
            packet.ReadUInt32("Count");

            for (var i = 0; i < 50; i++)
                packet.ReadEntry<Int32>(StoreNameType.Quest, "Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            packet.ReadInt32("NumPOIs");
            var int4 = packet.ReadInt32("QuestPOIData");

            for (var i = 0; i < int4; ++i)
            {
                var questId = packet.ReadUInt32("QuestID", i);
                packet.ReadUInt32("NumBlobs", i);

                var int2 = packet.ReadInt32("QuestPOIBlobData", i);

                for (var j = 0; j < int2; ++j)
                {
                    var questPoi = new QuestPOI();
                    var idx = packet.ReadInt32("BlobIndex", i, j);
                    questPoi.ObjectiveIndex = packet.ReadInt32("ObjectiveIndex", i, j);
                    packet.ReadInt32("QuestObjectiveID", i, j);
                    packet.ReadInt32("QuestObjectID", i, j);
                    questPoi.Map = (uint)packet.ReadInt32("MapID", i, j);
                    questPoi.WorldMapAreaId = (uint)packet.ReadInt32("WorldMapAreaID", i, j);
                    questPoi.FloorId = (uint)packet.ReadInt32("Floor", i, j);
                    packet.ReadInt32("Priority", i, j);
                    packet.ReadInt32("Flags", i, j);
                    packet.ReadInt32("WorldEffectID", i, j);
                    packet.ReadInt32("PlayerConditionID", i, j);
                    packet.ReadInt32("NumPoints", i, j);
                    packet.ReadInt32("Int12", i, j);

                    var int13 = packet.ReadInt32("QuestPOIBlobPoint", i, j);
                    questPoi.Points = new List<QuestPOIPoint>(int13);
                    for (var k = 0u; k < int13; ++k)
                    {
                        var questPoiPoint = new QuestPOIPoint
                        {
                            Index = k,
                            X = packet.ReadInt32("X", i, j, (int)k),
                            Y = packet.ReadInt32("Y", i, j, (int)k)
                        };
                        questPoi.Points.Add(questPoiPoint);
                    }

                    Storage.QuestPOIs.Add(new Tuple<uint, uint>(questId, (uint)idx), questPoi, packet.TimeSpan);
                }
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUEST_QUERY_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            packet.ReadInt32("Entry");

            var hasData = packet.ReadBit("Has Data");
            if (!hasData)
                return; // nothing to do

            var quest = new QuestTemplateWod();

            var id = packet.ReadEntry("Quest ID");
            quest.QuestType = packet.ReadEnum<QuestMethod>("QuestType", TypeCode.Int32);
            quest.QuestLevel = packet.ReadInt32("QuestLevel");
            quest.QuestPackageID = packet.ReadInt32("QuestPackageID");
            quest.MinLevel = packet.ReadInt32("QuestMinLevel");
            quest.QuestSortID = (QuestSort)packet.ReadInt32("QuestSortID");
            quest.QuestInfoID = packet.ReadEnum<QuestType>("QuestInfoID", TypeCode.Int32);
            quest.SuggestedGroupNum = packet.ReadInt32("SuggestedGroupNum");
            quest.RewardNextQuest = packet.ReadInt32("RewardNextQuest");
            quest.RewardXPDifficulty = packet.ReadInt32("RewardXPDifficulty");

            quest.Float10 = packet.ReadSingle("Float10");

            quest.RewardOrRequiredMoney = packet.ReadInt32("RewardMoney");
            quest.RewardMoneyMaxLevel = packet.ReadInt32("RewardMoneyDifficulty");

            quest.Float13 = packet.ReadSingle("Float13");

            quest.RewardBonusMoney = packet.ReadInt32("RewardBonusMoney");
            quest.RewardDisplaySpell = packet.ReadInt32("RewardDisplaySpell");
            quest.RewardSpell = packet.ReadInt32("RewardSpell");
            quest.RewardHonor = packet.ReadInt32("RewardHonor");

            quest.RewardKillHonor = packet.ReadSingle("RewardKillHonor");

            quest.StartItem = packet.ReadInt32("StartItem");
            quest.Flags = packet.ReadEnum<QuestFlags>("Flags", TypeCode.UInt32);
            quest.FlagsEx = packet.ReadInt32("FlagsEx");

            quest.RewardItems = new int[4];
            quest.RewardAmount = new int[4];
            quest.ItemDrop = new int[4];
            quest.ItemDropQuantity = new int[4];
            for (var i = 0; i < 4; ++i)
            {
                quest.RewardItems[i] = packet.ReadInt32("RewardItems", i);
                quest.RewardAmount[i] = packet.ReadInt32("RewardAmount", i);
                quest.ItemDrop[i] = packet.ReadInt32("ItemDrop", i);
                quest.ItemDropQuantity[i] = packet.ReadInt32("ItemDropQuantity", i);
            }

            quest.ItemID = new int[6];
            quest.Quantity = new int[6];
            quest.DisplayID = new int[6];
            for (var i = 0; i < 6; ++i) // CliQuestInfoChoiceItem
            {
                quest.ItemID[i] = packet.ReadInt32("ItemID", i);
                quest.Quantity[i] = packet.ReadInt32("Quantity", i);
                quest.DisplayID[i] = packet.ReadInt32("DisplayID", i);
            }

            quest.POIContinent = packet.ReadInt32("POIContinent");

            quest.POIx = packet.ReadSingle("POIx");
            quest.POIy = packet.ReadSingle("POIy");

            quest.POIPriority = packet.ReadInt32("POIPriority");
            quest.RewardTitle = packet.ReadInt32("RewardTitle");
            quest.RewardTalents = packet.ReadInt32("RewardTalents");
            quest.RewardArenaPoints = packet.ReadInt32("RewardArenaPoints");
            quest.RewardSkillLineID = packet.ReadInt32("RewardSkillLineID");
            quest.RewardNumSkillUps = packet.ReadInt32("RewardNumSkillUps");
            quest.PortraitGiver = packet.ReadInt32("PortraitGiver");
            quest.PortraitTurnIn = packet.ReadInt32("PortraitTurnIn");

            quest.RewardFactionID = new int[5];
            quest.RewardFactionValue = new int[5];
            quest.RewardFactionOverride = new int[5];
            for (var i = 0; i < 5; ++i)
            {
                quest.RewardFactionID[i] = packet.ReadInt32("RewardFactionID", i);
                quest.RewardFactionValue[i] = packet.ReadInt32("RewardFactionValue", i);
                quest.RewardFactionOverride[i] = packet.ReadInt32("RewardFactionOverride", i);
            }

            quest.RewardFactionFlags = packet.ReadInt32("RewardFactionFlags");

            quest.RewardCurrencyID = new int[4];
            quest.RewardCurrencyQty = new int[4];
            for (var i = 0; i < 4; ++i)
            {
                quest.RewardCurrencyID[i] = packet.ReadInt32("RewardCurrencyID");
                quest.RewardCurrencyQty[i] = packet.ReadInt32("RewardCurrencyQty");
            }

            quest.AcceptedSoundKitID = packet.ReadInt32("AcceptedSoundKitID");
            quest.CompleteSoundKitID = packet.ReadInt32("CompleteSoundKitID");
            quest.AreaGroupID = packet.ReadInt32("AreaGroupID");
            quest.TimeAllowed = packet.ReadInt32("TimeAllowed");
            var int2946 = packet.ReadInt32("CliQuestInfoObjective");
            quest.Int2950 = packet.ReadInt32("Int2950");

            for (var i = 0; i < int2946; ++i)
            {
                packet.ReadInt32("Id", i);
                packet.ReadByte("Type", i);
                packet.ReadByte("StorageIndex", i);
                packet.ReadInt32("ObjectID", i);
                packet.ReadInt32("Amount", i);
                packet.ReadInt32("Flags", i);
                packet.ReadSingle("Float5", i);
                var int280 = packet.ReadInt32("VisualEffects", i);
                for (var j = 0; j < int280; ++j)
                    packet.ReadInt32("VisualEffectId", i, j);

                packet.ResetBitReader();

                var bits6 = packet.ReadBits(8);
                packet.ReadWoWString("Description", bits6, i);
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

            Storage.QuestTemplatesWod.Add((uint)id.Key, quest, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
                packet.ReadEntry<Int32>(StoreNameType.Quest, "Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_DETAILS)]
        public static void HandleQuestgiverDetails(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadPackedGuid128("InformUnit");

            packet.ReadInt32("QuestID");
            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitGiver");
            packet.ReadInt32("SuggestedPartyMembers");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("PortraitTurnIn");
            var int5860 = packet.ReadInt32("LearnSpellsCount");

            ReadQuestRewards(ref packet);

            var int2584 = packet.ReadInt32("DescEmotesCount");
            var int5876 = packet.ReadInt32("ObjectivesCount");

            for (int i = 0; i < int5860; i++)
            {
                packet.ReadInt32("LearnSpells", i);
            }

            for (int i = 0; i < int2584; i++)
            {
                packet.ReadInt32("Type", i);
                packet.ReadInt32("Delay", i);
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
            packet.ReadWoWString("PortraitTurnInName", bits260);
            packet.ReadWoWString("PortraitGiverName", bits650);
            packet.ReadWoWString("PortraitGiverText", bits4);
            packet.ReadWoWString("PortraitTurnInText", bits1532);
        }

        [Parser(Opcode.CMSG_QUESTGIVER_STATUS_QUERY)]
        public static void HandleQuestgiverStatusQuery(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");
        }

        [Parser(Opcode.SMSG_QUESTGIVER_STATUS)]
        public static void HandleQuestgiverStatus(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadEnum<QuestGiverStatus4x>("StatusFlags", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_QUESTGIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatusMultiple(Packet packet)
        {
            var int16 = packet.ReadInt32("QuestGiverStatusCount");
            for (var i = 0; i < int16; ++i)
            {
                packet.ReadPackedGuid128("Guid");
                packet.ReadEnum<QuestGiverStatus4x>("Status", TypeCode.Int32);
            }
        }

        [Parser(Opcode.SMSG_QUEST_NPC_QUERY_RESPONSE)]
        public static void HandleUnknown6462(Packet packet)
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

        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_COMPLETE)]
        public static void HandleQuestCompleted(Packet packet)
        {
            packet.ReadUInt32("QuestId");
            packet.ReadUInt32("SkillLineIDReward");
            packet.ReadUInt32("MoneyReward");
            packet.ReadUInt32("NumSkillUpsReward");
            packet.ReadUInt32("XpReward");
            packet.ReadUInt32("TalentReward");

            packet.ReadUInt32("ItemID");
            packet.ReadUInt32("RandomPropertiesSeed");
            packet.ReadUInt32("RandomPropertiesID");
            packet.ResetBitReader();
            var hasBonuses = packet.ReadBit("HasItemBonus");
            var hasModifications = packet.ReadBit("HasModifications");
            if (hasBonuses)
            {
                packet.ReadByte("Context");

                var bonusCount = packet.ReadUInt32();
                for (var j = 0; j < bonusCount; ++j)
                    packet.ReadUInt32("BonusListID", j);
            }

            if (hasModifications)
            {
                var modificationCount = packet.ReadUInt32() / 4;
                for (var j = 0; j < modificationCount; ++j)
                    packet.ReadUInt32("Modification", j);
            }

            packet.ResetBitReader();

            packet.ReadBit("UseQuestReward");
            packet.ReadBit("LaunchGossip");
        }

        [Parser(Opcode.SMSG_QUESTGIVER_OFFER_REWARD)]
        public static void HandleQuestOfferReward(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");

            packet.ReadInt32("QuestGiverCreatureID");
            packet.ReadInt32("QuestID");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("SuggestedPartyMembers");

            ReadQuestRewards(ref packet);

            var int252 = packet.ReadInt32("EmotesCount");

            // QuestDescEmote
            for (int i = 0; i < int252; i++)
            {
                packet.ReadInt32("Type");
                packet.ReadUInt32("Delay");
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
            packet.ReadWoWString("RewardText", bits69);
            packet.ReadWoWString("PortraitTurnInText", bits883);
            packet.ReadWoWString("PortraitGiverName", bits819);
            packet.ReadWoWString("PortraitGiverText", bits1268);
            packet.ReadWoWString("PortraitTurnInName", bits4);
        }

        [Parser(Opcode.SMSG_QUESTUPDATE_ADD_KILL)]
        public static void HandleQuestUpdateAdd(Packet packet)
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
    }
}
