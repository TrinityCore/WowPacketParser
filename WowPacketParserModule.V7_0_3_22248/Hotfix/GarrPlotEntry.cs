using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GarrPlot, HasIndexInData = false)]
    public class GarrPlotEntry
    {
        public string Name { get; set; }
        public uint AllianceConstructionGameObjectID { get; set; }
        public uint HordeConstructionGameObjectID { get; set; }
        public byte GarrPlotUICategoryID { get; set; }
        public byte PlotType { get; set; }
        public byte Flags { get; set; }
        public uint MinCount { get; set; }
        public uint MaxCount { get; set; }
    }
}