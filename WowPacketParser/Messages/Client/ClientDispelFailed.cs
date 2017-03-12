using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDispelFailed
    {
        public List<int> FailedSpells;
        public uint SpellID;
        public ulong VictimGUID;
        public ulong CasterGUID;
    }
}
