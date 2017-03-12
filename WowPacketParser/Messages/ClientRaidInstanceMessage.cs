using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRaidInstanceMessage
    {
        public byte Type;
        public uint DifficultyID;
        public uint MapID;
        public bool Locked;
        public int TimeLeft;
        public bool Extended;
    }
}
