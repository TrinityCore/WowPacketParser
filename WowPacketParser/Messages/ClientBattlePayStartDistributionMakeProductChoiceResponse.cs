using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePayStartDistributionMakeProductChoiceResponse
    {
        public uint ClientToken;
        public ProductChoiceResult Result;
        public ulong DistributionID;
    }
}
