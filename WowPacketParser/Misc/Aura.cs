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
