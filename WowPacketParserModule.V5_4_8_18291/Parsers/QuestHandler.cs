using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.CMSG_QUEST_GIVER_STATUS_QUERY)]
        public static void HandleQuestgiverStatusQuery(Packet packet)
        {
            var guid = packet.StartBitStream(4, 3, 2, 1, 0, 5, 7, 6);
            packet.ParseBitStream(guid, 5, 7, 4, 0, 2, 1, 6, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS)]
        public static void HandleQuestgiverStatus(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 7, 4, 2, 5, 3, 6, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadInt32E<QuestGiverStatus4x>("Status");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatusMultiple(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];

                packet.StartBitStream(guid[i], 4, 0, 3, 6, 5, 7, 1, 2);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadInt32E<QuestGiverStatus4x>("Status");
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(guid[i], 0);

                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_INFO)]
        public static void HandleQuestQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Entry");
            packet.StartBitStream(guid, 0, 5, 2, 7, 6, 4, 1, 3);
            packet.ParseBitStream(guid, 4, 1, 7, 5, 2, 3, 6, 0);

            packet.WriteGuid("Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
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
                    objectivesCounts[i, 1] = packet.ReadBits(8); // +2949 + 20 objectives texts
                    objectivesCounts[i, 0] = packet.ReadBits(22); // +2949 + 0 objectives visuals
                }

                packet.ResetBitReader();

                for (var i = 0; i < questObjectivesCount; ++i)
                {
                    packet.ReadUInt32("Requirement Count ", i); // +2949 + 12
                    packet.ReadUInt32("Objective ID", i); // +2949 + 0
                    packet.ReadWoWString("Objective Text", objectivesCounts[i, 1], i); // +2949 + 20
                    packet.ReadUInt32("Unk2 UInt32", i); // +2949 + 16
                    packet.ReadByte("Objective", i); // +2949 + 5
                    var reqType = packet.ReadByteE<QuestRequirementType>("Requirement Type", i); // +2949 + 4

                    // +2949 + 8
                    switch (reqType)
                    {
                        case QuestRequirementType.CreatureKill:
                        case QuestRequirementType.CreatureInteract:
                        case QuestRequirementType.PetBattleDefeatCreature:
                            packet.ReadInt32<UnitId>("Required Creature ID", i);
                            break;
                        case QuestRequirementType.Item:
                            packet.ReadInt32<ItemId>("Required Item ID", i);
                            break;
                        case QuestRequirementType.GameObject:
                            packet.ReadInt32<GOId>("Required GameObject ID", i);
                            break;
                        case QuestRequirementType.Currency:
                            packet.ReadUInt32("Required Currency ID", i);
                            break;
                        case QuestRequirementType.Spell:
                            packet.ReadInt32<SpellId>("Required Spell ID", i);
                            break;
                        case QuestRequirementType.FactionRepHigher:
                        case QuestRequirementType.FactionRepLower:
                            packet.ReadUInt32("Required Faction ID", i);
                            break;
                        case QuestRequirementType.PetBattleDefeatSpecies:
                            packet.ReadUInt32("Required Species ID", i);
                            break;
                        default:
                            packet.ReadInt32("Required ID", i);
                            break;
                    }

                    for (var j = 0; j < objectivesCounts[i, 0]; j++)
                        packet.ReadUInt32("Objective Visual ID", i, j);
                }

                packet.ReadUInt32("Required Source Item ID 1"); // +2960
                packet.ReadUInt32("Reward Choice ItemID 2"); // +2980
                packet.ReadUInt32("Reward ItemID 3"); // +2955
                packet.ReadUInt32("Reward ItemID Count2"); // +2957
                packet.ReadUInt32("int2973"); // +2975

                var quest = new QuestTemplate
                {
                    Method = questIsntAutoComplete ? QuestMethod.Normal : QuestMethod.AutoComplete
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
                var type = packet.ReadInt32E<QuestType>("Type"); // +12
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
                quest.ZoneOrSort = packet.ReadInt32E<QuestSort>("Sort"); // +11
                packet.ReadUInt32("BonusTalents"); // +1789
                packet.ReadUInt32("Reward Choice ItemID Count1"); // +2969
                packet.ReadUInt32("Rewarded Spell"); // +19
                packet.ReadUInt32("RewSkillID"); // +1791
                packet.ReadUInt32("unk9"); // +2985
                packet.ReadUInt32("unk10"); // +2967
                quest.Flags = packet.ReadUInt32E<QuestFlags>("Flags"); // +24
                packet.ReadUInt32("Suggested Players"); // +9
                packet.ReadUInt32("SourceItemID"); // +23

                packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");

                Storage.QuestTemplates.Add((uint)id.Key, quest, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            var count = packet.ReadBits("Count", 20);

            var POIcounter = new uint[count];
            var pointsSize = new uint[count][];

            for (var i = 0; i < count; ++i)
            {
                POIcounter[i] = packet.ReadBits("POI Counter", 18, i);
                pointsSize[i] = new uint[POIcounter[i]];

                for (var j = 0; j < POIcounter[i]; ++j)
                    pointsSize[i][j] = packet.ReadBits("Points Counter", 21, i, j);
            }

            for (var i = 0; i < count; ++i)
            {
                var questPOIs = new List<QuestPOI>();

                for (var j = 0; j < POIcounter[i]; ++j)
                {
                    var questPoi = new QuestPOI();
                    questPoi.Points = new List<QuestPOIPoint>((int)pointsSize[i][j]);

                    packet.ReadInt32("World Effect ID", i, j);

                    for (var k = 0u; k < pointsSize[i][j]; ++k)
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
                    questPoi.Idx = (uint)packet.ReadInt32("POI Index", i, j);

                    packet.ReadInt32("Unk Int32 2", i, j);
                    packet.ReadInt32("Unk Int32 4", i, j);

                    questPoi.Map = packet.ReadUInt32<MapId>("Map Id", i, j);

                    questPoi.FloorId = packet.ReadUInt32("Floor Id", i, j);

                    questPoi.WorldMapAreaId = packet.ReadUInt32("World Map Area ID", i, j);

                    packet.ReadInt32("Unk Int32 3", i, j);
                    packet.ReadInt32("Unk Int32 1", i, j);
                    packet.ReadInt32("Player Condition ID", i, j);


                    questPOIs.Add(questPoi);
                }

                var questId = packet.ReadInt32<QuestId>("Quest ID", i);
                packet.ReadInt32("POI Counter?", i);

                foreach (var questpoi in questPOIs)
                    Storage.QuestPOIs.Add(new Tuple<uint, uint>((uint)questId, questpoi.Idx), questpoi, packet.TimeSpan);
            }

            packet.ReadInt32("Count");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_HELLO)]
        public static void HandleQuestgiverHello(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 5, 6, 7, 3, 4, 2, 1, 0);
            packet.ParseBitStream(guid, 4, 1, 7, 3, 6, 0, 5, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_LIST_MESSAGE)]
        public static void HandleQuestgiverQuestList(Packet packet)
        {
            packet.ReadUInt32("Emote");
            packet.ReadUInt32("Delay");

            var guid = new byte[8];

            guid[2] = packet.ReadBit();
            var titleLen = packet.ReadBits(11);
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var count = packet.ReadBits(19);

            var questTitleLen = new uint[count];

            for (var i = 0; i < count; i++)
            {
                packet.ReadBit("marker", i); // 0: yellow ! mark   1: blue ? mark
                questTitleLen[i] = packet.ReadBits(9);
            }

            packet.StartBitStream(guid, 1, 3, 4, 5, 7);
            packet.ReadXORBytes(guid, 1, 0, 6, 7);

            for (var i = 0; i < count; i++)
            {
                packet.ReadUInt32E<QuestFlags>("Quest Flags", i);
                packet.ReadUInt32<QuestId>("Quest ID", i);
                packet.ReadWoWString("Quest Title", questTitleLen[i], i);
                packet.ReadUInt32("Flags2", i);
                packet.ReadUInt32("Quest Icon", i);
                packet.ReadInt32("Quest Level", i);
            }
            packet.ReadXORBytes(guid, 5, 3, 2);
            packet.ReadWoWString("NPC Title", titleLen);
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_KILL)]
        public static void HandleQuestUpdateAdd(Packet packet)
        {
            packet.ReadInt16("Count");
            packet.ReadByteE<QuestRequirementType>("Quest Requirement Type");
            packet.ReadInt32<QuestId>("Quest ID");
            packet.ReadInt16("Required Count");

            var entry = packet.ReadEntry();
            packet.AddValue("Entry", StoreGetters.GetName(entry.Value ? StoreNameType.GameObject : StoreNameType.Unit, entry.Key));

            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 4, 2, 6, 1, 5, 7, 3);
            packet.ParseBitStream(guid, 2, 7, 3, 0, 4, 5, 1, 6);
            packet.WriteGuid("Guid", guid);
        }


        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE)]
        public static void HandleQuestOfferReward(Packet packet)
        {
            packet.ReadUInt32("RewardItemIdCount[2]");
            packet.ReadUInt32<QuestId>("Quest ID");
            packet.ReadUInt32("RewardItemId[3]");
            packet.ReadUInt32("RewardChoiceItemDisplayId(2)");

            for (var i = 0; i < 5; i++)
            {
                packet.ReadUInt32("RewardFactionId", i);
                packet.ReadUInt32("RewardFactionValueId", i);
                packet.ReadUInt32("RewardFactionValueIdOverride", i);
            }

            packet.ReadUInt32("RewardItemIdCount[0]");
            packet.ReadUInt32("RewardItemIdCount[3]");
            packet.ReadUInt32("RewardItemDisplayId(3)");
            packet.ReadUInt32("RewardItemId[1]");
            packet.ReadUInt32("RewardChoiceItemId[3]");
            packet.ReadUInt32("RewardChoiceItemDisplayId(3)");
            packet.ReadUInt32("GetRewChoiceItemsCount()");
            packet.ReadUInt32("RewSpellCast");
            packet.ReadUInt32("RewardItemDisplayId(1)");
            packet.ReadUInt32("RewardChoiceItemCount[5]");
            packet.ReadUInt32("RewardChoiceItemDisplayId(4)");
            packet.ReadUInt32("RewardChoiceItemCount[1]");
            packet.ReadUInt32("RewardChoiceItemDisplayId(0)");
            packet.ReadUInt32("RewardItemDisplayId(0)");
            packet.ReadUInt32("ClassRewardId");
            packet.ReadUInt32("QuestTurnInPortrait");                                      // model Id, usually used in wanted or boss quests
            packet.ReadUInt32("RewardItemIdCount[1]");
            packet.ReadUInt32("unk");                                      // unknown
            packet.ReadUInt32("RewardChoiceItemId[0]");
            packet.ReadUInt32("RewardChoiceItemCount[3]");
            packet.ReadUInt32("RewardChoiceItemCount[4]");
            packet.ReadUInt32("RewardChoiceItemId[1]");
            packet.ReadUInt32("BonusTalents");
            packet.ReadUInt32("RewardSkillId");

            for (var i = 0; i < 4; i++)
            {
                packet.ReadUInt32("CurrencyId", i);
                packet.ReadUInt32("CurrencyCount", i);
            }

            packet.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.ReadUInt32E<QuestFlags2>("Quest Flags2");
            packet.ReadUInt32("XPValue");
            packet.ReadUInt32("CharTitleId");
            packet.ReadUInt32("RewardChoiceItemId[2]");
            packet.ReadUInt32("RewItemsCount()");
            packet.ReadUInt32("unk");                                      // unknown
            packet.ReadUInt32("RewardChoiceItemId[4]");

            packet.ReadUInt32("Ender NPC or GO Entry");// ender NPC or GO entry

            packet.ReadUInt32("RewardItemId[2]");
            packet.ReadUInt32("RewardChoiceItemCount[0]");
            packet.ReadUInt32("RewardSkillPoints");
            packet.ReadUInt32("unk");                                      // unknown
            packet.ReadUInt32("RewMoney");
            packet.ReadUInt32("RewardChoiceItemId[5]");
            packet.ReadUInt32("RewardChoiceItemDisplayId(1)");
            packet.ReadUInt32("RewardChoiceItemCount[2]");
            packet.ReadUInt32("RewardItemDisplayId(2)");
            packet.ReadUInt32("unk");                                      // unknown
            packet.ReadUInt32("RewardItemId[0]");
            packet.ReadUInt32("RewardChoiceItemDisplayId(5)");

            var guid = new byte[8];



            var questTurnTextWindow = packet.ReadBits(10);
            var questGiverTargetName = packet.ReadBits(8);
            guid[6] = packet.ReadBit();
            var emoteCount = packet.ReadBits(21);
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var questTitle = packet.ReadBits(9);
            guid[4] = packet.ReadBit();
            var questTurnTargetName = packet.ReadBits(8);
            var questGiverTextWindow = packet.ReadBits(10);
            var questOfferRewardText = packet.ReadBits(12);
            guid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ReadBit("EnableNext");

            packet.ReadWoWString("questGiverTargetName", questGiverTargetName);
            packet.ReadWoWString("questTitle", questTitle);

            for (var i = 0; i < emoteCount; i++)
            {
                packet.ReadUInt32("OfferRewardEmoteDelay", i);
                packet.ReadUInt32("OfferRewardEmote", i);
            }

            packet.ParseBitStream(guid, 2);
            packet.ReadWoWString("questOfferRewardText", questOfferRewardText);
            packet.ReadWoWString("questTurnTextWindow", questTurnTextWindow);
            packet.ReadWoWString("questTurnTargetName", questTurnTargetName);
            packet.ParseBitStream(guid, 5, 1);
            packet.ReadWoWString("questGiverTextWindow", questGiverTextWindow);
            packet.ParseBitStream(guid, 0, 7, 6, 4, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS)]
        public static void HandleQuestRequestItems(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("unk");
            packet.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.ReadInt32("unk");
            packet.ReadInt32("canComplete?");
            packet.ReadInt32("Money");
            packet.ReadEntry("Quest Giver Entry");
            packet.ReadInt32("unk");
            packet.ReadInt32("Emote");
            var entry = packet.ReadUInt32<QuestId>("Quest ID");
            var countCurrencies = packet.ReadBits("Number of Required Currencies", 21);
            packet.ReadBit("CloseOnCancel?");
            packet.StartBitStream(guid, 2, 5, 1);
            var titleLen = packet.ReadBits(9);
            var textLen = packet.ReadBits(12);
            packet.StartBitStream(guid, 6, 0);
            var countItems = packet.ReadBits("Number of Required Items", 20);
            packet.StartBitStream(guid, 4, 7, 3);

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadWoWString("Title", titleLen);

            for (var i = 0; i < countCurrencies; i++)
            {
                packet.ReadUInt32("Required Currency Id", i);
                packet.ReadUInt32("Required Currency Count", i);
            }

            for (var i = 0; i < countItems; ++i)
            {
                packet.ReadUInt32("Required Item Display Id", i);
                packet.ReadUInt32<ItemId>("Required Item Id", i);
                packet.ReadUInt32("Required Item Count", i);
            }

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadWoWString("Text", textLen);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_REQUEST_REWARD)]
        public static void HandleQuestRequestReward(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadUInt32<QuestId>("Quest ID");

            packet.StartBitStream(guid, 6, 3, 1, 2, 4, 0, 5, 7);
            packet.ParseBitStream(guid, 3, 0, 7, 6, 2, 1, 5, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD)]
        public static void HandleQuestChooseReward(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadUInt32("Reward Item ID");
            packet.ReadInt32<QuestId>("Quest ID");
            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ParseBitStream(guid, 1, 2, 5, 7, 0, 3, 6, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_COMPLETE_QUEST)]
        public static void HandleQuestCompleteQuest(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32<QuestId>("Quest ID");
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ReadBit("autoCompleteMode");
            guid[0] = packet.ReadBit();

            packet.ParseBitStream(guid, 0, 2, 1, 4, 3, 6, 7, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);
            packet.ResetBitReader();

            for (var i = 0; i < count; i++)
                packet.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE)]
        public static void HandleQuestCompleted(Packet packet)
        {
            packet.ReadBit("Unk Bit 1");
            packet.ReadBit("Unk Bit 2");
            packet.ReadInt32("Talent Points");
            packet.ReadInt32("Money");
            packet.ReadInt32<QuestId>("Quest ID");
            packet.ReadInt32("RewSkillId");
            packet.ReadInt32("XP");
            packet.ReadInt32("RewSkillPoints");
        }
    }
}
