using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGTeleportDenied
    {
        public LfgTeleportResult Reason;
    }
}
