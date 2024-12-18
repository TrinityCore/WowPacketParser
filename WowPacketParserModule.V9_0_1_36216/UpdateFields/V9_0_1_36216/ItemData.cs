using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class ItemData : IItemData
    {
        public int[] BonusListIDs { get; set; }
        public WowGuid Owner { get; set; }
        public WowGuid ContainedIn { get; set; }
        public WowGuid Creator { get; set; }
        public WowGuid GiftCreator { get; set; }
        public uint StackCount { get; set; }
        public uint Expiration { get; set; }
        public int[] SpellCharges { get; } = new int[5];
        public uint DynamicFlags { get; set; }
        public IItemEnchantment[] Enchantment { get; } = new IItemEnchantment[13];
        public uint Durability { get; set; }
        public uint MaxDurability { get; set; }
        public uint CreatePlayedTime { get; set; }
        public int Context { get; set; }
        public int CreateTime { get; set; }
        public ulong ArtifactXP { get; set; }
        public byte ItemAppearanceModID { get; set; }
        public uint ZoneFlags { get; set; }
        public DynamicUpdateField<IArtifactPower> ArtifactPowers { get; } = new DynamicUpdateField<IArtifactPower>();
        public DynamicUpdateField<ISocketedGem> Gems { get; } = new DynamicUpdateField<ISocketedGem>();
        public IItemModList Modifiers { get; set; }
    }
}

