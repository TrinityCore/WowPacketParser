using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            var quest = new int[50];
            for (var i = 0; i < 50; i++)
                quest[i] = packet.Translator.ReadInt32();

            var count = packet.Translator.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                packet.AddValue("Quest ID", StoreGetters.GetName(StoreNameType.Quest, quest[i]));
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS)]
        public static void HandleQuestNpcQuery(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 22);

            for (var i = 0; i < count; i++)
                packet.Translator.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_COMPLETION_NPC_RESPONSE)]
        public static void HandleQuestNpcQueryResponse(Packet packet)
        {
            var bits10 = (int)packet.Translator.ReadBits(21);

            var bits4 = new uint[bits10];

            for (var i = 0; i < bits10; ++i)
                bits4[i] = packet.Translator.ReadBits(22);

            for (var i = 0; i < bits10; ++i)
            {
                for (var j = 0; j < bits4[i]; ++j)
                    packet.Translator.ReadInt32("Creature", i, j);

                packet.Translator.ReadInt32("Quest Id", i);
            }
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            uint count = packet.Translator.ReadBits("Count", 20);

            var poiCounter = new uint[count];
            var pointsSize = new uint[count][];

            for (int i = 0; i < count; ++i)
            {
                poiCounter[i] = packet.Translator.ReadBits("POI Counter", 18, i);
                pointsSize[i] = new uint[poiCounter[i]];

                for (int j = 0; j < poiCounter[i]; ++j)
                    pointsSize[i][j] = packet.Translator.ReadBits("Points Counter", 21, i, j);
            }

            for (int i = 0; i < count; ++i)
            {
                var questPOIs = new List<QuestPOI>();
                var questPoiPointsForQuest = new List<QuestPOIPoint>();

                for (int j = 0; j < poiCounter[i]; ++j)
                {
                    QuestPOI questPoi = new QuestPOI();

                    packet.Translator.ReadInt32("Unk Int32 1", i, j);
                    packet.Translator.ReadInt32("World Effect ID", i, j);
                    packet.Translator.ReadInt32("Player Condition ID", i, j);
                    packet.Translator.ReadInt32("Unk Int32 2", i, j);

                    var questPoiPoints = new List<QuestPOIPoint>();
                    for (int k = 0; k < pointsSize[i][j]; ++k)
                    {
                        QuestPOIPoint questPoiPoint = new QuestPOIPoint
                        {
                            Idx2 = k,
                            X = packet.Translator.ReadInt32("Point X", i, j, k),
                            Y = packet.Translator.ReadInt32("Point Y", i, j, k)
                        };
                        questPoiPoints.Add(questPoiPoint);
                    }

                    questPoi.MapID = (int)packet.Translator.ReadUInt32<MapId>("Map Id", i, j);
                    packet.Translator.ReadInt32("Unk Int32 3", i, j);
                    packet.Translator.ReadInt32("Unk Int32 4", i, j);
                    questPoi.Floor = (int)packet.Translator.ReadUInt32("Floor Id", i, j);
                    questPoi.WorldMapAreaId = (int)packet.Translator.ReadUInt32("World Map Area ID", i, j);

                    int idx = packet.Translator.ReadInt32("POI Index", i, j);
                    questPoi.ID = idx;

                    questPoi.ObjectiveIndex = packet.Translator.ReadInt32("Objective Index", i, j);

                    questPoiPoints.ForEach(p =>
                    {
                        p.Idx1 = idx;
                        questPoiPointsForQuest.Add(p);
                    });

                    questPOIs.Add(questPoi);
                }

                packet.Translator.ReadInt32("POI Counter?", i);
                int questId = packet.Translator.ReadInt32<QuestId>("Quest ID", i);

                questPoiPointsForQuest.ForEach(q =>
                {
                    q.QuestID = questId;
                    Storage.QuestPOIPoints.Add(q, packet.TimeSpan);
                });

                questPOIs.ForEach(q =>
                {
                    q.QuestID = questId;
                    Storage.QuestPOIs.Add(q, packet.TimeSpan);
                });
            }

            packet.Translator.ReadInt32("Count?");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_STATUS_QUERY)]
        public static void HandleQuestgiverStatusQuery(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(2, 7, 3, 1, 6, 0, 4, 5);
            packet.Translator.ParseBitStream(guid, 2, 3, 6, 5, 4, 1, 0, 7);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS)]
        public static void HandleQuestgiverStatus(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 1, 5, 2, 0, 4, 3, 7, 6);
            packet.Translator.ParseBitStream(guid, 7, 0, 4);
            packet.Translator.ReadInt32E<QuestGiverStatus4x>("Status");
            packet.Translator.ParseBitStream(guid, 2, 1, 6, 3, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatusMultiple(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 21);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];

                packet.Translator.StartBitStream(guid[i], 7, 0, 6, 2, 5, 1, 4, 3);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadInt32E<QuestGiverStatus4x>("Status");
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(guid[i], 0);

                packet.Translator.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_QUEST_LOG_REMOVE_QUEST)]
        public static void HandleQuestRemoveQuest(Packet packet)
        {
            packet.Translator.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD)]
        public static void HandleQuestChooseReward(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadUInt32("Reward");
            packet.Translator.ReadInt32<QuestId>("Quest ID");
            guid[2] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();

            packet.Translator.ParseBitStream(guid, 2, 0, 7, 5, 6, 4, 1, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_COMPLETE_QUEST)]
        public static void HandleQuestCompleteQuest(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32<QuestId>("Quest ID");
            guid[6] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit1C");
            guid[1] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();

            packet.Translator.ParseBitStream(guid, 0, 5, 3, 2, 4, 6, 1, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE)]
        public static void HandleQuestCompleted510(Packet packet)
        {
            packet.Translator.ReadInt32("RewSkillId");
            packet.Translator.ReadInt32<QuestId>("Quest ID");
            packet.Translator.ReadInt32("Talent Points");
            packet.Translator.ReadInt32("RewSkillPoints");
            packet.Translator.ReadInt32("Money");
            packet.Translator.ReadInt32("XP");
            packet.Translator.ReadBit("Unk Bit 1"); // if true EVENT_QUEST_FINISHED is fired, target cleared and gossip window is open
            packet.Translator.ReadBit("Unk Bit 2");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_ACCEPT_QUEST)]
        public static void HandleQuestgiverAcceptQuest(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            guid[3] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit18");
            guid[5] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_REQUEST_REWARD)]
        public static void HandleQuestRequestReward(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadUInt32<QuestId>("Quest ID");

            packet.Translator.StartBitStream(guid, 4, 1, 7, 0, 3, 2, 6, 5);
            packet.Translator.ParseBitStream(guid, 7, 2, 6, 4, 3, 1, 5, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS)]
        public static void HandleQuestgiverDetails(Packet packet)
        {
            packet.Translator.ReadInt32("QuestGiver Portrait?");
            packet.Translator.ReadInt32("Reward Item Id?"); // SMSG_ITEM_PUSH_RESULT is sent with this id in sniffs

            for (var i = 0; i < 5; i++)
            {
                packet.Translator.ReadUInt32("Reputation Value Id", i);
                packet.Translator.ReadUInt32("Reputation Faction", i);
                packet.Translator.ReadInt32("Reputation Value", i);
            }

            packet.Translator.ReadInt32("Quest XP");

            for (var i = 0; i < 4; i++)
            {
                packet.Translator.ReadUInt32("Currency Id", i);
                packet.Translator.ReadUInt32("Currency Count", i);
            }

            packet.Translator.ReadInt32("unk1");
            packet.Translator.ReadInt32("unk2");
            packet.Translator.ReadInt32("unk3");
            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadInt32("unk5");
            packet.Translator.ReadInt32("unk6");
            packet.Translator.ReadInt32("unk7");
            packet.Translator.ReadInt32("unk8");
            packet.Translator.ReadInt32("unk9");
            packet.Translator.ReadInt32("unk10");
            packet.Translator.ReadUInt32E<QuestFlags2>("Quest Flags 2");
            packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.Translator.ReadInt32("unk13");
            packet.Translator.ReadInt32("unk14");
            packet.Translator.ReadInt32("Money");
            packet.Translator.ReadInt32("unk16");
            packet.Translator.ReadInt32("unk17");
            packet.Translator.ReadInt32("unk18");
            packet.Translator.ReadInt32("unk19");
            packet.Translator.ReadInt32("unk20");
            packet.Translator.ReadInt32("unk21");
            packet.Translator.ReadInt32("unk22");
            packet.Translator.ReadInt32("unk23");
            packet.Translator.ReadInt32("unk24");
            packet.Translator.ReadInt32("unk25");
            packet.Translator.ReadInt32("unk26");
            packet.Translator.ReadInt32<SpellId>("Spell Cast Id");
            packet.Translator.ReadInt32("unk28");
            packet.Translator.ReadInt32("unk29");
            packet.Translator.ReadInt32("unk30");
            packet.Translator.ReadInt32("unk31");
            packet.Translator.ReadInt32("unk32");
            packet.Translator.ReadInt32("unk33");
            packet.Translator.ReadInt32("unk34");
            packet.Translator.ReadInt32("unk35");
            packet.Translator.ReadInt32("unk36");
            packet.Translator.ReadInt32("unk37");
            packet.Translator.ReadInt32("unk38");
            packet.Translator.ReadInt32("unk39");
            packet.Translator.ReadInt32("unk40");
            packet.Translator.ReadInt32("unk41");
            packet.Translator.ReadInt32("unk42");
            packet.Translator.ReadInt32("unk43");
            packet.Translator.ReadInt32("unk44");
            packet.Translator.ReadInt32("unk45");

            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[5] = packet.Translator.ReadBit();

            var str3len = (int)packet.Translator.ReadBits(8);
            var str1len = (int)packet.Translator.ReadBits(8);

            var counter5 = (int)packet.Translator.ReadBits(22);

            var bit3454 = !packet.Translator.ReadBit("!bit0");
            guid2[1] = packet.Translator.ReadBit();

            var str6len = (int)packet.Translator.ReadBits(12);
            guid2[0] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();

            var str5len = (int)packet.Translator.ReadBits(10);
            var counter3 = (int)packet.Translator.ReadBits(21);
            var counter4 = (int)packet.Translator.ReadBits(20);

            guid1[3] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            var bit11234 = !packet.Translator.ReadBit("!bit1");
            guid1[2] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();

            var str0len = (int)packet.Translator.ReadBits(10);//ok

            var str2len = (int)packet.Translator.ReadBits(9);//ok

            var str4len = (int)packet.Translator.ReadBits(12);//ok was 1

            guid2[4] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            var bit34354 = !packet.Translator.ReadBit("!bit2");
            guid2[6] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();

            for (var i = 0; i < counter3; i++) // 3308
            {
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);
            }

            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadWoWString("string0", str0len);

            for (var i = 0; i < counter4; i++)
            {
                packet.Translator.ReadUInt32("Required Count", i);
                var entry = packet.Translator.ReadUInt32();
                var type = packet.Translator.ReadByte("Type", i);
                switch (type)
                {
                    case 0: packet.AddValue("Required NPC", entry, i); break;
                    case 1: packet.AddValue("Required Item", entry, i); break;
                    default: packet.AddValue("Required Entry", entry, i); break;
                }
                packet.Translator.ReadInt32("unk", i);
            }

            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadWoWString("QuestGiver Target Name", str1len);
            packet.Translator.ReadWoWString("Title", str2len);
            packet.Translator.ReadWoWString("string3", str3len);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 5);

            for (int i = 0; i < counter5; i++)
                packet.Translator.ReadInt32("Int", i);

            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadWoWString("Objectives", str4len);
            packet.Translator.ReadWoWString("string5", str5len);
            packet.Translator.ReadWoWString("Details", str6len);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 0);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE)]
        public static void HandleQuestOfferReward(Packet packet)
        {
            var guid1 = new byte[8];

            var strlen = packet.Translator.ReadBits(10);
            guid1[6] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            var len2 = packet.Translator.ReadBits(21);
            var len3 = packet.Translator.ReadBits(8);
            guid1[4] = packet.Translator.ReadBit();
            var len4 = packet.Translator.ReadBits(8);
            var len5 = packet.Translator.ReadBits(12);
            guid1[5] = packet.Translator.ReadBit();
            var bit4312 = !packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            var len6 = packet.Translator.ReadBits(9);
            var len7 = packet.Translator.ReadBits(10);
            guid1[7] = packet.Translator.ReadBit();

            packet.Translator.ReadInt32("unk1");
            packet.Translator.ReadInt32("unk2");
            packet.Translator.ReadInt32("unk3");


            for (var i = 0; i < 4; i++)
            {
                packet.Translator.ReadUInt32("Currency Id", i);
                packet.Translator.ReadUInt32("Currency Count", i);
            }

            packet.Translator.ReadWoWString("str1", strlen);

            packet.Translator.ReadInt32("unk4");
            packet.Translator.ReadUInt32("Money");
            packet.Translator.ReadInt32("unk6");
            packet.Translator.ReadInt32("unk7");
            packet.Translator.ReadInt32("unk8");
            packet.Translator.ReadInt32("unk9");
            packet.Translator.ReadInt32("unk10");

            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 2);

            packet.Translator.ReadInt32("unk11");

            for (var i = 0; i < 5; i++)
            {
                packet.Translator.ReadUInt32("Reputation Faction", i);
                packet.Translator.ReadUInt32("Reputation Value Id", i);
                packet.Translator.ReadInt32("Reputation Value (x100)", i);
            }


            packet.Translator.ReadInt32("unk12");
            packet.Translator.ReadInt32("unk13");
            packet.Translator.ReadWoWString("str2", len3);
            packet.Translator.ReadInt32("unk14");
            packet.Translator.ReadInt32("unk15");
            packet.Translator.ReadWoWString("Completion Text", len5);
            packet.Translator.ReadInt32("unk16");
            packet.Translator.ReadInt32("unk17");
            packet.Translator.ReadInt32("unk18");
            packet.Translator.ReadWoWString("str4", len4);
            packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.Translator.ReadInt32("unk20");
            packet.Translator.ReadInt32("unk21");
            packet.Translator.ReadInt32("unk22");
            packet.Translator.ReadInt32("unk23");
            packet.Translator.ReadInt32("unk24");
            packet.Translator.ReadWoWString("str5", len7);
            packet.Translator.ReadInt32("unk25");
            packet.Translator.ReadInt32("unk26");
            packet.Translator.ReadInt32("unk27");

            for (var i = 0; i < len2; i++) // len2 is guessed
            {
                packet.Translator.ReadUInt32E<EmoteType>("Emote Id", i);
                packet.Translator.ReadUInt32("Emote Delay", i);
            }
            packet.Translator.ReadInt32("unk28");
            packet.Translator.ReadXORByte(guid1, 1);

            packet.Translator.ReadInt32("unk29");
            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadInt32("unk31");
            packet.Translator.ReadInt32("unk32");
            packet.Translator.ReadInt32("unk33");
            packet.Translator.ReadInt32("unk34");
            packet.Translator.ReadInt32("unk35");
            packet.Translator.ReadInt32("unk36");
            packet.Translator.ReadInt32("unk37");
            packet.Translator.ReadInt32("unk38");
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadInt32("unk39");
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadInt32("unk40");
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadInt32("unk41");
            packet.Translator.ReadInt32("unk42");
            packet.Translator.ReadInt32("unk43");
            packet.Translator.ReadWoWString("Title", len6);
            packet.Translator.ReadInt32("unk44");
            packet.Translator.ReadInt32("unk45");
            packet.Translator.ReadUInt32("XP");
            packet.Translator.ReadInt32("unk47");
            packet.Translator.ReadInt32("unk48");
            packet.Translator.ReadInt32("unk49");


            packet.Translator.WriteGuid("Guid1", guid1);

            /*




            // --------------------


            packet.Translator.ReadGuid("GUID");
            var entry = packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadCString("Title");
            var text = packet.Translator.ReadCString("Text");

            Storage.QuestOffers.Add((uint)entry, new QuestOffer { OfferRewardText = text }, null);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.Translator.ReadCString("QuestGiver Text Window");
                packet.Translator.ReadCString("QuestGiver Target Name");
                packet.Translator.ReadCString("QuestTurn Text Window");
                packet.Translator.ReadCString("QuestTurn Target Name");
                packet.Translator.ReadUInt32("QuestGiverPortrait");
                packet.Translator.ReadUInt32("QuestTurnInPortrait");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                packet.Translator.ReadBool("Auto Finish");
            else
                packet.Translator.ReadBool("Auto Finish", TypeCode.Int32);


            packet.Translator.ReadUInt32("Suggested Players");

            var count1 = packet.Translator.ReadUInt32("Emote Count");
            for (var i = 0; i < count1; i++)
            {
                packet.Translator.ReadUInt32("Emote Delay", i);
                packet.Translator.ReadUInt32E<EmoteType>("Emote Id", i);
            }

            // extra info

            packet.Translator.ReadUInt32("Choice Item Count");
            for (var i = 0; i < 6; i++)
            {
                packet.Translator.ReadUInt32<ItemId>("Choice Item Id", i);
                packet.Translator.ReadUInt32("Choice Item Count", i);
                packet.Translator.ReadUInt32("Choice Item Display Id", i);
            }

            packet.Translator.ReadUInt32("Reward Item Count");

            for (var i = 0; i < 4; i++)
                packet.Translator.ReadUInt32<ItemId>("Reward Item Id", i);
            for (var i = 0; i < 4; i++)
                packet.Translator.ReadUInt32("Reward Item Count", i);
            for (var i = 0; i < 4; i++)
                packet.Translator.ReadUInt32("Reward Item Display Id", i);



            packet.Translator.ReadUInt32("Title Id");
            packet.Translator.ReadUInt32("Bonus Talents");
            packet.Translator.ReadUInt32("Reward Reputation Mask");


            packet.Translator.ReadInt32<SpellId>("Spell Id");
            packet.Translator.ReadInt32<SpellId>("Spell Cast Id");


            packet.Translator.ReadUInt32("Reward SkillId");
            packet.Translator.ReadUInt32("Reward Skill Points");*/
        }


    }
}
