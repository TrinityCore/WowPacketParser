using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("garr_follower_x_ability")]
    public sealed record GarrFollowerXAbilityHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("FactionIndex")]
        public byte? FactionIndex;

        [DBFieldName("GarrAbilityID")]
        public ushort? GarrAbilityID;

        [DBFieldName("GarrFollowerID")]
        public uint? GarrFollowerID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_follower_x_ability")]
    public sealed record GarrFollowerXAbilityHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("FactionIndex")]
        public byte? FactionIndex;

        [DBFieldName("GarrAbilityID")]
        public ushort? GarrAbilityID;

        [DBFieldName("GarrFollowerID")]
        public int? GarrFollowerID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
