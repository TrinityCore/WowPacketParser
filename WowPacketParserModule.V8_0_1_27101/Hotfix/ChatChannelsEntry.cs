using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ChatChannels, HasIndexInData = false)]
    public class ChatChannelsEntry
    {
        public string Name { get; set; }
        public string Shortcut { get; set; }
        public int Flags { get; set; }
        public sbyte FactionGroup { get; set; }
    }
}
