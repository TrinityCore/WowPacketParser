using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    public sealed class CreatureDisplayInfoEntry
    {
        public uint   ID;
        public uint   ModelID;
        public uint   SoundID;
        public uint   ExtendedDisplayInfoID;
        public float  CreatureModelScale;
        public float  Unknown620;
        public uint   CreatureModelAlpha;
        [StoragePresence(StoragePresenceOption.Include, ArraySize = 3)]
        public string[] TextureVariation;
        public string PortraitTextureName;
        public uint   PortraitCreatureDisplayInfoID;
        public uint   SizeClass;
        public uint   BloodID;
        public uint   NPCSoundID;
        public uint   ParticleColorID;
        public uint   CreatureGeosetData;
        public uint   ObjectEffectPackageID;
        public uint   AnimReplacementSetID;
        public uint   Flags;
        public int    Gender;
        public uint   StateSpellVisualKitID;
    }
}
