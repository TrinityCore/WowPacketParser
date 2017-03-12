using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientResetAreaTrigger
    {
        public ulong TriggerGUID;
        public CliAreaTrigger AreaTrigger;
    }
}
