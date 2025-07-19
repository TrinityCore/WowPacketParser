using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("BroadcastTextDuration")]
    public sealed class BroadcastTextDurationEntry
    {
        [Index(true)]
        public uint ID;
        public int Locale;
        public int DurationMS;
        [NonInlineRelation(typeof(uint))]
        public int BroadcastTextID;
    }
}
