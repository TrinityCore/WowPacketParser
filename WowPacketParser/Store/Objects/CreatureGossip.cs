using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_gossip")]
    public class CreatureGossip : IDataModel
    {
        [DBFieldName("CreatureId", true)]
        public uint? CreatureId;

        [DBFieldName("GossipMenuId", true)]
        public uint? GossipMenuId;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
