using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePayStartDistributionMakeProductChoiceResponse
    {
        public uint ClientToken;
        public ProductChoiceResult Result;
        public ulong DistributionID;
    }
}
