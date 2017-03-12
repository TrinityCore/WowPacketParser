using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetFactionCheat
    {
        public uint FactionID;
        public int Level;
    }
}
