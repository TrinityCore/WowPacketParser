using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("CreatureDisplayInfo")]
    public sealed class CreatureDisplayInfoEntry
    {
        public uint ID;
        public float CreatureModelScale;
        public short ModelID;
        public short NPCSoundID;
        public byte SizeClass;
        public byte Flags;
        public sbyte Gender;
        public uint ExtendedDisplayInfoID;
        public uint[] TextureVariation;
        public uint PortraitTextureFileDataID;
        public byte CreatureModelAlpha;
        public short SoundID;
        public float PlayerModelScale;
        public int PortraitCreatureDisplayInfoID;
        public byte BloodID;
        public short ParticleColorID;
        public uint CreatureGeosetData;
        public short ObjectEffectPackageID;
        public short AnimReplacementSetID;
        public sbyte UnarmedWeaponSubclass;
        public int StateSpellVisualKitID;
        public float InstanceOtherPlayerPetScale;                             // scale of not own player pets inside dungeons/raids/scenarios
        public int MountSpellVisualKitID;
    }
}
