using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGMCreateTicketResponse
    {
        public ulong TargetGUID;
        public string Arguments;
    }
}
