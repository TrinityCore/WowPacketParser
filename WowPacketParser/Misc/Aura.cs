using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public class Aura
    {
        public uint Slot;

        public uint SpellId;

        public AuraFlag AuraFlags;

        public uint Level;

        public uint Charges;

        public Guid CasterGuid;

        public int MaxDuration;

        public int Duration;
    }
}
