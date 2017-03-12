using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryQuestInfoResponse
    {
        public bool Allow;
        public CliQuestInfo Info;
        public uint QuestID;
    }
}
