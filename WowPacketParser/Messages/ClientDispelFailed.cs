using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDispelFailed
    {
        public List<int> FailedSpells;
        public uint SpellID;
        public ulong VictimGUID;
        public ulong CasterGUID;
    }
}
