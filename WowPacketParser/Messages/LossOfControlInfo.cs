using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct LossOfControlInfo
    {
        public byte AuraSlot;
        public byte EffectIndex;
        public int Type;
        public int Mechanic;
    }
}
