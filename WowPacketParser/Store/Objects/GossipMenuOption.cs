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

        [DBFieldName("option_id")]
        public uint? OptionID;

        [DBFieldName("npc_option_npcflag")]
        public uint? NpcOptionNpcFlag;

        [DBFieldName("action_menu_id")]
        public uint? ActionMenuID;

        [DBFieldName("action_poi_id")]
        public uint? ActionPoiID;

        [DBFieldName("box_coded")]
        public bool? BoxCoded;

        [DBFieldName("box_money")]
        public uint? BoxMoney;

        [DBFieldName("box_text")]
        public string BoxText;

        [DBFieldName("BoxBroadcastTextID")]
        public int? BoxBroadcastTextID;
    }
}
