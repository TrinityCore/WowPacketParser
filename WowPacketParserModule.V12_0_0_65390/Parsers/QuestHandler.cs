using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

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
    }
}
