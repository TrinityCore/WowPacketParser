using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAccountInfoResponse
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
