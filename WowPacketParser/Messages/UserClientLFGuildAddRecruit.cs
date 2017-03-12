using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientLFGuildAddRecruit
    {
        public ulong GuildGUID;
        public int Availability;
        public int ClassRoles;
        public int PlayStyle;
        public string Comment;
    }
}
