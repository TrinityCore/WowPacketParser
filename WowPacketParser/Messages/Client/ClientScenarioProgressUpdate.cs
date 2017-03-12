using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientScenarioProgressUpdate
    {
        public CriteriaProgress Progress;
    }
}
