using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGossipPOI
    {
        public int Icon;
        public Vector2 Pos;
        public ushort Flags;
        public int Importance;
        public string Name;
    }
}
