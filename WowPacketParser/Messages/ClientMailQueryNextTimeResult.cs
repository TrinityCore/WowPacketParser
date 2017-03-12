using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMailQueryNextTimeResult
    {
        public List<CliMailNextTimeEntry> Next;
        public float NextMailTime;
    }
}
