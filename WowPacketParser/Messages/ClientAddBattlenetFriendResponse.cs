using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAddBattlenetFriendResponse
    {
        public ushort? BattlenetError; // Optional
        public ulong ClientToken;
        public addbattlenetfrienderror Result;
    }
}
