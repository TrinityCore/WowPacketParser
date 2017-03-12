using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryObjPosition
    {
        public float Facing;
        public Vector3 Position;
        public uint MapID;
        public bool ToClipboard;
    }
}
