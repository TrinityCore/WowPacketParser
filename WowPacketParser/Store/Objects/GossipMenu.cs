using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gossip_menu")]
    public sealed record GossipMenu : IDataModel
    {
        [DBFieldName("MenuID", true)]
        public uint? MenuID;

        [DBFieldName("TextID", true)]
        public uint? TextID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public ObjectType ObjectType;

        public uint ObjectEntry;

        //public ICollection<GossipMenuOption> GossipOptions;
    }

    [DBTableName("gossip_menu")]
    public sealed record GossipMenu925 : IDataModel
    {
        [DBFieldName("MenuID", true, true)]
        public uint? MenuID;

        [DBFieldName("TextID", true, true)]
        public string TextID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public ObjectType ObjectType;

        public uint ObjectEntry;

        //public ICollection<GossipMenuOption> GossipOptions;
    }
}
