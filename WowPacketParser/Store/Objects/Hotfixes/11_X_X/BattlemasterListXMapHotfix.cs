using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("battlemaster_list_x_map")]
    public sealed record BattlemasterListXMapHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MapID")]
        public int? MapID;

        [DBFieldName("BattlemasterListID")]
        public uint? BattlemasterListID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
