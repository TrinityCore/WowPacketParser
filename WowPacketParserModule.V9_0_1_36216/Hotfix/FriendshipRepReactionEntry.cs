using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.FriendshipRepReaction, HasIndexInData = false)]
    public class FriendshipRepReactionEntry
    {
        public string Reaction { get; set; }
        public uint FriendshipRepID { get; set; }
        public ushort ReactionThreshold { get; set; }
    }
}
