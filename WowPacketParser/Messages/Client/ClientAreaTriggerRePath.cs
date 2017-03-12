using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAreaTriggerRePath
    {
        public CliAreaTriggerSpline AreaTriggerSpline;
        public ulong TriggerGUID;
    }
}
