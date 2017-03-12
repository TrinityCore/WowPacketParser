using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTrainerListSpell
    {
        public int SpellID;
        public uint MoneyCost;
        public uint ReqSkillLine;
        public uint ReqSkillRank;
        public fixed int ReqAbility[3];
        public byte Usable;
        public byte ReqLevel;
    }
}
