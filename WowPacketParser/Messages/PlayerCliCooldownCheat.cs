using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliCooldownCheat
    {
        public ulong Guid;
        public byte CheatCode;
    }
}
