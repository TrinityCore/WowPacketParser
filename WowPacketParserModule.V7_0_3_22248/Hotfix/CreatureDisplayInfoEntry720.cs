using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2_0_23826.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureDisplayInfo, ClientVersionBuild.V7_2_0_23826)]
    public class CreatureDisplayInfoEntry
    {
        public uint ID { get; set; }
        public float CreatureModelScale { get; set; }
        public ushort ModelID { get; set; }
        public ushort NPCSoundID { get; set; }
        public byte SizeClass { get; set; }
        public byte Flags { get; set; }
        public sbyte Gender { get; set; }
        public uint ExtendedDisplayInfoID { get; set; }
        [HotfixArray(3)]
        public uint[] TextureVariation { get; set; }
        public uint PortraitTextureFileDataID { get; set; }
        public byte CreatureModelAlpha { get; set; }
        public ushort SoundID { get; set; }
        public float PlayerModelScale { get; set; }
        public uint PortraitCreatureDisplayInfoID { get; set; }
        public byte BloodID { get; set; }
        public ushort ParticleColorID { get; set; }
        public uint CreatureGeosetData { get; set; }
        public ushort ObjectEffectPackageID { get; set; }
        public ushort AnimReplacementSetID { get; set; }
        public sbyte UnarmedWeaponSubclass { get; set; }
        public uint StateSpellVisualKitID { get; set; }
        public float InstanceOtherPlayerPetScale { get; set; }
        public uint MountSpellVisualKitID { get; set; }
    }
}