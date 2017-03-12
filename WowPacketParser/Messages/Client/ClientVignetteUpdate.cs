using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientVignetteUpdate
    {
        public VignetteClientDataSet Updated;
        public bool ForceUpdate;
        public VignetteInstanceIDList Removed;
        public VignetteClientDataSet Added;
    }
}
