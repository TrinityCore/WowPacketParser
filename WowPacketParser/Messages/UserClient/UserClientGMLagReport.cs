using WowPacketParser.Misc;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientGMLagReport
    {
        public int MapID;
        public int LagKind;
        public Vector3 Loc;
    }
}
