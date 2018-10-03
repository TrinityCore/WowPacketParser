using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrType, HasIndexInData = false)]
    public class GarrTypeEntry
    {
        public uint PrimaryCurrencyTypeID { get; set; }
        public uint SecondaryCurrencyTypeID { get; set; }
        public uint ExpansionID { get; set; }
        public uint Flags { get; set; }
        [HotfixArray(2)]
        public int[] MapIDs { get; set; }
    }
}
