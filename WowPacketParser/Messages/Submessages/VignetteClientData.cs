using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct VignetteClientData
    {
        public ulong ObjGUID;
        public Vector3 Position;
        public int VignetteID;
    }
}
