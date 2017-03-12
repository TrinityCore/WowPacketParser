using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct TalentInfoUpdate
    {
        public byte ActiveGroup;
        public List<TalentGroupInfo> TalentGroups;
    }
}
