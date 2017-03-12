using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPowerUpdate
    {
        public List<ClientPowerUpdatePower> Powers;
        public ulong Guid;
    }
}
