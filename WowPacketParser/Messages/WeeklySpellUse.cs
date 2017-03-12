using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct WeeklySpellUse
    {
        public int Category;
        public byte Uses;
    }
}
