using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliGarrisonRemoteInfo
    {
        public List<CliGarrisonRemoteSiteInfo> Sites;
    }
}
