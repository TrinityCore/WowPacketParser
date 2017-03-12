using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
