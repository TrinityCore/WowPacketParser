using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("gem_properties")]
    public sealed record GemPropertiesHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("EnchantID")]
        public ushort? EnchantID;

        [DBFieldName("Type")]
        public int? Type;

        [DBFieldName("MinItemLevel")]
        public ushort? MinItemLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
