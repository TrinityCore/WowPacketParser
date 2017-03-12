using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDisplayGameError
    {
        public int Arg2; // Optional
        public int Arg; // Optional
        public uint Error;
    }
}
