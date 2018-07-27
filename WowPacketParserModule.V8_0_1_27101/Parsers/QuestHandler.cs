using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class QuestHandler
    {
        public static void ReadQuestRewards(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("ChoiceItemCount", idx);
            packet.ReadUInt32("ItemCount", idx);

            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("ID", idx, i);
                packet.ReadInt32("Quantity", idx, i);
            }

            packet.ReadUInt32("RewardMoney", idx);
            packet.ReadUInt32("UnkUint32_2", idx);
            packet.ReadUInt64("UnkUint64_1", idx);
            packet.ReadUInt32("UnkUint32_3", idx);
            packet.ReadUInt32("UnkUint32_4", idx);
            packet.ReadUInt32("UnkUint32_5", idx);
            packet.ReadUInt32("UnkUint32_6", idx);

            for (var i = 0; i < 5; ++i)
            {
                packet.ReadInt32("FactionID", idx, i);
                packet.ReadInt32("FactionValue", idx, i);
                packet.ReadInt32("FactionOverride", idx, i);
                packet.ReadInt32("FactionCapIn", idx, i);
            }

            for (var i = 0; i < 3; ++i)
                packet.ReadInt32("SpellCompletionDisplayID", idx, i);

            packet.ReadUInt32("RewardSpell", idx);

            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("CurrencyID", idx, i);
                packet.ReadInt32("CurrencyQty", idx, i);
            }

            packet.ReadUInt32("UnkUint32_7", idx);
            packet.ReadUInt32("UnkUint32_8", idx);
            packet.ReadUInt32("UnkUint32_9", idx);

            for (var i = 0; i < 6; ++i)
            {
                packet.ReadUInt32("UnkUint32_10", idx, i);
                packet.ReadUInt32("UnkUint32_11", idx, i);
                packet.ReadUInt32("UnkUint32_12", idx, i);

                packet.ResetBitReader();

                bool unkBit = packet.ReadBit("UnkBit", idx, i);

                if (unkBit)
                {
                    packet.ReadByte("UnkByte", idx, i);
                    int count = packet.ReadInt32("UnkUint32_9", idx, i);

                    for (var j = 0; j < count; ++j)
                        packet.ReadUInt32("UnkUint32_13", idx, i, j);

                    int length = packet.ReadInt32();
                    packet.ReadWoWString("UnkString", length, idx, i);
                }
                packet.ReadUInt32("UnkUint32_15", idx, i);
            }

            packet.ResetBitReader();
            packet.ReadBit("UnkBit2", idx);
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS)]
        public static void HandleQuestGiverQuestDetails(Packet packet)
        {
            packet.ReadPackedGuid128("QuestGiverGUID");
            packet.ReadPackedGuid128("InformUnit");

            int id = packet.ReadInt32("QuestID");
            QuestDetails questDetails = new QuestDetails
            {
                ID = (uint)id
            };

            packet.ReadInt32("QuestPackageID");
            packet.ReadInt32("PortraitGiver");
            packet.ReadInt32("PortraitTurnIn");
            packet.ReadInt32("UnkInt32");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("QuestFlags", i);

            packet.ReadInt32("SuggestedPartyMembers");
            int learnSpellsCount = packet.ReadInt32("LearnSpellsCount");

            int descEmotesCount = packet.ReadInt32("DescEmotesCount");
            int objectivesCount = packet.ReadInt32("ObjectivesCount");
            packet.ReadInt32("QuestStartItemID");

            for (int i = 0; i < learnSpellsCount; i++)
                packet.ReadInt32("LearnSpells", i);

            questDetails.Emote = new uint?[] { 0, 0, 0, 0 };
            questDetails.EmoteDelay = new uint?[] { 0, 0, 0, 0 };
            for (int i = 0; i < descEmotesCount; i++)
            {
                questDetails.Emote[i] = (uint)packet.ReadInt32("Type", i);
                questDetails.EmoteDelay[i] = packet.ReadUInt32("Delay", i);
            }

            for (int i = 0; i < objectivesCount; i++)
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

            packet.ReadBit("DisplayPopup");
            packet.ReadBit("StartCheat");
            packet.ReadBit("AutoLaunched");

            ReadQuestRewards(packet);

            packet.ReadWoWString("QuestTitle", questTitleLen);
            packet.ReadWoWString("DescriptionText", descriptionTextLen);
            packet.ReadWoWString("LogDescription", logDescriptionLen);
            packet.ReadWoWString("PortraitGiverText", portraitGiverTextLen);
            packet.ReadWoWString("PortraitGiverName", portraitGiverNameLen);
            packet.ReadWoWString("PortraitTurnInText", portraitTurnInTextLen);
            packet.ReadWoWString("PortraitTurnInName", portraitTurnInNameLen);

            Storage.QuestDetails.Add(questDetails, packet.TimeSpan);
        }
    }
}
