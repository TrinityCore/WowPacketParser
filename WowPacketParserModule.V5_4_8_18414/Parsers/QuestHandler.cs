using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_8_18414.Enums;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);

            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);
        }

        [Parser(Opcode.CMSG_QUESTGIVER_QUERY_QUEST)]
        public static void HandleQuestgiverQueryQuest(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_QUESTLOG_REMOVE_QUEST)]
        public static void HandleQuestlogRemoveQuest(Packet packet)
        {
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_QUESTGIVER_STATUS_QUERY)]
        public static void HandleQuestgiverStatusQuery(Packet packet)
        {
            var guid = packet.StartBitStream(4, 3, 2, 1, 0, 5, 7, 6);
            packet.ParseBitStream(guid, 5, 7, 4, 0, 2, 1, 6, 3);

            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            var count = packet.ReadBits("Count", 20);

            var counter = new uint[count];
            var points = new int[count][];

            for (var i = 0; i < count; i++)
            {
                counter[i] = packet.ReadBits("POI Counter", 18, i);
                points[i] = new int[counter[i]];
                for (var j = 0; j < counter[i]; j++)
                    points[i][j] = (int)packet.ReadBits("POI Points", 21, i, j);
            }
            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < counter[i]; j++)
                {
                    var questPoi = new QuestPOI();

                    questPoi.FloorId = packet.ReadUInt32("Floor Id", i, j);
                    questPoi.Points = new List<QuestPOIPoint>(points[i][j]);
                    for (var k = 0u; k < points[i][j]; k++)
                    {
                        var questPoiPoint = new QuestPOIPoint
                        {
                            Index = k,
                            X = packet.ReadInt32("Point X", i, j, (int)k),
                            Y = packet.ReadInt32("Point Y", i, j, (int)k)
                        };
                        questPoi.Points.Add(questPoiPoint);
                    }
                    questPoi.ObjectiveIndex = packet.ReadInt32("Objective Index", i, j);
                    var idx = packet.ReadInt32("POI Index", i, j);
                    packet.ReadInt32("Unk14*4", i, j);
                    packet.ReadInt32("Unk42*4", i, j);

                    questPoi.Map = (uint)packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map Id", i, j);
                    packet.ReadInt32("Points Counter", i, j);
                    questPoi.WorldMapAreaId = packet.ReadUInt32("World Map Area", i, j);
                    packet.ReadInt32("Unk200", i, j);

                    questPoi.UnkInt2 = packet.ReadUInt32("Unk Int32 3", i, j);
                    questPoi.UnkInt1 = packet.ReadUInt32("Unk Int32 2", i, j);

                    Storage.QuestPOIs.Add(new Tuple<uint, uint>((uint)0, (uint)idx), questPoi, packet.TimeSpan);
                }
                var questId = packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);
                packet.ReadInt32("POI size", i);
            }
            packet.ReadInt32("count");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUEST_QUERY_RESPONSE)]
        public static void HandleQuestQueryResp(Packet packet)
        {
            var id = packet.ReadEntry("Quest ID"); // +4
            if (id.Value) // entry is masked
                return;
            //sub_6B9CD3 quest failed 0x0B70

            //sub_6B575B -> 0x0F59
            bool questIsntAutoComplete = packet.ReadBit("Quest Isn't AutoComplete"); // +20

            if (questIsntAutoComplete)
            {
                var questTurnTextWindowLen = packet.ReadBits(10); // +2113
                var questTitleLen = packet.ReadBits(9); // +30
                var questCompletedTextLen = packet.ReadBits(11); // +2433
                var questDetailsLen = packet.ReadBits(12); // +908
                var questTurnTargetNameLen = packet.ReadBits(8); // +2369
                var questGiverTargetNameLen = packet.ReadBits(8); // +2049
                var questGiverTextWindowLen = packet.ReadBits(10); // +1793
                var questEndTextLen = packet.ReadBits(9); // +1658
                var questObjectivesCount = packet.ReadBits("Objectives Count", 19);
                var questObjectivesLen = packet.ReadBits(12); // +158

                uint[,] objectivesCounts = new uint[questObjectivesCount, 2];

                for (var i = 0; i < questObjectivesCount; ++i)
                {
                    objectivesCounts[i, 1] = packet.ReadBits(8); // +2949 + 20
                    objectivesCounts[i, 0] = packet.ReadBits(22); // +2949 + 0
                }

                packet.ResetBitReader();

                for (var i = 0; i < questObjectivesCount; ++i)
                {
                    packet.ReadUInt32("Requirement Count ", i); // +2949 + 12
                    packet.ReadUInt32("Unk UInt32", i); // +2949 + 0
                    packet.ReadWoWString("Objective Text", objectivesCounts[i, 1], i); // +2949 + 20
                    packet.ReadUInt32("Unk2 UInt32", i); // +2949 + 16
                    packet.ReadByte("Objective", i); // +2949 + 5
                    var reqType = packet.ReadEnum<QuestRequirementType>("Requirement Type", TypeCode.Byte, i); // +2949 + 4

                    // +2949 + 8
                    switch (reqType)
                    {
                        case QuestRequirementType.Creature:
                        case QuestRequirementType.Unknown3:
                            packet.ReadEntryWithName<Int32>(StoreNameType.Unit, "Required Creature ID", i);
                            break;
                        case QuestRequirementType.Item:
                            packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Required Item ID", i);
                            break;
                        case QuestRequirementType.GameObject:
                            packet.ReadEntryWithName<Int32>(StoreNameType.GameObject, "Required GameObject ID", i);
                            break;
                        case QuestRequirementType.Currency:
                            packet.ReadUInt32("Required Currency ID", i);
                            break;
                        case QuestRequirementType.Spell:
                            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Required Spell ID", i);
                            break;
                        case QuestRequirementType.Faction:
                            packet.ReadUInt32("Required Faction ID", i);
                            break;
                        default:
                            packet.ReadInt32("Required ID", i);
                            break;
                    }

                    for (var j = 0; j < objectivesCounts[i, 0]; j++)
                        packet.ReadUInt32("Unk Looped DWROD", i, j);
                }

                packet.ReadUInt32("Required Source Item ID 1"); // +2960
                packet.ReadUInt32("Reward Choice ItemID 2"); // +2980
                packet.ReadUInt32("Reward ItemID 3"); // +2955
                packet.ReadUInt32("Reward ItemID Count2"); // +2957
                packet.ReadUInt32("int2973"); // +2975

                var quest = new QuestTemplate
                {
                    Method = questIsntAutoComplete ? QuestMethod.Normal : QuestMethod.AutoComplete,
                };

                quest.RewardCurrencyId = new uint[4];
                quest.RewardCurrencyCount = new uint[4];
                for (var i = 0; i < 4; ++i)
                {
                    quest.RewardCurrencyId[i] = packet.ReadUInt32("Reward Currency ID", i); // +3001 + 16
                    quest.RewardCurrencyCount[i] = packet.ReadUInt32("Reward Currency Count", i); // +3001 + 0
                }

                packet.ReadUInt32("CharTittleID"); // +1787
                packet.ReadSingle("Point Y"); // +28
                quest.SoundTurnIn = packet.ReadUInt32("Sound TurnIn"); // +2947

                const int repCount = 5;
                quest.RewardFactionId = new uint[repCount];
                quest.RewardFactionValueId = new int[repCount];
                quest.RewardFactionValueIdOverride = new uint[repCount];

                for (var i = 0; i < repCount; i++)
                {
                    quest.RewardFactionValueId[i] = packet.ReadInt32("Reward Reputation ID", i); // +2986 + 20
                    quest.RewardFactionValueIdOverride[i] = packet.ReadUInt32("Reward Reputation ID Override", i); // +2986 + 0
                    quest.RewardFactionId[i] = packet.ReadUInt32("Reward Faction ID", i); // +2986 + 400
                }

                quest.RewardOrRequiredMoney = packet.ReadInt32("Reward Money"); // +16
                packet.ReadUInt32("EmoteOnComplete"); // +2981
                packet.ReadUInt32("Reward Choice ItemID Count5"); // +2972
                packet.ReadUInt32("MinimapTargetMask"); // +25
                quest.EndText = packet.ReadWoWString("QuestEndText", questEndTextLen); // +1658
                packet.ReadUInt32("Reward Choice ItemID 2"); // +2971
                quest.RewardMoneyMaxLevel = packet.ReadUInt32("Reward Money Max Level"); // +18
                packet.ReadUInt32("Reward Item1 ID"); // +2952
                quest.CompletedText = packet.ReadWoWString("QuestCompletedText", questCompletedTextLen); // +2433
                packet.ReadInt32("Reward Choice ItemID 4"); // +2977
                packet.ReadUInt32("RewardHonorAddition"); // +21
                quest.QuestGiverTextWindow = packet.ReadWoWString("QuestGiverTextWindow", questGiverTextWindowLen); // +1793
                quest.Objectives = packet.ReadWoWString("QuestObjectives", questObjectivesLen); // +158
                packet.ReadUInt32("RewArenaPoints"); // +1790
                packet.ReadUInt32("Reward Choice ItemID 6"); // +2983
                quest.SuggestedPlayers = packet.ReadUInt32("Suggested Players"); // +13
                packet.ReadUInt32("RepObjectiveFaction"); // +6
                packet.ReadUInt32("Required Source Item ID 2"); // +2961
                packet.ReadUInt32("Reward ItemID 2"); // +2953
                packet.ReadUInt32("MinLevel"); // +10
                packet.ReadUInt32("Reward Choice ItemID Count3"); // +2945
                packet.ReadUInt32("PointOpt"); // +29

                // +8
                quest.Level = packet.ReadInt32("Level"); // +8

                packet.ReadUInt32("RepObjectiveFaction2"); // +7
                packet.ReadUInt32("Required Source Item ID Count 3"); // +2966
                packet.ReadUInt32("XPId"); // +17
                quest.Details = packet.ReadWoWString("QuestDetails", questDetailsLen); // +908
                packet.ReadUInt32("Reward ItemID Count1"); // +2956
                packet.ReadUInt32("Reward Choice ItemID Count6"); // +2984
                packet.ReadUInt32("Reward ItemID Count3"); // +2958
                packet.ReadUInt32("RewardSpellCasted"); // +20
                packet.ReadUInt32("dword2E80"); // +2976
                quest.QuestTurnTargetName = packet.ReadWoWString("QuestTurnTargetName", questTurnTargetNameLen); // +2369
                packet.ReadUInt32("dword2E74"); // +2973
                packet.ReadUInt32("Required Source Item ID Count 2"); // +2965
                packet.ReadUInt32("Required Source Item ID 3"); // +2962
                packet.ReadUInt32("RewSkillPoints"); // +1792
                quest.Title = packet.ReadWoWString("QuestTitle", questTitleLen); // +30
                var type = packet.ReadEnum<QuestType>("Type", TypeCode.Int32); // +12
                packet.ReadUInt32("RepObjectiveValue2"); // +15
                packet.ReadUInt32("unk11"); // +2982
                packet.ReadUInt32("PlayersSlain"); // +1788
                packet.ReadUInt32("PointMapId"); // +26
                packet.ReadUInt32("NextQuestInChain"); // +14
                packet.ReadUInt32("Reward Choice ItemID 1"); // +2968
                var QuestGiverTargetName = packet.ReadWoWString("QuestGiverTargetName", questGiverTargetNameLen); // +2049
                packet.ReadUInt32("dword2E8C"); // +2979
                packet.ReadUInt32("Required Source Item ID 4"); // +2963
                packet.ReadSingle("Point X"); // +27
                packet.ReadUInt32("Reward Choice ItemID 3"); // +2974
                packet.ReadUInt32("unk"); // +2970
                packet.ReadUInt32("Reward ItemID Count4"); // +2959
                quest.SoundAccept = packet.ReadUInt32("Sound Accept"); // +2946
                packet.ReadUInt32("Reward ItemID 3"); // +2954
                packet.ReadSingle("RewardHonorMultiplier"); // +22
                packet.ReadUInt32("RequiredSpellID"); // +1786
                packet.ReadWoWString("QuestTurnTextWindow", questTurnTextWindowLen); // +2113
                packet.ReadUInt32("Reward Choice ItemID Count4"); // +2978
                packet.ReadUInt32("Required Source Item ID Count 1"); // +2964
                quest.ZoneOrSort = packet.ReadEnum<QuestSort>("Sort", TypeCode.Int32); // +11
                packet.ReadUInt32("BonusTalents"); // +1789
                packet.ReadUInt32("Reward Choice ItemID Count1"); // +2969
                packet.ReadUInt32("Rewarded Spell"); // +19
                packet.ReadUInt32("RewSkillID"); // +1791
                packet.ReadUInt32("unk9"); // +2985
                packet.ReadUInt32("unk10"); // +2967
                quest.Flags = packet.ReadEnum<QuestFlags>("Flags", TypeCode.UInt32); // +24
                packet.ReadUInt32("Suggested Players"); // +9
                packet.ReadUInt32("SourceItemID"); // +23

                packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");

                Storage.QuestTemplates.Add((uint)id.Key, quest, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_QUESTGIVER_STATUS)]
        public static void HandleQuestgiverStatus(Packet packet)
        {
            var guid = packet.StartBitStream(1, 7, 4, 2, 5, 3, 6, 0);
            packet.ParseBitStream(guid, 7);
            packet.ReadEnum<QuestGiverStatus4x>("Status", TypeCode.Int32);
            packet.ParseBitStream(guid, 4, 6, 1, 5, 2, 0, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUESTGIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatusMultiple(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);
            var guid = new byte[count][];
            for (var i = 0; i < count; i++)
                guid[i] = packet.StartBitStream(4, 0, 3, 6, 5, 7, 1, 2);

            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid[i], 6, 2, 7, 5, 4);
                packet.ReadEnum<QuestGiverStatus4x>("Status", TypeCode.Int32, i);
                packet.ParseBitStream(guid[i], 1, 3, 0);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_QUEST_QUERY)]
        [Parser(Opcode.CMSG_QUESTGIVER_ACCEPT_QUEST)]
        [Parser(Opcode.CMSG_QUESTGIVER_CHOOSE_REWARD)]
        [Parser(Opcode.CMSG_QUESTGIVER_COMPLETE_QUEST)]
        [Parser(Opcode.CMSG_QUESTGIVER_HELLO)]
        [Parser(Opcode.CMSG_QUESTGIVER_REQUEST_REWARD)]
        [Parser(Opcode.SMSG_QUEST_CONFIRM_ACCEPT)]
        [Parser(Opcode.SMSG_QUESTGIVER_OFFER_REWARD)]
        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_COMPLETE)]
        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_DETAILS)]
        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_LIST)]
        [Parser(Opcode.SMSG_QUESTGIVER_REQUEST_ITEMS)]
        [Parser(Opcode.SMSG_QUESTLOG_FULL)]
        [Parser(Opcode.SMSG_QUESTUPDATE_ADD_KILL)]
        [Parser(Opcode.SMSG_QUESTUPDATE_COMPLETE)]
        public static void HandleQuestNull(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
