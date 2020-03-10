﻿namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("MapDifficulty")]

    public class MapDifficultyEntry
    {
        public uint ID;
        public string Message;
        public int DifficultyID;
        public int LockID;
        public sbyte ResetInterval;
        public int MaxPlayers;
        public int ItemContext;
        public int ItemContextPickerID;
        public int Flags;
        public int ContentTuningID;
        public int MapID;
    }
}
