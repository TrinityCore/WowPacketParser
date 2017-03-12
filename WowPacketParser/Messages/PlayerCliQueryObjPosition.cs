using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQueryObjPosition
    {
        public bool ToClipboard;
        public ulong Guid;
    }
}
