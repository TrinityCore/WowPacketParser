using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct LFGuildRecruitData
    {
        public ulong RecruitGUID;
        public string Name;
        public uint RecruitVirtualRealm;
        public string Comment;
        public int CharacterClass;
        public int CharacterGender;
        public int CharacterLevel;
        public int ClassRoles;
        public int PlayStyle;
        public int Availability;
        public uint SecondsSinceCreated;
        public uint SecondsUntilExpiration;
    }
}
