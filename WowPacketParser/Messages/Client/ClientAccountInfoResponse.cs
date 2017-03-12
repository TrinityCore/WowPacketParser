using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAccountInfoResponse // FIXME: No handlers
    {
        public int BnetAccountID;
        public List<CliBnetLicense> GameAccountLicenses;
        public List<CliBnetLicense> BnetAccountLicenses;
        public int GameAccountFlags;
        public int LocalAccountFlags;
        public string BnetAccountName;
        public int BnetAccountFlags;
        public int GameAccountID;
        public string GameAccountName;
    }
}
