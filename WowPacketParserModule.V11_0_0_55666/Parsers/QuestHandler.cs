using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using ConditionalTextType = WowPacketParserModule.V10_0_0_46181.Parsers.QuestHandler.ConditionalTextType;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class QuestHandler
    {
        public static ItemInstance ReadQuestChoiceItem(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBitsE<LootItemType>("LootItemType", 2, idx);
            var hasContextFlags = packet.ReadBit();
            var itemInstance = Substructures.ItemHandler.ReadItemInstance(packet, idx);
            packet.ReadInt32("Quantity", idx);
            if (hasContextFlags)
                packet.ReadInt32("ContextFlags", idx);
            return itemInstance;
        }

        public static void ReadQuestRewardItem(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadInt32("ItemID", idx);
            packet.ReadInt32("ItemQty", idx);
            var hasContextFlags = packet.ReadBit();

            if (hasContextFlags)
                packet.ReadInt32("ContextFlags", idx);
        }

        public static void ReadQuestRewardCurrency(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadInt32("CurrencyID", idx);
            packet.ReadInt32("CurrencyQty", idx);
            packet.ReadInt32("BonusQty", idx);
            var hasContextFlags = packet.ReadBit();

            if (hasContextFlags)
                packet.ReadInt32("ContextFlags", idx);
        }

        public static void ReadQuestRewards(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();

            for (var i = 0; i < 4; ++i)
            {
                ReadQuestRewardItem(packet, idx, "QuestRewardItem");
            }

            packet.ReadInt32("ChoiceItemCount", idx);
            packet.ReadInt32("ItemCount", idx);
            packet.ReadInt32("RewardMoney", idx);
            packet.ReadInt32("XP", idx);
            packet.ReadInt64("ArtifactXP", idx);
            packet.ReadInt32("ArtifactCategoryID", idx);
            packet.ReadInt32("Honor", idx);
            packet.ReadInt32("Title", idx);
            packet.ReadInt32("FactionFlags", idx);

            for (var i = 0; i < 5; ++i)
            {
                packet.ReadInt32("FactionID", idx, i);
                packet.ReadInt32("FactionValue", idx, i);
                packet.ReadInt32("FactionOverride", idx, i);
                packet.ReadInt32("FactionCapIn", idx, i);
            }

            for (var i = 0; i < 3; ++i)
                packet.ReadInt32("SpellCompletionDisplayID", idx, i);

            packet.ReadInt32("SpellCompletionID", idx);

            packet.ReadInt32("SkillLineID", idx);
            packet.ReadInt32("NumSkillUps", idx);
            var treasurePickerCount = packet.ReadUInt32();
            for (var i = 0u; i < treasurePickerCount; ++i)
                packet.ReadInt32("TreasurePickerID", idx, i);

            for (var i = 0; i < 4; ++i)
            {
                ReadQuestRewardCurrency(packet, idx, i);
            }

            packet.ResetBitReader();
            packet.ReadBit("IsBoostSpell", idx);

            for (var i = 0; i < 6; ++i)
                ReadQuestChoiceItem(packet, idx, "ItemChoiceData", i);
        }

        public static QuestOfferReward ReadQuestGiverOfferRewardData(Packet packet, params object[] indexes)
        {
            var emotesCount = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_2_55959))
            {
                ReadQuestRewards(packet, indexes, "QuestRewards");
                emotesCount = packet.ReadUInt32("EmotesCount", indexes);
            }
            var questgiverGUID = packet.ReadPackedGuid128("QuestGiverGUID", indexes);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_2_55959))
            {
                for (int i = 0; i < 3; i++)
                    packet.ReadInt32("QuestFlags", indexes, i);
            }

            packet.ReadInt32("QuestGiverCreatureID", indexes);
            var id = packet.ReadInt32("QuestID", indexes);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_0_2_55959))
            {
                for (int i = 0; i < 3; i++)
                    packet.ReadInt32("QuestFlags", indexes, i);
            }

            QuestOfferReward questOfferReward = new QuestOfferReward
            {
                ID = (uint)id
            };

            CoreParsers.QuestHandler.AddQuestEnder(questgiverGUID, (uint)id);

            packet.ReadInt32("SuggestedPartyMembers", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_2_55959))
                packet.ReadInt32("QuestInfoID", indexes);
            else
                emotesCount = packet.ReadUInt32("EmotesCount", indexes);

            // QuestDescEmote
            questOfferReward.Emote = new int?[] { 0, 0, 0, 0 };
            questOfferReward.EmoteDelay = new uint?[] { 0, 0, 0, 0 };
            for (var i = 0; i < emotesCount; i++)
            {
                questOfferReward.Emote[i] = packet.ReadInt32("Type", indexes, "Emote");
                questOfferReward.EmoteDelay[i] = packet.ReadUInt32("Delay", indexes, "Emote");
            }

            packet.ResetBitReader();
            packet.ReadBit("AutoLaunched", indexes);
            packet.ReadBit("Unused", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_2_55959))
                packet.ReadBit("ResetByScheduler", indexes);
            else
                ReadQuestRewards(packet, indexes, "QuestRewards");

            return questOfferReward;
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS)]
        public static void HandleQuestGiverQuestDetails(Packet packet)
        {
            var questgiverGUID = packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadPackedGuid128("InformUnit");

            int id = packet.ReadInt32("QuestID");
            QuestDetails questDetails = new QuestDetails
            {
                ID = (uint)id
            };

            WowPacketParser.Parsing.Parsers.QuestHandler.AddQuestStarter(questgiverGUID, (uint)id);

            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitGiver");
            packet.ReadInt32("PortraitGiverMount");
            packet.ReadInt32("PortraitGiverModelSceneID");
            packet.ReadInt32("PortraitTurnIn");

            for (int i = 0; i < 3; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("SuggestedPartyMembers");
            var learnSpellsCount = packet.ReadUInt32("LearnSpellsCount");

            var descEmotesCount = packet.ReadUInt32("DescEmotesCount");
            var objectivesCount = packet.ReadUInt32("ObjectivesCount");
            packet.ReadInt32("QuestStartItemID");
            packet.ReadInt32("QuestInfoID");
            packet.ReadInt32("QuestSessionBonus");
            packet.ReadInt32("QuestGiverCreatureID");
            var conditionalDescriptionTextCount = packet.ReadUInt32();

            for (var i = 0; i < learnSpellsCount; i++)
                packet.ReadInt32("LearnSpells", i);

            questDetails.Emote = new uint?[] { 0, 0, 0, 0 };
            questDetails.EmoteDelay = new uint?[] { 0, 0, 0, 0 };
            for (var i = 0; i < descEmotesCount; i++)
            {
                questDetails.Emote[i] = (uint)packet.ReadInt32("Type", i);
                questDetails.EmoteDelay[i] = packet.ReadUInt32("Delay", i);
            }

            for (var i = 0; i < objectivesCount; i++)
            {
                packet.ReadInt32("ObjectiveID", i);
                packet.ReadInt32("Type", i);
                packet.ReadInt32("ObjectID", i);
                packet.ReadInt32("Amount", i);
            }

            packet.ResetBitReader();

            uint questTitleLen = packet.ReadBits(9);
            uint descriptionTextLen = packet.ReadBits(12);
            uint logDescriptionLen = packet.ReadBits(12);
            uint portraitGiverTextLen = packet.ReadBits(10);
            uint portraitGiverNameLen = packet.ReadBits(8);
            uint portraitTurnInTextLen = packet.ReadBits(10);
            uint portraitTurnInNameLen = packet.ReadBits(8);

            packet.ReadBit("AutoLaunched");
            packet.ReadBit("FromContentPush");
            packet.ReadBit("Unused");
            packet.ReadBit("StartCheat");
            packet.ReadBit("DisplayPopup");

            ReadQuestRewards(packet, "QuestRewards");

            packet.ReadWoWString("QuestTitle", questTitleLen);
            packet.ReadWoWString("DescriptionText", descriptionTextLen);
            packet.ReadWoWString("LogDescription", logDescriptionLen);
            packet.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            for (int i = 0; i < conditionalDescriptionTextCount; i++)
                V10_0_0_46181.Parsers.QuestHandler.ReadConditionalQuestText(packet, id, i, ConditionalTextType.Description, i, "ConditionalDescriptionText");

            Storage.QuestDetails.Add(questDetails, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE)]
        public static void QuestGiverOfferReward(Packet packet)
        {
            var questOfferReward = ReadQuestGiverOfferRewardData(packet, "QuestGiverOfferRewardData");
            packet.ResetBitReader();

            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitGiver");
            packet.ReadInt32("PortraitGiverMount");
            packet.ReadInt32("PortraitGiverModelSceneID");
            packet.ReadInt32("PortraitTurnIn");
            packet.ReadInt32("QuestGiverCreatureID");
            var conditionalRewardTextCount = packet.ReadUInt32();

            uint questTitleLen = packet.ReadBits(9);
            uint rewardTextLen = packet.ReadBits(12);
            uint portraitGiverTextLen = packet.ReadBits(10);
            uint portraitGiverNameLen = packet.ReadBits(8);
            uint portraitTurnInTextLen = packet.ReadBits(10);
            uint portraitTurnInNameLen = packet.ReadBits(8);

            for (int i = 0; i < conditionalRewardTextCount; i++)
                V10_0_0_46181.Parsers.QuestHandler.ReadConditionalQuestText(packet, (int)questOfferReward.ID, i, ConditionalTextType.OfferReward, i, "ConditionalRewardText");

            packet.ReadWoWString("QuestTitle", questTitleLen);
            questOfferReward.RewardText = packet.ReadWoWString("RewardText", rewardTextLen);
            packet.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            Storage.QuestOfferRewards.Add(questOfferReward, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS && questOfferReward.RewardText != string.Empty)
            {
                QuestOfferRewardLocale localesQuestOfferReward = new QuestOfferRewardLocale
                {
                    ID = questOfferReward.ID,
                    RewardText = questOfferReward.RewardText
                };

                Storage.LocalesQuestOfferRewards.Add(localesQuestOfferReward, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_UI_MAP_QUEST_LINES_RESPONSE)]
        public static void HandleUiMapQuestLinesResponse(Packet packet)
        {
            var uiMap = packet.ReadInt32("UiMapID");
            var questLineXQuestCount = packet.ReadUInt32();
            var questCount = packet.ReadUInt32();
            var questLineCount = packet.ReadUInt32();

            for (int i = 0; i < questLineXQuestCount; i++)
            {
                var questLineXQuestId = packet.ReadUInt32();

                if (Settings.UseDBC && DBC.QuestLineXQuest.ContainsKey((int)questLineXQuestId))
                {
                    if (DBC.QuestLineXQuest.TryGetValue((int)questLineXQuestId, out var questLineXQuest))
                    {
                        var questLineId = questLineXQuest.QuestLineID;
                        var questId = questLineXQuest.QuestID;

                        UIMapQuestLine uiMapQuestLine = new()
                        {
                            UIMapId = (uint)uiMap,
                            QuestLineId = questLineId
                        };
                        Storage.UIMapQuestLines.Add(uiMapQuestLine, packet.TimeSpan);

                        packet.WriteLine($"[{i}] QuestLineXQuestID: {questLineXQuestId} (QuestID: {questId} QuestLineID: {questLineId})");
                    }
                }
                else
                    packet.AddValue($"QuestLineXQuestID", questLineXQuestId, i);
            }

            for (int i = 0; i < questCount; i++)
            {
                var questId = packet.ReadUInt32<QuestId>("QuestID", i);

                UIMapQuest uiMapQuest = new UIMapQuest
                {
                    UIMapId = (uint)uiMap,
                    QuestId = questId
                };
                Storage.UIMapQuests.Add(uiMapQuest, packet.TimeSpan);
            }

            for (int i = 0; i < questLineCount; i++)
            {
                var questLineId = packet.ReadUInt32("QuestLineID", i);

                UIMapQuestLine uiMapQuestLine = new()
                {
                    UIMapId = (uint)uiMap,
                    QuestLineId = questLineId
                };
                Storage.UIMapQuestLines.Add(uiMapQuestLine, packet.TimeSpan);
            }
        }

        [Parser(Opcode.CMSG_SPAWN_TRACKING_UPDATE)]
        public static void HandleSpawnTrackingVignette(Packet packet)
        {
            var count = packet.ReadUInt32("SpawnTrackingCount");

            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("ObjectTypeMask", i);
                packet.ReadInt32("ObjectID", i);
                packet.ReadUInt32("SpawnTrackingID", i);
            }
        }

        [Parser(Opcode.SMSG_FORCE_SPAWN_TRACKING_UPDATE)]
        public static void HandleForceSpawnTrackingUpdate(Packet packet)
        {
            packet.ReadInt32<QuestId>("Quest");
        }
    }
}
