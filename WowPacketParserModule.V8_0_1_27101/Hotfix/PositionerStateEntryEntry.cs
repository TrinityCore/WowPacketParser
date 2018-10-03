using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PositionerStateEntry, HasIndexInData = false)]
    public class PositionerStateEntryEntry
    {
        public float ParamA { get; set; }
        public float ParamB { get; set; }
        public uint CurveID { get; set; }
        public short SrcValType { get; set; }
        public short SrcVal { get; set; }
        public short DstValType { get; set; }
        public short DstVal { get; set; }
        public sbyte EntryType { get; set; }
        public sbyte Style { get; set; }
        public sbyte SrcType { get; set; }
        public sbyte DstType { get; set; }
    }
}
