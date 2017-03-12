using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGameObjectCustomAnim
    {
        public ulong ObjectGUID;
        public uint CustomAnim;
        public bool PlayAsDespawn;
    }
}
