using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundKit)]
    public class SoundKitEntry
    {
        public int ID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, true)]
        public byte SoundType { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        public int soundType { get; set; }
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
