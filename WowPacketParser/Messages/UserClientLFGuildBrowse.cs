using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientLFGuildBrowse
    {
        public int CharacterLevel;
        public int Availability;
        public int ClassRoles;
        public int PlayStyle;
    }
}
