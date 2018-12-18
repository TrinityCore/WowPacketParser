using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.BroadcastText, HasIndexInData = false)]
    public class BroadcastTextEntry : IBroadcastTextEntry
    {
        public string Text { get; set; }
        public string Text1 { get; set; }
        [HotfixArray(3)]
        public ushort[] EmoteID { get; set; }
        [HotfixArray(3)]
        public ushort[] EmoteDelay { get; set; }
        public ushort EmotesID { get; set; }
        public byte LanguageID { get; set; }
        public byte Flags { get; set; }
        [HotfixArray(2)]
        public uint[] SoundEntriesID { get; set; }
        public uint ConditionID { get; set; }
    }
}