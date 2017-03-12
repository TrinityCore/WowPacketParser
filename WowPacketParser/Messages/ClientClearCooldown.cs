using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientClearCooldown
    {
        public bool ClearOnHold;
        public ulong CasterGUID;
        public int SpellID;
    }
}
