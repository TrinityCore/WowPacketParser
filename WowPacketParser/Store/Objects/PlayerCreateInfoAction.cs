using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    public sealed class PlayerCreateInfoAction : IDataModel
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
