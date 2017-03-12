using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryGameObjectResponse
    {
        public uint GameObjectID;
        public bool Allow;
        public Data Stats;
    }
}
