using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
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
