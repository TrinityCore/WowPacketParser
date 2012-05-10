using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template")]
    public class UnitGossip
    {
        [DBFieldName("gossip_menu_id")]
        public uint GossipId;
    }
}
