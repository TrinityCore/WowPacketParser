using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientUpdateObject
    {
        public uint NumObjUpdates;
        public ushort MapID;
        public Data Data;
    }
}
