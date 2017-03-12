namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGameObjectActivateAnimKit
    {
        public bool Maintain;
        public ulong ObjectGUID;
        public int AnimKitID;
    }
}
