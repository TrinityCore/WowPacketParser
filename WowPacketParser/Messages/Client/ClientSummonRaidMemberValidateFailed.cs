using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSummonRaidMemberValidateFailed
    {
        public List<ClientSummonRaidMemberValidateReason> Members;
    }
}
