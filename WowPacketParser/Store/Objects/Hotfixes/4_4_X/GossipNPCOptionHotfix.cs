using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("gossip_npc_option")]
    public sealed record GossipNPCOptionHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GossipNpcOption")]
        public int? GossipNpcOption;

        [DBFieldName("LFGDungeonsID")]
        public int? LFGDungeonsID;

        [DBFieldName("TrainerID")]
        public int? TrainerID;

        [DBFieldName("Unknown341_0")]
        public int? Unknown341_0;

        [DBFieldName("Unknown341_1")]
        public int? Unknown341_1;

        [DBFieldName("Unknown341_2")]
        public int? Unknown341_2;

        [DBFieldName("Unknown341_3")]
        public int? Unknown341_3;

        [DBFieldName("Unknown341_4")]
        public int? Unknown341_4;

        [DBFieldName("Unknown341_5")]
        public int? Unknown341_5;

        [DBFieldName("Unknown341_6")]
        public int? Unknown341_6;

        [DBFieldName("Unknown341_7")]
        public int? Unknown341_7;

        [DBFieldName("GossipOptionID")]
        public int? GossipOptionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
