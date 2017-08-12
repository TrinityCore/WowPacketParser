using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ChatChannels, HasIndexInData = false)]
    public class ChatChannelsEntry
    {
        public uint Flags { get; set; }
        public string Name { get; set; }
        public string Shortcut { get; set; }
        public byte FactionGroup { get; set; }
    }
}