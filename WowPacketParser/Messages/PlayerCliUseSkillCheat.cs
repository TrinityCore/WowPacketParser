using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliUseSkillCheat
    {
        public uint SkillID;
        public uint Count;
    }
}
