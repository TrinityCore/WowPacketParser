using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WowPacketParser.Enums
{
    public enum StoreNameType
    {
        None = 0,
        Spell = 1,
        Map = 2,
        LFGDungeon = 3,
        Battleground = 4,
        Unit = 5,
        GameObject = 6,
        Item = 7,
        Quest = 8,
        Opcode = 9 // Packet
    }
}
