using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientLFGuildSetGuildPost
    {
        public string Comment;
        public int Availability;
        public bool Active;
        public int PlayStyle;
        public int ClassRoles;
        public int LevelRange;
    }
}
