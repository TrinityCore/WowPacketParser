using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ConversationLine, HasIndexInData = false)]
    public class ConversationLineEntry
    {
        public uint BroadcastTextID { get; set; }
        public uint SpellVisualKitID { get; set; }
        public int AdditionalDuration { get; set; }
        public ushort NextConversationLineID { get; set; }
        public ushort AnimKitID { get; set; }
        public byte SpeechType { get; set; }
        public byte StartAnimation { get; set; }
        public byte EndAnimation { get; set; }
    }
}
