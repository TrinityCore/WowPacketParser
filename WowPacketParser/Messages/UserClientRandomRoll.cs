using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientRandomRoll
    {
        public int Min;
        public int Max;
        public byte PartyIndex;
    }
}
