using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Midnight
{
    [DBFile("QuestLineXQuest")]
    public sealed class QuestLineXQuestEntry
    {
        [Index(true)]
        public uint ID;
        public uint QuestLineID;
        public uint QuestID;
        public uint OrderIndex;
        public int Flags;
        public int Unknown1110;
    }
}
