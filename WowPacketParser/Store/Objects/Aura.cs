using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Store.Objects
{
    public class Aura
    {
        public uint Slot;

        public uint SpellId;

        public System.Enum AuraFlags;

        public uint Level;

        public uint Charges;

        public Guid CasterGuid;

        public int MaxDuration;

        public int Duration;
    }
}
