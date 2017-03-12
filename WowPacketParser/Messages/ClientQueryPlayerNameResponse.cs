using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryPlayerNameResponse
    {
        public ulong Player;
        public byte Result;
        public PlayerGuidLookupData Data;
    }
}
