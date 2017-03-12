using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetEncounterVar
    {
        public int DungeonEncounterID;
        public string VarName;
        public bool Attempt;
        public float VarValue;
    }
}
