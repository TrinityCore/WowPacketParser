using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliConfirmRespecWipe
    {
        public ulong RespecMaster;
        public byte RespecType;
    }
}
