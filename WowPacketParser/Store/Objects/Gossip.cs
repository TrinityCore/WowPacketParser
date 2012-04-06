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

    public class GossipOption
    {
        public uint Index;

        public GossipOptionIcon OptionIcon;

        public bool Box;

        public uint RequiredMoney;

        public string OptionText;

        public string BoxText;
    }
}
