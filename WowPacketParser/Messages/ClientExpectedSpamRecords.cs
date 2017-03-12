using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientExpectedSpamRecords
    {
        public List<ClientSpamRecord> SpamRecord;
    }
}
