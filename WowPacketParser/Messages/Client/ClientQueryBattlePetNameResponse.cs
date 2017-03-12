using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryBattlePetNameResponse
    {
        public ulong BattlePetID;
        public int CreatureID;
        public bool Allow;
        public string Name;
        public bool HasDeclined;
        public string[/*5*/] DeclinedNames;
        public UnixTime Timestamp;
    }
}
