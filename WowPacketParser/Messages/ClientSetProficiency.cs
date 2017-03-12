using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetProficiency
    {
        public uint ProficiencyMask;
        public byte ProficiencyClass;
    }
}
