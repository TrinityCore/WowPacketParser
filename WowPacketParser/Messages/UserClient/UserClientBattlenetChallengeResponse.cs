using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlenetChallengeResponse
    {
        public BattlenetChallengeResult Result;
        public uint Token;
        public string BattlenetError;
    }
}
