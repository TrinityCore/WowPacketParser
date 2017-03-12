using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientConnectionAuthChallenge
    {
        public uint Challenge;
        public fixed uint DosChallenge[8];
        public byte DosZeroBits;
    }
}
