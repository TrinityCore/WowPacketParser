using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientExpectedSpamRecords
    {
        public List<ClientSpamRecord> SpamRecord;
    }
}
