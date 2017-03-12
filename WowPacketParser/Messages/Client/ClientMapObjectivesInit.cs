using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMapObjectivesInit
    {
        public List<BattlegroundCapturePointInfo> CapturePointInfo;
    }
}
