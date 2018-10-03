using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellMissile, HasIndexInData = false)]
    public class SpellMissileEntry
    {
        public int SpellID { get; set; }
        public byte Flags { get; set; }
        public float DefaultPitchMin { get; set; }
        public float DefaultPitchMax { get; set; }
        public float DefaultSpeedMin { get; set; }
        public float DefaultSpeedMax { get; set; }
        public float RandomizeFacingMin { get; set; }
        public float RandomizeFacingMax { get; set; }
        public float RandomizePitchMin { get; set; }
        public float RandomizePitchMax { get; set; }
        public float RandomizeSpeedMin { get; set; }
        public float RandomizeSpeedMax { get; set; }
        public float Gravity { get; set; }
        public float MaxDuration { get; set; }
        public float CollisionRadius { get; set; }
    }
}
