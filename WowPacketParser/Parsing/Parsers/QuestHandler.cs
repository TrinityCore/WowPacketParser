using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class QuestHandler
    {
        private static void ReadExtraQuestInfo510(Packet packet)
        {
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

            packet.Translator.ReadUInt32("Money");
            packet.Translator.ReadUInt32("XP");
            packet.Translator.ReadUInt32("Title Id");
            packet.Translator.ReadUInt32("Bonus Talents");
            packet.Translator.ReadUInt32("Reward Reputation Mask");

            for (var i = 0; i < 5; i++)
                packet.Translator.ReadUInt32("Reputation Faction", i);
            for (var i = 0; i < 5; i++)
                packet.Translator.ReadUInt32("Reputation Value Id", i);
            for (var i = 0; i < 5; i++)
                packet.Translator.ReadInt32("Reputation Value", i);

            packet.Translator.ReadInt32<SpellId>("Spell Id");
            packet.Translator.ReadInt32<SpellId>("Spell Cast Id");

            for (var i = 0; i < 4; i++)
                packet.Translator.ReadUInt32("Currency Id", i);
            for (var i = 0; i < 4; i++)
                packet.Translator.ReadUInt32("Currency Count", i);

            packet.Translator.ReadUInt32("Reward SkillId");
            packet.Translator.ReadUInt32("Reward Skill Points");
        }

        private static void ReadExtraQuestInfo(Packet packet, bool readFlags = true)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
            {
                ReadExtraQuestInfo510(packet);
                return;
            }

            var choiceCount = packet.Translator.ReadUInt32("Choice Item Count");
            var effectiveChoiceCount = ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164) ? 6 : choiceCount;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                for (var i = 0; i < effectiveChoiceCount; i++)
                    packet.Translator.ReadUInt32<ItemId>("Choice Item Id", i);
                for (var i = 0; i < effectiveChoiceCount; i++)
                    packet.Translator.ReadUInt32("Choice Item Count", i);
                for (var i = 0; i < effectiveChoiceCount; i++)
                    packet.Translator.ReadUInt32("Choice Item Display Id", i);

                packet.Translator.ReadUInt32("Reward Item Count");
                const int effectiveRewardCount = 4;

                for (var i = 0; i < effectiveRewardCount; i++)
                    packet.Translator.ReadUInt32<ItemId>("Reward Item Id", i);
                for (var i = 0; i < effectiveRewardCount; i++)
                    packet.Translator.ReadUInt32("Reward Item Count", i);
                for (var i = 0; i < effectiveRewardCount; i++)
                    packet.Translator.ReadUInt32("Reward Item Display Id", i);
            }
            else
            {
                for (var i = 0; i < choiceCount; i++)
                {
                    packet.Translator.ReadUInt32<ItemId>("Choice Item Id", i);
                    packet.Translator.ReadUInt32("Choice Item Count", i);
                    packet.Translator.ReadUInt32("Choice Item Display Id", i);
                }

                var rewardCount = packet.Translator.ReadUInt32("Reward Item Count");
                for (var i = 0; i < rewardCount; i++)
                {
                    packet.Translator.ReadUInt32<ItemId>("Reward Item Id", i);
                    packet.Translator.ReadUInt32("Reward Item Count", i);
                    packet.Translator.ReadUInt32("Reward Item Display Id", i);
                }
            }

            packet.Translator.ReadUInt32("Money");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_2_10482))
                packet.Translator.ReadUInt32("XP");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.Translator.ReadUInt32("Title Id");
                packet.Translator.ReadUInt32("Unknown UInt32 1");
                packet.Translator.ReadSingle("Unknown float");
                packet.Translator.ReadUInt32("Bonus Talents");
                packet.Translator.ReadUInt32("Unknown UInt32 2");
                packet.Translator.ReadUInt32("Reward Reputation Mask");
            }
            else
            {
                packet.Translator.ReadUInt32("Honor Points");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                    packet.Translator.ReadSingle("Honor Multiplier");

                if (readFlags)
                        packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");

                packet.Translator.ReadInt32<SpellId>("Spell Id");
                packet.Translator.ReadInt32<SpellId>("Spell Cast Id");
                packet.Translator.ReadUInt32("Title Id");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                    packet.Translator.ReadUInt32("Bonus Talents");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                {
                    packet.Translator.ReadUInt32("Arena Points");
                    packet.Translator.ReadUInt32("Unk UInt32");
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
            {
                for (var i = 0; i < 5; i++)
                    packet.Translator.ReadUInt32("Reputation Faction", i);

                for (var i = 0; i < 5; i++)
                    packet.Translator.ReadUInt32("Reputation Value Id", i);

                for (var i = 0; i < 5; i++)
                    packet.Translator.ReadInt32("Reputation Value", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.Translator.ReadInt32<SpellId>("Spell Id");
                packet.Translator.ReadInt32<SpellId>("Spell Cast Id");

                for (var i = 0; i < 4; i++)
                    packet.Translator.ReadUInt32("Currency Id", i);
                for (var i = 0; i < 4; i++)
                    packet.Translator.ReadUInt32("Currency Count", i);

                packet.Translator.ReadUInt32("Reward SkillId");
                packet.Translator.ReadUInt32("Reward Skill Points");
            }
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_INFO)]
        [Parser(Opcode.CMSG_PUSH_QUEST_TO_PARTY)]
        public static void HandleQuestQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V5_0_5_16048)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            var id = packet.Translator.ReadEntry("Quest ID");
            if (id.Value) // entry is masked
                return;

            QuestTemplate quest = new QuestTemplate
            {
                ID = (uint)id.Key
            };

            quest.QuestType = packet.Translator.ReadInt32E<QuestType>("QuestType");
            quest.QuestLevel = packet.Translator.ReadInt32("QuestLevel");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.MinLevel = packet.Translator.ReadInt32("QuestMinLevel");

            quest.QuestSortID = packet.Translator.ReadInt32E<QuestSort>("QuestSortID");

            quest.QuestInfoID = packet.Translator.ReadInt32E<QuestInfo>("QuestInfoID");

            quest.SuggestedGroupNum = packet.Translator.ReadUInt32("SuggestedGroupNum");

            quest.RequiredFactionID = new uint?[2];
            quest.RequiredFactionValue = new int?[2];
            for (int i = 0; i < 2; i++)
            {
                quest.RequiredFactionID[i] = packet.Translator.ReadUInt32("RequiredFactionID", i);
                quest.RequiredFactionValue[i] = packet.Translator.ReadInt32("RequiredFactionValue", i);
            }

            quest.NextQuestID = packet.Translator.ReadInt32<QuestId>("NextQuestID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.RewardXPDifficulty = packet.Translator.ReadUInt32("RewardXPDifficulty");

            quest.RewardMoney = packet.Translator.ReadInt32("RewardMoney");
            quest.RewardBonusMoney = packet.Translator.ReadUInt32("RewardBonusMoney");
            quest.RewardDisplaySpell = (uint)packet.Translator.ReadInt32<SpellId>("RewardDisplaySpell");
            quest.RewardSpell = packet.Translator.ReadInt32<SpellId>("RewardSpell");
            quest.RewardHonor = packet.Translator.ReadInt32("RewardHonor");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.RewardKillHonor = packet.Translator.ReadSingle("RewardKillHonor");

            quest.StartItem = packet.Translator.ReadUInt32<ItemId>("StartItem");
            quest.Flags = packet.Translator.ReadUInt32E<QuestFlags>("Flags");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                quest.MinimapTargetMark = packet.Translator.ReadUInt32("MinimapTargetMark"); // missing enum. 1- Skull, 16 - Unknown, but exists

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_4_0_8089))
                quest.RewardTitle = packet.Translator.ReadUInt32("RewardTitle");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                quest.RequiredPlayerKills = packet.Translator.ReadUInt32("RequiredPlayerKills");
                quest.RewardTalents = packet.Translator.ReadUInt32("RewardTalents");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.RewardArenaPoints = packet.Translator.ReadUInt32("RewardArenaPoints");

            // TODO: Find when was this added/removed and what is it
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958) && (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_1_13164)))
                packet.Translator.ReadInt32("Unknown Int32");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                quest.RewardSkillLineID = packet.Translator.ReadUInt32("RewardSkillLineID");
                quest.RewardNumSkillUps = packet.Translator.ReadUInt32("RewardNumSkillUps");
                quest.RewardReputationMask = packet.Translator.ReadUInt32("RewardReputationMask");
                quest.QuestGiverPortrait = packet.Translator.ReadUInt32("QuestGiverPortrait");
                quest.QuestTurnInPortrait = packet.Translator.ReadUInt32("QuestTurnInPortrait");
            }

            quest.RewardItem = new uint?[4];
            quest.RewardAmount = new uint?[4];
            for (int i = 0; i < 4; i++)
            {
                quest.RewardItem[i] = (uint) packet.Translator.ReadInt32<ItemId>("RewardItems", i);
                quest.RewardAmount[i] = packet.Translator.ReadUInt32("RewardAmount", i);
            }

            quest.RewardChoiceItemID = new uint?[6];
            quest.RewardChoiceItemQuantity = new uint?[6];
            for (int i = 0; i < 6; i++)
            {
                quest.RewardChoiceItemID[i] = (uint) packet.Translator.ReadInt32<ItemId>("RewardChoiceItemID", i);
                quest.RewardChoiceItemQuantity[i] = packet.Translator.ReadUInt32("RewardChoiceItemQuantity", i);
            }

            const int repCount = 5;
            quest.RewardFactionID = new uint?[repCount];
            quest.RewardFactionValue = new int?[repCount];
            quest.RewardFactionOverride = new int?[repCount];
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
            {
                for (int i = 0; i < repCount; i++)
                    quest.RewardFactionID[i] = packet.Translator.ReadUInt32("RewardFactionID", i);

                for (int i = 0; i < repCount; i++)
                    quest.RewardFactionValue[i] = packet.Translator.ReadInt32("RewardFactionValue", i);

                for (int i = 0; i < repCount; i++)
                    quest.RewardFactionOverride[i] = (int)packet.Translator.ReadUInt32("RewardFactionOverride", i);
            }

            quest.POIContinent = packet.Translator.ReadUInt32("POIContinent");
            quest.POIx = packet.Translator.ReadSingle("POIx");
            quest.POIy = packet.Translator.ReadSingle("POIy");
            quest.POIPriority = packet.Translator.ReadUInt32("POIPriority");
            quest.LogTitle = packet.Translator.ReadCString("LogTitle");
            quest.LogDescription = packet.Translator.ReadCString("LogDescription");
            quest.QuestDescription = packet.Translator.ReadCString("QuestDescription");
            quest.AreaDescription = packet.Translator.ReadCString("AreaDescription");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                quest.QuestCompletionLog = packet.Translator.ReadCString("QuestCompletionLog");

            var reqId = new KeyValuePair<int, bool>[4];
            quest.RequiredNpcOrGo = new int?[4];
            quest.RequiredNpcOrGoCount = new uint?[4];
            quest.RequiredItemID = new uint?[4];
            quest.RequiredItemCount = new uint?[4];
            int reqItemFieldCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464) ? 6 : 4;
            quest.RequiredItemID = new uint?[reqItemFieldCount];
            quest.RequiredItemCount = new uint?[reqItemFieldCount];

            for (int i = 0; i < 4; i++)
            {
                reqId[i] = packet.Translator.ReadEntry();
                bool isGo = reqId[i].Value;
                quest.RequiredNpcOrGo[i] = reqId[i].Key * (isGo ? -1 : 1);

                packet.AddValue("Required", (isGo ? "GO" : "NPC") +
                    " ID: " + StoreGetters.GetName(isGo ? StoreNameType.GameObject : StoreNameType.Unit, reqId[i].Key), i);

                quest.RequiredNpcOrGoCount[i] = packet.Translator.ReadUInt32("RequiredCount", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                    quest.RequiredItemID[i] = (uint) packet.Translator.ReadInt32<ItemId>("RequiredItemID", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                    quest.RequiredItemCount[i] = packet.Translator.ReadUInt32("RequiredItemCount", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_8_9464))
                {
                    quest.RequiredItemID[i] = (uint) packet.Translator.ReadInt32<ItemId>("RequiredItemID", i);
                    quest.RequiredItemCount[i] = packet.Translator.ReadUInt32("RequiredItemCount", i);
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
            {
                for (int i = 0; i < reqItemFieldCount; i++)
                {
                    quest.RequiredItemID[i] = (uint) packet.Translator.ReadInt32<ItemId>("RequiredItemID", i);
                    quest.RequiredItemCount[i] = packet.Translator.ReadUInt32("RequiredItemCount", i);
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                quest.RequiredSpell = packet.Translator.ReadUInt32<SpellId>("RequiredSpell");

            quest.ObjectiveText = new string[4];
            for (int i = 0; i < 4; i++)
                quest.ObjectiveText[i] = packet.Translator.ReadCString("Objective Text", i);

            quest.RewardCurrencyID = new uint?[4];
            quest.RewardCurrencyCount = new uint?[4];
            quest.RequiredCurrencyID = new uint?[4];
            quest.RequiredCurrencyCount = new uint?[4];
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                for (int i = 0; i < 4; ++i)
                {
                    quest.RewardCurrencyID[i] = packet.Translator.ReadUInt32("Reward Currency ID", i);
                    quest.RewardCurrencyCount[i] = packet.Translator.ReadUInt32("Reward Currency Count", i);
                }

                for (int i = 0; i < 4; ++i)
                {
                    quest.RequiredCurrencyID[i] = packet.Translator.ReadUInt32("Required Currency ID", i);
                    quest.RequiredCurrencyCount[i] = packet.Translator.ReadUInt32("Required Currency Count", i);
                }

                quest.QuestGiverTextWindow = packet.Translator.ReadCString("QuestGiver Text Window");
                quest.QuestGiverTargetName = packet.Translator.ReadCString("QuestGiver Target Name");
                quest.QuestTurnTextWindow = packet.Translator.ReadCString("QuestTurn Text Window");
                quest.QuestTurnTargetName = packet.Translator.ReadCString("QuestTurn Target Name");

                quest.SoundAccept = packet.Translator.ReadUInt32("Sound Accept");
                quest.SoundTurnIn = packet.Translator.ReadUInt32("Sound TurnIn");
            }

            packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");

            Storage.QuestTemplates.Add(quest, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestQueryResponse510(Packet packet)
        {
            var id = packet.Translator.ReadEntry("Quest ID");
            if (id.Value) // entry is masked
                return;

            QuestTemplate quest = new QuestTemplate
            {
                ID = (uint)id.Key
            };

            quest.QuestType = packet.Translator.ReadInt32E<QuestType>("QuestType");
            quest.QuestLevel = packet.Translator.ReadInt32("QuestLevel");

            quest.QuestPackageID = packet.Translator.ReadUInt32("QuestPackageID");
            quest.MinLevel = packet.Translator.ReadInt32("QuestMinLevel");
            quest.QuestSortID = packet.Translator.ReadInt32E<QuestSort>("QuestSortID");
            quest.QuestInfoID = packet.Translator.ReadInt32E<QuestInfo>("QuestInfoID");
            quest.SuggestedGroupNum = packet.Translator.ReadUInt32("SuggestedGroupNum");
            quest.RewardNextQuest = (uint)packet.Translator.ReadInt32<QuestId>("RewardNextQuest");
            quest.RewardXPDifficulty = packet.Translator.ReadUInt32("RewardXPDifficulty");
            quest.RewardMoney = packet.Translator.ReadInt32("RewardMoney");
            quest.RewardBonusMoney = packet.Translator.ReadUInt32("RewardBonusMoney");
            quest.RewardDisplaySpell = (uint)packet.Translator.ReadInt32<SpellId>("RewardDisplaySpell");
            quest.RewardSpell = packet.Translator.ReadInt32<SpellId>("RewardSpell");
            quest.RewardHonor = packet.Translator.ReadInt32("Reward Honor");
            quest.RewardKillHonor = packet.Translator.ReadSingle("RewardKillHonor");
            quest.StartItem = packet.Translator.ReadUInt32<ItemId>("StartItem");
            quest.Flags = packet.Translator.ReadUInt32E<QuestFlags>("Flags");
            quest.FlagsEx = packet.Translator.ReadUInt32E<QuestFlags2>("FlagsEx");
            quest.MinimapTargetMark = packet.Translator.ReadUInt32("MinimapTargetMark"); // missing enum. 1- Skull, 16 - Unknown, but exists
            quest.RewardTitle = packet.Translator.ReadUInt32("RewardTitle");
            quest.RequiredPlayerKills = packet.Translator.ReadUInt32("RequiredPlayerKills");
            quest.RewardSkillLineID = packet.Translator.ReadUInt32("RewardSkillLineID");
            quest.RewardNumSkillUps = packet.Translator.ReadUInt32("RewardNumSkillUps");
            quest.RewardReputationMask = packet.Translator.ReadUInt32("RewRepMask");
            quest.QuestGiverPortrait = packet.Translator.ReadUInt32("QuestGiverPortrait");
            quest.QuestTurnInPortrait = packet.Translator.ReadUInt32("QuestTurnInPortrait");

            quest.RewardItem = new uint?[4];
            quest.RewardAmount = new uint?[4];
            for (int i = 0; i < 4; i++)
            {
                quest.RewardItem[i] = (uint)packet.Translator.ReadInt32<ItemId>("Reward Item ID", i);
                quest.RewardAmount[i] = packet.Translator.ReadUInt32("Reward Item Count", i);
            }

            quest.RewardChoiceItemID = new uint?[6];
            quest.RewardChoiceItemQuantity = new uint?[6];
            for (int i = 0; i < 6; i++)
            {
                quest.RewardChoiceItemID[i] = (uint)packet.Translator.ReadInt32<ItemId>("Reward Choice Item ID", i);
                quest.RewardChoiceItemQuantity[i] = packet.Translator.ReadUInt32("Reward Choice Item Count", i);
            }

            const int repCount = 5;
            quest.RewardFactionID = new uint?[repCount];
            quest.RewardFactionValue = new int?[repCount];
            quest.RewardFactionOverride = new int?[repCount];
            for (int i = 0; i < repCount; i++)
                quest.RewardFactionID[i] = packet.Translator.ReadUInt32("RewardFactionID", i);

            for (int i = 0; i < repCount; i++)
                quest.RewardFactionValue[i] = packet.Translator.ReadInt32("RewardFactionValue", i);

            for (int i = 0; i < repCount; i++)
                quest.RewardFactionOverride[i] = packet.Translator.ReadInt32("RewardFactionOverride", i);

            quest.RewardCurrencyID = new uint?[4];
            quest.RewardCurrencyCount = new uint?[4];
            for (int i = 0; i < 4; i++)
            {
                quest.RewardCurrencyID[i] = packet.Translator.ReadUInt32("Reward Currency ID", i);
                quest.RewardCurrencyCount[i] = packet.Translator.ReadUInt32("Reward Currency Count", i);
            }

            quest.POIContinent = packet.Translator.ReadUInt32("POIContinent");
            quest.POIx = packet.Translator.ReadSingle("POIx");
            quest.POIy = packet.Translator.ReadSingle("POIy");
            quest.POIPriority = packet.Translator.ReadUInt32("POIPriority");

            quest.LogTitle = packet.Translator.ReadCString("LogTitle");
            quest.LogDescription = packet.Translator.ReadCString("LogDescription");
            quest.QuestDescription = packet.Translator.ReadCString("QuestDescription");
            quest.AreaDescription = packet.Translator.ReadCString("AreaDescription");
            quest.QuestCompletionLog = packet.Translator.ReadCString("QuestCompletionLog");
            quest.QuestGiverTextWindow = packet.Translator.ReadCString("QuestGiver Text Window");
            quest.QuestGiverTargetName = packet.Translator.ReadCString("QuestGiver Target Name");
            quest.QuestTurnTextWindow = packet.Translator.ReadCString("QuestTurn Text Window");
            quest.QuestTurnTargetName = packet.Translator.ReadCString("QuestTurn Target Name");

            quest.SoundAccept = packet.Translator.ReadUInt32("Sound Accept");
            quest.SoundTurnIn = packet.Translator.ReadUInt32("Sound TurnIn");

            quest.RequiredItemID = new uint?[4];
            quest.RequiredItemCount = new uint?[4];
            for (int i = 0; i < 4; i++)
            {
                quest.RequiredItemID[i] = (uint)packet.Translator.ReadInt32<ItemId>("RequiredItemID", i);
                quest.RequiredItemCount[i] = packet.Translator.ReadUInt32("RequiredItemCount", i);
            }

            byte requirementCount = packet.Translator.ReadByte("Requirement Count");
            for (int i = 0; i < requirementCount; i++)
            {
                packet.Translator.ReadUInt32("Unk UInt32", i);

                QuestRequirementType reqType = packet.Translator.ReadByteE<QuestRequirementType>("Requirement Type", i);
                switch (reqType)
                {
                    case QuestRequirementType.CreatureKill:
                    case QuestRequirementType.CreatureInteract:
                    case QuestRequirementType.PetBattleDefeatCreature:
                        packet.Translator.ReadInt32<UnitId>("Required Creature ID", i);
                        break;
                    case QuestRequirementType.Item:
                        packet.Translator.ReadInt32<ItemId>("Required Item ID", i);
                        break;
                    case QuestRequirementType.GameObject:
                        packet.Translator.ReadInt32<GOId>("Required GameObject ID", i);
                        break;
                    case QuestRequirementType.Currency:
                        packet.Translator.ReadUInt32("Required Currency ID", i);
                        break;
                    case QuestRequirementType.Spell:
                        packet.Translator.ReadInt32<SpellId>("Required Spell ID", i);
                        break;
                    case QuestRequirementType.FactionRepHigher:
                    case QuestRequirementType.FactionRepLower:
                        packet.Translator.ReadUInt32("Required Faction ID", i);
                        break;
                    case QuestRequirementType.PetBattleDefeatSpecies:
                        packet.Translator.ReadUInt32("Required Species ID", i);
                        break;
                    default:
                        packet.Translator.ReadInt32("Required ID", i);
                        break;
                }

                packet.Translator.ReadInt32("Required Count", i);
                packet.Translator.ReadUInt32("Unk UInt32", i);
                packet.Translator.ReadCString("Objective Text", i);
                packet.Translator.ReadByte("Unk Byte", i);
                byte count = packet.Translator.ReadByte("Unk Byte", i);
                for (int j = 0; j < count; j++)
                    packet.Translator.ReadUInt32("Unk UInt32", i, j);
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
            var count = packet.Translator.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
                packet.Translator.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleQuestNPCQuery430(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 24);
            for (int i = 0; i < count; ++i)
                packet.Translator.ReadUInt32<QuestId>("Quest", i);
        }

        [Parser(Opcode.SMSG_QUEST_COMPLETION_NPC_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestNpcQueryResponse(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                var count2 = packet.Translator.ReadUInt32("Number of NPC", i);
                for (var j = 0; j < count2; ++j)
                {
                    var entry = packet.Translator.ReadEntry();
                    packet.AddValue(!entry.Value ? "Creature" : "GameObject",
                        StoreGetters.GetName(entry.Value ? StoreNameType.GameObject : StoreNameType.Unit, entry.Key), i, j);
                }
            }

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_COMPLETION_NPC_RESPONSE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestNpcQueryResponse434(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 23);
            var counts = new uint[count];

            for (int i = 0; i < count; ++i)
                counts[i] = packet.Translator.ReadBits("Count", 24, i);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32<QuestId>("Quest ID", i);
                for (int j = 0; j < counts[i]; ++j)
                {
                    var entry = packet.Translator.ReadEntry();
                    packet.AddValue(!entry.Value ? "Creature" : "GameObject",
                        StoreGetters.GetName(entry.Value ? StoreNameType.GameObject : StoreNameType.Unit, entry.Key), i, j);
                }
            }
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            int count = packet.Translator.ReadInt32("Count");

            for (int i = 0; i < count; ++i)
            {
                int questId = packet.Translator.ReadInt32<QuestId>("Quest ID", i);

                int counter = packet.Translator.ReadInt32("POI Counter", i);
                for (int j = 0; j < counter; ++j)
                {
                    int idx = packet.Translator.ReadInt32("POI Index", i, j);
                    QuestPOI questPoi = new QuestPOI
                    {
                        QuestID = questId,
                        ID = idx
                    };

                    questPoi.ObjectiveIndex = packet.Translator.ReadInt32("Objective Index", i, j);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_0_5_16048))
                        packet.Translator.ReadUInt32("Unk Int32 1", i, j);

                    questPoi.MapID = (int)packet.Translator.ReadUInt32<MapId>("Map Id", i);
                    questPoi.WorldMapAreaId = (int)packet.Translator.ReadUInt32("World Map Area ID", i, j);
                    questPoi.Floor = (int)packet.Translator.ReadUInt32("Floor Id", i, j);
                    questPoi.Priority = (int)packet.Translator.ReadUInt32("Unk Int32 2", i, j);
                    questPoi.Flags = (int)packet.Translator.ReadUInt32("Unk Int32 3", i, j);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_0_5_16048))
                    {
                        packet.Translator.ReadUInt32("World Effect ID", i, j);
                        packet.Translator.ReadUInt32("Player Row ID", i, j);
                    }

                    int pointsSize = packet.Translator.ReadInt32("Points Counter", i, j);
                    for (int k = 0; k < pointsSize; ++k)
                    {
                        QuestPOIPoint questPoiPoint = new QuestPOIPoint
                        {
                            QuestID = questId,
                            Idx1 = idx,
                            Idx2 = k,
                            X = packet.Translator.ReadInt32("Point X", i, j, k),
                            Y = packet.Translator.ReadInt32("Point Y", i, j, k)
                        };
                        Storage.QuestPOIPoints.Add(questPoiPoint, packet.TimeSpan);
                    }

                    Storage.QuestPOIs.Add(questPoi, packet.TimeSpan);
                }
            }
        }

        [Parser(Opcode.SMSG_QUEST_FORCE_REMOVED)]
        [Parser(Opcode.CMSG_QUEST_CONFIRM_ACCEPT)]
        [Parser(Opcode.SMSG_QUEST_UPDATE_FAILED)]
        [Parser(Opcode.SMSG_QUEST_UPDATE_FAILED_TIMER)]
        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestForceRemoved(Packet packet)
        {
            packet.Translator.ReadInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestUpdateComplete422(Packet packet)
        {
            packet.Translator.ReadGuid("Guid");
            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadCString("Title");
            packet.Translator.ReadCString("Complete Text");
            packet.Translator.ReadCString("QuestGiver Text Window");
            packet.Translator.ReadCString("QuestGiver Target Name");
            packet.Translator.ReadCString("QuestTurn Text Window");
            packet.Translator.ReadCString("QuestTurn Target Name");
            packet.Translator.ReadInt32("QuestGiver Portrait");
            packet.Translator.ReadInt32("QuestTurn Portrait");
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.Translator.ReadInt32("Unk Int32");
            var emoteCount = packet.Translator.ReadUInt32("Quest Emote Count");
            for (var i = 0; i < emoteCount; i++)
            {
                packet.Translator.ReadUInt32("Emote Id", i);
                packet.Translator.ReadUInt32("Emote Delay (ms)", i);
            }

            ReadExtraQuestInfo(packet);
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestUpdateComplete510(Packet packet)
        {
            packet.Translator.ReadGuid("Guid");
            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadCString("Title");
            packet.Translator.ReadCString("Complete Text");
            packet.Translator.ReadCString("QuestGiver Text Window");
            packet.Translator.ReadCString("QuestGiver Target Name");
            packet.Translator.ReadCString("QuestTurn Text Window");
            packet.Translator.ReadCString("QuestTurn Target Name");
            packet.Translator.ReadInt32("QuestGiver Portrait");
            packet.Translator.ReadInt32("QuestTurn Portrait");
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.Translator.ReadUInt32E<QuestFlags2>("Quest Flags 2");
            packet.Translator.ReadInt32("Unk Int32");

            var emoteCount = packet.Translator.ReadUInt32("Quest Emote Count");
            for (var i = 0; i < emoteCount; i++)
            {
                packet.Translator.ReadUInt32("Emote Delay (ms)", i);
                packet.Translator.ReadUInt32("Emote Id", i);
            }

            ReadExtraQuestInfo(packet);
        }

        [Parser(Opcode.SMSG_QUERY_QUESTS_COMPLETED_RESPONSE)]
        public static void HandleQuestCompletedResponse(Packet packet)
        {
            packet.Translator.ReadInt32("Count");
            // Prints ~4k lines of quest IDs, should be DEBUG only or something...
            /*
            for (var i = 0; i < count; i++)
                packet.Translator.ReadInt32<QuestId>("Rewarded Quest");
            */
            packet.AddValue("Error", "Packet is currently not printed");
            packet.Translator.ReadBytes((int)packet.Length);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_STATUS_QUERY)]
        [Parser(Opcode.CMSG_QUEST_GIVER_HELLO)]
        [Parser(Opcode.CMSG_QUEST_GIVER_QUEST_AUTOLAUNCH)]
        public static void HandleQuestgiverStatusQuery(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_LIST_MESSAGE)]
        public static void HandleQuestgiverQuestList(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadCString("Title");
            packet.Translator.ReadUInt32("Delay");
            packet.Translator.ReadUInt32("Emote");

            var count = packet.Translator.ReadByte("Count");
            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadUInt32<QuestId>("Quest ID", i);
                packet.Translator.ReadUInt32("Quest Icon", i);
                packet.Translator.ReadInt32("Quest Level", i);
                packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                    packet.Translator.ReadUInt32E<QuestFlags2>("Quest Flags 2", i);

                packet.Translator.ReadBool("Change icon", i);
                packet.Translator.ReadCString("Title", i);
            }

        }

        [Parser(Opcode.CMSG_QUEST_GIVER_QUERY_QUEST)]
        public static void HandleQuestgiverQueryQuest(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadByte("Start/End (1/2)");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_ACCEPT_QUEST)]
        public static void HandleQuestgiverAcceptQuest(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32<QuestId>("Quest ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                packet.Translator.ReadUInt32("Unk UInt32");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestgiverDetails(Packet packet)
        {
            packet.Translator.ReadGuid("GUID1");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.Translator.ReadGuid("Unk NPC GUID");

            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadCString("Title");
            packet.Translator.ReadCString("Details");
            packet.Translator.ReadCString("Objectives");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.Translator.ReadCString("QuestGiver Text Window");
                packet.Translator.ReadCString("QuestGiver Target Name");
                packet.Translator.ReadCString("QuestTurn Text Window");
                packet.Translator.ReadCString("QuestTurn Target Name");
                packet.Translator.ReadUInt32("QuestGiver Portrait");
                packet.Translator.ReadUInt32("QuestTurn Portrait");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                packet.Translator.ReadBool("Auto Accept");
            else
                packet.Translator.ReadBool<Int32>("Auto Accept");

            var flags = QuestFlags.None;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                flags = packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");

            packet.Translator.ReadUInt32("Suggested Players");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.Translator.ReadByte("Unknown byte");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_3_3a_11723))
            {
                packet.Translator.ReadByte("Unk");
                packet.Translator.ReadByte("Unk");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.Translator.ReadBool("Starts at AreaTrigger");
                packet.Translator.ReadInt32<SpellId>("Required Spell");
            }

            if (flags.HasAnyFlag(QuestFlags.HiddenRewards) && ClientVersion.RemovedInVersion(ClientVersionBuild.V3_3_5a_12340))
            {
                packet.Translator.ReadUInt32("Hidden Chosen Items");
                packet.Translator.ReadUInt32("Hidden Items");
                packet.Translator.ReadUInt32("Hidden Money");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_2_10482))
                    packet.Translator.ReadUInt32("Hidden XP");
            }

            ReadExtraQuestInfo(packet, false);

            var emoteCount = packet.Translator.ReadUInt32("Quest Emote Count");
            for (var i = 0; i < emoteCount; i++)
            {
                packet.Translator.ReadUInt32("Emote Id", i);
                packet.Translator.ReadUInt32("Emote Delay (ms)", i);
            }
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS, ClientVersionBuild.V5_1_0_16309, ClientVersionBuild.V5_1_0a_16357)]
        public static void HandleQuestgiverDetails510(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadGuid("Unk NPC GUID");
            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadCString("Title");
            packet.Translator.ReadCString("Details");
            packet.Translator.ReadCString("Objectives");
            packet.Translator.ReadCString("QuestGiver Text Window");
            packet.Translator.ReadCString("QuestGiver Target Name");
            packet.Translator.ReadCString("QuestTurn Text Window");
            packet.Translator.ReadCString("QuestTurn Target Name");
            packet.Translator.ReadUInt32("QuestGiver Portrait");
            packet.Translator.ReadUInt32("QuestTurn Portrait");
            packet.Translator.ReadBool("Auto Accept");
            packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.Translator.ReadUInt32E<QuestFlags2>("Quest Flags 2");
            packet.Translator.ReadUInt32("Suggested Players");
            packet.Translator.ReadByte("Unknown byte");
            packet.Translator.ReadBool("Starts at AreaTrigger");

            var reqSpellCount = packet.Translator.ReadUInt32("Required Spell Count");
            for (var i = 0; i < reqSpellCount; i++)
                packet.Translator.ReadInt32<SpellId>("Required Spell", i);

            ReadExtraQuestInfo(packet, false);

            var emoteCount = packet.Translator.ReadUInt32("Quest Emote Count");
            for (var i = 0; i < emoteCount; i++)
            {
                packet.Translator.ReadUInt32("Emote Id", i);
                packet.Translator.ReadUInt32("Emote Delay (ms)", i);
            }
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_COMPLETE_QUEST)]
        public static void HandleQuestCompleteQuest(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.Translator.ReadByte("Unk byte");
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_REQUEST_REWARD)]
        public static void HandleQuestRequestReward(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595)) // remove confirmed for 434
                packet.Translator.ReadUInt32("Unk UInt32");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestRequestItems(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            uint entry = packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadCString("Title");
            string text = packet.Translator.ReadCString("Text");

            QuestRequestItems requestItems = new QuestRequestItems
            {
                ID = entry,
                CompletionText = text
            };

            requestItems.EmoteOnComplete = packet.Translator.ReadUInt32("Emote");
            packet.Translator.ReadUInt32("Unk UInt32 1");
            packet.Translator.ReadUInt32("Close Window on Cancel");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");

            packet.Translator.ReadUInt32("Suggested Players");
            packet.Translator.ReadUInt32("Money");

            uint count = packet.Translator.ReadUInt32("Number of Required Items");
            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadUInt32<ItemId>("Required Item Id", i);
                packet.Translator.ReadUInt32("Required Item Count", i);
                packet.Translator.ReadUInt32("Required Item Display Id", i);
            }

            // flags
            packet.Translator.ReadUInt32("Unk flags 1");
            packet.Translator.ReadUInt32("Unk flags 2");
            packet.Translator.ReadUInt32("Unk flags 3");
            packet.Translator.ReadUInt32("Unk flags 4");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.Translator.ReadUInt32("Unk flags 5");
                packet.Translator.ReadUInt32("Unk flags 6");
            }
            requestItems.EmoteOnCompleteDelay = 0;
            Storage.QuestRequestItems.Add(requestItems, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestRequestItems434(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            uint entry = packet.Translator.ReadUInt32<QuestId>("Quest ID");
            
            packet.Translator.ReadCString("Title");
            string text = packet.Translator.ReadCString("Text");

            QuestRequestItems requestItems = new QuestRequestItems
            {
                ID = entry,
                CompletionText = text
            };

            requestItems.EmoteOnCompleteDelay = packet.Translator.ReadUInt32("Delay");  // not confirmed
            requestItems.EmoteOnComplete = packet.Translator.ReadUInt32("Emote");  // not confirmed

            packet.Translator.ReadUInt32("Close Window on Cancel");
            packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.Translator.ReadUInt32("Suggested Players");
            packet.Translator.ReadUInt32("Money");

            uint countItems = packet.Translator.ReadUInt32("Number of Required Items");
            for (int i = 0; i < countItems; i++)
            {
                packet.Translator.ReadUInt32<ItemId>("Required Item Id", i);
                packet.Translator.ReadUInt32("Required Item Count", i);
                packet.Translator.ReadUInt32("Required Item Display Id", i);
            }

            uint countCurrencies = packet.Translator.ReadUInt32("Number of Required Currencies");
            for (int i = 0; i < countCurrencies; i++)
            {
                packet.Translator.ReadUInt32("Required Currency Id", i);
                packet.Translator.ReadUInt32("Required Currency Count", i);
            }

            // flags, if any of these flags is 0 quest is not completable
            packet.Translator.ReadUInt32("Unk flags 1"); // 2
            packet.Translator.ReadUInt32("Unk flags 2"); // 4
            packet.Translator.ReadUInt32("Unk flags 3"); // 8
            packet.Translator.ReadUInt32("Unk flags 4"); // 16
            packet.Translator.ReadUInt32("Unk flags 5"); // 64

            Storage.QuestRequestItems.Add(requestItems, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestRequestItems510(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            uint entry = packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadCString("Title");
            string text = packet.Translator.ReadCString("Text");

            QuestRequestItems requestItems = new QuestRequestItems
            {
                ID = entry,
                CompletionText = text
            };

            requestItems.EmoteOnComplete = packet.Translator.ReadUInt32("Emote");
            requestItems.EmoteOnCompleteDelay = packet.Translator.ReadUInt32("Delay");
            packet.Translator.ReadUInt32("Close Window on Cancel");
            packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.Translator.ReadUInt32E<QuestFlags2>("Quest Flags 2");
            packet.Translator.ReadUInt32("Suggested Players");
            packet.Translator.ReadUInt32("Money");

            uint countItems = packet.Translator.ReadUInt32("Number of Required Items");
            for (int i = 0; i < countItems; i++)
            {
                packet.Translator.ReadUInt32<ItemId>("Required Item Id", i);
                packet.Translator.ReadUInt32("Required Item Count", i);
                packet.Translator.ReadUInt32("Required Item Display Id", i);
            }

            uint countCurrencies = packet.Translator.ReadUInt32("Number of Required Currencies");
            for (int i = 0; i < countCurrencies; i++)
            {
                packet.Translator.ReadUInt32("Required Currency Id", i);
                packet.Translator.ReadUInt32("Required Currency Count", i);
            }

            // flags, if any of these flags is 0 quest is not completable
            packet.Translator.ReadUInt32("Unk flags 1"); // 2
            packet.Translator.ReadUInt32("Unk flags 2"); // 4
            packet.Translator.ReadUInt32("Unk flags 3"); // 8
            packet.Translator.ReadUInt32("Unk flags 4"); // 16
            packet.Translator.ReadUInt32("Unk flags 5"); // 64

            Storage.QuestRequestItems.Add(requestItems, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE)]
        public static void HandleQuestOfferReward(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            uint entry = packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadCString("Title");
            string text = packet.Translator.ReadCString("Text");
            QuestOfferReward offerReward = new QuestOfferReward
            {
                ID = entry,
                RewardText = text
            };

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
                packet.Translator.ReadBool<Int32>("Auto Finish");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");

            packet.Translator.ReadUInt32("Suggested Players");

            uint count1 = packet.Translator.ReadUInt32("Emote Count");
            uint?[] emoteIDs = {0, 0, 0, 0};
            uint?[] emoteDelays = {0, 0, 0, 0};
            for (int i = 0; i < count1; i++)
            {
                emoteDelays[i] = packet.Translator.ReadUInt32("Emote Delay", i);
                emoteIDs[i] = (uint)packet.Translator.ReadUInt32E<EmoteType>("Emote Id", i);
            }
            offerReward.Emote = emoteIDs;
            offerReward.EmoteDelay = emoteDelays;

            ReadExtraQuestInfo(packet);

            Storage.QuestOfferRewards.Add(offerReward, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD)]
        public static void HandleQuestChooseReward(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadUInt32("Reward");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_INVALID_QUEST)]
        public static void HandleQuestInvalid(Packet packet)
        {
            packet.Translator.ReadUInt32E<QuestReasonType>("Reason");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_FAILED)]
        public static void HandleQuestFailed(Packet packet)
        {
            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadUInt32E<QuestReasonType>("Reason");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleQuestCompleted(Packet packet)
        {
            packet.Translator.ReadInt32<QuestId>("Quest ID");
            packet.Translator.ReadInt32("Reward");
            packet.Translator.ReadInt32("Money");
            packet.Translator.ReadInt32("Honor");
            packet.Translator.ReadInt32("Talents");
            packet.Translator.ReadInt32("Arena Points");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleQuestCompleted406(Packet packet)
        {
            packet.Translator.ReadBit("Unk");
            packet.Translator.ReadUInt32("Reward Skill Id");
            packet.Translator.ReadInt32<QuestId>("Quest ID");
            packet.Translator.ReadInt32("Money");
            packet.Translator.ReadInt32("Talent Points");
            packet.Translator.ReadUInt32("Reward Skill Points");
            packet.Translator.ReadInt32("Reward XP");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleQuestCompleted422(Packet packet)
        {
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadInt32("Reward XP");
            packet.Translator.ReadInt32("Money");
            packet.Translator.ReadInt32("Reward Skill Points");
            packet.Translator.ReadInt32<QuestId>("Quest ID");
            packet.Translator.ReadInt32("Reward Skill Id");
            packet.Translator.ReadInt32("Talent Points");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestCompleted434(Packet packet)
        {
            packet.Translator.ReadInt32("Talent Points");
            packet.Translator.ReadInt32("RewSkillPoints");
            packet.Translator.ReadInt32("Money");
            packet.Translator.ReadInt32("XP");
            packet.Translator.ReadInt32<QuestId>("Quest ID");
            packet.Translator.ReadInt32("RewSkillId");
            packet.Translator.ReadBit("Unk Bit 1"); // if true EVENT_QUEST_FINISHED is fired, target cleared and gossip window is open
            packet.Translator.ReadBit("Unk Bit 2");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQuestCompleted510(Packet packet)
        {
            packet.Translator.ReadInt32("Talent Points");
            packet.Translator.ReadInt32("Money");
            packet.Translator.ReadInt32<QuestId>("Quest ID");
            packet.Translator.ReadInt32("XP");
            packet.Translator.ReadInt32("RewSkillPoints");
            packet.Translator.ReadInt32("RewSkillId");
            packet.Translator.ReadBit("Unk Bit 1"); // if true EVENT_QUEST_FINISHED is fired, target cleared and gossip window is open
            packet.Translator.ReadBit("Unk Bit 2");
        }

        [Parser(Opcode.CMSG_QUEST_LOG_SWAP_QUEST)]
        public static void HandleQuestSwapQuest(Packet packet)
        {
            packet.Translator.ReadByte("Slot 1");
            packet.Translator.ReadByte("Slot 2");
        }

        [Parser(Opcode.CMSG_QUEST_LOG_REMOVE_QUEST)]
        public static void HandleQuestRemoveQuest(Packet packet)
        {
            packet.Translator.ReadByte("Slot");
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_KILL)]
        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_ITEM)]
        public static void HandleQuestUpdateAdd(Packet packet)
        {
            packet.Translator.ReadInt32<QuestId>("Quest ID");
            var entry = packet.Translator.ReadEntry();
            packet.AddValue("Entry", StoreGetters.GetName(entry.Value ? StoreNameType.GameObject : StoreNameType.Unit, entry.Key));

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.Translator.ReadInt16("Count");
            else
                packet.Translator.ReadInt32("Count");

            packet.Translator.ReadInt32("Required Count");
            packet.Translator.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.Translator.ReadByteE<QuestRequirementType>("Quest Requirement Type");
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS)]
        [Parser(Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatus(Packet packet)
        {
            uint count = 1;
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE, Direction.ServerToClient))
                count = packet.Translator.ReadUInt32("Count");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                for (var i = 0; i < count; i++)
                {
                    packet.Translator.ReadGuid("GUID", i);
                    packet.Translator.ReadInt32E<QuestGiverStatus4x>("Status", i);
                }
            else
                for (var i = 0; i < count; i++)
                {
                    packet.Translator.ReadGuid("GUID", i);
                    packet.Translator.ReadByteE<QuestGiverStatus>("Status", i);
                }
        }

        [Parser(Opcode.SMSG_QUEST_UPDATE_ADD_PVP_CREDIT)]
        public static void HandleQuestUpdateAddPvPCredit(Packet packet)
        {
            packet.Translator.ReadInt32<QuestId>("Quest ID");
            packet.Translator.ReadInt32("Count");
            packet.Translator.ReadInt32("Required Count");
        }

        [Parser(Opcode.SMSG_QUEST_CONFIRM_ACCEPT)]
        public static void HandleQuestConfirAccept(Packet packet)
        {
            packet.Translator.ReadInt32<QuestId>("Quest ID");
            packet.Translator.ReadCString("Title");
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.MSG_QUEST_PUSH_RESULT)]
        public static void HandleQuestPushResult(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            if (packet.Direction == Direction.ClientToServer)
                packet.Translator.ReadUInt32("Quest Id");
            packet.Translator.ReadByteE<QuestPartyResult>("Result");
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
