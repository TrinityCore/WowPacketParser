using PacketParser.Misc;
using PacketParser.SQL;

namespace PacketParser.DataStructures
{
    [DBTableName("quest_poi_points")]
    public class QuestPOIPoint : ITextOutputDisabled
    {
        [DBFieldName("idx")]
        public uint Index; // Client expects a certain order although this is not on sniffs

        [DBFieldName("x")]
        public int X;

        [DBFieldName("y")]
        public int Y;
    }
}
