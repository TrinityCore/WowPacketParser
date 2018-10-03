using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundBus)]
    public class SoundBusEntry
    {
        public int ID { get; set; }
        public byte Flags { get; set; }
        public byte DefaultPriority { get; set; }
        public byte DefaultPriorityPenalty { get; set; }
        public float DefaultVolume { get; set; }
        public byte DefaultPlaybackLimit { get; set; }
        public sbyte BusEnumID { get; set; }
        public ushort Parent { get; set; }
    }
}
