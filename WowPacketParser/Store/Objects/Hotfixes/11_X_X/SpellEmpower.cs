using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_empower")]
    public sealed record SpellEmpowerHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("Unused1000")]
        public int? Unused1000;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
