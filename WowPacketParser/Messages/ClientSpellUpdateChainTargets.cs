using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellUpdateChainTargets
    {
        public ulong Guid;
        public List<ulong> Targets;
        public int SpellID;
    }
}
