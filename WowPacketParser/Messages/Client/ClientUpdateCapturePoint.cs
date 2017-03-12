using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientUpdateCapturePoint
    {
        public BattlegroundCapturePointInfo CapturePointInfo;
    }
}
