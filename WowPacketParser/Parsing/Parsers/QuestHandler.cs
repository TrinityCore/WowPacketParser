using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;


namespace WowPacketParser.Parsing.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.CMSG_QUEST_QUERY)]
        public static void HandleQuestQuery(Packet packet)
        {
            var entry = packet.ReadInt32();
            Console.WriteLine("Entry: " + entry);
        }

        [Parser(Opcode.SMSG_QUEST_QUERY_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            var id = packet.ReadInt32();
            Console.WriteLine("Entry: " + id);

            var method = (QuestMethod)packet.ReadInt32();
            Console.WriteLine("Method: " + method);

            var level = packet.ReadInt32();
            Console.WriteLine("Level: " + level);

            var minLevel = packet.ReadInt32();
            Console.WriteLine("Min Level: " + minLevel);

            var sort = (QuestSort)packet.ReadInt32();
            Console.WriteLine("Sort: " + sort);

            var type = (QuestType)packet.ReadInt32();
            Console.WriteLine("Type: " + type);

            var players = packet.ReadInt32();
            Console.WriteLine("Suggested Players: " + players);

            var factId = new int[2];
            var factRep = new int[2];
            for (var i = 0; i < 2; i++)
            {
                factId[i] = packet.ReadInt32();
                Console.WriteLine("Required Faction ID " + i + ": " + factId[i]);

                factRep[i] = packet.ReadInt32();
                Console.WriteLine("Required Faction Rep " + i + ": " + factRep[i]);
            }

            var nextQuest = packet.ReadInt32();
            Console.WriteLine("Next Chain Quest: " + nextQuest);

            var xpId = packet.ReadInt32();
            Console.WriteLine("Quest XP ID: " + xpId);

            var rewReqMoney = packet.ReadInt32();
            Console.WriteLine("Reward/Required Money: " + rewReqMoney);

            var rewMoneyMaxLvl = packet.ReadInt32();
            Console.WriteLine("Reward Money Max Level: " + rewMoneyMaxLvl);

            var rewSpell = packet.ReadInt32();
            Console.WriteLine("Reward Spell: " + Extensions.SpellLine(rewSpell));

            var rewSpellCast = packet.ReadInt32();
            Console.WriteLine("Reward Spell Cast: " + Extensions.SpellLine(rewSpellCast));

            var rewHonor = packet.ReadInt32();
            Console.WriteLine("Reward Honor: " + rewHonor);

            var rewHonorBonus = packet.ReadSingle();
            Console.WriteLine("Reward Honor Multiplier: " + rewHonorBonus);

            var srcItemId = packet.ReadInt32();
            Console.WriteLine("Source Item ID: " + srcItemId);

            var flags = (QuestFlag)(packet.ReadInt32() | 0xFFFF);
            Console.WriteLine("Flags: " + flags);

            var titleId = packet.ReadInt32();
            Console.WriteLine("Title ID: " + titleId);

            var reqPlayerKills = packet.ReadInt32();
            Console.WriteLine("Required Player Kills: " + reqPlayerKills);

            var bonusTalents = packet.ReadInt32();
            Console.WriteLine("Bonus Talents: " + bonusTalents);

            var bonusArenaPoints = packet.ReadInt32();
            Console.WriteLine("Bonus Arena Points: " + bonusArenaPoints);

            var bonusUnk = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + bonusUnk);

            var rewItemId = new int[4];
            var rewItemCnt = new int[4];
            for (var i = 0; i < 4; i++)
            {
                rewItemId[i] = packet.ReadInt32();
                Console.WriteLine("Reward Item ID " + i + ": " + rewItemId[i]);

                rewItemCnt[i] = packet.ReadInt32();
                Console.WriteLine("Reward Item Count " + i + ": " + rewItemCnt[i]);
            }

            var rewChoiceItemId = new int[6];
            var rewChoiceItemCnt = new int[6];
            for (var i = 0; i < 6; i++)
            {
                rewChoiceItemId[i] = packet.ReadInt32();
                Console.WriteLine("Reward Choice Item ID " + i + ": " + rewChoiceItemId[i]);

                rewChoiceItemCnt[i] = packet.ReadInt32();
                Console.WriteLine("Reward Choice Item Count " + i + ": " + rewChoiceItemCnt[i]);
            }

            var rewFactionId = new int[5];
            for (var i = 0; i < 5; i++)
            {
                rewFactionId[i] = packet.ReadInt32();
                Console.WriteLine("Reward Faction ID " + i + ": " + rewFactionId[i]);
            }

            var rewRepIdx = new int[5];
            for (var i = 0; i < 5; i++)
            {
                rewRepIdx[i] = packet.ReadInt32();
                Console.WriteLine("Reward Reputation ID " + i + ": " + rewRepIdx[i]);
            }

            var rewRepOverride = new int[5];
            for (var i = 0; i < 5; i++)
            {
                rewRepOverride[i] = packet.ReadInt32();
                Console.WriteLine("Reward Rep Override " + i + ": " + rewRepOverride[i]);
            }

            var pointMap = packet.ReadInt32();
            Console.WriteLine("Point Map ID: " + pointMap);

            var pointX = packet.ReadSingle();
            Console.WriteLine("Point X: " + pointX);

            var pointY = packet.ReadSingle();
            Console.WriteLine("Point Y: " + pointY);

            var pointOpt = packet.ReadInt32();
            Console.WriteLine("Point Opt: " + pointOpt);

            var title = packet.ReadCString();
            Console.WriteLine("Title: " + title);

            var objectives = packet.ReadCString();
            Console.WriteLine("Objectives: " + objectives);

            var details = packet.ReadCString();
            Console.WriteLine("Details: " + details);

            var endText = packet.ReadCString();
            Console.WriteLine("End Text: " + endText);

            var returnText = packet.ReadCString();
            Console.WriteLine("Return Text: " + returnText);

            var reqId = new KeyValuePair<int, bool>[4];
            var reqCnt = new int[4];
            var srcId = new int[4];
            var srcCnt = new int[4];
            for (var i = 0; i < 4; i++)
            {
                reqId[i] = packet.ReadEntry();
                Console.WriteLine("Required " + (reqId[i].Value ? "GO" : "NPC") +
                    " ID " + i + ": " + reqId[i].Key);

                reqCnt[i] = packet.ReadInt32();
                Console.WriteLine("Required Count: " + i + ": " + reqCnt[i]);

                srcId[i] = packet.ReadInt32();
                Console.WriteLine("Source ID: " + i + ": " + srcId[i]);

                srcCnt[i] = packet.ReadInt32();
                Console.WriteLine("Source Count: " + i + ": " + srcCnt[i]);
            }

            var reqItemId = new int[6];
            var reqItemCnt = new int[6];
            for (var i = 0; i < 6; i++)
            {
                reqItemId[i] = packet.ReadInt32();
                Console.WriteLine("Required Item ID " + i + ": " + reqItemId[i]);

                reqItemCnt[i] = packet.ReadInt32();
                Console.WriteLine("Required Item Count: " + i + ": " + reqItemCnt[i]);
            }

            var objectiveText = new string[4];
            for (var i = 0; i < 4; i++)
            {
                objectiveText[i] = packet.ReadCString();
                Console.WriteLine("Objective Text " + i + ": " + objectiveText[i]);
            }

            SQLStore.WriteData(SQLStore.Quests.GetCommand(id, method, level, minLevel, sort, type,
                players, factId, factRep, nextQuest, xpId, rewReqMoney, rewMoneyMaxLvl,
                rewSpell, rewSpellCast, rewHonor, rewHonorBonus, srcItemId, flags, titleId,
                reqPlayerKills, bonusTalents, bonusArenaPoints, bonusUnk, rewItemId, rewItemCnt,
                rewChoiceItemId, rewChoiceItemCnt, rewFactionId, rewRepIdx, rewRepOverride,
                pointMap, pointX, pointY, pointOpt, title, objectives, details, endText,
                returnText, reqId, reqCnt, srcId, srcCnt, reqItemId, reqItemCnt, objectiveText));
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            var count = packet.ReadInt32();
            Console.WriteLine("Count: " + count);

            for (var i = 0; i < count; i++)
            {
                var questId = packet.ReadInt32();
                Console.WriteLine("Quest ID " + i + ": " + questId);
            }
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            var count = packet.ReadInt32();
            Console.WriteLine("Count: " + count);

            for (var i = 0; i < count; i++)
            {
                var questId = packet.ReadInt32();
                Console.WriteLine("Quest ID " + i + ": " + questId);

                var poiSize = packet.ReadInt32();
                Console.WriteLine("POI Size " + i + ": " + poiSize);

                for (var j = 0; j < poiSize; j++)
                {
                    var idx = packet.ReadInt32();
                    Console.WriteLine("POI Index " + j + ": " + idx);

                    var objIndex = packet.ReadInt32();
                    Console.WriteLine("Objective Index " + j + ": " + objIndex);

                    var mapId = packet.ReadInt32();
                    Console.WriteLine("Map ID " + j + ": " + mapId);

                    var wmaId = packet.ReadInt32();
                    Console.WriteLine("World Map Area " + j + ": " + wmaId);

                    var unk2 = packet.ReadInt32();
                    Console.WriteLine("Floor ID " + j + ": " + unk2);

                    var unk3 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 2 " + j + ": " + unk3);

                    var unk4 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 3 " + j + ": " + unk4);

                    SQLStore.WriteData(SQLStore.QuestPois.GetCommand(questId, idx, objIndex, mapId, wmaId,
                        unk2, unk3, unk4));

                    var pointsSize = packet.ReadInt32();
                    Console.WriteLine("Point Size " + j + ": " + pointsSize);

                    for (var k = 0; k < pointsSize; k++)
                    {
                        var pointX = packet.ReadInt32();
                        Console.WriteLine("Point X " + k + ": " + pointX);

                        var pointY = packet.ReadInt32();
                        Console.WriteLine("Point Y " + k + ": " + pointY);

                        SQLStore.WriteData(SQLStore.QuestPoiPoints.GetCommand(questId, idx, objIndex, pointX,
                            pointY));
                    }
                }
            }
        }

        [Parser(Opcode.SMSG_QUEST_FORCE_REMOVE)]
        public static void HandleQuestForceRemoved(Packet packet)
        {
            var questId = packet.ReadInt32();
            Console.WriteLine("Quest ID: " + questId);
        }

        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_COMPLETE)]
        public static void HandleQuestCompleted(Packet packet)
        {
            var questId = packet.ReadInt32();
            Console.WriteLine("Quest ID: " + questId);

            Console.WriteLine("Reward:");
            var xp = packet.ReadInt32();
            Console.WriteLine("XP: " + xp);

            var money = packet.ReadInt32();
            Console.WriteLine("Money: " + money);

            var honor = packet.ReadInt32();
            if (honor < 0)
                Console.WriteLine("Honor: " + honor);

            var talentpoints = packet.ReadInt32();
            if (talentpoints < 0)
                Console.WriteLine("Talentpoints: " + talentpoints);

            var arenapoints = packet.ReadInt32();
            if (arenapoints < 0)
                Console.WriteLine("Arenapoints: " + arenapoints);
        }

        [Parser(Opcode.SMSG_QUERY_QUESTS_COMPLETED_RESPONSE)]
        public static void HandleQuestCompletedResponse(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            // Prints ~4k lines of quest IDs, should be DEBUG only or something...
            /*
            for (var i = 0; i < count; i++)
                packet.ReadInt32("[" + i + "] Rewarded Quest");
            */
            Console.WriteLine("Packet is currently not printed");
            packet.ReadBytes((int)packet.GetLength());
        }
    }
}
