using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellModifierData
    {
        public float ModifierValue;
        public byte ClassIndex;
    }
}
