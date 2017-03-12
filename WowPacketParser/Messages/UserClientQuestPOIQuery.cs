using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientQuestPOIQuery
    {
        public int MissingQuestCount;
        public fixed int MissingQuestPOIs[50];
    }
}
