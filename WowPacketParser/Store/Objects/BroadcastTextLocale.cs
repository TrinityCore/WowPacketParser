using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("broadcast_text_locale")]
    public sealed class BroadcastTextLocale
    {
        [DBFieldName("MaleText_lang")]
        public string MaleText_lang;
        
        [DBFieldName("FemaleText_lang")]
        public string FemaleText_lang;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
