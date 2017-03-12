using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetRuneCount
    {
        public byte Frost;
        public byte Unholy;
        public byte Blood;
    }
}
