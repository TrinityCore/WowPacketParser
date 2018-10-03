using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BroadcastText)]
    public class BroadcastTextEntry
    {
        public string Text { get; set; }
        public string Text1 { get; set; }
        public int ID { get; set; }
        public byte LanguageID { get; set; }
        public int ConditionID { get; set; }
        public ushort EmotesID { get; set; }
        public byte Flags { get; set; }
        public uint ChatBubbleDurationMs { get; set; }
        [HotfixArray(2)]
        public uint[] SoundEntriesID { get; set; }
        [HotfixArray(3)]
        public ushort[] EmoteID { get; set; }
        [HotfixArray(3)]
        public ushort[] EmoteDelay { get; set; }
    }
}
