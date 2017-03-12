using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAccountProfile // FIXME: No handlers
    {
        public string Filename;
        public Data Profile;
    }
}
