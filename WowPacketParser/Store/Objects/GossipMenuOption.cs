using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gossip_menu_option")]
    public sealed record GossipMenuOption : IDataModel
    {
        [DBFieldName("MenuID", true)]
        public uint? MenuID;

        [DBFieldName("GossipOptionID", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.WotlkClassic)]
        public int? GossipOptionID;

        [DBFieldName("OptionID", true)]
        public uint? OptionID;

        [DBFieldName("OptionIcon", TargetedDatabaseFlag.TillBattleForAzeroth)]
        [DBFieldName("OptionNpc", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic)]
        public GossipOptionNpc? OptionNpc;

        [DBFieldName("OptionText")]
        public string OptionText;

        [DBFieldName("OptionBroadcastTextID")]
        public int? OptionBroadcastTextId;

        [DBFieldName("OptionType", TargetedDatabaseFlag.TillBattleForAzeroth)]
        public GossipOptionType? OptionType;

        [DBFieldName("OptionNpcFlag", TargetedDatabaseFlag.TillBattleForAzeroth)]
        public NPCFlags? OptionNpcFlag;

        [DBFieldName("Language", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic)]
        public Language? Language;

        [DBFieldName("Flags", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.WotlkClassic)]
        public int? Flags;

        [DBFieldName("ActionMenuID")]
        public uint? ActionMenuID = 0;

        [DBFieldName("ActionPoiID", false, true)]
        public object ActionPoiID = 0;

        [DBFieldName("GossipNpcOptionID", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.WotlkClassic, false, false, true)]
        public int? GossipNpcOptionID;

        [DBFieldName("BoxCoded")]
        public bool? BoxCoded;

        [DBFieldName("BoxMoney")]
        public uint? BoxMoney;

        [DBFieldName("BoxText", false, false, true)]
        public string BoxText;

        [DBFieldName("BoxBroadcastTextID")]
        public int? BoxBroadcastTextID;

        [DBFieldName("SpellID", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic, false, false, true)]
        public int? SpellID;

        [DBFieldName("OverrideIconID", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.WotlkClassic, false, false, true)]
        public int? OverrideIconID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public string BroadcastTextIDHelper;

        public void FillBroadcastTextIDs()
        {
            List<int> boxTextList;
            List<int> optionTextList;

            if (!string.IsNullOrEmpty(BoxText) && SQLDatabase.BroadcastTexts.TryGetValue(BoxText, out boxTextList))
            {
                BoxBroadcastTextID = boxTextList[0];
                if (boxTextList.Count != 1)
                {
                    BroadcastTextIDHelper += "BoxBroadcastTextID: ";
                    BroadcastTextIDHelper += string.Join(" - ", boxTextList);
                }
            }
            else
                BoxBroadcastTextID = 0;

            if (!string.IsNullOrEmpty(OptionText) && SQLDatabase.BroadcastTexts.TryGetValue(OptionText, out optionTextList))
            {
                OptionBroadcastTextId = optionTextList[0];
                if (optionTextList.Count != 1)
                {
                    BroadcastTextIDHelper += "OptionBroadcastTextID: ";
                    BroadcastTextIDHelper += string.Join(" - ", optionTextList);
                }
            }
            else
                OptionBroadcastTextId = 0;
        }

        public void FillOptionType(WowGuid npcGuid)
        {
            NPCFlags[] npcFlags = new NPCFlags[2];
            if (Storage.Objects.TryGetValue(npcGuid, out WoWObject npcObj))
            {
                var npc = npcObj as Unit;
                if (npc != null && npc.UnitData.NpcFlags != null)
                {
                    for (var i = 0; i< npc.UnitData.NpcFlags.Length; i++)
                    {
                        if (npc.UnitData.NpcFlags[i] == null)
                            continue;
                        npcFlags[i] = (NPCFlags)npc.UnitData.NpcFlags[i];
                    }
                }
            }

            switch (OptionNpc)
            {
                case GossipOptionNpc.None:
                    if (npcFlags[0].HasAnyFlag(NPCFlags.Gossip))
                    {
                        OptionType = GossipOptionType.Gossip;
                        OptionNpcFlag = NPCFlags.Gossip;
                    }
                    break;
                case GossipOptionNpc.Vendor:
                    OptionType = GossipOptionType.Vendor;
                    if (npcFlags[0].HasAnyFlag(NPCFlags.AmmoVendor))
                        OptionNpcFlag = NPCFlags.AmmoVendor;
                    else if (npcFlags[0].HasAnyFlag(NPCFlags.FoodVendor))
                        OptionNpcFlag = NPCFlags.FoodVendor;
                    else if (npcFlags[0].HasAnyFlag(NPCFlags.PoisonVendor))
                        OptionNpcFlag = NPCFlags.PoisonVendor;
                    else if (npcFlags[0].HasAnyFlag(NPCFlags.ReagentVendor))
                        OptionNpcFlag = NPCFlags.ReagentVendor;
                    else
                        OptionNpcFlag = NPCFlags.Vendor;
                    break;
                case GossipOptionNpc.TaxiNode:
                    OptionType = GossipOptionType.Taxivendor;
                    OptionNpcFlag = NPCFlags.FlightMaster;
                    break;
                case GossipOptionNpc.Trainer:
                    OptionType = GossipOptionType.Trainer;
                    if (npcFlags[0].HasAnyFlag(NPCFlags.Trainer))
                        OptionNpcFlag = NPCFlags.Trainer;
                    else if (npcFlags[0].HasAnyFlag(NPCFlags.ProfessionTrainer))
                        OptionNpcFlag = NPCFlags.ProfessionTrainer;
                    else if (npcFlags[0].HasAnyFlag(NPCFlags.ClassTrainer))
                        OptionNpcFlag = NPCFlags.ClassTrainer;
                    break;
                case GossipOptionNpc.SpiritHealer:
                    OptionType = GossipOptionType.Spirithealer;
                    OptionNpcFlag = NPCFlags.SpiritHealer;
                    break;
                case GossipOptionNpc.Binder:
                    OptionType = GossipOptionType.Innkeeper;
                    OptionNpcFlag = NPCFlags.InnKeeper;
                    break;
                case GossipOptionNpc.Banker:
                    OptionType = GossipOptionType.Banker;
                    OptionNpcFlag = NPCFlags.Banker;
                    break;
                case GossipOptionNpc.PetitionVendor:
                    OptionType = GossipOptionType.Petitioner;
                    OptionNpcFlag = NPCFlags.Petitioner;
                    break;
                case GossipOptionNpc.TabardVendor:
                    OptionType = GossipOptionType.TabardDesigner;
                    OptionNpcFlag = NPCFlags.TabardDesigner;
                    break;
                case GossipOptionNpc.BattleMaster:
                    OptionType = GossipOptionType.Battlefield;
                    OptionNpcFlag = NPCFlags.BattleMaster;
                    break;
                case GossipOptionNpc.Auctioneer:
                    OptionType = GossipOptionType.Auctioneer;
                    OptionNpcFlag = NPCFlags.Auctioneer;
                    break;
                case GossipOptionNpc.StableMaster:
                    OptionType = GossipOptionType.StablePet;
                    OptionNpcFlag = NPCFlags.StableMaster;
                    break;
                case GossipOptionNpc.Transmogrify:
                    OptionType = GossipOptionType.Transmogrifier;
                    OptionNpcFlag = NPCFlags.Transmogrifier;
                    break;
            }
        }
    }
}
