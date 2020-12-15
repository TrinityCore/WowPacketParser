using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.GarrPlot, HasIndexInData = false)]
    public class GarrPlotEntry
    {
        public string Name { get; set; }
        public byte PlotType { get; set; }
        public int HordeConstructObjID { get; set; }
        public int AllianceConstructObjID { get; set; }
        public byte Flags { get; set; }
        public byte UiCategoryID { get; set; }
        [HotfixArray(2)]
        public uint[] UpgradeRequirement { get; set; }
    }
}
