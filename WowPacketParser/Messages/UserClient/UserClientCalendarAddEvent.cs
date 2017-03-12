using WowPacketParser.Messages.Client;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCalendarAddEvent
    {
        public uint MaxSize;
        public ClientCalendarAddEventInfo EventInfo;
    }
}
