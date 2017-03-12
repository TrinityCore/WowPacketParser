using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct VignetteClientDataSet
    {
        public VignetteInstanceIDList IdList;
        public List<VignetteClientData> Data;
    }
}
