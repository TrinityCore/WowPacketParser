using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct BattlepayDisplayInfo
    {
        public uint? CreatureDisplayInfoID; // Optional
        public uint? FileDataID; // Optional
        public string Name1;
        public string Name2;
        public string Name3;
        public uint? Flags; // Optional
    }
}
