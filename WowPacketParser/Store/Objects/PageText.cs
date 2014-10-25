using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("page_text")]
    public class PageText
    {
        [DBFieldName("Text")]
        public string Text;

        [DBFieldName("NextPageID")]
        public uint NextPageID;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
