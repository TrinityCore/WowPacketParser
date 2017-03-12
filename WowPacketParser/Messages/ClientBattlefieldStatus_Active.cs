using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlefieldStatus_Active
    {
        public ClientBattlefieldStatus_Header Hdr;
        public uint ShutdownTimer;
        public byte ArenaFaction;
        public bool LeftEarly;
        public uint StartTimer;
        public uint Mapid;
    }
}
