using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.UnitPowerBar, HasIndexInData = false)]
    public class UnitPowerBarEntry
    {
        public float RegenerationPeace { get; set; }
        public float RegenerationCombat { get; set; }
        [HotfixArray(6)]
        public uint[] FileDataID { get; set; }
        [HotfixArray(6)]
        public uint[] Color { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public string OutOfError { get; set; }
        public string ToolTip { get; set; }
        public float StartInset { get; set; }
        public float EndInset { get; set; }
        public ushort StartPower { get; set; }
        public ushort Flags { get; set; }
        public byte CenterPower { get; set; }
        public byte BarType { get; set; }
        public uint MinPower { get; set; }
        public uint MaxPower { get; set; }
    }
}