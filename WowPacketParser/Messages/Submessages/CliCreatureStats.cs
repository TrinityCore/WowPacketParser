using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliCreatureStats
    {
        public string Title;
        public string TitleAlt;
        public string CursorName;
        public int CreatureType;
        public int CreatureFamily;
        public int Classification;
        public float HpMulti;
        public float EnergyMulti;
        public bool Leader;
        public List<int> QuestItems;
        public int CreatureMovementInfoID;
        public int RequiredExpansion;
        public fixed int Flags[2];
        public fixed int ProxyCreatureID[2];
        public fixed int CreatureDisplayID[4];
        public string[/*4*/] Name;
        public string[/*4*/] NameAlt;
    }
}
