using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAccountProfile
    {
        public string Filename;
        public Data Profile;
    }
}
