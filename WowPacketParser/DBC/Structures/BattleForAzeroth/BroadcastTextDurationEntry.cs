using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("BroadcastTextDuration")]
    public sealed class BroadcastTextDurationEntry
    {
        [Index(true)]
        public uint ID;
        public uint BroadcastTextID;
        public int Locale;
        public int DurationMS;
    }
}
