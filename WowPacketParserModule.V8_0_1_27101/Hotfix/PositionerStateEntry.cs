using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PositionerState, HasIndexInData = false)]
    public class PositionerStateEntry
    {
        public uint NextStateID { get; set; }
        public uint TransformMatrixID { get; set; }
        public uint PosEntryID { get; set; }
        public uint RotEntryID { get; set; }
        public uint ScaleEntryID { get; set; }
        public uint Flags { get; set; }
        public float EndLife { get; set; }
        public byte EndLifePercent { get; set; }
    }
}
