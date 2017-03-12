using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPVPLogData_Player
    {
        public ulong PlayerGUID;
        public uint Kills;
        public byte Faction;
        public bool IsInWorld;
        public ClientPVPLogData_Honor? Honor; // Optional
        public uint DamageDone;
        public uint HealingDone;
        public uint? PreMatchRating; // Optional
        public int? RatingChange; // Optional
        public uint? PreMatchMMR; // Optional
        public int? MmrChange; // Optional
        public List<int> Stats;
        public int PrimaryTalentTree;
    }
}
