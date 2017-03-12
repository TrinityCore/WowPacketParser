using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellUpdateChainTargets
    {
        public ulong Guid;
        public List<ulong> Targets;
        public int SpellID;
    }
}
