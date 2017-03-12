using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientFlagQuest
    {
        public string Target;
        public uint QuestID;
    }
}
