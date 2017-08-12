using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SoundKit)]
    public class SoundKitEntry
    {
        public string Name { get; set; }
        public float VolumeFloat { get; set; }
        public float MinDistance { get; set; }
        public float DistanceCutoff { get; set; }
        public float VolumeVariationPlus { get; set; }
        public float VolumeVariationMinus { get; set; }
        public float PitchVariationPlus { get; set; }
        public float PitchVariationMinus { get; set; }
        public float PitchAdjust { get; set; }
        public ushort Flags { get; set; }
        public ushort SoundEntriesAdvancedID { get; set; }
        public ushort BusOverwriteID { get; set; }
        public byte SoundType { get; set; }
        public byte EAXDef { get; set; }
        public byte DialogType { get; set; }
        public byte Unk700 { get; set; }
        public uint ID { get; set; }
    }
}