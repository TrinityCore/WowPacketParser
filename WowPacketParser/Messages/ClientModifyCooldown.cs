using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientModifyCooldown
    {
        public ulong UnitGUID;
        public int DeltaTime;
        public int SpellID;
    }
}
