using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSessionAlertConfig
    {
        public int Delay;
        public int Period;
        public int DisplayTime;
    }
}
