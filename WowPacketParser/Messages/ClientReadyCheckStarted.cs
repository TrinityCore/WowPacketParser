using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientReadyCheckStarted
    {
        public byte PartyIndex;
        public ulong InitiatorGUID;
        public ulong PartyGUID;
        public UnixTime Duration;
    }
}
