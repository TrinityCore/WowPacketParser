using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiCamera, HasIndexInData = false)]
    public class UiCameraEntry
    {
        public string Name { get; set; }
        [HotfixArray(3)]
        public float[] Pos { get; set; }
        [HotfixArray(3)]
        public float[] LookAt { get; set; }
        [HotfixArray(3)]
        public float[] Up { get; set; }
        public byte UiCameraTypeID { get; set; }
        public int AnimID { get; set; }
        public short AnimFrame { get; set; }
        public sbyte AnimVariation { get; set; }
        public byte Flags { get; set; }
    }
}
