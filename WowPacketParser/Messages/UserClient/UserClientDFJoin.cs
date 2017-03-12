using System.Collections.Generic;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientDFJoin
    {
        public bool QueueAsGroup;
        public uint Roles;
        public byte PartyIndex;
        public string Comment;
        public List<uint> Slots;
        public fixed uint Needs[3];
    }
}
