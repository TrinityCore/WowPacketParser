using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_race_x_chr_model")]
    public sealed record ChrRaceXChrModelHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrRacesID")]
        public int? ChrRacesID;

        [DBFieldName("ChrModelID")]
        public int? ChrModelID;

        [DBFieldName("Sex")]
        public int? Sex;

        [DBFieldName("AllowedTransmogSlots")]
        public int? AllowedTransmogSlots;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_race_x_chr_model")]
    public sealed record ChrRaceXChrModelHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrRacesID")]
        public int? ChrRacesID;

        [DBFieldName("ChrModelID")]
        public int? ChrModelID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("chr_race_x_chr_model")]
    public sealed record ChrRaceXChrModelHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrRacesID")]
        public int? ChrRacesID;

        [DBFieldName("ChrModelID")]
        public int? ChrModelID;

        [DBFieldName("Sex")]
        public int? Sex;

        [DBFieldName("AllowedTransmogSlots")]
        public int? AllowedTransmogSlots;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
