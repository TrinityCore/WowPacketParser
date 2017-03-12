using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientVignetteUpdate
    {
        public VignetteClientDataSet Updated;
        public bool ForceUpdate;
        public VignetteInstanceIDList Removed;
        public VignetteClientDataSet Added;
    }
}
