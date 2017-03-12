using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildCommandResult
    {
        public string Name;
        public int Result;
        public int Command;
    }
}
