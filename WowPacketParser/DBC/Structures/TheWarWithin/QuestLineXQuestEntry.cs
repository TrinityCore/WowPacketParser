﻿using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("QuestLineXQuest")]
    public sealed class QuestLineXQuestEntry
    {
        [Index(true)]
        public int ID;
        public uint QuestLineID;
        public uint QuestID;
        public uint OrderIndex;
        public int Flags;
    }
}
