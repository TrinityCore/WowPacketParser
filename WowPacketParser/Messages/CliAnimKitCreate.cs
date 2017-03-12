using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliAnimKitCreate
    {
        public ushort AiID;
        public ushort MovementID;
        public ushort MeleeID;
    }
}
