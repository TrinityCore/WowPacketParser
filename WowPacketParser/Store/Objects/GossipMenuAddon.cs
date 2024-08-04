using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gossip_menu_addon")]
    public sealed record GossipMenuAddon : IDataModel
    {
        [DBFieldName("MenuID", true)]
        public uint? MenuID;

        [DBFieldName("FriendshipFactionID")]
        public int? FriendshipFactionID;

        [DBFieldName("LfgDungeonsID", TargetedDatabaseFlag.SinceTheWarWithin)]
        public int? LfgDungeonsID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public ObjectType ObjectType;

        public uint ObjectEntry;
    }
}
