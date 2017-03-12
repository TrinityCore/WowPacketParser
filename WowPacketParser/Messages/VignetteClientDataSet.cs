using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct VignetteClientDataSet
    {
        public VignetteInstanceIDList IdList;
        public List<VignetteClientData> Data;
    }
}
