using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryObjPosition
    {
        public float Facing;
        public Vector3 Position;
        public uint MapID;
        public bool ToClipboard;
    }
}
