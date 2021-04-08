using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class PVPInfo : IPVPInfo
    {
        public uint Field_0 { get; set; }
        public uint Field_4 { get; set; }
        public uint Field_8 { get; set; }
        public uint Field_C { get; set; }
        public uint Rating { get; set; }
        public uint Field_14 { get; set; }
        public uint Field_18 { get; set; }
        public uint PvpTierID { get; set; }
        public bool Field_20 { get; set; }
    }
}

