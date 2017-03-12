using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientEnumCharactersResult
    {
        public List<ClientCharacterListEntry> Characters;
        public bool Success;
        public List<ClientRestrictedFactionChangeRule> FactionChangeRestrictions;
        public bool IsDeletedCharacters;
    }
}
