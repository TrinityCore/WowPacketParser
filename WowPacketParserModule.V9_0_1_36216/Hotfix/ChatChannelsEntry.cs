using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ChatChannels, ClientVersionBuild.V9_0_1_36216, ClientVersionBuild.V9_1_0_39185)]
    public class ChatChannelsEntry
    {
        public string Name { get; set; }
        public string Shortcut { get; set; }
        public uint ID { get; set; }
        public int Flags { get; set; }
        public sbyte FactionGroup { get; set; }
        public int Ruleset { get; set; }
    }
}
