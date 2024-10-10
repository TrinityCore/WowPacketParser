using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_armor_shield")]
    public sealed record ItemArmorShieldHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Quality", 7)]
        public float?[] Quality;

        [DBFieldName("ItemLevel")]
        public ushort? ItemLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
