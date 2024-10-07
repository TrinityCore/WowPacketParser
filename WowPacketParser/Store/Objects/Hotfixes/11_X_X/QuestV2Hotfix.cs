using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("quest_v2")]
    public sealed record QuestV2Hotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("UniqueBitFlag")]
        public ushort? UniqueBitFlag;

        [DBFieldName("UiQuestDetailsTheme")]
        public int? UiQuestDetailsTheme;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
