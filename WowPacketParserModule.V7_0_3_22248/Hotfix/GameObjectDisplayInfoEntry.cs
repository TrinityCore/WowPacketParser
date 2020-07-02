using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GameobjectDisplayInfo, HasIndexInData = false)]
    public class GameObjectDisplayInfoEntry
    {
        public uint FileDataID { get; set; }
        [HotfixArray(3)]
        public float[] GeoBoxMin { get; set; }
        [HotfixArray(3)]
        public float[] GeoBoxMax { get; set; }
        public float OverrideLootEffectScale { get; set; }
        public float OverrideNameScale { get; set; }
        public ushort ObjectEffectPackageID { get; set; }
    }
}