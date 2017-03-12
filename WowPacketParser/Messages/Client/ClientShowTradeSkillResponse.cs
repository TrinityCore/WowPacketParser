using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientShowTradeSkillResponse
    {
        public SkillLineData SkillLineData;
        public ulong PlayerGUID;
    }
}
