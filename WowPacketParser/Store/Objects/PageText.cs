using PacketParser.SQL;
using PacketParser.Misc;

namespace PacketParser.DataStructures
{
    [DBTableName("page_text")]
    public class PageText : ITextOutputDisabled
    {
        [DBFieldName("text")]
        public string Text;

        [DBFieldName("next_page")]
        public uint NextPageId;

        [DBFieldName("WDBVerified")]
        public int WDBVerified;
    }
}
