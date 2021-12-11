using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("playercreateinfo_action")]
    public sealed record PlayerCreateInfoAction : IDataModel
    {
        [DBFieldName("race", true)]
        public Race? Race;

        [DBFieldName("class", true)]
        public Class? Class;

        [DBFieldName("button", true)]
        public uint? Button;

        [DBFieldName("action")]
        public uint? Action;

        [DBFieldName("type")]
        public ActionButtonType? Type;
    }
}
