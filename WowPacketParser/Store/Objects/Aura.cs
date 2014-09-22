using System;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public class Aura
    {
        public uint Slot;

        public uint SpellId;

        public Enum AuraFlags;

        public uint Level;

        public uint Charges;

        public WowGuid CasterGuid;

        public int MaxDuration;

        public int Duration;
    }
}
