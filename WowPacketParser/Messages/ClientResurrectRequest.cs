using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientResurrectRequest
    {
        public ulong ResurrectOffererGUID;
        public bool Sickness;
        public uint ResurrectOffererVirtualRealmAddress;
        public string Name;
        public uint PetNumber;
        public bool UseTimer;
        public int SpellID;
    }
}
