using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuthResponse
    {
        public AuthWaitInfo WaitInfo; // Optional
        public byte Result;
        public AuthSuccessInfo SuccessInfo; // Optional
    }
}
