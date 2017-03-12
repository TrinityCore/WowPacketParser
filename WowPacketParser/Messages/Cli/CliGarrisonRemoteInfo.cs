using System.Collections.Generic;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliGarrisonRemoteInfo
    {
        public List<CliGarrisonRemoteSiteInfo> Sites;
    }
}
