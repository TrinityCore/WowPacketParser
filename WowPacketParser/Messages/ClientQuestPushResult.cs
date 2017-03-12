using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQuestPushResult
    {
        public ulong SenderGUID;
        public byte Result;
    }
}
