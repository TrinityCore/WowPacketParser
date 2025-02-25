using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class QuestHandler
    {
        public class RequestItemEmote
        {
            public uint ID { get; set; }
            public int EmoteOnIncompleteDelay { get; set; }
            public int EmoteOnIncomplete { get; set; }
            public int EmoteOnCompleteDelay { get; set; }
            public int EmoteOnComplete { get; set; }
            public string CompletionText { get; set; }
        }

        public static ConcurrentDictionary<int, RequestItemEmote> RequestItemEmoteStore = new ConcurrentDictionary<int, RequestItemEmote>();

        public static void AddQuestStarter(WowGuid questgiverGUID, uint questID)
        {
            if (questgiverGUID.GetObjectType() == ObjectType.Unit)
            {
                CreatureQuestStarter ender = new()
                {
                    CreatureID = questgiverGUID.GetEntry(),
                    QuestID = questID
                };
                Storage.CreatureQuestStarters.Add(ender);
            }
            else if (questgiverGUID.GetObjectType() == ObjectType.GameObject)
            {
                GameObjectQuestStarter ender = new()
                {
                    GameObjectID = questgiverGUID.GetEntry(),
                    QuestID = questID
                };
                Storage.GameObjectQuestStarters.Add(ender);
            }
        }

        public static void AddQuestEnder(WowGuid questgiverGUID, uint questID)
        {
            if (questgiverGUID.GetObjectType() == ObjectType.Unit)
            {
                CreatureQuestEnder ender = new()
                {
                    CreatureID = questgiverGUID.GetEntry(),
                    QuestID = questID
                };
                Storage.CreatureQuestEnders.Add(ender);
            }
            else if (questgiverGUID.GetObjectType() == ObjectType.GameObject)
            {
                GameObjectQuestEnder ender = new()
                {
                    GameObjectID = questgiverGUID.GetEntry(),
                    QuestID = questID
                };
                Storage.GameObjectQuestEnders.Add(ender);
            }
        }

        public static void AddSpawnTrackingData(QuestPOI questPoi, TimeSpan TimeSpan)
        {
            // spawn_tracking_quest_objective
            if (Storage.QuestObjectives.ContainsKey((uint)questPoi.QuestObjectiveID))
            {
                if (questPoi.SpawnTrackingID != 0 && questPoi.QuestObjectiveID != 0)
                {
                    SpawnTrackingQuestObjective spawnTrackingQuestObjective = new SpawnTrackingQuestObjective
                    {
                        SpawnTrackingId = (uint)questPoi.SpawnTrackingID,
                        QuestObjectiveId = (uint)questPoi.QuestObjectiveID
                    };

                    Storage.SpawnTrackingQuestObjectives.Add(spawnTrackingQuestObjective, TimeSpan);
                }
            }

            // spawn_tracking_template (helper to retrieve the mapId)
            if (questPoi.SpawnTrackingID != 0)
                Storage.SpawnTrackingMaps.Add((uint)questPoi.SpawnTrackingID, (int)questPoi.MapID);
        }

        private static void ReadExtraQuestInfo510(Packet packet)
        {
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

            packet.ReadUInt32("Money");
            packet.ReadUInt32("XP");
            packet.ReadUInt32("Title Id");
            packet.ReadUInt32("Bonus Talents");
            packet.ReadUInt32("Reward Reputation Mask");

            for (var i = 0; i < 5; i++)
                packet.ReadUInt32("Reputation Faction", i);
            for (var i = 0; i < 5; i++)
                packet.ReadUInt32("Reputation Value Id", i);
            for (var i = 0; i < 5; i++)
                packet.ReadInt32("Reputation Value", i);

            packet.ReadInt32<SpellId>("Spell Id");
            packet.ReadInt32<SpellId>("Spell Cast Id");

            for (var i = 0; i < 4; i++)
                packet.ReadUInt32("Currency Id", i);
            for (var i = 0; i < 4; i++)
                packet.ReadUInt32("Currency Count", i);

            packet.ReadUInt32("Reward SkillId");
            packet.ReadUInt32("Reward Skill Points");
        }

        private static void ReadExtraQuestInfo(Packet packet, bool readFlags = true)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
            {
                ReadExtraQuestInfo510(packet);
                return;
            }

            var choiceCount = packet.ReadUInt32("Choice Item Count");
            var effectiveChoiceCount = ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164) ? 6 : choiceCount;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                for (var i = 0; i < effectiveChoiceCount; i++)
                    packet.ReadUInt32<ItemId>("Choice Item Id", i);
                for (var i = 0; i < effectiveChoiceCount; i++)
                    packet.ReadUInt32("Choice Item Count", i);
                for (var i = 0; i < effectiveChoiceCount; i++)
                    packet.ReadUInt32("Choice Item Display Id", i);

                packet.ReadUInt32("Reward Item Count");
                const int effectiveRewardCount = 4;

                for (var i = 0; i < effectiveRewardCount; i++)
                    packet.ReadUInt32<ItemId>("Reward Item Id", i);
                for (var i = 0; i < effectiveRewardCount; i++)
                    packet.ReadUInt32("Reward Item Count", i);
                for (var i = 0; i < effectiveRewardCount; i++)
                    packet.ReadUInt32("Reward Item Display Id", i);
            }
            else
            {
                for (var i = 0; i < choiceCount; i++)
                {
                    packet.ReadUInt32<ItemId>("Choice Item Id", i);
                    packet.ReadUInt32("Choice Item Count", i);
                    packet.ReadUInt32("Choice Item Display Id", i);
                }

                var rewardCount = packet.ReadUInt32("Reward Item Count");
                for (var i = 0; i < rewardCount; i++)
                {
                    packet.ReadUInt32<ItemId>("Reward Item Id", i);
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
                packet.ReadUInt32("Unknown UInt32 1");
                packet.ReadSingle("Unknown float");
                packet.ReadUInt32("Bonus Talents");
                packet.ReadUInt32("Unknown UInt32 2");
                packet.ReadUInt32("Reward Reputation Mask");
            }
            else
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_3_0_7561))
                    packet.ReadUInt32("Honor Points");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                    packet.ReadSingle("Honor Multiplier");

                if (readFlags)
                    packet.ReadUInt32E<QuestFlags>("Quest Flags");

                packet.ReadInt32<SpellId>("Spell Id");
                packet.ReadInt32<SpellId>("Spell Cast Id");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_4_0_8089))
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
                packet.ReadInt32<SpellId>("Spell Id");
                packet.ReadInt32<SpellId>("Spell Cast Id");

                for (var i = 0; i < 4; i++)
                    packet.ReadUInt32("Currency Id", i);
                for (var i = 0; i < 4; i++)
                    packet.ReadUInt32("Currency Count", i);

                packet.ReadUInt32("Reward SkillId");
                packet.ReadUInt32("Reward Skill Points");
            }
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_INFO)]
        [Parser(Opcode.CMSG_PUSH_QUEST_TO_PARTY)]
        public static void HandleQuestQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V5_0_5_16048)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            var id = packet.ReadEntry("Quest ID");
            if (id.Value) // entry is masked
                return;

            QuestTemplate quest = new QuestTemplate
            {
                ID = (uint)id.Key
            };

            quest.QuestType = packet.ReadInt32E<QuestType>("QuestType");
            quest.QuestLevel = packet.ReadInt32("QuestLevel");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.MinLevel = packet.ReadInt32("QuestMinLevel");

            quest.QuestSortID = packet.ReadInt32E<QuestSort>("QuestSortID");

            quest.QuestInfoID = packet.ReadInt32E<QuestInfo>("QuestInfoID");

            quest.SuggestedGroupNum = packet.ReadUInt32("SuggestedGroupNum");

            quest.RequiredFactionID = new uint?[2];
            quest.RequiredFactionValue = new int?[2];
            for (int i = 0; i < 2; i++)
            {
                quest.RequiredFactionID[i] = packet.ReadUInt32("RequiredFactionID", i);
                quest.RequiredFactionValue[i] = packet.ReadInt32("RequiredFactionValue", i);
            }

            quest.NextQuestID = packet.ReadInt32<QuestId>("NextQuestID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.RewardXPDifficulty = packet.ReadUInt32("RewardXPDifficulty");

            quest.RewardMoney = packet.ReadInt32("RewardMoney");
            quest.RewardBonusMoney = packet.ReadUInt32("RewardBonusMoney");
            quest.RewardDisplaySpell = (uint)packet.ReadInt32<SpellId>("RewardDisplaySpell");
            quest.RewardSpell = packet.ReadInt32<SpellId>("RewardSpell");
            quest.RewardHonor = packet.ReadInt32("RewardHonor");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.RewardKillHonor = packet.ReadSingle("RewardKillHonor");

            quest.StartItem = packet.ReadUInt32<ItemId>("StartItem");
            quest.Flags = packet.ReadUInt32E<QuestFlags>("Flags");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                quest.MinimapTargetMark = packet.ReadUInt32("MinimapTargetMark"); // missing enum. 1- Skull, 16 - Unknown, but exists

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_4_0_8089))
                quest.RewardTitle = packet.ReadUInt32("RewardTitle");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                quest.RequiredPlayerKills = packet.ReadUInt32("RequiredPlayerKills");
                quest.RewardTalents = packet.ReadUInt32("RewardTalents");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.RewardArenaPoints = packet.ReadUInt32("RewardArenaPoints");

            // TODO: Find when was this added/removed and what is it
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958) && (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_1_13164)))
                packet.ReadInt32("Unknown Int32");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                quest.RewardSkillLineID = packet.ReadUInt32("RewardSkillLineID");
                quest.RewardNumSkillUps = packet.ReadUInt32("RewardNumSkillUps");
                quest.RewardReputationMask = packet.ReadUInt32("RewardReputationMask");
                quest.QuestGiverPortrait = packet.ReadUInt32("QuestGiverPortrait");
                quest.QuestTurnInPortrait = packet.ReadUInt32("QuestTurnInPortrait");
            }

            quest.RewardItem = new uint?[4];
            quest.RewardAmount = new uint?[4];
            for (int i = 0; i < 4; i++)
            {
                quest.RewardItem[i] = (uint) packet.ReadInt32<ItemId>("RewardItems", i);
                quest.RewardAmount[i] = packet.ReadUInt32("RewardAmount", i);
            }

            quest.RewardChoiceItemID = new uint?[6];
            quest.RewardChoiceItemQuantity = new uint?[6];
            for (int i = 0; i < 6; i++)
            {
                quest.RewardChoiceItemID[i] = (uint) packet.ReadInt32<ItemId>("RewardChoiceItemID", i);
                quest.RewardChoiceItemQuantity[i] = packet.ReadUInt32("RewardChoiceItemQuantity", i);
            }

            const int repCount = 5;
            quest.RewardFactionID = new uint?[repCount];
            quest.RewardFactionValue = new int?[repCount];
            quest.RewardFactionOverride = new int?[repCount];
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
            {
                for (int i = 0; i < repCount; i++)
                    quest.RewardFactionID[i] = packet.ReadUInt32("RewardFactionID", i);

                for (int i = 0; i < repCount; i++)
                    quest.RewardFactionValue[i] = packet.ReadInt32("RewardFactionValue", i);

                for (int i = 0; i < repCount; i++)
                    quest.RewardFactionOverride[i] = (int)packet.ReadUInt32("RewardFactionOverride", i);
            }

            quest.POIContinent = packet.ReadUInt32("POIContinent");
            quest.POIx = packet.ReadSingle("POIx");
            quest.POIy = packet.ReadSingle("POIy");
            quest.POIPriority = packet.ReadUInt32("POIPriority");
            quest.LogTitle = packet.ReadCString("LogTitle");
            quest.LogDescription = packet.ReadCString("LogDescription");
            quest.QuestDescription = packet.ReadCString("QuestDescription");
            quest.AreaDescription = packet.ReadCString("AreaDescription");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.QuestCompletionLog = packet.ReadCString("QuestCompletionLog");

            var reqId = new KeyValuePair<int, bool>[4];
            quest.RequiredNpcOrGo = new int?[4];
            quest.RequiredNpcOrGoCount = new uint?[4];
            quest.RequiredItemID = new uint?[4];
            quest.RequiredItemCount = new uint?[4];
            var reqItemFieldCount = 4;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
                reqItemFieldCount = 5;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                reqItemFieldCount = 6;
            quest.RequiredItemID = new uint?[reqItemFieldCount];
            quest.RequiredItemCount = new uint?[reqItemFieldCount];

            for (int i = 0; i < 4; i++)
            {
                reqId[i] = packet.ReadEntry();
                bool isGo = reqId[i].Value;
                quest.RequiredNpcOrGo[i] = reqId[i].Key * (isGo ? -1 : 1);

                packet.AddValue("Required", (isGo ? "GO" : "NPC") +
                    " ID: " + StoreGetters.GetName(isGo ? StoreNameType.GameObject : StoreNameType.Unit, reqId[i].Key), i);

                quest.RequiredNpcOrGoCount[i] = packet.ReadUInt32("RequiredCount", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                    quest.RequiredItemID[i] = (uint) packet.ReadInt32<ItemId>("RequiredItemID", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                    quest.RequiredItemCount[i] = packet.ReadUInt32("RequiredItemCount", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_8_9464))
                {
                    quest.RequiredItemID[i] = (uint) packet.ReadInt32<ItemId>("RequiredItemID", i);
                    quest.RequiredItemCount[i] = packet.ReadUInt32("RequiredItemCount", i);
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
            {
                for (int i = 0; i < reqItemFieldCount; i++)
                {
                    quest.RequiredItemID[i] = (uint) packet.ReadInt32<ItemId>("RequiredItemID", i);
                    quest.RequiredItemCount[i] = packet.ReadUInt32("RequiredItemCount", i);
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                quest.RequiredSpell = packet.ReadUInt32<SpellId>("RequiredSpell");

            quest.ObjectiveText = new string[4];
            for (int i = 0; i < 4; i++)
                quest.ObjectiveText[i] = packet.ReadCString("Objective Text", i);

            quest.RewardCurrencyID = new uint?[4];
            quest.RewardCurrencyCount = new uint?[4];
            quest.RequiredCurrencyID = new uint?[4];
            quest.RequiredCurrencyCount = new uint?[4];
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                for (int i = 0; i < 4; ++i)
                {
                    quest.RewardCurrencyID[i] = packet.ReadUInt32("Reward Currency ID", i);
                    quest.RewardCurrencyCount[i] = packet.ReadUInt32("Reward Currency Count", i);
                }

                for (int i = 0; i < 4; ++i)
                {
                    quest.RequiredCurrencyID[i] = packet.ReadUInt32("Required Currency ID", i);
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

            Storage.QuestTemplates.Add(quest, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestQueryResponse510(Packet packet)
        {
            var id = packet.ReadEntry("Quest ID");
            if (id.Value) // entry is masked
                return;

            QuestTemplate quest = new QuestTemplate
            {
                ID = (uint)id.Key
            };

            quest.QuestType = packet.ReadInt32E<QuestType>("QuestType");
            quest.QuestLevel = packet.ReadInt32("QuestLevel");

            quest.QuestPackageID = packet.ReadUInt32("QuestPackageID");
            quest.MinLevel = packet.ReadInt32("QuestMinLevel");
            quest.QuestSortID = packet.ReadInt32E<QuestSort>("QuestSortID");
            quest.QuestInfoID = packet.ReadInt32E<QuestInfo>("QuestInfoID");
            quest.SuggestedGroupNum = packet.ReadUInt32("SuggestedGroupNum");
            quest.RewardNextQuest = (uint)packet.ReadInt32<QuestId>("RewardNextQuest");
            quest.RewardXPDifficulty = packet.ReadUInt32("RewardXPDifficulty");
            quest.RewardMoney = packet.ReadInt32("RewardMoney");
            quest.RewardBonusMoney = packet.ReadUInt32("RewardBonusMoney");
            quest.RewardDisplaySpell = (uint)packet.ReadInt32<SpellId>("RewardDisplaySpell");
            quest.RewardSpell = packet.ReadInt32<SpellId>("RewardSpell");
            quest.RewardHonor = packet.ReadInt32("Reward Honor");
            quest.RewardKillHonor = packet.ReadSingle("RewardKillHonor");
            quest.StartItem = packet.ReadUInt32<ItemId>("StartItem");
            quest.Flags = packet.ReadUInt32E<QuestFlags>("Flags");
            quest.FlagsEx = packet.ReadUInt32E<QuestFlagsEx>("FlagsEx");
            quest.MinimapTargetMark = packet.ReadUInt32("MinimapTargetMark"); // missing enum. 1- Skull, 16 - Unknown, but exists
            quest.RewardTitle = packet.ReadUInt32("RewardTitle");
            quest.RequiredPlayerKills = packet.ReadUInt32("RequiredPlayerKills");
            quest.RewardSkillLineID = packet.ReadUInt32("RewardSkillLineID");
            quest.RewardNumSkillUps = packet.ReadUInt32("RewardNumSkillUps");
            quest.RewardReputationMask = packet.ReadUInt32("RewRepMask");
            quest.QuestGiverPortrait = packet.ReadUInt32("QuestGiverPortrait");
            quest.QuestTurnInPortrait = packet.ReadUInt32("QuestTurnInPortrait");

            quest.RewardItem = new uint?[4];
            quest.RewardAmount = new uint?[4];
            for (int i = 0; i < 4; i++)
            {
                quest.RewardItem[i] = (uint)packet.ReadInt32<ItemId>("Reward Item ID", i);
                quest.RewardAmount[i] = packet.ReadUInt32("Reward Item Count", i);
            }

            quest.RewardChoiceItemID = new uint?[6];
            quest.RewardChoiceItemQuantity = new uint?[6];
            for (int i = 0; i < 6; i++)
            {
                quest.RewardChoiceItemID[i] = (uint)packet.ReadInt32<ItemId>("Reward Choice Item ID", i);
                quest.RewardChoiceItemQuantity[i] = packet.ReadUInt32("Reward Choice Item Count", i);
            }

            const int repCount = 5;
            quest.RewardFactionID = new uint?[repCount];
            quest.RewardFactionValue = new int?[repCount];
            quest.RewardFactionOverride = new int?[repCount];
            for (int i = 0; i < repCount; i++)
                quest.RewardFactionID[i] = packet.ReadUInt32("RewardFactionID", i);

            for (int i = 0; i < repCount; i++)
                quest.RewardFactionValue[i] = packet.ReadInt32("RewardFactionValue", i);

            for (int i = 0; i < repCount; i++)
                quest.RewardFactionOverride[i] = packet.ReadInt32("RewardFactionOverride", i);

            quest.RewardCurrencyID = new uint?[4];
            quest.RewardCurrencyCount = new uint?[4];
            for (int i = 0; i < 4; i++)
            {
                quest.RewardCurrencyID[i] = packet.ReadUInt32("Reward Currency ID", i);
                quest.RewardCurrencyCount[i] = packet.ReadUInt32("Reward Currency Count", i);
            }

            quest.POIContinent = packet.ReadUInt32("POIContinent");
            quest.POIx = packet.ReadSingle("POIx");
            quest.POIy = packet.ReadSingle("POIy");
            quest.POIPriority = packet.ReadUInt32("POIPriority");

            quest.LogTitle = packet.ReadCString("LogTitle");
            quest.LogDescription = packet.ReadCString("LogDescription");
            quest.QuestDescription = packet.ReadCString("QuestDescription");
            quest.AreaDescription = packet.ReadCString("AreaDescription");
            quest.QuestCompletionLog = packet.ReadCString("QuestCompletionLog");
            quest.QuestGiverTextWindow = packet.ReadCString("QuestGiver Text Window");
            quest.QuestGiverTargetName = packet.ReadCString("QuestGiver Target Name");
            quest.QuestTurnTextWindow = packet.ReadCString("QuestTurn Text Window");
            quest.QuestTurnTargetName = packet.ReadCString("QuestTurn Target Name");

            quest.SoundAccept = packet.ReadUInt32("Sound Accept");
            quest.SoundTurnIn = packet.ReadUInt32("Sound TurnIn");

            quest.RequiredItemID = new uint?[4];
            quest.RequiredItemCount = new uint?[4];
            for (int i = 0; i < 4; i++)
            {
                quest.RequiredItemID[i] = (uint)packet.ReadInt32<ItemId>("RequiredItemID", i);
                quest.RequiredItemCount[i] = packet.ReadUInt32("RequiredItemCount", i);
            }

            byte requirementCount = packet.ReadByte("Requirement Count");
            for (int i = 0; i < requirementCount; i++)
            {
                packet.ReadUInt32("Unk UInt32", i);

                QuestRequirementType reqType = packet.ReadByteE<QuestRequirementType>("Requirement Type", i);
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

                packet.ReadInt32("Required Count", i);
                packet.ReadUInt32("Unk UInt32", i);
                packet.ReadCString("Objective Text", i);
                packet.ReadByte("Unk Byte", i);
                byte count = packet.ReadByte("Unk Byte", i);
                for (int j = 0; j < count; j++)
                    packet.ReadUInt32("Unk UInt32", i, j);
            }

            // unused in MoP, but required for SQL building
            quest.RequiredNpcOrGo = new int?[4];
            quest.RequiredNpcOrGoCount = new uint?[4];
            quest.RequiredItemID = new uint?[6];
            quest.RequiredItemCount = new uint?[6];
            quest.RequiredCurrencyID = new uint?[4];
            quest.RequiredCurrencyCount = new uint?[4];
            quest.RequiredFactionID = new uint?[2];
            quest.RequiredFactionValue = new int?[2];
            quest.ObjectiveText = new string[4];
            quest.RewardTalents = 0;

            packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");

            Storage.QuestTemplates.Add(quest, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        [Parser(Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
                packet.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleQuestNPCQuery430(Packet packet)
        {
            var count = packet.ReadBits("Count", 24);
            for (int i = 0; i < count; ++i)
                packet.ReadUInt32<QuestId>("Quest", i);
        }

        [Parser(Opcode.SMSG_QUEST_COMPLETION_NPC_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestNpcQueryResponse(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                var count2 = packet.ReadUInt32("Number of NPC", i);
                for (var j = 0; j < count2; ++j)
                {
                    var entry = packet.ReadEntry();
                    packet.AddValue(!entry.Value ? "Creature" : "GameObject",
                        StoreGetters.GetName(entry.Value ? StoreNameType.GameObject : StoreNameType.Unit, entry.Key), i, j);
                }
            }

            for (var i = 0; i < count; ++i)
                packet.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_COMPLETION_NPC_RESPONSE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestNpcQueryResponse434(Packet packet)
        {
            var count = packet.ReadBits("Count", 23);
            var counts = new uint[count];

            for (int i = 0; i < count; ++i)
                counts[i] = packet.ReadBits("Count", 24, i);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32<QuestId>("Quest ID", i);
                for (int j = 0; j < counts[i]; ++j)
                {
                    var entry = packet.ReadEntry();
                    packet.AddValue(!entry.Value ? "Creature" : "GameObject",
                        StoreGetters.GetName(entry.Value ? StoreNameType.GameObject : StoreNameType.Unit, entry.Key), i, j);
                }
            }
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            int count = packet.ReadInt32("Count");

            for (int i = 0; i < count; ++i)
            {
                int questId = packet.ReadInt32<QuestId>("Quest ID", i);

                int counter = packet.ReadInt32("POI Counter", i);
                for (int j = 0; j < counter; ++j)
                {
                    int idx = packet.ReadInt32("POI Index", i, j);
                    QuestPOI questPoi = new QuestPOI
                    {
                        QuestID = questId,
                        ID = idx
                    };

                    questPoi.ObjectiveIndex = packet.ReadInt32("Objective Index", i, j);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_0_5_16048))
                        packet.ReadUInt32("Unk Int32 1", i, j);

                    questPoi.MapID = (int)packet.ReadUInt32<MapId>("Map Id", i);
                    questPoi.WorldMapAreaId = (int)packet.ReadUInt32("World Map Area ID", i, j);
                    questPoi.Floor = (int)packet.ReadUInt32("Floor Id", i, j);
                    questPoi.Priority = (int)packet.ReadUInt32("Unk Int32 2", i, j);
                    questPoi.Flags = (int)packet.ReadUInt32("Unk Int32 3", i, j);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_0_5_16048))
                    {
                        packet.ReadUInt32("World Effect ID", i, j);
                        packet.ReadUInt32("Player Row ID", i, j);
                    }

                    int pointsSize = packet.ReadInt32("Points Counter", i, j);
                    for (int k = 0; k < pointsSize; ++k)
                    {
                        QuestPOIPoint questPoiPoint = new QuestPOIPoint
                        {
                            QuestID = questId,
                            Idx1 = idx,
                            Idx2 = k,
                            X = packet.ReadInt32("Point X", i, j, k),
                            Y = packet.ReadInt32("Point Y", i, j, k)
                        };
                        Storage.QuestPOIPoints.Add(questPoiPoint, packet.TimeSpan);
                    }

                    Storage.QuestPOIs.Add(questPoi, packet.TimeSpan);
                }
            }
        }

        [Parser(Opcode.SMSG_QUEST_FORCE_REMOVED)]
        [Parser(Opcode.CMSG_QUEST_CONFIRM_ACCEPT)]
        public static void HandleQuestForceRemoved(Packet packet)
        {
            packet.ReadInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestUpdateComplete(Packet packet)
        {
            var questComplete = packet.Holder.QuestComplete = new();
            questComplete.QuestId = (uint)packet.ReadInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_FAILED)]
        public static void HandleQuestUpdateFailed(Packet packet)
        {
            var questFailed = packet.Holder.QuestFailed = new();
            questFailed.QuestId = (uint)packet.ReadInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_FAILED_TIMER)]
        public static void HandleQuestUpdateFailedTimer(Packet packet)
        {
            var questFailed = packet.Holder.QuestFailed = new();
            questFailed.QuestId = (uint)packet.ReadInt32<QuestId>("QuestID");
            questFailed.TimerFail = true;
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestUpdateComplete422(Packet packet)
        {
            var questComplete = packet.Holder.QuestComplete = new();
            packet.ReadGuid("Guid");
            questComplete.QuestId = packet.ReadUInt32<QuestId>("Quest ID");
            packet.ReadCString("Title");
            packet.ReadCString("Complete Text");
            packet.ReadCString("QuestGiver Text Window");
            packet.ReadCString("QuestGiver Target Name");
            packet.ReadCString("QuestTurn Text Window");
            packet.ReadCString("QuestTurn Target Name");
            packet.ReadInt32("QuestGiver Portrait");
            packet.ReadInt32("QuestTurn Portrait");
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.ReadInt32("Unk Int32");
            var emoteCount = packet.ReadUInt32("Quest Emote Count");
            for (var i = 0; i < emoteCount; i++)
            {
                packet.ReadUInt32("Emote Id", i);
                packet.ReadUInt32("Emote Delay (ms)", i);
            }

            ReadExtraQuestInfo(packet);
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestUpdateComplete510(Packet packet)
        {
            var questComplete = packet.Holder.QuestComplete = new();
            packet.ReadGuid("Guid");
            questComplete.QuestId = packet.ReadUInt32<QuestId>("Quest ID");
            packet.ReadInt32("Unk Int32");
            packet.ReadCString("Title");
            packet.ReadCString("Complete Text");
            packet.ReadCString("QuestGiver Text Window");
            packet.ReadCString("QuestGiver Target Name");
            packet.ReadCString("QuestTurn Text Window");
            packet.ReadCString("QuestTurn Target Name");
            packet.ReadInt32("QuestGiver Portrait");
            packet.ReadInt32("QuestTurn Portrait");
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.ReadUInt32E<QuestFlagsEx>("Quest Flags 2");
            packet.ReadInt32("Unk Int32");

            var emoteCount = packet.ReadUInt32("Quest Emote Count");
            for (var i = 0; i < emoteCount; i++)
            {
                packet.ReadUInt32("Emote Delay (ms)", i);
                packet.ReadUInt32("Emote Id", i);
            }

            ReadExtraQuestInfo(packet);
        }

        [Parser(Opcode.SMSG_QUERY_QUESTS_COMPLETED_RESPONSE)]
        public static void HandleQuestCompletedResponse(Packet packet)
        {
            packet.ReadInt32("Count");
            // Prints ~4k lines of quest IDs, should be DEBUG only or something...
            /*
            for (var i = 0; i < count; i++)
                packet.ReadInt32<QuestId>("Rewarded Quest");
            */
            packet.AddValue("Error", "Packet is currently not printed");
            packet.ReadBytes((int)packet.Length);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_STATUS_QUERY)]
        [Parser(Opcode.CMSG_QUEST_GIVER_HELLO)]
        [Parser(Opcode.CMSG_QUEST_GIVER_QUEST_AUTOLAUNCH)]
        public static void HandleQuestgiverStatusQuery(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_LIST_MESSAGE)]
        public static void HandleQuestgiverQuestList(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadCString("Title");
            packet.ReadUInt32("Delay");
            packet.ReadUInt32("Emote");

            var count = packet.ReadByte("GossipQuestsCount");
            for (var i = 0; i < count; i++)
                NpcHandler.ReadGossipQuestTextData(packet, i, "GossipQuests");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_QUERY_QUEST)]
        public static void HandleQuestgiverQueryQuest(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32<QuestId>("Quest ID");
            packet.ReadByte("Start/End (1/2)");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_ACCEPT_QUEST)]
        public static void HandleQuestgiverAcceptQuest(Packet packet)
        {
            var questGiverAcceptQuest = packet.Holder.QuestGiverAcceptQuest = new();
            questGiverAcceptQuest.QuestGiver = packet.ReadGuid("GUID");
            questGiverAcceptQuest.QuestId = packet.ReadUInt32<QuestId>("Quest ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                packet.ReadUInt32("Unk UInt32");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestgiverDetails(Packet packet)
        {
            packet.ReadGuid("QuestGiverGUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadGuid("InformUnit");

            uint id = packet.ReadUInt32<QuestId>("Quest ID");

            QuestDetails questDetails = new QuestDetails
            {
                ID = id
            };
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
                packet.ReadBool("Auto Accept");
            else
                packet.ReadBool<Int32>("Auto Accept");

            var flags = QuestFlags.None;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                flags = packet.ReadUInt32E<QuestFlags>("Quest Flags");

            packet.ReadUInt32("Suggested Players");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadByte("Unknown byte");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_3_3a_11723))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                {
                    var questAccepted = packet.ReadByte("QuestAccepted");
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                        packet.ReadByte("Unk");
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.ReadBool("Starts at AreaTrigger");
                packet.ReadInt32<SpellId>("Required Spell");
            }

            if (flags.HasAnyFlag(QuestFlags.HiddenRewards) && ClientVersion.RemovedInVersion(ClientVersionBuild.V3_3_5a_12340))
            {
                packet.ReadUInt32("Hidden Chosen Items");
                packet.ReadUInt32("Hidden Items");
                packet.ReadUInt32("Hidden Money");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_2_10482))
                    packet.ReadUInt32("Hidden XP");
            }

            ReadExtraQuestInfo(packet, false);

            questDetails.Emote = new uint?[] { 0, 0, 0, 0 };
            questDetails.EmoteDelay = new uint?[] { 0, 0, 0, 0 };

            var emoteCount = packet.ReadUInt32("Quest Emote Count");
            for (var i = 0; i < emoteCount; i++)
            {
                questDetails.Emote[i] = packet.ReadUInt32("Emote Id", i);
                questDetails.EmoteDelay[i] = packet.ReadUInt32("Emote Delay (ms)", i);
            }

            Storage.QuestDetails.Add(questDetails, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS, ClientVersionBuild.V5_1_0_16309, ClientVersionBuild.V5_1_0a_16357)]
        public static void HandleQuestgiverDetails510(Packet packet)
        {
            packet.ReadGuid("QuestGiverGUID");
            packet.ReadGuid("InformUnit");
            packet.ReadUInt32<QuestId>("Quest ID");
            packet.ReadInt32("Unk Int32");
            packet.ReadCString("Title");
            packet.ReadCString("Details");
            packet.ReadCString("Objectives");
            packet.ReadCString("QuestGiver Text Window");
            packet.ReadCString("QuestGiver Target Name");
            packet.ReadCString("QuestTurn Text Window");
            packet.ReadCString("QuestTurn Target Name");
            packet.ReadUInt32("QuestGiver Portrait");
            packet.ReadUInt32("QuestTurn Portrait");
            packet.ReadBool("Auto Accept");
            packet.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.ReadUInt32E<QuestFlagsEx>("Quest Flags 2");
            packet.ReadUInt32("Suggested Players");
            packet.ReadByte("Unknown byte");
            packet.ReadBool("Starts at AreaTrigger");

            var reqSpellCount = packet.ReadUInt32("Required Spell Count");
            for (var i = 0; i < reqSpellCount; i++)
                packet.ReadInt32<SpellId>("Required Spell", i);

            ReadExtraQuestInfo(packet, false);

            var emoteCount = packet.ReadUInt32("Quest Emote Count");
            for (var i = 0; i < emoteCount; i++)
            {
                packet.ReadUInt32("Emote Id", i);
                packet.ReadUInt32("Emote Delay (ms)", i);
            }
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_COMPLETE_QUEST)]
        public static void HandleQuestCompleteQuest(Packet packet)
        {
            var questGiverCompleteQuest = packet.Holder.QuestGiverCompleteQuestRequest = new();
            questGiverCompleteQuest.QuestGiver = packet.ReadGuid("GUID");
            questGiverCompleteQuest.QuestId = packet.ReadUInt32<QuestId>("Quest ID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.ReadByte("Unk byte");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_REQUEST_REWARD)]
        public static void HandleQuestRequestReward(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32<QuestId>("Quest ID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595)) // remove confirmed for 434
                packet.ReadUInt32("Unk UInt32");
        }

        public static void QuestRequestItemHelper(int id, string completionText, int delay, int emote, bool isComplete, Packet packet, bool noRequestOnComplete = false)
        {
            RequestItemEmote requestItemEmote;
            if (RequestItemEmoteStore.TryGetValue(id, out requestItemEmote))
            {
                if (isComplete)
                {
                    requestItemEmote.EmoteOnCompleteDelay = delay;
                    requestItemEmote.EmoteOnComplete = emote;
                }
                else
                {
                    requestItemEmote.EmoteOnIncompleteDelay = delay;
                    requestItemEmote.EmoteOnIncomplete = emote;

                    if (noRequestOnComplete)
                    {
                        requestItemEmote.EmoteOnCompleteDelay = 0;
                        requestItemEmote.EmoteOnComplete = 0;
                    }
                }
            }
            else
            {
                var emotes = new RequestItemEmote();

                emotes.ID = (uint)id;
                emotes.CompletionText = completionText;

                if (isComplete)
                {
                    emotes.EmoteOnCompleteDelay = delay;
                    emotes.EmoteOnComplete = emote;
                    emotes.EmoteOnIncompleteDelay = -1;
                    emotes.EmoteOnIncomplete = -1;
                }
                else
                {
                    emotes.EmoteOnIncompleteDelay = delay;
                    emotes.EmoteOnIncomplete = emote;

                    if (noRequestOnComplete)
                    {
                        emotes.EmoteOnCompleteDelay = 0;
                        emotes.EmoteOnComplete = 0;
                    }
                    else
                    {
                        emotes.EmoteOnComplete = -1;
                        emotes.EmoteOnCompleteDelay = -1;
                    }
                }

                RequestItemEmoteStore.TryAdd(id, emotes);
            }
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestRequestItems(Packet packet)
        {
            var questgiverGUID = packet.ReadGuid("GUID");
            var requestItems = packet.Holder.QuestGiverRequestItems = new();
            requestItems.QuestGiver = questgiverGUID;
            requestItems.QuestGiverEntry = requestItems.QuestGiver.Entry;
            int id = packet.ReadInt32<QuestId>("Quest ID");
            requestItems.QuestId = (uint)id;
            requestItems.QuestTitle = packet.ReadCString("Title");
            string text = requestItems.CompletionText = packet.ReadCString("Text");
            int emoteDelay = requestItems.EmoteDelay = (int)packet.ReadUInt32("Emote Delay");
            int emoteID = requestItems.EmoteType = (int)packet.ReadUInt32("Emote");

            AddQuestEnder(questgiverGUID, (uint)id);
            packet.ReadUInt32("Close Window on Cancel");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                requestItems.QuestFlags = (uint)packet.ReadUInt32E<QuestFlags>("Quest Flags");

            requestItems.SuggestedPartyMembers = (int)packet.ReadUInt32("Suggested Players");
            requestItems.MoneyToGet = (int)packet.ReadUInt32("Money");

            uint count = requestItems.CollectCount = packet.ReadUInt32("Number of Required Items");
            for (int i = 0; i < count; i++)
            {
                var itemId = packet.ReadUInt32<ItemId>("Required Item Id", i);
                var amount = packet.ReadUInt32("Required Item Count", i);
                packet.ReadUInt32("Required Item Display Id", i);
                requestItems.Collect.Add(new QuestCollect()
                {
                    Id = (int)itemId,
                    Count = (int)amount
                });
            }

            // flags
            var flags = packet.ReadUInt32("Unk flags 1");
            requestItems.StatusFlags = (PacketQuestStatusFlags)flags;
            bool isComplete = (flags & 0x3) != 0;

            packet.ReadUInt32("Unk flags 2");
            packet.ReadUInt32("Unk flags 3");
            packet.ReadUInt32("Unk flags 4");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.ReadUInt32("Unk flags 5");
                packet.ReadUInt32("Unk flags 6");
            }

            QuestRequestItemHelper(id, text, emoteDelay, emoteID, isComplete, packet);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestRequestItems434(Packet packet)
        {
            var questgiverGUID = packet.ReadGuid("GUID");
            var requestItems = packet.Holder.QuestGiverRequestItems = new();
            requestItems.QuestGiver = questgiverGUID;
            requestItems.QuestGiverEntry = requestItems.QuestGiver.Entry;
            int id = packet.ReadInt32<QuestId>("QuestID");
            requestItems.QuestId = (uint)id;
            requestItems.QuestTitle = packet.ReadCString("Title");
            string completionText = requestItems.CompletionText = packet.ReadCString("CompletionText");
            int delay = requestItems.EmoteDelay = packet.ReadInt32("EmoteDelay");
            int emote = requestItems.EmoteType = packet.ReadInt32("EmoteType");

            AddQuestEnder(questgiverGUID, (uint)id);

            packet.ReadUInt32("Close Window on Cancel");
            requestItems.QuestFlags = (uint)packet.ReadUInt32E<QuestFlags>("QuestFlags");
            requestItems.SuggestedPartyMembers = (int)packet.ReadUInt32("SuggestedPlayers");
            requestItems.MoneyToGet = (int)packet.ReadUInt32("Money");

            uint countItems = requestItems.CollectCount = packet.ReadUInt32("Number of Required Items");
            for (int i = 0; i < countItems; i++)
            {
                var itemId = packet.ReadUInt32<ItemId>("Required Item Id", i);
                var amount = packet.ReadUInt32("Required Item Count", i);
                packet.ReadUInt32("Required Item Display Id", i);
                requestItems.Collect.Add(new QuestCollect()
                {
                    Id = (int)itemId,
                    Count = (int)amount
                });
            }

            uint countCurrencies = packet.ReadUInt32("Number of Required Currencies");
            for (int i = 0; i < countCurrencies; i++)
            {
                var currencyId = packet.ReadUInt32("Required Currency Id", i);
                var amount = packet.ReadUInt32("Required Currency Count", i);
                requestItems.Currencies.Add(new Currency()
                {
                    Id = currencyId,
                    Count = amount
                });
            }

            // flags, if any of these flags is 0 quest is not completable
            QuestStatusFlags[] statusFlags = new QuestStatusFlags[] { QuestStatusFlags.None, QuestStatusFlags.None, QuestStatusFlags.None, QuestStatusFlags.None, QuestStatusFlags.None };
            QuestStatusFlags[] completableStatusFlags = new QuestStatusFlags[] { QuestStatusFlags.KillCreditComplete, QuestStatusFlags.CollectableComplete, QuestStatusFlags.QuestStatusUnk8, QuestStatusFlags.QuestStatusUnk16, QuestStatusFlags.QuestStatusUnk64 };

            statusFlags[0] = packet.ReadUInt32E<QuestStatusFlags>("StatusFlags1"); // 2
            statusFlags[1] = packet.ReadUInt32E<QuestStatusFlags>("StatusFlags2"); // 4
            statusFlags[2] = packet.ReadUInt32E<QuestStatusFlags>("StatusFlags3"); // 8
            statusFlags[3] = packet.ReadUInt32E<QuestStatusFlags>("StatusFlags4"); // 16
            statusFlags[4] = packet.ReadUInt32E<QuestStatusFlags>("StatusFlags5"); // 64

            bool isComplete = false;
            for (int i = 0; i < statusFlags.Length; i++)
            {
                if ((statusFlags[i] & completableStatusFlags[i]) == completableStatusFlags[i])
                    isComplete = true;
                else
                {
                    isComplete = false;
                    break; // if any of these flags is 0 quest is not completable
                }
            }

            QuestRequestItemHelper(id, completionText, delay, emote, isComplete, packet);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestRequestItems510(Packet packet)
        {
            var questgiverGUID = packet.ReadGuid("GUID");
            var requestItems = packet.Holder.QuestGiverRequestItems = new();
            requestItems.QuestGiver = questgiverGUID;
            requestItems.QuestGiverEntry = requestItems.QuestGiver.Entry;
            int id = packet.ReadInt32<QuestId>("QuestID");
            requestItems.QuestId = (uint)id;
            requestItems.QuestTitle = packet.ReadCString("Title");
            string text = requestItems.CompletionText = packet.ReadCString("Text");
            int delay = requestItems.EmoteDelay = packet.ReadInt32("EmoteDelay");
            int emote = requestItems.EmoteType = packet.ReadInt32("EmoteType");

            AddQuestEnder(questgiverGUID, (uint)id);
            packet.ReadUInt32("Close Window on Cancel");
            requestItems.QuestFlags = (uint)packet.ReadUInt32E<QuestFlags>("Quest Flags");
            requestItems.QuestFlags2 = (uint)packet.ReadUInt32E<QuestFlagsEx>("Quest Flags 2");
            requestItems.SuggestedPartyMembers = (int)packet.ReadUInt32("Suggested Players");
            requestItems.MoneyToGet = (int)packet.ReadUInt32("Money");

            uint countItems = requestItems.CollectCount = packet.ReadUInt32("Number of Required Items");
            for (int i = 0; i < countItems; i++)
            {
                var itemId = packet.ReadUInt32<ItemId>("Required Item Id", i);
                var amount = packet.ReadUInt32("Required Item Count", i);
                packet.ReadUInt32("Required Item Display Id", i);
                requestItems.Collect.Add(new QuestCollect()
                {
                    Id = (int)itemId,
                    Count = (int)amount
                });
            }

            uint countCurrencies = packet.ReadUInt32("Number of Required Currencies");
            for (int i = 0; i < countCurrencies; i++)
            {
                var currencyId = packet.ReadUInt32("Required Currency Id", i);
                var amount = packet.ReadUInt32("Required Currency Count", i);
                requestItems.Currencies.Add(new Currency()
                {
                    Id = currencyId,
                    Count = amount
                });
            }

            // flags, if any of these flags is 0 quest is not completable
            QuestStatusFlags[] statusFlags = new QuestStatusFlags[] { QuestStatusFlags.None, QuestStatusFlags.None, QuestStatusFlags.None, QuestStatusFlags.None, QuestStatusFlags.None };
            QuestStatusFlags[] completableStatusFlags = new QuestStatusFlags[] { QuestStatusFlags.KillCreditComplete, QuestStatusFlags.CollectableComplete, QuestStatusFlags.QuestStatusUnk8, QuestStatusFlags.QuestStatusUnk16, QuestStatusFlags.QuestStatusUnk64 };

            statusFlags[0] = packet.ReadUInt32E<QuestStatusFlags>("StatusFlags1"); // 2
            statusFlags[1] = packet.ReadUInt32E<QuestStatusFlags>("StatusFlags2"); // 4
            statusFlags[2] = packet.ReadUInt32E<QuestStatusFlags>("StatusFlags3"); // 8
            statusFlags[3] = packet.ReadUInt32E<QuestStatusFlags>("StatusFlags4"); // 16
            statusFlags[4] = packet.ReadUInt32E<QuestStatusFlags>("StatusFlags5"); // 64

            bool isComplete = false;
            for (int i = 0; i < statusFlags.Length; i++)
            {
                if ((statusFlags[i] & completableStatusFlags[i]) == completableStatusFlags[i])
                    isComplete = true;
                else
                {
                    isComplete = false;
                    break; // if any of these flags is 0 quest is not completable
                }
            }

            QuestRequestItemHelper(id, text, delay, emote, isComplete, packet);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE)]
        public static void HandleQuestOfferReward(Packet packet)
        {
            packet.ReadGuid("GUID");
            uint entry = packet.ReadUInt32<QuestId>("Quest ID");
            packet.ReadCString("Title");
            string text = packet.ReadCString("Text");
            QuestOfferReward offerReward = new QuestOfferReward
            {
                ID = entry,
                RewardText = text
            };

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
                packet.ReadBool<Int32>("Auto Finish");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                packet.ReadUInt32E<QuestFlags>("Quest Flags");

            packet.ReadUInt32("Suggested Players");

            uint count1 = packet.ReadUInt32("Emote Count");
            int?[] emoteIDs = {0, 0, 0, 0};
            uint?[] emoteDelays = {0, 0, 0, 0};
            for (int i = 0; i < count1; i++)
            {
                emoteDelays[i] = packet.ReadUInt32("Emote Delay", i);
                emoteIDs[i] = (int)packet.ReadInt32E<EmoteType>("Emote Id", i);
            }
            offerReward.Emote = emoteIDs;
            offerReward.EmoteDelay = emoteDelays;

            ReadExtraQuestInfo(packet);

            Storage.QuestOfferRewards.Add(offerReward, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD)]
        public static void HandleQuestChooseReward(Packet packet)
        {
            var chooseReward = packet.Holder.ClientQuestGiverChooseReward = new();
            chooseReward.QuestGiver = packet.ReadGuid("GUID");
            chooseReward.QuestId = packet.ReadUInt32<QuestId>("Quest ID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_0_4_16016))
                chooseReward.Item = packet.ReadUInt32("Item Id");
            else
                chooseReward.RewardIndex = packet.ReadUInt32("Reward Index");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_INVALID_QUEST)]
        public static void HandleQuestInvalid(Packet packet)
        {
            packet.ReadUInt32E<QuestReasonType>("Reason");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_FAILED)]
        public static void HandleQuestFailed(Packet packet)
        {
            var questFailed = packet.Holder.QuestFailed = new();
            questFailed.QuestId = packet.ReadUInt32<QuestId>("Quest ID");
            packet.ReadUInt32E<QuestReasonType>("Reason");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleQuestCompleted(Packet packet)
        {
            var questComplete = packet.Holder.QuestGiverQuestComplete = new();
            questComplete.QuestId = (uint)packet.ReadInt32<QuestId>("Quest ID");
            packet.ReadInt32("Reward");
            packet.ReadInt32("Money");
            packet.ReadInt32("Honor");
            packet.ReadInt32("Talents");
            packet.ReadInt32("Arena Points");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleQuestCompleted406(Packet packet)
        {
            var questComplete = packet.Holder.QuestGiverQuestComplete = new();
            packet.ReadBit("Unk");
            packet.ReadUInt32("Reward Skill Id");
            questComplete.QuestId = (uint)packet.ReadInt32<QuestId>("Quest ID");
            packet.ReadInt32("Money");
            packet.ReadInt32("Talent Points");
            packet.ReadUInt32("Reward Skill Points");
            packet.ReadInt32("Reward XP");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestCompleted422(Packet packet)
        {
            var questComplete = packet.Holder.QuestGiverQuestComplete = new();
            packet.ReadByte("Unk Byte");
            packet.ReadInt32("Reward XP");
            packet.ReadInt32("Money");
            packet.ReadInt32("Reward Skill Points");
            questComplete.QuestId = (uint)packet.ReadInt32<QuestId>("Quest ID");
            packet.ReadInt32("Reward Skill Id");
            packet.ReadInt32("Talent Points");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestCompleted434(Packet packet)
        {
            var questComplete = packet.Holder.QuestGiverQuestComplete = new();
            packet.ReadInt32("Talent Points");
            packet.ReadInt32("RewSkillPoints");
            packet.ReadInt32("Money");
            packet.ReadInt32("XP");
            questComplete.QuestId = (uint)packet.ReadInt32<QuestId>("Quest ID");
            packet.ReadInt32("RewSkillId");
            packet.ReadBit("Unk Bit 1"); // if true EVENT_QUEST_FINISHED is fired, target cleared and gossip window is open
            packet.ReadBit("Unk Bit 2");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestCompleted510(Packet packet)
        {
            var questComplete = packet.Holder.QuestGiverQuestComplete = new();
            packet.ReadInt32("Talent Points");
            packet.ReadInt32("Money");
            questComplete.QuestId = (uint)packet.ReadInt32<QuestId>("Quest ID");
            packet.ReadInt32("XP");
            packet.ReadInt32("RewSkillPoints");
            packet.ReadInt32("RewSkillId");
            packet.ReadBit("Unk Bit 1"); // if true EVENT_QUEST_FINISHED is fired, target cleared and gossip window is open
            packet.ReadBit("Unk Bit 2");
        }

        [Parser(Opcode.CMSG_QUEST_LOG_SWAP_QUEST)]
        public static void HandleQuestSwapQuest(Packet packet)
        {
            packet.ReadByte("Slot 1");
            packet.ReadByte("Slot 2");
        }

        [Parser(Opcode.CMSG_QUEST_LOG_REMOVE_QUEST)]
        public static void HandleQuestRemoveQuest(Packet packet)
        {
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_KILL)]
        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_ITEM)]
        public static void HandleQuestUpdateAdd(Packet packet)
        {
            int questId = packet.ReadInt32<QuestId>("Quest ID");
            var entry = packet.ReadEntry();
            packet.AddValue("Entry", StoreGetters.GetName(entry.Value ? StoreNameType.GameObject : StoreNameType.Unit, entry.Key));

            int count = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                count = packet.ReadInt16("Count");
            else
                count = packet.ReadInt32("Count");

            int requiredCount = packet.ReadInt32("Required Count");
            var victim = packet.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.ReadByteE<QuestRequirementType>("Quest Requirement Type");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_QUEST_UPDATE_ADD_KILL, Direction.ServerToClient))
            {
                var addCredit = packet.Holder.QuestAddKillCredit = new();
                addCredit.QuestId = (uint)questId;
                addCredit.KillCredit = (uint)entry.Key;
                addCredit.Count = (uint)count;
                addCredit.RequiredCount = (uint)requiredCount;
                addCredit.Victim = victim;
            }
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS)]
        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatus(Packet packet)
        {
            uint count = 1;
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE, Direction.ServerToClient))
                count = packet.ReadUInt32("Count");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                for (var i = 0; i < count; i++)
                {
                    packet.ReadGuid("GUID", i);
                    packet.ReadInt32E<QuestGiverStatus4x>("Status", i);
                }
            else
                for (var i = 0; i < count; i++)
                {
                    packet.ReadGuid("GUID", i);
                    packet.ReadByteE<QuestGiverStatus>("Status", i);
                }
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_PVP_CREDIT)]
        public static void HandleQuestUpdateAddPvPCredit(Packet packet)
        {
            packet.ReadInt32<QuestId>("Quest ID");
            packet.ReadInt32("Count");
            packet.ReadInt32("Required Count");
        }

        [Parser(Opcode.SMSG_QUEST_CONFIRM_ACCEPT)]
        public static void HandleQuestConfirAccept(Packet packet)
        {
            packet.ReadInt32<QuestId>("Quest ID");
            packet.ReadCString("Title");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.MSG_QUEST_PUSH_RESULT)]
        public static void HandleQuestPushResult(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (packet.Direction == Direction.ClientToServer)
                packet.ReadUInt32("Quest Id");
            packet.ReadByteE<QuestPartyResult>("Result");
        }

        [Parser(Opcode.CMSG_QUERY_QUESTS_COMPLETED)]
        [Parser(Opcode.SMSG_QUEST_LOG_FULL)]
        [Parser(Opcode.CMSG_QUEST_GIVER_CANCEL)]
        [Parser(Opcode.CMSG_QUEST_GIVER_STATUS_MULTIPLE_QUERY)]
        public static void HandleQuestZeroLengthPackets(Packet packet)
        {
        }

        //[Parser(Opcode.CMSG_FLAG_QUEST)]
        //[Parser(Opcode.CMSG_FLAG_QUEST_FINISH)]
        //[Parser(Opcode.CMSG_CLEAR_QUEST)]
    }
}
