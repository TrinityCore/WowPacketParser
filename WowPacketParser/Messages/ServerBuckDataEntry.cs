using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ServerBuckDataEntry
    {
        public ulong Arg;
        public string Argname;
        public ulong Count;
        public ulong Accum;
        public ulong Sqaccum;
        public ulong Maximum;
        public ulong Minimum;
    }
}
