using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDeathReleaseLoc
    {
        public Vector3 Loc;
        public int MapID;
    }
}
