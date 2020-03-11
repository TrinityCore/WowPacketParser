using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_2_5_31921.Hotfix
{
    [HotfixStructure(DB2Hash.UnitPowerBar, ClientVersionBuild.V8_2_5_31921, HasIndexInData = false)]
    public class UnitPowerBarEntry
    {
        public string Name { get; set; }
        public string Cost { get; set; }
        public string OutOfError { get; set; }
        public string ToolTip { get; set; }
        public uint MinPower { get; set; }
        public uint MaxPower { get; set; }
        public uint StartPower { get; set; }
        public byte CenterPower { get; set; }
        public float RegenerationPeace { get; set; }
        public float RegenerationCombat { get; set; }
        public byte BarType { get; set; }
        public ushort Flags { get; set; }
        public float StartInset { get; set; }
        public float EndInset { get; set; }
        [HotfixArray(6)]
        public int[] FileDataID { get; set; }
        [HotfixArray(6)]
        public int[] Color { get; set; }
    }
}
