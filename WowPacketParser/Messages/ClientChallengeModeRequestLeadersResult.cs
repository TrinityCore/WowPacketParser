using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientChallengeModeRequestLeadersResult
    {
        public UnixTime LastRealmUpdate;
        public List<ClientChallengeModeAttempt> GuildLeaders;
        public int MapID;
        public UnixTime LastGuildUpdate;
        public List<ClientChallengeModeAttempt> RealmLeaders;
    }
}
