using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_3_0_33062
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
        public uint ModifiersMask { get; set; }
        public int Context { get; set; }
        public ulong ArtifactXP { get; set; }
        public byte ItemAppearanceModID { get; set; }
        public uint ZoneFlags { get; set; }
        public DynamicUpdateField<int> Modifiers { get; } = new DynamicUpdateField<int>();
        public DynamicUpdateField<IArtifactPower> ArtifactPowers { get; } = new DynamicUpdateField<IArtifactPower>();
        public DynamicUpdateField<ISocketedGem> Gems { get; } = new DynamicUpdateField<ISocketedGem>();
    }
}

