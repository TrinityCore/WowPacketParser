using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("page_text")]
    public sealed record PageText : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Text")]
        public string Text;

        [DBFieldName("NextPageID")]
        public uint? NextPageID;

        [DBFieldName("PlayerConditionID", TargetedDatabase.Legion)]
        public int? PlayerConditionID;

        [DBFieldName("Flags", TargetedDatabase.Legion)]
        public byte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
