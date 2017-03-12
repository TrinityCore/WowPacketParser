using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliGarrisonRemoteInfo
    {
        public List<CliGarrisonRemoteSiteInfo> Sites;
    }
}
