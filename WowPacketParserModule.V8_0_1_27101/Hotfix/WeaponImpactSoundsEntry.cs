using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WeaponImpactSounds, HasIndexInData = false)]
    public class WeaponImpactSoundsEntry
    {
        public byte WeaponSubClassID { get; set; }
        public byte ParrySoundType { get; set; }
        public byte ImpactSource { get; set; }
        [HotfixArray(11)]
        public uint[] ImpactSoundID { get; set; }
        [HotfixArray(11)]
        public uint[] CritImpactSoundID { get; set; }
        [HotfixArray(11)]
        public uint[] PierceImpactSoundID { get; set; }
        [HotfixArray(11)]
        public uint[] PierceCritImpactSoundID { get; set; }
    }
}
