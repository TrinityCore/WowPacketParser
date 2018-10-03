using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellVisualEffectName, HasIndexInData = false)]
    public class SpellVisualEffectNameEntry
    {
        public int ModelFileDataID { get; set; }
        public float BaseMissileSpeed { get; set; }
        public float Scale { get; set; }
        public float MinAllowedScale { get; set; }
        public float MaxAllowedScale { get; set; }
        public float Alpha { get; set; }
        public uint Flags { get; set; }
        public int TextureFileDataID { get; set; }
        public float EffectRadius { get; set; }
        public uint Type { get; set; }
        public int GenericID { get; set; }
        public uint RibbonQualityID { get; set; }
        public int DissolveEffectID { get; set; }
        public int ModelPosition { get; set; }
    }
}
