using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLiveRegionGetAccountCharacterListResult
    {
        public bool Success;
        public List<CliAccountCharacterData> Characters;
        public uint Token;
    }
}
