using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gossip_menu_option")]
    public class GossipMenuOption : IDataModel
    {
        [DBFieldName("MenuId", true)]
        public uint? MenuId;

        [DBFieldName("OptionIndex", true)]
        public uint? OptionIndex;

        [DBFieldName("OptionIcon")]
        public GossipOptionIcon? OptionIcon;

        [DBFieldName("OptionText")]
        public string OptionText;

        [DBFieldName("OptionBroadcastTextId")]
        public int? OptionBroadcastTextId;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public string BroadcastTextIDHelper;
    }
}
