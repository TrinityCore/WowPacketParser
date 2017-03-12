using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLossOfControlAuraUpdate
    {
        public List<LossOfControlInfo> LossOfControlInfo;
    }
}
