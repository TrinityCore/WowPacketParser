using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

// This file is automatically generated, DO NOT EDIT

namespace WowPacketParserModule.V3_4_0_45166.UpdateFields.V3_4_1_47014
{
    public class PlayerData : IPlayerData
    {
        public WowGuid DuelArbiter { get; set; }
        public WowGuid WowAccount { get; set; }
        public WowGuid LootTargetGUID { get; set; }
        public System.Nullable<uint> PlayerFlags { get; set; }
        public System.Nullable<uint> PlayerFlagsEx { get; set; }
        public System.Nullable<uint> GuildRankID { get; set; }
        public System.Nullable<uint> GuildDeleteDate { get; set; }
        public System.Nullable<int> GuildLevel { get; set; }
        public System.Nullable<byte> PartyType { get; set; }
        public System.Nullable<byte> NativeSex { get; set; }
        public System.Nullable<byte> Inebriation { get; set; }
        public System.Nullable<byte> PvpTitle { get; set; }
        public System.Nullable<byte> ArenaFaction { get; set; }
        public System.Nullable<byte> PvpRank { get; set; }
        public System.Nullable<byte> Unk254 { get; set; }
        public System.Nullable<uint> DuelTeam { get; set; }
        public System.Nullable<int> GuildTimeStamp { get; set; }
        public IQuestLog[] QuestLog { get; } = new IQuestLog[25];
        public IVisibleItem[] VisibleItems { get; } = new IVisibleItem[19];
        public System.Nullable<int> PlayerTitle { get; set; }
        public System.Nullable<int> FakeInebriation { get; set; }
        public System.Nullable<uint> VirtualPlayerRealm { get; set; }
        public System.Nullable<uint> CurrentSpecID { get; set; }
        public System.Nullable<int> TaxiMountAnimKitID { get; set; }
        public System.Nullable<float>[] AvgItemLevel { get; } = new System.Nullable<float>[6];
        public System.Nullable<byte> CurrentBattlePetBreedQuality { get; set; }
        public System.Nullable<int> HonorLevel { get; set; }
        public System.Nullable<long> LogoutTime { get; set; }
        public DynamicUpdateField<IChrCustomizationChoice> Customizations { get; } = new DynamicUpdateField<IChrCustomizationChoice>();
        public DynamicUpdateField<IArenaCooldown> ArenaCooldowns { get; } = new DynamicUpdateField<IArenaCooldown>();
        public DynamicUpdateField<System.Nullable<int>> VisualItemReplacements { get; } = new DynamicUpdateField<System.Nullable<int>>();
    }
}

