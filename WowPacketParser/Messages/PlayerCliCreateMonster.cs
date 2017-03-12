using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliCreateMonster
    {
        public int EntryID;
        public Vector3 Offset;
    }
}
