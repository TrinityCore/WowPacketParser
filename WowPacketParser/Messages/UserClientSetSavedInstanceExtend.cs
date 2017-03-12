using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSetSavedInstanceExtend
    {
        public int MapID;
        public bool Extend;
        public uint DifficultyID;
    }
}
