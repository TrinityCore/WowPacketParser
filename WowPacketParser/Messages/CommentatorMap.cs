using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CommentatorMap
    {
        public uint TeamSize;
        public uint MinLevelRange;
        public uint MaxLevelRange;
        public int DifficultyID;
        public List<CommentatorInstance> Instances;
    }
}
