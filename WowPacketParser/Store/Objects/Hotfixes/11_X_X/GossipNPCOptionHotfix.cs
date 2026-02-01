using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("gossip_npc_option")]
    public sealed record GossipNpcOptionHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GossipNpcOption")]
        public int? GossipNpcOption;

        [DBFieldName("LFGDungeonsID")]
        public int? LFGDungeonsID;

        [DBFieldName("TrainerID")]
        public int? TrainerID;

        [DBFieldName("GarrFollowerTypeID")]
        public sbyte? GarrFollowerTypeID;

        [DBFieldName("CharShipmentID")]
        public int? CharShipmentID;

        [DBFieldName("GarrTalentTreeID")]
        public int? GarrTalentTreeID;

        [DBFieldName("UiMapID")]
        public int? UiMapID;

        [DBFieldName("UiItemInteractionID")]
        public int? UiItemInteractionID;

        [DBFieldName("Unknown_1000_8")]
        public int? Unknown_1000_8;

        [DBFieldName("Unknown_1000_9")]
        public int? Unknown_1000_9;

        [DBFieldName("CovenantID")]
        public int? CovenantID;

        [DBFieldName("GossipOptionID")]
        public int? GossipOptionID;

        [DBFieldName("TraitTreeID")]
        public int? TraitTreeID;

        [DBFieldName("ProfessionID")]
        public int? ProfessionID;

        [DBFieldName("Unknown_1002_14")]
        public int? Unknown_1002_14;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("gossip_npc_option")]
    public sealed record GossipNpcOptionHotfix1110 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GossipNpcOption")]
        public int? GossipNpcOption;

        [DBFieldName("LFGDungeonsID")]
        public int? LFGDungeonsID;

        [DBFieldName("TrainerID")]
        public int? TrainerID;

        [DBFieldName("GarrFollowerTypeID")]
        public sbyte? GarrFollowerTypeID;

        [DBFieldName("CharShipmentID")]
        public int? CharShipmentID;

        [DBFieldName("GarrTalentTreeID")]
        public int? GarrTalentTreeID;

        [DBFieldName("UiMapID")]
        public int? UiMapID;

        [DBFieldName("UiItemInteractionID")]
        public int? UiItemInteractionID;

        [DBFieldName("Unknown_1000_8")]
        public int? Unknown_1000_8;

        [DBFieldName("Unknown_1000_9")]
        public int? Unknown_1000_9;

        [DBFieldName("CovenantID")]
        public int? CovenantID;

        [DBFieldName("GossipOptionID")]
        public int? GossipOptionID;

        [DBFieldName("TraitTreeID")]
        public int? TraitTreeID;

        [DBFieldName("ProfessionID")]
        public int? ProfessionID;

        [DBFieldName("Unknown_1002_14")]
        public int? Unknown_1002_14;

        [DBFieldName("SkillLineID")]
        public int? SkillLineID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("gossip_npc_option")]
    public sealed record GossipNpcOptionHotfix1127 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GossipNpcOption")]
        public int? GossipNpcOption;

        [DBFieldName("LFGDungeonsID")]
        public int? LFGDungeonsID;

        [DBFieldName("TrainerID")]
        public int? TrainerID;

        [DBFieldName("GarrFollowerTypeID")]
        public sbyte? GarrFollowerTypeID;

        [DBFieldName("CharShipmentID")]
        public int? CharShipmentID;

        [DBFieldName("GarrTalentTreeID")]
        public int? GarrTalentTreeID;

        [DBFieldName("UiMapID")]
        public int? UiMapID;

        [DBFieldName("UiItemInteractionID")]
        public int? UiItemInteractionID;

        [DBFieldName("Unknown_1000_8")]
        public int? Unknown_1000_8;

        [DBFieldName("Unknown_1000_9")]
        public int? Unknown_1000_9;

        [DBFieldName("CovenantID")]
        public int? CovenantID;

        [DBFieldName("GossipOptionID")]
        public int? GossipOptionID;

        [DBFieldName("TraitTreeID")]
        public int? TraitTreeID;

        [DBFieldName("ProfessionID")]
        public int? ProfessionID;

        [DBFieldName("Unknown_1002_14")]
        public int? Unknown_1002_14;

        [DBFieldName("NeighborhoodMapID")]
        public int? NeighborhoodMapID;

        [DBFieldName("SkillLineID")]
        public int? SkillLineID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
