using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliStartQuest
    {
        public bool AbandonExisting;
        public int QuestID;
        public bool AutoAccept;
    }
}
