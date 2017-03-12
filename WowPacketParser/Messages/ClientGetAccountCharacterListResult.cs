using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGetAccountCharacterListResult
    {
        public uint Token;
        public List<CliAccountCharacterData> Characters;
    }
}
