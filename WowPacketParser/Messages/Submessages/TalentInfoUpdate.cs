using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct TalentInfoUpdate
    {
        public byte ActiveGroup;
        public List<TalentGroupInfo> TalentGroups;
    }
}
