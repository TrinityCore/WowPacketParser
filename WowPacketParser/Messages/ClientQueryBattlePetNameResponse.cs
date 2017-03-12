using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
