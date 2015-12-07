using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("broadcast_text_locale")]
    public sealed class BroadcastTextLocale : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale;

        [DBFieldName("MaleText_lang")]
        public string MaleTextLang;
        
        [DBFieldName("FemaleText_lang")]
        public string FemaleTextLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
