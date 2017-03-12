using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct BattlePayProductGroup
    {
        public uint GroupID;
        public string Name;
        public int IconFileDataID;
        public byte DisplayType;
        public int Ordering;
    }
}
