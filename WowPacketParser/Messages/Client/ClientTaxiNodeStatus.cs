using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientTaxiNodeStatus
    {
        public Taxistatus Status;
        public ulong Unit;
    }
}
