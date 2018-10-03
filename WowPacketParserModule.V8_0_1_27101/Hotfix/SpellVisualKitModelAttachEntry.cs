using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellVisualKitModelAttach)]
    public class SpellVisualKitModelAttachEntry
    {
        [HotfixArray(3)]
        public float[] Offset { get; set; }
        [HotfixArray(3)]
        public float[] OffsetVariation { get; set; }
        public int ID { get; set; }
        public ushort SpellVisualEffectNameID { get; set; }
        public sbyte AttachmentID { get; set; }
        public ushort PositionerID { get; set; }
        public float Yaw { get; set; }
        public float Pitch { get; set; }
        public float Roll { get; set; }
        public float YawVariation { get; set; }
        public float PitchVariation { get; set; }
        public float RollVariation { get; set; }
        public float Scale { get; set; }
        public float ScaleVariation { get; set; }
        public short StartAnimID { get; set; }
        public short AnimID { get; set; }
        public short EndAnimID { get; set; }
        public ushort AnimKitID { get; set; }
        public byte Flags { get; set; }
        public uint LowDefModelAttachID { get; set; }
        public float StartDelay { get; set; }
        public int ParentSpellVisualKitID { get; set; }
    }
}
