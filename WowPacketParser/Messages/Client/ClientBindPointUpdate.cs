using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBindPointUpdate
    {
        public uint BindMapID;
        public Vector3 BindPosition;
        public uint BindAreaID;
    }
}
