using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientMinimapPing
    {
        public Vector2 Position;
        public byte PartyIndex;
    }
}
