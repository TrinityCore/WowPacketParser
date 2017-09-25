using WowPacketParser.Messages.Client;

namespace WowPacketParser.Messages.UserClient.Calendar
{
    public unsafe struct UpdateEvent
    {
        public uint MaxSize;
        public ClientCalendarUpdateEventInfo EventInfo;
    }
}
