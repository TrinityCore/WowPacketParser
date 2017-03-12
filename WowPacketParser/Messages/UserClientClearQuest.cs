using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientClearQuest
    {
        public string Target;
        public uint QuestID;
    }
}
