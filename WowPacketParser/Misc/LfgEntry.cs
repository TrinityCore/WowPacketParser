using PacketParser.Enums;
using System.Text;
using PacketParser.Processing;

namespace PacketParser.DataStructures
{
    public struct LfgEntry
    {
        public LfgEntry(int full)
        {
            Full = full;
        }

        public readonly int Full;

        public LfgType LfgType
        {
            get { return (LfgType) ((Full & 0xFF000000) >> 24); }
        }

        public int InstanceId
        {
            get { return (Full & 0x00FFFFFF) >> 0; }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(80);
            builder.Append("Full: 0x");
            builder.Append(Full.ToString("X4"));
            builder.Append(" Type: ");
            builder.Append(LfgType);
            builder.Append(" Instance: ");
            builder.Append(PacketFileProcessor.Current.GetProcessor<NameStore>().GetName(StoreNameType.LFGDungeon, InstanceId));
            return builder.ToString();
        }

        public bool Equals(LfgEntry other)
        {
            return Full == other.Full;
        }

        public override bool Equals(object obj)
        {
            if (obj is LfgEntry)
                return (LfgEntry)obj == this;

            return false;
        }

        public static bool operator ==(LfgEntry a, LfgEntry b)
        {
            return a.Full == b.Full;
        }

        public static bool operator !=(LfgEntry a, LfgEntry b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Full.GetHashCode();
        }
    }
}
