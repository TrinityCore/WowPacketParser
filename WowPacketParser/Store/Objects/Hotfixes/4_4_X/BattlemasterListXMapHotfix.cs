using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("battlemaster_list_x_map")]
    public sealed record BattlemasterListXMapHotfix441: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MapID")]
        public int? MapID;

        [DBFieldName("BattlemasterListID")]
        public int? BattlemasterListID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
