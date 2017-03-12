using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuthResponse
    {
        public AuthWaitInfo? WaitInfo; // Optional
        public byte Result;
        public AuthSuccessInfo? SuccessInfo; // Optional
    }
}
