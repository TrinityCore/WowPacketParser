using System;
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
                quest[i] = packet.ReadInt32();

            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                packet.AddValue("Quest ID", StoreGetters.GetName(StoreNameType.Quest, quest[i]));
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS)]
        public static void HandleQuestNpcQuery(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);

            for (var i = 0; i < count; i++)
                packet.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_COMPLETION_NPC_RESPONSE)]
        public static void HandleQuestNpcQueryResponse(Packet packet)
        {
            var bits10 = (int)packet.ReadBits(21);

            var bits4 = new uint[bits10];

            for (var i = 0; i < bits10; ++i)
                bits4[i] = packet.ReadBits(22);

            for (var i = 0; i < bits10; ++i)
            {
                for (var j = 0; j < bits4[i]; ++j)
                    packet.ReadInt32("Creature", i, j);

                packet.ReadInt32("Quest Id", i);
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

                    packet.ReadInt32("Unk Int32 1", i, j);
                    packet.ReadInt32("World Effect ID", i, j);
                    packet.ReadInt32("Player Condition ID", i, j);
                    packet.ReadInt32("Unk Int32 2", i, j);

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

                    questPoi.Map = packet.ReadUInt32<MapId>("Map Id", i, j);
                    packet.ReadInt32("Unk Int32 3", i, j);
                    packet.ReadInt32("Unk Int32 4", i, j);
                    questPoi.FloorId = packet.ReadUInt32("Floor Id", i, j);
                    questPoi.WorldMapAreaId = packet.ReadUInt32("World Map Area ID", i, j);
                    questPoi.Idx = (uint)packet.ReadInt32("POI Index", i, j);
                    questPoi.ObjectiveIndex = packet.ReadInt32("Objective Index", i, j);

                    questPOIs.Add(questPoi);
                }

                packet.ReadInt32("POI Counter?", i);
                var questId = packet.ReadInt32<QuestId>("Quest ID", i);

                foreach (var questpoi in questPOIs)
                    Storage.QuestPOIs.Add(new Tuple<uint, uint>((uint)questId, questpoi.Idx), questpoi, packet.TimeSpan);
            }

            packet.ReadInt32("Count?");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_STATUS_QUERY)]
        public static void HandleQuestgiverStatusQuery(Packet packet)
        {
            var guid = packet.StartBitStream(2, 7, 3, 1, 6, 0, 4, 5);
            packet.ParseBitStream(guid, 2, 3, 6, 5, 4, 1, 0, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS)]
        public static void HandleQuestgiverStatus(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 5, 2, 0, 4, 3, 7, 6);
            packet.ParseBitStream(guid, 7, 0, 4);
            packet.ReadInt32E<QuestGiverStatus4x>("Status");
            packet.ParseBitStream(guid, 2, 1, 6, 3, 5);

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

                packet.StartBitStream(guid[i], 7, 0, 6, 2, 5, 1, 4, 3);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guid[i], 5);
                packet.ReadInt32E<QuestGiverStatus4x>("Status");
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 0);

                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_QUEST_LOG_REMOVE_QUEST)]
        public static void HandleQuestRemoveQuest(Packet packet)
        {
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD)]
        public static void HandleQuestChooseReward(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadUInt32("Reward");
            packet.ReadInt32<QuestId>("Quest ID");
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            packet.ParseBitStream(guid, 2, 0, 7, 5, 6, 4, 1, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_COMPLETE_QUEST)]
        public static void HandleQuestCompleteQuest(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32<QuestId>("Quest ID");
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            packet.ReadBit("bit1C");
            guid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            packet.ParseBitStream(guid, 0, 5, 3, 2, 4, 6, 1, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE)]
        public static void HandleQuestCompleted510(Packet packet)
        {
            packet.ReadInt32("RewSkillId");
            packet.ReadInt32<QuestId>("Quest ID");
            packet.ReadInt32("Talent Points");
            packet.ReadInt32("RewSkillPoints");
            packet.ReadInt32("Money");
            packet.ReadInt32("XP");
            packet.ReadBit("Unk Bit 1"); // if true EVENT_QUEST_FINISHED is fired, target cleared and gossip window is open
            packet.ReadBit("Unk Bit 2");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_ACCEPT_QUEST)]
        public static void HandleQuestgiverAcceptQuest(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadUInt32<QuestId>("Quest ID");
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("bit18");
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_REQUEST_REWARD)]
        public static void HandleQuestRequestReward(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadUInt32<QuestId>("Quest ID");

            packet.StartBitStream(guid, 4, 1, 7, 0, 3, 2, 6, 5);
            packet.ParseBitStream(guid, 7, 2, 6, 4, 3, 1, 5, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS)]
        public static void HandleQuestgiverDetails(Packet packet)
        {
            packet.ReadInt32("QuestGiver Portrait?");
            packet.ReadInt32("Reward Item Id?"); // SMSG_ITEM_PUSH_RESULT is sent with this id in sniffs

            for (var i = 0; i < 5; i++)
            {
                packet.ReadUInt32("Reputation Value Id", i);
                packet.ReadUInt32("Reputation Faction", i);
                packet.ReadInt32("Reputation Value", i);
            }

            packet.ReadInt32("Quest XP");

            for (var i = 0; i < 4; i++)
            {
                packet.ReadUInt32("Currency Id", i);
                packet.ReadUInt32("Currency Count", i);
            }

            packet.ReadInt32("unk1");
            packet.ReadInt32("unk2");
            packet.ReadInt32("unk3");
            packet.ReadUInt32<QuestId>("Quest ID");
            packet.ReadInt32("unk5");
            packet.ReadInt32("unk6");
            packet.ReadInt32("unk7");
            packet.ReadInt32("unk8");
            packet.ReadInt32("unk9");
            packet.ReadInt32("unk10");
            packet.ReadUInt32E<QuestFlags2>("Quest Flags 2");
            packet.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.ReadInt32("unk13");
            packet.ReadInt32("unk14");
            packet.ReadInt32("Money");
            packet.ReadInt32("unk16");
            packet.ReadInt32("unk17");
            packet.ReadInt32("unk18");
            packet.ReadInt32("unk19");
            packet.ReadInt32("unk20");
            packet.ReadInt32("unk21");
            packet.ReadInt32("unk22");
            packet.ReadInt32("unk23");
            packet.ReadInt32("unk24");
            packet.ReadInt32("unk25");
            packet.ReadInt32("unk26");
            packet.ReadInt32<SpellId>("Spell Cast Id");
            packet.ReadInt32("unk28");
            packet.ReadInt32("unk29");
            packet.ReadInt32("unk30");
            packet.ReadInt32("unk31");
            packet.ReadInt32("unk32");
            packet.ReadInt32("unk33");
            packet.ReadInt32("unk34");
            packet.ReadInt32("unk35");
            packet.ReadInt32("unk36");
            packet.ReadInt32("unk37");
            packet.ReadInt32("unk38");
            packet.ReadInt32("unk39");
            packet.ReadInt32("unk40");
            packet.ReadInt32("unk41");
            packet.ReadInt32("unk42");
            packet.ReadInt32("unk43");
            packet.ReadInt32("unk44");
            packet.ReadInt32("unk45");

            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[5] = packet.ReadBit();

            var str3len = (int)packet.ReadBits(8);
            var str1len = (int)packet.ReadBits(8);

            var counter5 = (int)packet.ReadBits(22);

            var bit3454 = !packet.ReadBit("!bit0");
            guid2[1] = packet.ReadBit();

            var str6len = (int)packet.ReadBits(12);
            guid2[0] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[1] = packet.ReadBit();

            var str5len = (int)packet.ReadBits(10);
            var counter3 = (int)packet.ReadBits(21);
            var counter4 = (int)packet.ReadBits(20);

            guid1[3] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            var bit11234 = !packet.ReadBit("!bit1");
            guid1[2] = packet.ReadBit();
            guid2[2] = packet.ReadBit();

            var str0len = (int)packet.ReadBits(10);//ok

            var str2len = (int)packet.ReadBits(9);//ok

            var str4len = (int)packet.ReadBits(12);//ok was 1

            guid2[4] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            var bit34354 = !packet.ReadBit("!bit2");
            guid2[6] = packet.ReadBit();
            guid2[7] = packet.ReadBit();

            for (var i = 0; i < counter3; i++) // 3308
            {
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
            }

            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadWoWString("string0", str0len);

            for (var i = 0; i < counter4; i++)
            {
                packet.ReadUInt32("Required Count", i);
                var entry = packet.ReadUInt32();
                var type = packet.ReadByte("Type", i);
                switch (type)
                {
                    case 0: packet.AddValue("Required NPC", entry, i); break;
                    case 1: packet.AddValue("Required Item", entry, i); break;
                    default: packet.AddValue("Required Entry", entry, i); break;
                }
                packet.ReadInt32("unk", i);
            }

            packet.ReadXORByte(guid2, 7);
            packet.ReadWoWString("QuestGiver Target Name", str1len);
            packet.ReadWoWString("Title", str2len);
            packet.ReadWoWString("string3", str3len);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 5);

            for (int i = 0; i < counter5; i++)
                packet.ReadInt32("Int", i);

            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 4);
            packet.ReadWoWString("Objectives", str4len);
            packet.ReadWoWString("string5", str5len);
            packet.ReadWoWString("Details", str6len);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 0);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE)]
        public static void HandleQuestOfferReward(Packet packet)
        {
            var guid1 = new byte[8];

            var strlen = packet.ReadBits(10);
            guid1[6] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            var len2 = packet.ReadBits(21);
            var len3 = packet.ReadBits(8);
            guid1[4] = packet.ReadBit();
            var len4 = packet.ReadBits(8);
            var len5 = packet.ReadBits(12);
            guid1[5] = packet.ReadBit();
            var bit4312 = !packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            var len6 = packet.ReadBits(9);
            var len7 = packet.ReadBits(10);
            guid1[7] = packet.ReadBit();

            packet.ReadInt32("unk1");
            packet.ReadInt32("unk2");
            packet.ReadInt32("unk3");


            for (var i = 0; i < 4; i++)
            {
                packet.ReadUInt32("Currency Id", i);
                packet.ReadUInt32("Currency Count", i);
            }

            packet.ReadWoWString("str1", strlen);

            packet.ReadInt32("unk4");
            packet.ReadUInt32("Money");
            packet.ReadInt32("unk6");
            packet.ReadInt32("unk7");
            packet.ReadInt32("unk8");
            packet.ReadInt32("unk9");
            packet.ReadInt32("unk10");

            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 2);

            packet.ReadInt32("unk11");

            for (var i = 0; i < 5; i++)
            {
                packet.ReadUInt32("Reputation Faction", i);
                packet.ReadUInt32("Reputation Value Id", i);
                packet.ReadInt32("Reputation Value (x100)", i);
            }


            packet.ReadInt32("unk12");
            packet.ReadInt32("unk13");
            packet.ReadWoWString("str2", len3);
            packet.ReadInt32("unk14");
            packet.ReadInt32("unk15");
            packet.ReadWoWString("Completion Text", len5);
            packet.ReadInt32("unk16");
            packet.ReadInt32("unk17");
            packet.ReadInt32("unk18");
            packet.ReadWoWString("str4", len4);
            packet.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.ReadInt32("unk20");
            packet.ReadInt32("unk21");
            packet.ReadInt32("unk22");
            packet.ReadInt32("unk23");
            packet.ReadInt32("unk24");
            packet.ReadWoWString("str5", len7);
            packet.ReadInt32("unk25");
            packet.ReadInt32("unk26");
            packet.ReadInt32("unk27");

            for (var i = 0; i < len2; i++) // len2 is guessed
            {
                packet.ReadUInt32E<EmoteType>("Emote Id", i);
                packet.ReadUInt32("Emote Delay", i);
            }
            packet.ReadInt32("unk28");
            packet.ReadXORByte(guid1, 1);

            packet.ReadInt32("unk29");
            packet.ReadUInt32<QuestId>("Quest ID");
            packet.ReadInt32("unk31");
            packet.ReadInt32("unk32");
            packet.ReadInt32("unk33");
            packet.ReadInt32("unk34");
            packet.ReadInt32("unk35");
            packet.ReadInt32("unk36");
            packet.ReadInt32("unk37");
            packet.ReadInt32("unk38");
            packet.ReadXORByte(guid1, 0);
            packet.ReadInt32("unk39");
            packet.ReadXORByte(guid1, 4);
            packet.ReadInt32("unk40");
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 5);
            packet.ReadInt32("unk41");
            packet.ReadInt32("unk42");
            packet.ReadInt32("unk43");
            packet.ReadWoWString("Title", len6);
            packet.ReadInt32("unk44");
            packet.ReadInt32("unk45");
            packet.ReadUInt32("XP");
            packet.ReadInt32("unk47");
            packet.ReadInt32("unk48");
            packet.ReadInt32("unk49");


            packet.WriteGuid("Guid1", guid1);

            /*




            // --------------------


            packet.ReadGuid("GUID");
            var entry = packet.ReadUInt32<QuestId>("Quest ID");
            packet.ReadCString("Title");
            var text = packet.ReadCString("Text");

            Storage.QuestOffers.Add((uint)entry, new QuestOffer { OfferRewardText = text }, null);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.ReadCString("QuestGiver Text Window");
                packet.ReadCString("QuestGiver Target Name");
                packet.ReadCString("QuestTurn Text Window");
                packet.ReadCString("QuestTurn Target Name");
                packet.ReadUInt32("QuestGiverPortrait");
                packet.ReadUInt32("QuestTurnInPortrait");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                packet.ReadBool("Auto Finish");
            else
                packet.ReadBool("Auto Finish", TypeCode.Int32);


            packet.ReadUInt32("Suggested Players");

            var count1 = packet.ReadUInt32("Emote Count");
            for (var i = 0; i < count1; i++)
            {
                packet.ReadUInt32("Emote Delay", i);
                packet.ReadUInt32E<EmoteType>("Emote Id", i);
            }

            // extra info

            packet.ReadUInt32("Choice Item Count");
            for (var i = 0; i < 6; i++)
            {
                packet.ReadUInt32<ItemId>("Choice Item Id", i);
                packet.ReadUInt32("Choice Item Count", i);
                packet.ReadUInt32("Choice Item Display Id", i);
            }

            packet.ReadUInt32("Reward Item Count");

            for (var i = 0; i < 4; i++)
                packet.ReadUInt32<ItemId>("Reward Item Id", i);
            for (var i = 0; i < 4; i++)
                packet.ReadUInt32("Reward Item Count", i);
            for (var i = 0; i < 4; i++)
                packet.ReadUInt32("Reward Item Display Id", i);



            packet.ReadUInt32("Title Id");
            packet.ReadUInt32("Bonus Talents");
            packet.ReadUInt32("Reward Reputation Mask");


            packet.ReadInt32<SpellId>("Spell Id");
            packet.ReadInt32<SpellId>("Spell Cast Id");


            packet.ReadUInt32("Reward SkillId");
            packet.ReadUInt32("Reward Skill Points");*/
        }


    }
}
