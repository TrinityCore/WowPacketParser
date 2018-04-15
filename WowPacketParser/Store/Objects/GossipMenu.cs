using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gossip_menu")]
    public class GossipMenu : IDataModel
    {
        [DBFieldName("MenuId", true)]
        public uint? Entry;

        [DBFieldName("TextId", true)]
        public uint? TextID;

        public ObjectType ObjectType;

        public uint ObjectEntry;

        //public ICollection<GossipMenuOption> GossipOptions;
    }
}
