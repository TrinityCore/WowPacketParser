using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct VirtualRealmInfo
    {
        public uint RealmAddress;
        public VirtualRealmNameInfo RealmNameInfo;
    }
}
