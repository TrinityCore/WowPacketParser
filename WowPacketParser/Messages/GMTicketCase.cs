using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GMTicketCase
    {
        public int CaseID;
        public UnixTime CaseOpened;
        public int CaseStatus;
        public string Url;
        public uint CfgRealmID;
        public ulong CharacterID;
        public string WaitTimeOverrideMessage;
        public int WaitTimeOverrideMinutes;
    }
}
