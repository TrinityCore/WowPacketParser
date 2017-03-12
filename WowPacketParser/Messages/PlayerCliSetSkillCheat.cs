using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetSkillCheat
    {
        public uint Level;
        public uint SkillID;
    }
}
