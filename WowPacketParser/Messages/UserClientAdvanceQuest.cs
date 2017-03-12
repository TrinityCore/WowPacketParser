using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientAdvanceQuest
    {
        public string Target;
        public uint QuestID;
    }
}
