using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSocketGems
    {
        public ulong Item;
        public fixed int Sockets[3];
        public int SocketMatch;
    }
}
