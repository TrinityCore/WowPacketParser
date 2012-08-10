using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("page_text")]
    public class PageText
    {
        [DBFieldName("text")]
        public string Text;

        [DBFieldName("next_page")]
        public uint NextPageId;

        [DBFieldName("WDBVerified")]
        public int WDBVerified;
    }
}
