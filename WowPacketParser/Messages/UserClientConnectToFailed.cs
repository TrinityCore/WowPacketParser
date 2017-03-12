using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientConnectToFailed
    {
        public uint Serial;
        public sbyte Con;
    }
}
