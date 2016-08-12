using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.EmotesTextSound, HasIndexInData = false)]
    public class EmotesTextSoundEntry
    {
        public ushort EmotesTextId { get; set; }
        public byte RaceId { get; set; }
        public byte SexId { get; set; }
        public byte ClassId { get; set; }
        public uint SoundId { get; set; }
    }
}