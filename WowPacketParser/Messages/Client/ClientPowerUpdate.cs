using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPowerUpdate
    {
        public List<ClientPowerUpdatePower> Powers;
        public ulong Guid;
    }
}
