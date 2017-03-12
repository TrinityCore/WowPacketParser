using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientGMTicketCreate
    {
        public Vector3 Pos;
        public int MapID;
        public byte Flags;
        public bool NeedMoreHelp;
        public bool NeedResponse;
        public string Description;
        public Data ChatHistoryData;
    }
}
