using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGuildCommandResult
    {
        public int CommandID;
        public sbyte Success;
    }
}
