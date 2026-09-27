using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using ConditionalTextType = WowPacketParserModule.V10_0_0_46181.Parsers.QuestHandler.ConditionalTextType;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.SMSG_QUEST_UPDATE_COMPLETE)]
        public static void HandleQuestUpdateComplete(Packet packet)
        {
            var questComplete = packet.Holder.QuestComplete = new();
            questComplete.QuestId = (uint)packet.ReadInt32<QuestId>("QuestID");
            questComplete.HideCreditMessage = packet.ReadBit("HideCreditMessage");
        }

        public static void ReadQuestRewards(Packet packet, params object[] idx)
        {
            for (var i = 0; i < 4; ++i)
                V11_0_0_55666.Parsers.QuestHandler.ReadQuestRewardItem(packet, idx, "QuestRewardItem");

            for (var i = 0; i < 4; ++i)
                V11_0_0_55666.Parsers.QuestHandler.ReadQuestRewardCurrency(packet, idx, i);

            packet.ReadInt32("ChoiceItemCount", idx);
            for (var i = 0; i < 6; ++i)
                V11_0_0_55666.Parsers.QuestHandler.ReadQuestChoiceItem(packet, idx, "ItemChoiceData", i);

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

            packet.ResetBitReader();
            packet.ReadBit("IsBoostSpell", idx);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleQuestGiverQuestDetails(Packet packet)
        {
            var questgiverGUID = packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadPackedGuid128("InformUnit");

            var id = packet.ReadInt32("QuestID");
            var questDetails = new QuestDetails
            {
                ID = (uint)id
            };

            CoreParsers.QuestHandler.AddQuestStarter(questgiverGUID, (uint)id);

            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitGiver");
            packet.ReadInt32("PortraitGiverMount");
            packet.ReadInt32("PortraitGiverModelSceneID");
            packet.ReadInt32("PortraitTurnIn");

            for (var i = 0; i < 4; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("SuggestedPartyMembers");
            var learnSpellsCount = packet.ReadUInt32("LearnSpellsCount");

            ReadQuestRewards(packet, "QuestRewards");

            var descEmotesCount = packet.ReadUInt32("DescEmotesCount");
            var objectivesCount = packet.ReadUInt32("ObjectivesCount");
            packet.ReadInt32("QuestStartItemID");
            packet.ReadInt32("QuestInfoID");
            packet.ReadInt32("QuestSessionBonus");
            packet.ReadInt32("QuestGiverCreatureID");
            var conditionalDescriptionTextCount = packet.ReadUInt32();

            for (var i = 0; i < learnSpellsCount; i++)
                packet.ReadInt32("LearnSpells", i);

            questDetails.Emote = [0, 0, 0, 0];
            questDetails.EmoteDelay = [0, 0, 0, 0];
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

            for (var i = 0; i < conditionalDescriptionTextCount; i++)
                V10_0_0_46181.Parsers.QuestHandler.ReadConditionalQuestText(packet, id, i, ConditionalTextType.Description, i, "ConditionalDescriptionText");

            packet.ResetBitReader();

            var questTitleLen = packet.ReadBits(9);
            var descriptionTextLen = packet.ReadBits(12);
            var logDescriptionLen = packet.ReadBits(12);
            var portraitGiverTextLen = packet.ReadBits(10);
            var portraitGiverNameLen = packet.ReadBits(8);
            var portraitTurnInTextLen = packet.ReadBits(10);
            var portraitTurnInNameLen = packet.ReadBits(8);

            packet.ReadBit("AutoLaunched");
            packet.ReadBit("FromContentPush");
            packet.ReadBit("Unused");
            packet.ReadBit("StartCheat");
            packet.ReadBit("DisplayPopup");

            packet.ReadWoWString("QuestTitle", questTitleLen);
            packet.ReadWoWString("DescriptionText", descriptionTextLen);
            packet.ReadWoWString("LogDescription", logDescriptionLen);
            packet.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            Storage.QuestDetails.Add(questDetails, packet.TimeSpan);
        }

        public static QuestOfferReward ReadQuestGiverOfferRewardData(Packet packet, params object[] indexes)
        {
            ReadQuestRewards(packet, indexes, "QuestRewards");
            var emotesCount = packet.ReadUInt32("EmotesCount", indexes);
            var questgiverGUID = packet.ReadPackedGuid128("QuestGiverGUID", indexes);

            for (var i = 0; i < 4; i++)
                packet.ReadInt32("QuestFlags", indexes, i);

            packet.ReadInt32("QuestGiverCreatureID", indexes);
            var id = packet.ReadInt32("QuestID", indexes);

            var questOfferReward = new QuestOfferReward
            {
                ID = (uint)id
            };

            CoreParsers.QuestHandler.AddQuestEnder(questgiverGUID, (uint)id);

            packet.ReadInt32("SuggestedPartyMembers", indexes);
            packet.ReadInt32("QuestInfoID", indexes);

            // QuestDescEmote
            questOfferReward.Emote = [0, 0, 0, 0];
            questOfferReward.EmoteDelay = [0, 0, 0, 0];
            for (var i = 0; i < emotesCount; i++)
            {
                questOfferReward.Emote[i] = packet.ReadInt32("Type", indexes, "Emote");
                questOfferReward.EmoteDelay[i] = packet.ReadUInt32("Delay", indexes, "Emote");
            }

            packet.ResetBitReader();
            packet.ReadBit("AutoLaunched", indexes);
            packet.ReadBit("Unused", indexes);
            packet.ReadBit("ResetByScheduler", indexes);

            return questOfferReward;
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE, ClientVersionBuild.V12_1_0_69214)]
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

            for (var i = 0; i < conditionalRewardTextCount; i++)
                V10_0_0_46181.Parsers.QuestHandler.ReadConditionalQuestText(packet, (int)questOfferReward.ID, i, ConditionalTextType.OfferReward, i, "ConditionalRewardText");

            var questTitleLen = packet.ReadBits(9);
            var rewardTextLen = packet.ReadBits(12);
            var portraitGiverTextLen = packet.ReadBits(10);
            var portraitGiverNameLen = packet.ReadBits(8);
            var portraitTurnInTextLen = packet.ReadBits(10);
            var portraitTurnInNameLen = packet.ReadBits(8);

            packet.ReadWoWString("QuestTitle", questTitleLen);
            questOfferReward.RewardText = packet.ReadWoWString("RewardText", rewardTextLen);
            packet.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            Storage.QuestOfferRewards.Add(questOfferReward, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS && questOfferReward.RewardText != string.Empty)
            {
                var localesQuestOfferReward = new QuestOfferRewardLocale
                {
                    ID = questOfferReward.ID,
                    RewardText = questOfferReward.RewardText
                };

                Storage.LocalesQuestOfferRewards.Add(localesQuestOfferReward, packet.TimeSpan);
            }
        }
    }
}
