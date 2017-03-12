using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientFinishQuest
    {
        public uint QuestID;
        public string Target;
    }
}
