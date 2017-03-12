using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGetAccountCharacterListResult
    {
        public uint Token;
        public List<CliAccountCharacterData> Characters;
    }
}
