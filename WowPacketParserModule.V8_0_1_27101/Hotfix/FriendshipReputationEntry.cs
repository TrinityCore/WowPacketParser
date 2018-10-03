using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.FriendshipReputation)]
    public class FriendshipReputationEntry
    {
        public string Description { get; set; }
        public uint ID { get; set; }
        public ushort FactionID { get; set; }
        public int TextureFileID { get; set; }
    }
}
