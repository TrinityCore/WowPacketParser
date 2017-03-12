using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GuildPostData
    {
        public bool Active;
        public int PlayStyle;
        public int Availability;
        public int ClassRoles;
        public int LevelRange;
        public UnixTime SecondsRemaining;
        public string Comment;
    }
}
