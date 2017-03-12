using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct VignetteClientData
    {
        public ulong ObjGUID;
        public Vector3 Position;
        public int VignetteID;
    }
}
