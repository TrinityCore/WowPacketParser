using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBattlenetChallengeResponse
    {
        public BattlenetChallengeResult Result;
        public uint Token;
        public string BattlenetError;
    }
}
