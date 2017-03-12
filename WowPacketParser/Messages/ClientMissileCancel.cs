using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMissileCancel
    {
        public ulong OwnerGUID;
        public bool Reverse;
        public int SpellID;
    }
}
