using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliBnetLicense
    {
        public int LicenseID;
        public UnixTime Expiration;
    }
}
