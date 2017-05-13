using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gossip_menu_option")]
    public class GossipMenuOption : IDataModel
    {
        [DBFieldName("menu_id", true)]
        public uint? MenuID;

        [DBFieldName("id", true)]
        public uint? ID;

        [DBFieldName("option_icon")]
        public GossipOptionIcon? OptionIcon;

        [DBFieldName("option_text")]
        public string OptionText;

        [DBFieldName("OptionBroadcastTextID")]
        public int? OptionBroadcastTextID;

        [DBFieldName("action_poi_id")]
        public uint? ActionPoiID = 0;

        [DBFieldName("box_coded")]
        public bool? BoxCoded;

        [DBFieldName("box_money")]
        public uint? BoxMoney;

        [DBFieldName("box_text")]
        public string BoxText;

        [DBFieldName("BoxBroadcastTextID")]
        public int? BoxBroadcastTextID;

        public string BroadcastTextIDHelper;
    }
}
