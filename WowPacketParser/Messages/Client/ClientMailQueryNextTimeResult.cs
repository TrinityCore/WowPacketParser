using System.Collections.Generic;
using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMailQueryNextTimeResult
    {
        public List<CliMailNextTimeEntry> Next;
        public float NextMailTime;
    }
}
