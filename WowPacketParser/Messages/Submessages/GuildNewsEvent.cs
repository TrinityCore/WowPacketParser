using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct GuildNewsEvent
    {
        public int Id;
        public Data CompletedDate;
        public int Type;
        public int Flags;
        public ulong MemberGuid;
        public List<ulong> MemberList;
        public fixed int Data[2];
    }
}
