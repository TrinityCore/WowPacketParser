using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("battle_pet_species")]
    public sealed record BattlePetSpeciesHotfix1100: IDataModel
    {
        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("SourceText")]
        public string SourceText;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CreatureID")]
        public int? CreatureID;

        [DBFieldName("SummonSpellID")]
        public int? SummonSpellID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("PetTypeEnum")]
        public sbyte? PetTypeEnum;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("SourceTypeEnum")]
        public sbyte? SourceTypeEnum;

        [DBFieldName("CardUIModelSceneID")]
        public int? CardUIModelSceneID;

        [DBFieldName("LoadoutUIModelSceneID")]
        public int? LoadoutUIModelSceneID;

        [DBFieldName("CovenantID")]
        public int? CovenantID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("battle_pet_species_locale")]
    public sealed record BattlePetSpeciesLocaleHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("SourceText_lang")]
        public string SourceTextLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
