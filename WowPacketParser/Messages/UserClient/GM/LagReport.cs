using WowPacketParser.Misc;

namespace WowPacketParser.Messages.UserClient.GM
{
    public unsafe struct LagReport
    {
        public int MapID;
        public int LagKind;
        public Vector3 Loc;
    }
}
