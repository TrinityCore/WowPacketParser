using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliCreateItemCheat
    {
        public bool BattlePayLocked;
        public bool AutoEquip;
        public string Context;
        public int Count;
        public uint EntryID;
    }
}
