using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAreaTriggerReShape
    {
        public CliAreaTriggerPolygon AreaTriggerPolygon;
        public ulong TriggerGUID;
    }
}
