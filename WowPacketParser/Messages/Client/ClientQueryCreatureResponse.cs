using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryCreatureResponse
    {
        public bool Allow;
        public CliCreatureStats Stats;
        public uint CreatureID;
    }
}
