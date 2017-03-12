using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct WhoRequestServerInfo
    {
        public int FactionGroup;
        public int Locale;
        public uint RequesterVirtualRealmAddress;
    }
}
