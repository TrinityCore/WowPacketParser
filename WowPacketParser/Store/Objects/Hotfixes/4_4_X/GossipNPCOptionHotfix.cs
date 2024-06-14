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

        [DBFieldName("Unk341_1")]
        public int? Unk341_1;

        [DBFieldName("Unk341_2")]
        public int? Unk341_2;

        [DBFieldName("Unk341_3")]
        public int? Unk341_3;

        [DBFieldName("Unk341_4")]
        public int? Unk341_4;

        [DBFieldName("Unk341_5")]
        public int? Unk341_5;

        [DBFieldName("Unk341_6")]
        public int? Unk341_6;

        [DBFieldName("Unk341_7")]
        public int? Unk341_7;

        [DBFieldName("Unk341_8")]
        public int? Unk341_8;

        [DBFieldName("GossipOptionID")]
        public int? GossipOptionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
