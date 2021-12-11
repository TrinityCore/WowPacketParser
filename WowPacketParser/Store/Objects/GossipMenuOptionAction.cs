using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gossip_menu_option_action")]
    public sealed record GossipMenuOptionAction : IDataModel
    {
        [DBFieldName("MenuId", true)]
        public uint? MenuId;

        [DBFieldName("OptionIndex", true)]
        public uint? OptionIndex;

        [DBFieldName("ActionMenuId")]
        public uint? ActionMenuId;

        [DBFieldName("ActionPoiId", false, true)]
        public object ActionPoiId;
    }
}
