using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSetupCurrency
    {
        public List<ClientSetupCurrencyRecord> Data;
    }
}
