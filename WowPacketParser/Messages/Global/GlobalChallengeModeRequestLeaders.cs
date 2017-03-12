using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Global
{
    public unsafe struct GlobalChallengeModeRequestLeaders
    {
        public UnixTime LastGuildUpdate;
        public int MapID;
        public UnixTime LastRealmUpdate;
    }
}
