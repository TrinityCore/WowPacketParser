using WowPacketParser.Messages.Client;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCalendarUpdateEvent
    {
        public uint MaxSize;
        public ClientCalendarUpdateEventInfo EventInfo;
    }
}
