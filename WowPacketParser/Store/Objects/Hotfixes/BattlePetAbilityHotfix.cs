using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("battle_pet_ability")]
    public sealed record BattlePetAbilityHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("PetTypeEnum")]
        public sbyte? PetTypeEnum;

        [DBFieldName("Cooldown")]
        public uint? Cooldown;

        [DBFieldName("BattlePetVisualID")]
        public ushort? BattlePetVisualID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("battle_pet_ability_locale")]
    public sealed record BattlePetAbilityLocaleHotfix1000 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("battle_pet_ability")]
    public sealed record BattlePetAbilityHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("PetTypeEnum")]
        public sbyte? PetTypeEnum;

        [DBFieldName("Cooldown")]
        public uint? Cooldown;

        [DBFieldName("BattlePetVisualID")]
        public ushort? BattlePetVisualID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("battle_pet_ability_locale")]
    public sealed record BattlePetAbilityLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
