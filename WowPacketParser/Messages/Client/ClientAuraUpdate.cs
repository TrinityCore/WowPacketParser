using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuraUpdate
    {
        public bool UpdateAll;
        public ulong UnitGUID;
        public List<ClientAuraInfo> Auras;
    }
}
