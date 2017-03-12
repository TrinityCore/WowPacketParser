using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarCommandResult
    {
        public string Name;
        public byte Command;
        public byte Result;
    }
}
