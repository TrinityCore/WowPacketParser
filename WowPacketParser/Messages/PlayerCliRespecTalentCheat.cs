using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliRespecTalentCheat
    {
        public int Id;
        public byte RespecType;
    }
}
