using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuraUpdate
    {
        public bool UpdateAll;
        public ulong UnitGUID;
        public List<ClientAuraInfo> Auras;
    }
}
