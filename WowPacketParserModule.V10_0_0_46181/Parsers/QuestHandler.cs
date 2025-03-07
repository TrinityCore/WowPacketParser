using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class QuestHandler
    {
        public enum ConditionalTextType
        {
            Description    = 0,
            CompletionLog  = 1,
            OfferReward    = 2,
            RequestItems   = 3
        }

        public static void ReadConditionalQuestText(Packet packet, int questId, int idx, ConditionalTextType type, params object[] indexes)
        {
            var playerConditionId = packet.ReadInt32("PlayerConditionID", indexes);
            var questgiverCreatureId = packet.ReadInt32("QuestGiverCreatureID", indexes);

            packet.ResetBitReader();
            var textLength = packet.ReadBits(12);
            var text = packet.ReadWoWString("Text", textLength, indexes);

            QuestTextConditional questTextConditional = new QuestTextConditional
            {
                QuestId = questId,
                PlayerConditionId = playerConditionId,
                QuestgiverCreatureId = questgiverCreatureId,
                Text = text,
                OrderIndex = idx
            };

            switch (type)
            {
                case ConditionalTextType.Description:
                    Storage.QuestDescriptionConditional.Add(new QuestDescriptionConditional(questTextConditional), packet.TimeSpan);
                    break;
                case ConditionalTextType.CompletionLog:
                    Storage.QuestCompletionLogConditional.Add(new QuestCompletionLogConditional(questTextConditional), packet.TimeSpan);
                    break;
                case ConditionalTextType.OfferReward:
                    Storage.QuestOfferRewardConditional.Add(new QuestOfferRewardConditional(questTextConditional), packet.TimeSpan);
                    break;
                case ConditionalTextType.RequestItems:
                    Storage.QuestRequestItemsConditional.Add(new QuestRequestItemsConditional(questTextConditional), packet.TimeSpan);
                    break;
                default:
                    break;
            }
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

            CoreParsers.QuestHandler.AddQuestStarter(questgiverGUID, (uint)id);

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
                packet.ReadInt32("ObjectID", i);
                packet.ReadInt32("Amount", i);
                packet.ReadByte("Type", i);
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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_7_54577))
                packet.ReadBit("FromContentPush");
            packet.ReadBit("Unused");
            packet.ReadBit("StartCheat");
            packet.ReadBit("DisplayPopup");

            V9_0_1_36216.Parsers.QuestHandler.ReadQuestRewards(packet, "QuestRewards");

            packet.ReadWoWString("QuestTitle", questTitleLen);
            packet.ReadWoWString("DescriptionText", descriptionTextLen);
            packet.ReadWoWString("LogDescription", logDescriptionLen);
            packet.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            for (int i = 0; i < conditionalDescriptionTextCount; i++)
                ReadConditionalQuestText(packet, id, i, ConditionalTextType.Description, i, "ConditionalDescriptionText");

            Storage.QuestDetails.Add(questDetails, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS)]
        public static void HandleQuestGiverRequestItems(Packet packet)
        {
            var requestItems = packet.Holder.QuestGiverRequestItems = new();
            var questgiverGUID = packet.ReadPackedGuid128("QuestGiverGUID");
            requestItems.QuestGiver = questgiverGUID;
            requestItems.QuestGiverEntry = (uint)packet.ReadInt32("QuestGiverCreatureID");

            int id = packet.ReadInt32("QuestID");
            int delay = requestItems.EmoteDelay = packet.ReadInt32("EmoteDelay");
            int emote = requestItems.EmoteType = packet.ReadInt32("EmoteType");
            requestItems.QuestId = (uint)id;

            CoreParsers.QuestHandler.AddQuestEnder(questgiverGUID, (uint)id);

            for (int i = 0; i < 3; i++)
            {
                var flags = packet.ReadInt32("QuestFlags", i);
                if (i == 0)
                    requestItems.QuestFlags = (uint)flags;
                else if (i == 1)
                    requestItems.QuestFlags2 = (uint)flags;
            }

            requestItems.SuggestedPartyMembers = packet.ReadInt32("SuggestPartyMembers");
            requestItems.MoneyToGet = packet.ReadInt32("MoneyToGet");

            var collectCount = requestItems.CollectCount = packet.ReadUInt32("CollectCount");
            var currencyCount = requestItems.CurrencyCount = packet.ReadUInt32("CurrencyCount");
            QuestStatusFlags statusFlags = packet.ReadInt32E<QuestStatusFlags>("StatusFlags");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
                packet.ReadInt32("QuestInfoID");
            requestItems.StatusFlags = (PacketQuestStatusFlags)statusFlags;
            bool isComplete = (statusFlags & (QuestStatusFlags.Complete)) == QuestStatusFlags.Complete;
            bool noRequestOnComplete = (statusFlags & QuestStatusFlags.NoRequestOnComplete) != 0;

            for (int i = 0; i < collectCount; i++)
            {
                var objectId = packet.ReadInt32("ObjectID", i);
                var amount = packet.ReadInt32("Amount", i);
                var flags = packet.ReadUInt32("Flags", i);
                requestItems.Collect.Add(new QuestCollect()
                {
                    Id = objectId,
                    Count = amount,
                    Flags = flags
                });
            }

            for (int i = 0; i < currencyCount; i++)
            {
                var currencyId = packet.ReadInt32("CurrencyID", i);
                var amount = packet.ReadInt32("Amount", i);
                requestItems.Currencies.Add(new Currency()
                {
                    Id = (uint)currencyId,
                    Count = (uint)amount
                });
            }

            packet.ResetBitReader();

            requestItems.AutoLaunched = packet.ReadBit("AutoLaunched");

            packet.ResetBitReader();
            packet.ReadInt32("QuestGiverCreatureID"); // questgiver entry?
            var conditionalCompletionTextCount = packet.ReadUInt32();

            uint questTitleLen = 0;
            uint completionTextLen = 0;

            questTitleLen = packet.ReadBits(9);
            completionTextLen = packet.ReadBits(12);

            for (int i = 0; i < conditionalCompletionTextCount; i++)
                ReadConditionalQuestText(packet, id, i, ConditionalTextType.RequestItems, i, "ConditionalCompletionText");

            requestItems.QuestTitle = packet.ReadWoWString("QuestTitle", questTitleLen);
            string completionText = requestItems.CompletionText = packet.ReadWoWString("CompletionText", completionTextLen);

            CoreParsers.QuestHandler.QuestRequestItemHelper(id, completionText, delay, emote, isComplete, packet, noRequestOnComplete);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS && completionText != string.Empty)
            {
                QuestRequestItemsLocale localesQuestRequestItems = new QuestRequestItemsLocale
                {
                    ID = (uint)id,
                    CompletionText = completionText
                };
                Storage.LocalesQuestRequestItems.Add(localesQuestRequestItems, packet.TimeSpan);
            }
        }

        public static QuestOfferReward ReadQuestGiverOfferRewardData(Packet packet, params object[] indexes)
        {
            var emotesCount = 0u;
            var questgiverGUID = packet.ReadPackedGuid128("QuestGiverGUID", indexes);

            packet.ReadInt32("QuestGiverCreatureID", indexes);
            int id = packet.ReadInt32("QuestID", indexes);

            QuestOfferReward questOfferReward = new QuestOfferReward
            {
                ID = (uint)id
            };

            CoreParsers.QuestHandler.AddQuestEnder(questgiverGUID, (uint)id);

            for (int i = 0; i < 3; i++)
                packet.ReadInt32("QuestFlags", indexes, i);

            packet.ReadInt32("SuggestedPartyMembers", indexes);
            emotesCount = packet.ReadUInt32("EmotesCount", indexes);

            // QuestDescEmote
            questOfferReward.Emote = new int?[] { 0, 0, 0, 0 };
            questOfferReward.EmoteDelay = new uint?[] { 0, 0, 0, 0 };
            for (var i = 0; i < emotesCount; i++)
            {
                questOfferReward.Emote[i] = packet.ReadInt32("Type", indexes);
                questOfferReward.EmoteDelay[i] = packet.ReadUInt32("Delay", indexes);
            }

            packet.ResetBitReader();

            packet.ReadBit("AutoLaunched", indexes);
            packet.ReadBit("Unused", indexes);

            V9_0_1_36216.Parsers.QuestHandler.ReadQuestRewards(packet, "QuestRewards", indexes);

            return questOfferReward;
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE)]
        public static void QuestGiverOfferReward(Packet packet)
        {
            var questOfferReward = ReadQuestGiverOfferRewardData(packet, "QuestGiverOfferRewardData");

            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitGiver");
            packet.ReadInt32("PortraitGiverMount");
            packet.ReadInt32("PortraitGiverModelSceneID");
            packet.ReadInt32("PortraitTurnIn");
            packet.ReadInt32("QuestGiverCreatureID");
            var conditionalRewardTextCount = packet.ReadUInt32();

            packet.ResetBitReader();

            uint questTitleLen = packet.ReadBits(9);
            uint rewardTextLen = packet.ReadBits(12);
            uint portraitGiverTextLen = packet.ReadBits(10);
            uint portraitGiverNameLen = packet.ReadBits(8);
            uint portraitTurnInTextLen = packet.ReadBits(10);
            uint portraitTurnInNameLen = packet.ReadBits(8);

            for (int i = 0; i < conditionalRewardTextCount; i++)
                ReadConditionalQuestText(packet, (int)questOfferReward.ID, i, ConditionalTextType.OfferReward, i, "ConditionalRewardText");

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

        [Parser(Opcode.CMSG_QUEST_GIVER_STATUS_TRACKED_QUERY)]
        public static void HandleQuestGiverStatusTrackedQuery(Packet packet)
        {
            var guidCount = packet.ReadUInt32("GUIDCount");
            for (var i = 0; i< guidCount; i++)
            {
                packet.ReadPackedGuid128("QuestGiverGUID", i);
            }
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_ITEM_USABILITY)]
        public static void QueryQuestItemUsability(Packet packet)
        {
            packet.ReadPackedGuid128("CreatureGUID");
            var itemGuidCount = packet.ReadUInt32("ItemGuidCount");
            for (var i = 0; i < itemGuidCount; ++i)
                packet.ReadPackedGuid128("ItemGUID", i);
        }

        [Parser(Opcode.SMSG_UI_MAP_QUEST_LINES_RESPONSE)]
        public static void HandleUiMapQuestLinesResponse(Packet packet)
        {
            var uiMap = packet.ReadInt32("UiMapID");
            var questLineXQuestCount = packet.ReadUInt32();
            var questCount = packet.ReadUInt32();

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
        }

        [Parser(Opcode.CMSG_SPAWN_TRACKING_UPDATE)]
        public static void HandleSpawnTrackingVignette(Packet packet)
        {
            var count = packet.ReadUInt32("SpawnTrackingCount");

            for (var i = 0; i < count; i++)
            {
                packet.ReadUInt32("SpawnTrackingID", i);
                packet.ReadInt32("ObjectID", i);
                packet.ReadInt32("ObjectTypeMask", i);
            }
        }

        [Parser(Opcode.SMSG_QUEST_POI_UPDATE_RESPONSE)]
        public static void HandleQuestPOIUpdateResponse(Packet packet)
        {
            var count = packet.ReadUInt32("SpawnTrackingCount");

            for (var i = 0; i < count; i++)
            {
                var spawnTrackingId = packet.ReadUInt32("SpawnTrackingID", i);
                packet.ReadInt32("ObjectID", i);
                var phaseId = packet.ReadInt32("PhaseID", i);
                var phaseGroup = packet.ReadInt32("PhaseGroupID", i);
                var phaseUseFlags = packet.ReadInt32("PhaseUseFlags", i);

                packet.ResetBitReader();

                packet.ReadBit("Visible", i);

                SpawnTrackingTemplate spawnTrackingTemplate = new SpawnTrackingTemplate
                {
                    SpawnTrackingId = spawnTrackingId,
                    MapId = CoreParsers.MovementHandler.CurrentMapId,
                    PhaseId = phaseId,
                    PhaseGroup = phaseGroup,
                    PhaseUseFlags = (byte)phaseUseFlags
                };

                Storage.SpawnTrackingTemplates.Add(spawnTrackingTemplate, packet.TimeSpan);
            }
        }
    }
}
