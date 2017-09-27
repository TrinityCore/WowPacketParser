using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryQuestInfoResponse
    {
        public bool Allow;
        public CliQuestInfo Info;
        public uint QuestID;
    }
}
