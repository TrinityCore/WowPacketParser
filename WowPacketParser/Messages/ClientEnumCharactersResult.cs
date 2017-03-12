using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientEnumCharactersResult
    {
        public List<ClientCharacterListEntry> Characters;
        public bool Success;
        public List<ClientRestrictedFactionChangeRule> FactionChangeRestrictions;
        public bool IsDeletedCharacters;
    }
}
