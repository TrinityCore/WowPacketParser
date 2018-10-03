using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellVisualKitAreaModel, HasIndexInData = false)]
    public class SpellVisualKitAreaModelEntry
    {
        public int ModelFileDataID { get; set; }
        public byte Flags { get; set; }
        public ushort LifeTime { get; set; }
        public float EmissionRate { get; set; }
        public float Spacing { get; set; }
        public float ModelScale { get; set; }
    }
}
