using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.CataclysmClassic
{
    [DBFile("QuestLineXQuest")]
    public sealed class QuestLineXQuestEntry
    {
        [Index(true)]
        public uint ID;
        public uint QuestLineID;
        public uint QuestID;
        public uint OrderIndex;
    }
}
