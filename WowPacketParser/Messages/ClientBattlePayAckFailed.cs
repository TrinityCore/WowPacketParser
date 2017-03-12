using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePayAckFailed
    {
        public ulong PurchaseID;
        public uint Status;
        public uint Result;
        public uint ServerToken;
    }
}
