using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQuestGiverStatus
    {
        public ulong QuestGiverGUID;
        public uint StatusFlags;
    }
}
