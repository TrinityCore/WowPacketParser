using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gossip_menu_option_box")]
    public class GossipMenuOptionBox : IDataModel
    {
        [DBFieldName("MenuId", true)]
        public uint? MenuId;

        [DBFieldName("OptionIndex", true)]
        public uint? OptionIndex;

        [DBFieldName("BoxCoded")]
        public bool? BoxCoded;

        [DBFieldName("BoxMoney")]
        public uint? BoxMoney;

        [DBFieldName("BoxText")]
        public string BoxText;

        [DBFieldName("BoxBroadcastTextId")]
        public int? BoxBroadcastTextId;

        public bool IsEmpty { get { return !BoxCoded.HasValue || !BoxCoded.Value; } }

        public string BroadcastTextIdHelper;
    }
}
