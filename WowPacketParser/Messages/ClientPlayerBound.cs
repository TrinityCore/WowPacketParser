using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPlayerBound
    {
        public ulong BinderID;
        public uint AreaID;
    }
}
