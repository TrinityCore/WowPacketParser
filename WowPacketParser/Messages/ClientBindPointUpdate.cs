using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBindPointUpdate
    {
        public uint BindMapID;
        public Vector3 BindPosition;
        public uint BindAreaID;
    }
}
