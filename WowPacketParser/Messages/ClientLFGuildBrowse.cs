using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGuildBrowse
    {
        public List<LFGuildBrowseData> Post;
    }
}
