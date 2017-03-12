using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuraPointsDepleted
    {
        public ulong Unit;
        public byte Slot;
        public byte EffectIndex;
    }
}
