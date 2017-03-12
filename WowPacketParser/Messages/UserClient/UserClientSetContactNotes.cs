using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSetContactNotes
    {
        public QualifiedGUID Player;
        public string Notes;
    }
}
