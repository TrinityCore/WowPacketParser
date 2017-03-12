using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSummonRaidMemberValidateFailed
    {
        public List<ClientSummonRaidMemberValidateReason> Members;
    }
}
