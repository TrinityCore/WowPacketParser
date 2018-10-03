using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.FriendshipRepReaction, HasIndexInData = false)]
    public class FriendshipRepReactionEntry
    {
        public string Reaction { get; set; }
        public byte FriendshipRepID { get; set; }
        public ushort ReactionThreshold { get; set; }
    }
}
