using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GameObjectDisplayInfo, HasIndexInData = false)]
    public class GameObjectDisplayInfoEntry
    {
        [HotfixArray(6)]
        public float[] GeoBox { get; set; }
        public int FileDataID { get; set; }
        public short ObjectEffectPackageID { get; set; }
        public float OverrideLootEffectScale { get; set; }
        public float OverrideNameScale { get; set; }
    }
}
