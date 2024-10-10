using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("faction_template")]
    public sealed record FactionTemplateHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Faction")]
        public ushort? Faction;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("FactionGroup")]
        public byte? FactionGroup;

        [DBFieldName("FriendGroup")]
        public byte? FriendGroup;

        [DBFieldName("EnemyGroup")]
        public byte? EnemyGroup;

        [DBFieldName("Enemies", 8)]
        public ushort?[] Enemies;

        [DBFieldName("Friend", 8)]
        public ushort?[] Friend;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
