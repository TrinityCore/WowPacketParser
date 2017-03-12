using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildRosterMemberData
    {
        public ulong Guid;
        public long WeeklyXP;
        public long TotalXP;
        public int RankID;
        public int AreaID;
        public int PersonalAchievementPoints;
        public int GuildReputation;
        public int GuildRepToCap;
        public float LastSave;
        public string Name;
        public uint VirtualRealmAddress;
        public string Note;
        public string OfficerNote;
        public byte Status;
        public byte Level;
        public byte ClassID;
        public byte Gender;
        public bool Authenticated;
        public bool SorEligible;
        public ClientGuildRosterProfessionData[/*2*/] Profession;
    }
}
