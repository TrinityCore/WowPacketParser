using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliQuestInfoObjectiveSimple
    {
        public int Id;
        public int ObjectID;
        public int Amount;
        public byte Type;
    }
}
