using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureDisplayInfo)]
    public class CreatureDisplayInfoEntry
    {
        public int ID { get; set; }
        public ushort ModelID { get; set; }
        public ushort SoundID { get; set; }
        public sbyte SizeClass { get; set; }
        public float CreatureModelScale { get; set; }
        public byte CreatureModelAlpha { get; set; }
        public byte BloodID { get; set; }
        public int ExtendedDisplayInfoID { get; set; }
        public ushort NPCSoundID { get; set; }
        public ushort ParticleColorID { get; set; }
        public int PortraitCreatureDisplayInfoID { get; set; }
        public int PortraitTextureFileDataID { get; set; }
        public ushort ObjectEffectPackageID { get; set; }
        public ushort AnimReplacementSetID { get; set; }
        public byte Flags { get; set; }
        public int StateSpellVisualKitID { get; set; }
        public float PlayerOverrideScale { get; set; }
        public float PetInstanceScale { get; set; }
        public sbyte UnarmedWeaponType { get; set; }
        public int MountPoofSpellVisualKitID { get; set; }
        public int DissolveEffectID { get; set; }
        public sbyte Gender { get; set; }
        public int DissolveOutEffectID { get; set; }
        public sbyte CreatureModelMinLod { get; set; }
        [HotfixArray(3)]
        public int[] TextureVariationFileDataID { get; set; }
    }
}
