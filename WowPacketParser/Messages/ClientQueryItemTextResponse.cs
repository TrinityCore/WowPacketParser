using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryItemTextResponse
    {
        public ulong Id;
        public bool Valid;
        public CliItemTextCache Item;
    }
}
