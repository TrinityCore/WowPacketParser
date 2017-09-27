using System.Collections.Generic;
using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMailQueryNextTimeResult
    {
        public List<CliMailNextTimeEntry> Next;
        public float NextMailTime;
    }
}
