using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryPageTextResponse
    {
        public bool Allow;
        public CliPageTextInfo Info;
        public uint PageTextID;
    }
}
