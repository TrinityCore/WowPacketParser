using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalChallengeModeRequestLeaders
    {
        public UnixTime LastGuildUpdate;
        public int MapID;
        public UnixTime LastRealmUpdate;
    }
}
