using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
