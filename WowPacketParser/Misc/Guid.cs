using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public struct Guid
    {
        public readonly ulong Full;

        public Guid(ulong id)
            : this()
        {
            Full = id;
        }

        public bool HasEntry()
        {
            switch (GetHighType())
            {
                case HighGuidType.Player1:
                case HighGuidType.Player2:
                {
                    return false;
                }
            }

            return true;
        }

        public ulong GetLow()
        {
            switch (GetHighType())
            {
                case HighGuidType.Player1:
                case HighGuidType.Player2:
                {
                    return (Full & 0x000FFFFFFFFFFFFF) >> 0;
                }
                case HighGuidType.GameObject:
                case HighGuidType.Transport:
                case HighGuidType.MOTransport:
                {
                    return (Full & 0x0000000000FFFFFF) >> 0;
                }
            }

            return (Full & 0x00000000FFFFFFFF) >> 0;
        }

        public uint GetEntry()
        {
            if (!HasEntry())
                return 0;

            return (uint)((Full & 0x000FFFFFFF000000) >> 24);
        }

        public HighGuidType GetHighType()
        {
            return (HighGuidType)((Full & 0x00F0000000000000) >> 52);
        }

        public HighGuidMask GetHighMask()
        {
            return (HighGuidMask)((Full & 0xFF00000000000000) >> 56);
        }

        public static bool operator ==(Guid first, Guid other)
        {
            return first.Full == other.Full;
        }

        public static bool operator !=(Guid first, Guid other)
        {
            return !(first == other);
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Guid && Equals((Guid)obj);
        }

        public bool Equals(Guid other)
        {
            return other.Full == Full;
        }

        public override int GetHashCode()
        {
            return Full.GetHashCode();
        }

        public override string ToString()
        {
            return "Full: 0x" + Full.ToString("X8") + " Flags: " + GetHighMask() + " Type: " +
                GetHighType() + (HasEntry() ? " Entry: " + GetEntry() : string.Empty) + " Low: " + GetLow();
        }
    }
}
