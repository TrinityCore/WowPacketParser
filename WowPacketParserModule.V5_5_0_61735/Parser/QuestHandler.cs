using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class QuestHandler
    {
        public static void ReadPlayerChoiceResponseRewardItem(Packet packet, int choiceId, int responseId, uint index, params object[] indexes)
        {
            ItemInstance instance = Substructures.ItemHandler.ReadItemInstance(packet, indexes);
            var quantity = packet.ReadInt32("Quantity", indexes);

            string bonusListIds = "";
            if (instance.BonusListIDs != null)
            {
                for (var i = 0; i < instance.BonusListIDs.Length; i++)
                    bonusListIds += instance.BonusListIDs[i] + " ";
                bonusListIds = bonusListIds.TrimEnd(' ');
            }

            Storage.PlayerChoiceResponseRewardItems.Add(new PlayerChoiceResponseRewardItemTemplate
            {
                ChoiceId = choiceId,
                ResponseId = responseId,
                Index = index,
                ItemId = instance.ItemID,
                BonusListIDs = bonusListIds,
                Quantity = quantity
            }, packet.TimeSpan);
        }

        public static void ReadPlayerChoiceResponseRewardCurrency(Packet packet, int choiceId, int responseId, uint index, params object[] indexes)
        {
            ItemInstance instance = Substructures.ItemHandler.ReadItemInstance(packet, indexes);
            var quantity = packet.ReadInt32("Quantity", indexes);

            Storage.PlayerChoiceResponseRewardCurrencies.Add(new PlayerChoiceResponseRewardCurrencyTemplate
            {
                ChoiceId = choiceId,
                ResponseId = responseId,
                Index = index,
                CurrencyId = instance.ItemID,
                Quantity = quantity
            }, packet.TimeSpan);
        }

        public static void ReadPlayerChoiceResponseRewardFactions(Packet packet, int choiceId, int responseId, uint index, params object[] indexes)
        {
            ItemInstance instance = Substructures.ItemHandler.ReadItemInstance(packet, indexes);
            var quantity = packet.ReadInt32("Quantity", indexes);

            Storage.PlayerChoiceResponseRewardFactions.Add(new PlayerChoiceResponseRewardFactionTemplate
            {
                ChoiceId = choiceId,
                ResponseId = responseId,
                Index = index,
                FactionId = instance.ItemID,
                Quantity = quantity
            }, packet.TimeSpan);
        }

        public static void ReadPlayerChoiceResponseRewardItemChoice(Packet packet, int choiceId, int responseId, uint index, params object[] indexes)
        {
            ItemInstance instance = Substructures.ItemHandler.ReadItemInstance(packet, indexes);
            var quantity = packet.ReadInt32("Quantity", indexes);

            // these fields are unused in client, last checked in 8.2.5
        }

        public static void ReadPlayerChoiceResponseReward(Packet packet, int choiceId, int responseId, params object[] indexes)
        {
            packet.ResetBitReader();
            var titleID = packet.ReadInt32("TitleID", indexes);
            var packageID = packet.ReadInt32("PackageID", indexes);
            var skillLineID = packet.ReadInt32("SkillLineID", indexes);
            var skillPointCount = packet.ReadUInt32("SkillPointCount", indexes);
            var arenaPointCount = packet.ReadUInt32("ArenaPointCount", indexes);
            var honorPointCount = packet.ReadUInt32("HonorPointCount", indexes);
            var money = packet.ReadUInt64("Money", indexes);
            var xp = packet.ReadUInt32("Xp", indexes);

            Storage.PlayerChoiceResponseRewards.Add(new PlayerChoiceResponseRewardTemplate
            {
                ChoiceId = choiceId,
                ResponseId = responseId,
                TitleId = titleID,
                PackageId = packageID,
                SkillLineId = skillLineID,
                SkillPointCount = skillPointCount,
                ArenaPointCount = arenaPointCount,
                HonorPointCount = honorPointCount,
                Money = money,
                Xp = xp
            }, packet.TimeSpan);

            var itemCount = packet.ReadUInt32();
            var currencyCount = packet.ReadUInt32();
            var factionCount = packet.ReadUInt32();
            var itemChoiceCount = packet.ReadUInt32();

            for (var i = 0u; i < itemCount; ++i)
                ReadPlayerChoiceResponseRewardItem(packet, choiceId, responseId, i, "Item", i);

            for (var i = 0u; i < currencyCount; ++i)
                ReadPlayerChoiceResponseRewardCurrency(packet, choiceId, responseId, i, "Currency", i);

            for (var i = 0u; i < factionCount; ++i)
                ReadPlayerChoiceResponseRewardFactions(packet, choiceId, responseId, i, "Faction", i);

            for (var i = 0u; i < itemChoiceCount; ++i)
                ReadPlayerChoiceResponseRewardItemChoice(packet, choiceId, responseId, i, "ItemChoice", i);
        }

        public static void ReadPlayerChoiceResponse(Packet packet, int choiceId, uint index, params object[] indexes)
        {
            var responseId = packet.ReadInt32("ResponseID", indexes);
            var responseIdentifier = packet.ReadInt16("ResponseIdentifier", indexes);
            var choiceArtFileId = packet.ReadInt32("ChoiceArtFileID", indexes);
            var flags = packet.ReadInt32("Flags", indexes);
            var widgetSetId = packet.ReadUInt32("WidgetSetID", indexes);
            var uiTextureAtlasElementID = packet.ReadUInt32("UiTextureAtlasElementID", indexes);
            var soundKitId = packet.ReadUInt32("SoundKitID", indexes);
            var groupID = packet.ReadByte("GroupID", indexes);
            var uiTextureKitID = packet.ReadUInt32("UiTextureKitID", indexes);

            packet.ResetBitReader();
            var answerLength = packet.ReadBits(9);
            var headerLength = packet.ReadBits(9);
            var subHeaderLength = packet.ReadBits(7);
            var buttonTooltipLength = packet.ReadBits(9);
            var descriptionLength = packet.ReadBits(11);
            var confirmationTextLength = packet.ReadBits(7);
            var hasRewardQuestID = packet.ReadBit();
            var hasReward = packet.ReadBit();
            packet.ReadBit(); // Unused
            if (hasReward)
                ReadPlayerChoiceResponseReward(packet, choiceId, responseId, "PlayerChoiceResponseReward", indexes);

            var answer = packet.ReadWoWString("Answer", answerLength, indexes);
            var header = packet.ReadWoWString("Header", headerLength, indexes);
            var subheader = packet.ReadWoWString("SubHeader", subHeaderLength, indexes);
            var buttonTooltip = packet.ReadWoWString("ButtonTooltip", buttonTooltipLength, indexes);
            var description = packet.ReadWoWString("Description", descriptionLength, indexes);
            var confirmation = packet.ReadWoWString("ConfirmationText", confirmationTextLength, indexes);

            var rewardQuestID = 0u;
            if (hasRewardQuestID)
                rewardQuestID = packet.ReadUInt32<QuestId>("RewardQuestID", indexes);

            Storage.PlayerChoiceResponses.Add(new PlayerChoiceResponseTemplate
            {
                ChoiceId = choiceId,
                ResponseIdentifier = responseIdentifier,
                ResponseId = responseId,
                Index = index,
                ChoiceArtFileId = choiceArtFileId,
                Flags = flags,
                WidgetSetId = widgetSetId,
                UiTextureAtlasElementID = uiTextureAtlasElementID,
                SoundKitId = soundKitId,
                GroupId = groupID,
                Header = header,
                Subheader = subheader,
                ButtonTooltip = buttonTooltip,
                Answer = answer,
                Description = description,
                Confirmation = confirmation,
                RewardQuestID = rewardQuestID,
                UiTextureKitID = uiTextureKitID
            }, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                Storage.PlayerChoiceResponseLocales.Add(new PlayerChoiceResponseLocaleTemplate
                {
                    ChoiceId = choiceId,
                    ResponseId = responseId,
                    Locale = ClientLocale.PacketLocaleString,
                    Header = header,
                    Subheader = subheader,
                    ButtonTooltip = buttonTooltip,
                    Description = description,
                    Answer = answer,
                    Confirmation = confirmation
                }, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_DISPLAY_PLAYER_CHOICE)]
        public static void HandleDisplayPlayerChoice(Packet packet)
        {
            var choiceId = packet.ReadInt32("ChoiceID");
            var responseCount = packet.ReadUInt32();
            packet.ReadPackedGuid128("SenderGUID");
            var uiTextureKitId = packet.ReadInt32("UiTextureKitID");
            var soundKitId = packet.ReadUInt32("SoundKitID");
            var closeSoundKitId = packet.ReadUInt32("CloseUISoundKitID");
            packet.ReadByte("NumRerolls");
            var duration = packet.ReadInt64("Duration");
            packet.ResetBitReader();
            var questionLength = packet.ReadBits(8);
            var pendingChoiceTextLength = packet.ReadBits(8);
            var infiniteRange = packet.ReadBit("InfiniteRange");
            var hideWarboardHeader = packet.ReadBit("HideWarboardHeader");
            var keepOpenAfterChoice = packet.ReadBit("KeepOpenAfterChoice");
            var showChoicesAsList = packet.ReadBit("ShowChoicesAsList");
            var forceDontShowChoicesAsList = packet.ReadBit("ForceDontShowChoicesAsList");

            for (var i = 0u; i < responseCount; ++i)
                ReadPlayerChoiceResponse(packet, choiceId, i, "PlayerChoiceResponse", i);

            var question = packet.ReadWoWString("Question", questionLength);
            var pendingChoiceText = packet.ReadWoWString("PendingChoiceText", pendingChoiceTextLength);

            Storage.PlayerChoices.Add(new PlayerChoiceTemplate
            {
                ChoiceId = choiceId,
                UiTextureKitId = uiTextureKitId,
                SoundKitId = soundKitId,
                CloseSoundKitId = closeSoundKitId,
                Duration = duration,
                Question = question,
                PendingChoiceText = pendingChoiceText,
                InfiniteRange = infiniteRange,
                HideWarboardHeader = hideWarboardHeader,
                KeepOpenAfterChoice = keepOpenAfterChoice,
                ShowChoicesAsList = showChoicesAsList,
                ForceDontShowChoicesAsList = forceDontShowChoicesAsList
            }, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                Storage.PlayerChoiceLocales.Add(new PlayerChoiceLocaleTemplate
                {
                    ChoiceId = choiceId,
                    Locale = ClientLocale.PacketLocaleString,
                    Question = question
                }, packet.TimeSpan);
            }
        }
    }
}
