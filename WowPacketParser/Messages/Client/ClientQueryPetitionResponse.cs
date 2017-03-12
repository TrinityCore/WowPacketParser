using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryPetitionResponse
    {
        public uint PetitionID;
        public bool Allow;
        public CliPetitionInfo Info;
    }
}
