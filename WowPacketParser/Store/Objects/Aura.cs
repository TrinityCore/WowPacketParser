using PacketParser.Enums;
using PacketParser.Misc;

namespace PacketParser.DataStructures
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
