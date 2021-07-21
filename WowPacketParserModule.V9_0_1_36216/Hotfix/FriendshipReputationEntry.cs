using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.FriendshipReputation)]
    public class FriendshipReputationEntry
    {
        public string Description { get; set; }
        public string StandingModified { get; set; }
        public string StandingChanged { get; set; }
        public uint ID { get; set; }
        public int FactionID { get; set; }
        public int TextureFileID { get; set; }
        public int Flags { get; set; }
    }
}
