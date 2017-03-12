using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMapObjEvents
    {
        public uint UniqueID;
        public Data Events;
    }
}
