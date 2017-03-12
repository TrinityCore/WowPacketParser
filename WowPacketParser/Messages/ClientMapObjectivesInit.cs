using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMapObjectivesInit
    {
        public List<BattlegroundCapturePointInfo> CapturePointInfo;
    }
}
