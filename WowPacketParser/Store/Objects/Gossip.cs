using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public class Gossip
    {
        public ObjectType ObjectType;

        public uint ObjectEntry;

        public ICollection<GossipOption> GossipOptions;
    }
}
