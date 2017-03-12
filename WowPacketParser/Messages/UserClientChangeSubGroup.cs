using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientChangeSubGroup
    {
        public ulong Target;
        public byte Subgroup;
        public byte PartyIndex;
    }
}
