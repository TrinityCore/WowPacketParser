using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_1_0_28724.Hotfix
{
    [HotfixStructure(DB2Hash.SoundKit, ClientVersionBuild.V8_1_0_28724)]
    public class SoundKitEntry
    {
        public uint ID { get; set; }
        public int SoundType { get; set; }
        public float VolumeFloat { get; set; }
        public ushort Flags { get; set; }
        public float MinDistance { get; set; }
        public float DistanceCutoff { get; set; }
        public byte EAXDef { get; set; }
        public uint SoundKitAdvancedID { get; set; }
        public float VolumeVariationPlus { get; set; }
        public float VolumeVariationMinus { get; set; }
        public float PitchVariationPlus { get; set; }
        public float PitchVariationMinus { get; set; }
        public sbyte DialogType { get; set; }
        public float PitchAdjust { get; set; }
        public ushort BusOverwriteID { get; set; }
        public byte MaxInstances { get; set; }
    }
}
