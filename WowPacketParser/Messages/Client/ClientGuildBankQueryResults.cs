using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildBankQueryResults
    {
        public List<GuildBankItemInfo> ItemInfo;
        public int WithdrawalsRemaining;
        public int Tab;
        public List<GuildBankTabInfo> TabInfo;
        public bool FullUpdate;
        public ulong Money;
    }
}
