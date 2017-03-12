using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
