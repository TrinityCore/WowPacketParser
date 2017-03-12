using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliPageTextInfo
    {
        public uint ID;
        public uint NextPageID;
        public string Text;
    }
}
