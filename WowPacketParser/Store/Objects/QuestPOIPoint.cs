namespace WowPacketParser.Store.Objects
{
    public class QuestPOIPoint
    {
        public int Index; // Client expects a certain order although this is not on sniffs

        public int X;

        public int Y;
    }
}