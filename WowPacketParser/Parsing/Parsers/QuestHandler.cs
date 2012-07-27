using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class QuestHandler
    {
        private static void ReadExtraQuestInfo(ref Packet packet, bool readFlags = true)
        {
            var choiceCount = packet.ReadUInt32("Choice Item Count");
            var effectiveChoiceCount = ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164) ? 6 : choiceCount;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                for (var i = 0; i < effectiveChoiceCount; i++)
                    packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Choice Item Id", i);
                for (var i = 0; i < effectiveChoiceCount; i++)
                    packet.ReadUInt32("Choice Item Count", i);
                for (var i = 0; i < effectiveChoiceCount; i++)
                    packet.ReadUInt32("Choice Item Display Id", i);

                packet.ReadUInt32("Reward Item Count");
                const int effectiveRewardCount = 4;

                for (var i = 0; i < effectiveRewardCount; i++)
                    packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Reward Item Id", i);
                for (var i = 0; i < effectiveRewardCount; i++)
                    packet.ReadUInt32("Reward Item Count", i);
                for (var i = 0; i < effectiveRewardCount; i++)
                    packet.ReadUInt32("Reward Item Display Id", i);
            }
            else
            {
                for (var i = 0; i < choiceCount; i++)
                {
                    packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Choice Item Id", i);
                    packet.ReadUInt32("Choice Item Count", i);
                    packet.ReadUInt32("Choice Item Display Id", i);
                }

                var rewardCount = packet.ReadUInt32("Reward Item Count");
                for (var i = 0; i < rewardCount; i++)
                {
                    packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Reward Item Id", i);
                    packet.ReadUInt32("Reward Item Count", i);
                    packet.ReadUInt32("Reward Item Display Id", i);
                }
            }

            packet.ReadUInt32("Money");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_2_10482))
                packet.ReadUInt32("XP");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.ReadUInt32("Title Id");
                packet.ReadUInt32("Unknown UInt32");
                packet.ReadSingle("Unknown float");
                packet.ReadUInt32("Bonus Talents");
                packet.ReadUInt32("Unknown UInt32");
                packet.ReadUInt32("Unknown UInt32");
            }
            else
            {
                packet.ReadUInt32("Honor Points");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                    packet.ReadSingle("Honor Multiplier");

                if (readFlags)
                        packet.ReadEnum<QuestFlags>("Quest Flags", TypeCode.UInt32);

                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell Id");
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell Cast Id");
                packet.ReadUInt32("Title Id");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                    packet.ReadUInt32("Bonus Talents");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                {
                    packet.ReadUInt32("Arena Points");
                    packet.ReadUInt32("Unk UInt32");
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
            {
                for (var i = 0; i < 5; i++)
                    packet.ReadUInt32("Reputation Faction", i);

                for (var i = 0; i < 5; i++)
                    packet.ReadUInt32("Reputation Value Id", i);

                for (var i = 0; i < 5; i++)
                    packet.ReadInt32("Reputation Value", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell Id");
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell Cast Id");

                for (var i = 0; i < 4; i++)
                    packet.ReadUInt32("Currency Id", i);
                for (var i = 0; i < 4; i++)
                    packet.ReadUInt32("Currency Count", i);

                packet.ReadUInt32("Unknown UInt32");
                packet.ReadUInt32("Unknown UInt32");
            }
        }

        [Parser(Opcode.CMSG_QUEST_QUERY)]
        [Parser(Opcode.CMSG_PUSHQUESTTOPARTY)]
        public static void HandleQuestQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
        }

        [Parser(Opcode.SMSG_QUEST_QUERY_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            var id = packet.ReadEntry("Quest ID");
            if (id.Value) // entry is masked
                return;

            var quest = new QuestTemplate
            {
                Method = packet.ReadEnum<QuestMethod>("Method", TypeCode.Int32),
                Level = packet.ReadInt32("Level")
            };

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.MinLevel = packet.ReadInt32("Min Level");

            quest.ZoneOrSort = packet.ReadEnum<QuestSort>("Sort", TypeCode.Int32);

            quest.Type = packet.ReadEnum<QuestType>("Type", TypeCode.Int32);

            quest.SuggestedPlayers = packet.ReadUInt32("Suggested Players");

            quest.RequiredFactionId = new uint[2];
            quest.RequiredFactionValue = new int[2];
            for (var i = 0; i < 2; i++)
            {
                quest.RequiredFactionId[i] = packet.ReadUInt32("Required Faction ID", i);
                quest.RequiredFactionValue[i] = packet.ReadInt32("Required Faction Rep", i);
            }

            quest.NextQuestIdChain = (uint) packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Next Chain Quest");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.RewardXPId = packet.ReadUInt32("Quest XP ID");

            quest.RewardOrRequiredMoney = packet.ReadInt32("Reward/Required Money");

            quest.RewardMoneyMaxLevel = packet.ReadUInt32("Reward Money Max Level");

            quest.RewardSpell = (uint) packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Reward Spell");

            quest.RewardSpellCast = packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Reward Spell Cast");

            quest.RewardHonor = packet.ReadInt32("Reward Honor");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.RewardHonorMultiplier = packet.ReadSingle("Reward Honor Multiplier");

            quest.SourceItemId = (uint) packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Source Item ID");

            quest.Flags = packet.ReadEnum<QuestFlags>("Flags", TypeCode.Int32);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                quest.MinimapTargetMark = packet.ReadUInt32("Minimap Target Mark"); // missing enum. 1- Skull, 16 - Unknown, but exist

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_4_0_8089))
                quest.RewardTitleId = packet.ReadUInt32("Reward Title ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                quest.RequiredPlayerKills = packet.ReadUInt32("Required Player Kills");
                quest.RewardTalents = packet.ReadUInt32("Bonus Talents");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.RewardArenaPoints = packet.ReadUInt32("Bonus Arena Points");

            // TODO: Find when was this added/removed and what is it
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958) && (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_1_13164)))
                packet.ReadInt32("Unknown Int32");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                quest.RewardSkillId = packet.ReadUInt32("RewSkillId");
                quest.RewardSkillPoints = packet.ReadUInt32("RewSkillPoints");
                quest.RewardReputationMask = packet.ReadUInt32("RewRepMask");
                quest.QuestGiverPortrait = packet.ReadUInt32("QuestGiverPortrait");
                quest.QuestTurnInPortrait = packet.ReadUInt32("QuestTurnInPortrait");
            }

            quest.RewardItemId = new uint[4];
            quest.RewardItemCount = new uint[4];
            for (var i = 0; i < 4; i++)
            {
                quest.RewardItemId[i] = (uint) packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Reward Item ID", i);
                quest.RewardItemCount[i] = packet.ReadUInt32("Reward Item Count", i);
            }

            quest.RewardChoiceItemId = new uint[6];
            quest.RewardChoiceItemCount = new uint[6];
            for (var i = 0; i < 6; i++)
            {
                quest.RewardChoiceItemId[i] = (uint) packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Reward Choice Item ID", i);
                quest.RewardChoiceItemCount[i] = packet.ReadUInt32("Reward Choice Item Count", i);
            }

            const int repCount = 5;
            quest.RewardFactionId = new uint[repCount];
            quest.RewardFactionValueId = new int[repCount];
            quest.RewardFactionValueIdOverride = new uint[repCount];
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
            {
                for (var i = 0; i < repCount; i++)
                    quest.RewardFactionId[i] = packet.ReadUInt32("Reward Faction ID", i);

                for (var i = 0; i < repCount; i++)
                    quest.RewardFactionValueId[i] = packet.ReadInt32("Reward Reputation ID", i);

                for (var i = 0; i < repCount; i++)
                    quest.RewardFactionValueIdOverride[i] = packet.ReadUInt32("Reward Reputation ID Override", i);
            }

            quest.PointMapId = packet.ReadUInt32("Point Map ID");

            quest.PointX = packet.ReadSingle("Point X");

            quest.PointY = packet.ReadSingle("Point Y");

            quest.PointOption = packet.ReadUInt32("Point Opt");

            quest.Title = packet.ReadCString("Title");

            quest.Objectives = packet.ReadCString("Objectives");

            quest.Details = packet.ReadCString("Details");

            quest.EndText = packet.ReadCString("End Text");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.CompletedText = packet.ReadCString("Completed Text");

            var reqId = new KeyValuePair<int, bool>[4];
            quest.RequiredNpcOrGo = new int[4];
            quest.RequiredNpcOrGoCount = new uint[4];
            quest.RequiredSourceItemId = new uint[4];
            quest.RequiredSourceItemCount = new uint[4];
            var reqItemFieldCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464) ? 6 : 4;
            quest.RequiredItemId = new uint[reqItemFieldCount];
            quest.RequiredItemCount = new uint[reqItemFieldCount];

            for (var i = 0; i < 4; i++)
            {
                reqId[i] = packet.ReadEntry();
                quest.RequiredNpcOrGo[i] = reqId[i].Key;
                var isGo = reqId[i].Value;

                packet.WriteLine("[" + i + "] Required " + (isGo ? "GO" : "NPC") +
                    " ID: " + StoreGetters.GetName(isGo ? StoreNameType.GameObject : StoreNameType.Unit, reqId[i].Key));

                quest.RequiredNpcOrGoCount[i] = packet.ReadUInt32("Required Count", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                    quest.RequiredSourceItemId[i] = (uint) packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Required Source Item ID", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                    quest.RequiredSourceItemCount[i] = packet.ReadUInt32("Source Item Count", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_8_9464))
                {
                    quest.RequiredItemId[i] = (uint) packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Required Item ID", i);
                    quest.RequiredItemCount[i] = packet.ReadUInt32("Required Item Count", i);
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
            {
                for (var i = 0; i < reqItemFieldCount; i++)
                {
                    quest.RequiredItemId[i] = (uint) packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Required Item ID", i);
                    quest.RequiredItemCount[i] = packet.ReadUInt32("Required Item Count", i);
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                quest.RequiredSpell = (uint) packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Required Spell");

            quest.ObjectiveText = new string[4];
            for (var i = 0; i < 4; i++)
                quest.ObjectiveText[i] = packet.ReadCString("Objective Text", i);

            quest.RewardCurrencyId = new uint[4];
            quest.RewardCurrencyCount = new uint[4];
            quest.RequiredCurrencyId = new uint[4];
            quest.RequiredCurrencyCount = new uint[4];
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                for (var i = 0; i < 4; ++i)
                {
                    quest.RewardCurrencyId[i] = packet.ReadUInt32("Reward Currency ID", i);
                    quest.RewardCurrencyCount[i] = packet.ReadUInt32("Reward Currency Count", i);
                }

                for (var i = 0; i < 4; ++i)
                {
                    quest.RequiredCurrencyId[i] = packet.ReadUInt32("Required Currency ID", i);
                    quest.RequiredCurrencyCount[i] = packet.ReadUInt32("Required Currency Count", i);
                }

                quest.QuestGiverTextWindow = packet.ReadCString("QuestGiver Text Window");
                quest.QuestGiverTargetName = packet.ReadCString("QuestGiver Target Name");
                quest.QuestTurnTextWindow = packet.ReadCString("QuestTurn Text Window");
                quest.QuestTurnTargetName = packet.ReadCString("QuestTurn Target Name");

                quest.SoundAccept = packet.ReadUInt32("Sound Accept");
                quest.SoundTurnIn = packet.ReadUInt32("Sound TurnIn");
            }

            packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");

            Storage.QuestTemplates.Add((uint) id.Key, quest, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        [Parser(Opcode.CMSG_QUEST_NPC_QUERY)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            int count;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
            {
                if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_QUEST_NPC_QUERY))
                {
                    packet.ReadByte("unk");
                    packet.ReadByte("unk2");
                    count = packet.ReadByte("Count");
                }
                else
                {
                    count = packet.ReadByte("Count");
                    packet.ReadByte("unk");
                    packet.ReadByte("unk2");
                    packet.ReadByte("unk3");
                }
            }
            else
                count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_NPC_QUERY_RESPONSE)]
        public static void HandleQuestNpcQueryResponse(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                var count2 = packet.ReadUInt32("Number of NPC", i);
                for (var j = 0; j < count2; ++j)
                {
                    var entry = packet.ReadEntry();
                    packet.WriteLine("[{0}] [{1}] {2}: {3}", i, j, entry.Value ? "Creature" : "GameObject",
                        StoreGetters.GetName(entry.Value ? StoreNameType.GameObject : StoreNameType.Unit, entry.Key));
                }
            }

            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                var questId = packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);

                var counter = packet.ReadInt32("POI Counter", i);
                for (var j = 0; j < counter; j++)
                {
                    var questPoi = new QuestPOI();

                    var idx = packet.ReadInt32("POI Index", i, j);
                    questPoi.ObjectiveIndex = packet.ReadInt32("Objective Index", i, j);

                    questPoi.Map = packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id", i);
                    questPoi.WorldMapAreaId = packet.ReadInt32("World Map Area", i, j);
                    questPoi.FloorId = packet.ReadInt32("Floor Id", i, j);
                    questPoi.UnkInt1 = packet.ReadInt32("Unk Int32 2", i, j);
                    questPoi.UnkInt2 = packet.ReadInt32("Unk Int32 3", i, j);

                    var pointsSize = packet.ReadInt32("Points Counter", i, j);
                    questPoi.Points = new List<QuestPOIPoint>(pointsSize);
                    for (var k = 0; k < pointsSize; k++)
                    {
                        var questPoiPoint = new QuestPOIPoint
                                            {
                                                Index = k,
                                                X = packet.ReadInt32("Point X", i, j, k),
                                                Y = packet.ReadInt32("Point Y", i, j, k)
                                            };
                        questPoi.Points.Add(questPoiPoint);
                    }

                    Storage.QuestPOIs.Add(new Tuple<uint, uint>((uint) questId, (uint) idx), questPoi, packet.TimeSpan);
                }
            }
        }

        [Parser(Opcode.SMSG_QUEST_FORCE_REMOVE)]
        [Parser(Opcode.CMSG_QUEST_CONFIRM_ACCEPT)]
        [Parser(Opcode.SMSG_QUESTUPDATE_FAILED)]
        [Parser(Opcode.SMSG_QUESTUPDATE_FAILEDTIMER)]
        [Parser(Opcode.SMSG_QUESTUPDATE_COMPLETE, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_QUESTUPDATE_COMPLETE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestForceRemoved(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID");
        }

        [Parser(Opcode.SMSG_QUESTUPDATE_COMPLETE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestUpdateComplete422(Packet packet)
        {
            packet.ReadGuid("Guid");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID");
            packet.ReadCString("Title");
            packet.ReadCString("Complete Text");
            packet.ReadCString("QuestGiver Text Window");
            packet.ReadCString("QuestGiver Target Name");
            packet.ReadCString("QuestTurn Text Window");
            packet.ReadCString("QuestTurn Target Name");
            packet.ReadInt32("QuestGiver Portrait");
            packet.ReadInt32("QuestTurn Portrait");
            packet.ReadByte("Unk Byte");
            packet.ReadEnum<QuestFlags>("Quest Flags", TypeCode.UInt32);
            packet.ReadInt32("Unk Int32");
            var emoteCount = packet.ReadUInt32("Quest Emote Count");
            for (var i = 0; i < emoteCount; i++)
            {
                packet.ReadUInt32("Emote Id", i);
                packet.ReadUInt32("Emote Delay (ms)", i);
            }

            ReadExtraQuestInfo(ref packet);
        }

        [Parser(Opcode.SMSG_QUERY_QUESTS_COMPLETED_RESPONSE)]
        public static void HandleQuestCompletedResponse(Packet packet)
        {
            packet.ReadInt32("Count");
            // Prints ~4k lines of quest IDs, should be DEBUG only or something...
            /*
            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Rewarded Quest");
            */
            packet.WriteLine("Packet is currently not printed");
            packet.ReadBytes((int)packet.Length);
        }

        [Parser(Opcode.CMSG_QUESTGIVER_STATUS_QUERY)]
        [Parser(Opcode.CMSG_QUESTGIVER_HELLO)]
        [Parser(Opcode.CMSG_QUESTGIVER_QUEST_AUTOLAUNCH)]
        public static void HandleQuestgiverStatusQuery(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_LIST)]
        public static void HandleQuestgiverQuestList(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadCString("Title");
            packet.ReadUInt32("Delay");
            packet.ReadUInt32("Emote");

            var count = packet.ReadByte("Count");
            for (var i = 0; i < count; i++)
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID", i);
                packet.ReadUInt32("Quest Icon", i);
                packet.ReadInt32("Quest Level", i);
                packet.ReadEnum<QuestFlags>("Quest Flags", TypeCode.UInt32, i);
                packet.ReadBoolean("Change icon", i);
                packet.ReadCString("Title", i);
            }

        }

        [Parser(Opcode.CMSG_QUESTGIVER_QUERY_QUEST)]
        public static void HandleQuestgiverQueryQuest(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID");
            packet.ReadByte("Unknown Byte");
        }

        [Parser(Opcode.CMSG_QUESTGIVER_ACCEPT_QUEST)]
        public static void HandleQuestgiverAcceptQuest(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                packet.ReadUInt32("Unk UInt32");
        }

        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_DETAILS)]
        public static void HandleQuestgiverDetails(Packet packet)
        {
            packet.ReadGuid("GUID1");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadGuid("GUID2");

            packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID");
            packet.ReadCString("Title");
            packet.ReadCString("Details");
            packet.ReadCString("Objectives");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.ReadCString("QuestGiver Text Window");
                packet.ReadCString("QuestGiver Target Name");
                packet.ReadCString("QuestTurn Text Window");
                packet.ReadCString("QuestTurn Target Name");
                packet.ReadUInt32("QuestGiver Portrait");
                packet.ReadUInt32("QuestTurn Portrait");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                packet.ReadBoolean("Auto Accept");
            else
                packet.ReadBoolean("Auto Accept", TypeCode.Int32);

            var flags = QuestFlags.None;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                flags = packet.ReadEnum<QuestFlags>("Quest Flags", TypeCode.UInt32);

            packet.ReadUInt32("Suggested Players");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadByte("Unknown byte");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_3_3a_11723))
            {
                packet.ReadByte("Unk");
                packet.ReadByte("Unk");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.ReadByte("Start Type");
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Required Spell");
            }

            if (flags.HasAnyFlag(QuestFlags.HiddenRewards) && ClientVersion.RemovedInVersion(ClientVersionBuild.V3_3_5a_12340))
            {
                packet.ReadUInt32("Hidden Chosen Items");
                packet.ReadUInt32("Hidden Items");
                packet.ReadUInt32("Hidden Money");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_2_10482))
                    packet.ReadUInt32("Hidden XP");
            }

            ReadExtraQuestInfo(ref packet, false);

            var emoteCount = packet.ReadUInt32("Quest Emote Count");
            for (var i = 0; i < emoteCount; i++)
            {
                packet.ReadUInt32("Emote Id", i);
                packet.ReadUInt32("Emote Delay (ms)", i);
            }
        }

        [Parser(Opcode.CMSG_QUESTGIVER_COMPLETE_QUEST)]
        public static void HandleQuestCompleteQuest(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.ReadByte("Unk byte");
        }

        [Parser(Opcode.CMSG_QUESTGIVER_REQUEST_REWARD)]
        public static void HandleQuestRequestReward(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595)) // remove confirmed for 434
                packet.ReadUInt32("Unk UInt32");
        }

        [Parser(Opcode.SMSG_QUESTGIVER_REQUEST_ITEMS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestRequestItems(Packet packet)
        {
            packet.ReadGuid("GUID");
            var entry = packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID");
            packet.ReadCString("Title");
            var text = packet.ReadCString("Text");
            packet.ReadUInt32("Emote");
            packet.ReadUInt32("Unk UInt32 1");
            packet.ReadUInt32("Close Window on Cancel");

            Storage.QuestRewards.Add((uint) entry, new QuestReward {RequestItemsText = text}, null);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                packet.ReadEnum<QuestFlags>("Quest Flags", TypeCode.UInt32);

            packet.ReadUInt32("Suggested Players");
            packet.ReadUInt32("Money");

            var count = packet.ReadUInt32("Number of Required Items");
            for (var i = 0; i < count; i++)
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Required Item Id", i);
                packet.ReadUInt32("Required Item Count", i);
                packet.ReadUInt32("Required Item Display Id", i);
            }

            // flags
            packet.ReadUInt32("Unk flags 1");
            packet.ReadUInt32("Unk flags 2");
            packet.ReadUInt32("Unk flags 3");
            packet.ReadUInt32("Unk flags 4");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.ReadUInt32("Unk flags 5");
                packet.ReadUInt32("Unk flags 6");
            }
        }

        [Parser(Opcode.SMSG_QUESTGIVER_REQUEST_ITEMS,ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestRequestItems434(Packet packet)
        {
            packet.ReadGuid("GUID");
            var entry = packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID");
            packet.ReadCString("Title");
            var text = packet.ReadCString("Text");
            packet.ReadUInt32("Emote");
            packet.ReadUInt32("Unk UInt32 1");
            packet.ReadUInt32("Close Window on Cancel");

            Storage.QuestRewards.Add((uint)entry, new QuestReward { RequestItemsText = text }, null);

            packet.ReadEnum<QuestFlags>("Quest Flags", TypeCode.UInt32);

            packet.ReadUInt32("Suggested Players");
            packet.ReadUInt32("Money");

            var countItems = packet.ReadUInt32("Number of Required Items");
            for (var i = 0; i < countItems; i++)
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Required Item Id", i);
                packet.ReadUInt32("Required Item Count", i);
                packet.ReadUInt32("Required Item Display Id", i);
            }

            var countCurrencies = packet.ReadUInt32("Number of Required Currencies");
            for (var i = 0; i < countCurrencies; i++)
            {
                packet.ReadUInt32("Required Currency Id", i);
                packet.ReadUInt32("Required Currency Count", i);
            }

            // flags
            packet.ReadUInt32("Unk flags 1");
            packet.ReadUInt32("Unk flags 2");
            packet.ReadUInt32("Unk flags 3");
            packet.ReadUInt32("Unk flags 4");
            packet.ReadUInt32("Unk flags 5");
        }

        [Parser(Opcode.SMSG_QUESTGIVER_OFFER_REWARD)]
        public static void HandleQuestOfferReward(Packet packet)
        {
            packet.ReadGuid("GUID");
            var entry = packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID");
            packet.ReadCString("Title");
            var text = packet.ReadCString("Text");

            Storage.QuestOffers.Add((uint) entry, new QuestOffer {OfferRewardText = text}, null);

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
                packet.ReadBoolean("Auto Finish");
            else
                packet.ReadBoolean("Auto Finish", TypeCode.Int32);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                packet.ReadEnum<QuestFlags>("Quest Flags", TypeCode.UInt32);

            packet.ReadUInt32("Suggested Players");

            var count1 = packet.ReadUInt32("Emote Count");
            for (var i = 0; i < count1; i++)
            {
                packet.ReadUInt32("Emote Delay", i);
                packet.ReadEnum<EmoteType>("Emote Id", TypeCode.UInt32, i);
            }

            ReadExtraQuestInfo(ref packet);
        }

        [Parser(Opcode.CMSG_QUESTGIVER_CHOOSE_REWARD)]
        public static void HandleQuestChooseReward(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID");
            packet.ReadUInt32("Reward");
        }

        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_INVALID)]
        public static void HandleQuestInvalid(Packet packet)
        {
            packet.ReadEnum<QuestReasonType>("Reason", TypeCode.UInt32);
        }

        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_FAILED)]
        public static void HandleQuestFailed(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID");
            packet.ReadEnum<QuestReasonType>("Reason", TypeCode.UInt32);
        }

        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_COMPLETE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleQuestCompleted(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID");
            packet.ReadInt32("Reward");
            packet.ReadInt32("Money");
            var honor = packet.ReadInt32();
            if (honor < 0)
                packet.WriteLine("Honor: " + honor);

            var talentpoints = packet.ReadInt32();
            if (talentpoints < 0)
                packet.WriteLine("Talentpoints: " + talentpoints);

            var arenapoints = packet.ReadInt32();
            if (arenapoints < 0)
                packet.WriteLine("Arenapoints: " + arenapoints);
        }

        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_COMPLETE, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleQuestCompleted406(Packet packet)
        {
            packet.ReadBit("Unk");
            packet.ReadUInt32("Reward Skill Id");
            packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID");
            packet.ReadInt32("Money");
            packet.ReadInt32("Talent Points");
            packet.ReadUInt32("Reward Skill Points");
            packet.ReadInt32("Reward XP");
        }

        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_COMPLETE, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleQuestCompleted422(Packet packet)
        {
            packet.ReadByte("Unk Byte");
            packet.ReadInt32("Reward XP");
            packet.ReadInt32("Money");
            packet.ReadInt32("Reward Skill Points");
            packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID");
            packet.ReadInt32("Reward Skill Id");
            packet.ReadInt32("Unk5"); // Talent points?
        }

        [Parser(Opcode.CMSG_QUESTLOG_SWAP_QUEST)]
        public static void HandleQuestSwapQuest(Packet packet)
        {
            packet.ReadByte("Slot 1");
            packet.ReadByte("Slot 2");
        }

        [Parser(Opcode.CMSG_QUESTLOG_REMOVE_QUEST)]
        public static void HandleQuestRemoveQuest(Packet packet)
        {
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.SMSG_QUESTUPDATE_ADD_KILL)]
        [Parser(Opcode.SMSG_QUESTUPDATE_ADD_ITEM)]
        public static void HandleQuestUpdateAdd(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID");
            var entry = packet.ReadEntry();
            packet.WriteLine("Entry: " +
                StoreGetters.GetName(entry.Value ? StoreNameType.GameObject : StoreNameType.Unit, entry.Key));
            packet.ReadInt32("Count");
            packet.ReadInt32("Required Count");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_QUESTGIVER_STATUS)]
        [Parser(Opcode.SMSG_QUESTGIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatus(Packet packet)
        {
            uint count = 1;
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_QUESTGIVER_STATUS_MULTIPLE))
                count = packet.ReadUInt32("Count");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                for (var i = 0; i < count; i++)
                {
                    packet.ReadGuid("GUID", i);
                    packet.ReadEnum<QuestGiverStatus4x>("Status", TypeCode.Int32, i);
                }
            else
                for (var i = 0; i < count; i++)
                {
                    packet.ReadGuid("GUID", i);
                    packet.ReadEnum<QuestGiverStatus>("Status", TypeCode.Byte, i);
                }
        }

        [Parser(Opcode.SMSG_QUESTUPDATE_ADD_PVP_KILL)]
        public static void HandleQuestupdateAddPvpKill(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID");
            packet.ReadInt32("Count");
            packet.ReadInt32("Required Count");
        }

        [Parser(Opcode.SMSG_QUEST_CONFIRM_ACCEPT)]
        public static void HandleQuestConfirAccept(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID");
            packet.ReadCString("Title");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.MSG_QUEST_PUSH_RESULT)]
        public static void HandleQuestPushResult(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (packet.Direction == Direction.ClientToServer)
                packet.ReadUInt32("Quest Id");
            packet.ReadEnum<QuestPartyResult>("Result", TypeCode.Byte);
        }

        [Parser(Opcode.CMSG_QUERY_QUESTS_COMPLETED)]
        [Parser(Opcode.SMSG_QUESTLOG_FULL)]
        [Parser(Opcode.CMSG_QUESTGIVER_CANCEL)]
        [Parser(Opcode.CMSG_QUESTGIVER_STATUS_MULTIPLE_QUERY)]
        public static void HandleQuestZeroLengthPackets(Packet packet)
        {
        }

        //[Parser(Opcode.CMSG_FLAG_QUEST)]
        //[Parser(Opcode.CMSG_FLAG_QUEST_FINISH)]
        //[Parser(Opcode.CMSG_CLEAR_QUEST)]
    }
}
