using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetupCurrency
    {
        public List<ClientSetupCurrencyRecord> Data;
    }
}
