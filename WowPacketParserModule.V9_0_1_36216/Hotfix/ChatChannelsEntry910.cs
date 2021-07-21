using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.ChatChannels, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class ChatChannelsEntry
    {
        public string Name { get; set; }
        public string Shortcut { get; set; }
        public int Flags { get; set; }
        public sbyte FactionGroup { get; set; }
        public int Ruleset { get; set; }
    }
}
