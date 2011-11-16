using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WowPacketParser.Store.Objects
{
    public class GossipMenu
    {
        public uint MenuId;

        public List<uint> NpcTextIds; // should this be List<NpcText> ?

        public List<GossipOption> GossipOptions;
    }
}
