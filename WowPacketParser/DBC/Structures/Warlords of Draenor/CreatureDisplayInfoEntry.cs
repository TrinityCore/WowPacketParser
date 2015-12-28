using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFileName("CreatureDisplayInfo")]
    public sealed class CreatureDisplayInfoEntry
    {
        public uint ExtendedDisplayInfoID;
        public float CreatureModelScale;
        public float PlayerModelScale;
        public uint[] TextureVariation;
        public string PortraitTextureName;
        public uint PortraitCreatureDisplayInfoID;
        public uint CreatureGeosetData;
        public uint StateSpellVisualKitID;
        public float InstanceOtherPlayerPetScale;
        public ushort ModelID;
        public ushort SoundID;
        public ushort NPCSoundID;
        public ushort ParticleColorID;
        public ushort ObjectEffectPackageID;
        public ushort AnimReplacementSetID;
        public byte CreatureModelAlpha;
        public byte SizeClass;
        public byte BloodID;
        public byte Flags;
        public sbyte Gender;
        public sbyte Unk700;
    }
}
