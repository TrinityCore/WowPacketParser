using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("CreatureDisplayInfo")]
    public sealed class CreatureDisplayInfoEntry
    {
        [Index(false)]
        public uint ID;
        public ushort ModelID;
        public ushort SoundID;
        public sbyte SizeClass;
        public float CreatureModelScale;
        public byte CreatureModelAlpha;
        public byte BloodID;
        public int ExtendedDisplayInfoID;
        public ushort NPCSoundID;
        public ushort ParticleColorID;
        public int PortraitCreatureDisplayInfoID;
        public int PortraitTextureFileDataID;
        public ushort ObjectEffectPackageID;
        public ushort AnimReplacementSetID;
        public int Flags;
        public int StateSpellVisualKitID;
        public float PlayerOverrideScale;
        public float PetInstanceScale;
        public sbyte UnarmedWeaponType;
        public int MountPoofSpellVisualKitID;
        public int DissolveEffectID;
        public sbyte Gender;
        public int DissolveOutEffectID;
        public sbyte CreatureModelMinLod;
        public ushort ConditionalCreatureModelID;
        public float Unknown_1100_1;
        public ushort Unknown_1100_2;
        [Cardinality(4)]
        public int[] TextureVariationFileDataID = new int[4];
    }
}
