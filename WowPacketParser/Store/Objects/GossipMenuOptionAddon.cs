using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gossip_menu_option_addon", TargetedDatabaseFlag.Shadowlands)]
    public sealed record GossipMenuOptionAddon : IDataModel
    {
        [DBFieldName("MenuID", true)]
        public uint? MenuID;

        [DBFieldName("OptionID", true)]
        public uint? OptionID;

        [DBFieldName("GarrTalentTreeID")]
        public int? GarrTalentTreeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
