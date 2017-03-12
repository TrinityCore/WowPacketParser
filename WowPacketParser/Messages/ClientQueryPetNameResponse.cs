using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryPetNameResponse
    {
        public ulong PetID;
        public bool Allow;
        public string Name;
        public bool HasDeclined;
        public UnixTime Timestamp;
        public string[/*5*/] DeclinedNames;
    }
}
