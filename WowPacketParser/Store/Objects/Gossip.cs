using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    //[DBTableName("gossip_menu")]
    public class Gossip
    {
        public ObjectType ObjectType;

        public uint ObjectEntry;

        public ICollection<GossipOption> GossipOptions;
    }

    //[DBTableName("gossip_menu_option")]
    public class GossipOption
    {
        //[DBFieldName("id")]
        public uint Index;

        //[DBFieldName("option_icon")]
        public GossipOptionIcon OptionIcon;

        //[DBFieldName("box_coded")]
        public bool Box;

        //[DBFieldName("box_money")]
        public uint RequiredMoney;

        //[DBFieldName("box_text")]
        public string BoxText;

        //[DBFieldName("option_text")]
        public string OptionText;

        // action_menu_id ?
        // action_poi_id ?
        // npc_option_npcflag ?
    }
}
