using System.Collections.Generic;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliQuestInfoObjective
    {
        public int Id;
        public byte Type;
        public sbyte StorageIndex;
        public int ObjectID;
        public int Amount;
        public int Flags;
        public string Description;
        public List<int> VisualEffects;
    }
}
