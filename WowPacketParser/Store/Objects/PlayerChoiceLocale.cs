using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("playerchoice_locale")]
    public sealed class PlayerChoiceLocaleTemplate : IDataModel
    {
        [DBFieldName("ChoiceId", true)]
        public int? ChoiceId;

        [DBFieldName("Locale")]
        public string Locale;

        [DBFieldName("Question")]
        public string Question;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("playerchoice_response_locale")]
    public sealed class PlayerChoiceResponseLocaleTemplate : IDataModel
    {
        [DBFieldName("ChoiceId", true)]
        public int? ChoiceId;

        [DBFieldName("ResponseId", true)]
        public int? ResponseId;

        [DBFieldName("Locale")]
        public string Locale;

        [DBFieldName("Header", TargetedDatabase.Legion)]
        public string Header;

        [DBFieldName("Subheader", TargetedDatabase.BattleForAzeroth)]
        public string Subheader;

        [DBFieldName("ButtonTooltip", TargetedDatabase.BattleForAzeroth)]
        public string ButtonTooltip;

        [DBFieldName("Answer")]
        public string Answer;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Confirmation", TargetedDatabase.Legion)]
        public string Confirmation;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
