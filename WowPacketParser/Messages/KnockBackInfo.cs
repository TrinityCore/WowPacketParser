using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct KnockBackInfo
    {
        public float HorzSpeed;
        public Vector2 Direction;
        public float InitVertSpeed;
    }
}
