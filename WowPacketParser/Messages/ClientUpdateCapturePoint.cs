using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUpdateCapturePoint
    {
        public BattlegroundCapturePointInfo CapturePointInfo;
    }
}
