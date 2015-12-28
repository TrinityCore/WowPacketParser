using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    public sealed class SoundEntriesEntry
    {
        public uint   ID;
        public uint   SoundType;
        public string Name;
        [StoragePresence(StoragePresenceOption.Include, ArraySize = 20)]
        public uint[] FileDataID;
        [StoragePresence(StoragePresenceOption.Include, ArraySize = 20)]
        public uint[] Freq;
        public float  VolumeFloat;
        public uint   Flags;
        public float  MinDistance;
        public float  DistanceCutoff;
        public uint   EAXDef;
        public uint   SoundEntriesAdvancedID;
        public float  VolumeVariationPlus;
        public float  VolumeVariationMinus;
        public float  PitchVariationPlus;
        public float  PitchVariationMinus;
        public float  PitchAdjust;
        public uint   DialogType;
        public uint   BusOverwriteID;
    }
}
