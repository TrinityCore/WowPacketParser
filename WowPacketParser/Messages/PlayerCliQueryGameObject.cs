using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQueryGameObject
    {
        public ulong Guid;
        public uint GameObjectID;
    }
}
