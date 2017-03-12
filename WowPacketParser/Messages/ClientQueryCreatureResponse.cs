using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryCreatureResponse
    {
        public bool Allow;
        public CliCreatureStats Stats;
        public uint CreatureID;
    }
}
