using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CameraEffectEntry, HasIndexInData = false)]
    public class CameraEffectEntryEntry
    {
        public byte OrderIndex { get; set; }
        public ushort AmplitudeCurveID { get; set; }
        public float Duration { get; set; }
        public float Delay { get; set; }
        public float Phase { get; set; }
        public float Amplitude { get; set; }
        public float AmplitudeB { get; set; }
        public float Frequency { get; set; }
        public float RadiusMin { get; set; }
        public float RadiusMax { get; set; }
        public byte Flags { get; set; }
        public sbyte EffectType { get; set; }
        public byte DirectionType { get; set; }
        public byte MovementType { get; set; }
        public sbyte AttenuationType { get; set; }
        public ushort CameraEffectID { get; set; }
    }
}
