using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientRealmQueryResponse
    {
        public uint VirtualRealmAddress;
        public byte LookupState;
        public VirtualRealmNameInfo NameInfo;
    }
}
