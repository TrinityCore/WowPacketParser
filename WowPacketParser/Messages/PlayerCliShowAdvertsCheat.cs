using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliShowAdvertsCheat
    {
        public uint DetailLevel;
        public float Radius;
        public bool On;
    }
}
