using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("SoundKit")]
    public sealed class SoundKitEntry
    {
        public string Name;
        public float VolumeFloat;
        public float MinDistance;
        public float DistanceCutoff;
        public float VolumeVariationPlus;
        public float VolumeVariationMinus;
        public float PitchVariationPlus;
        public float PitchVariationMinus;
        public float PitchAdjust;
        public ushort Flags;
        public ushort SoundEntriesAdvancedID;
        public ushort BusOverwriteID;
        public byte SoundType;
        public byte EAXDef;
        public byte DialogType;
        public byte Unk700;
        public uint ID;
    }
}
