using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_totems")]
    public sealed class SpellTotems : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RequiredTotemCategoryID", 2)]
        public uint?[] RequiredTotemCategoryID;

        [DBFieldName("Totem", 2)]
        public uint?[] Totem;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
