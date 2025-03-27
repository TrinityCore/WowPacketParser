using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_race_x_chr_model")]
    public sealed record ChrRaceXChrModelHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrRacesID")]
        public uint? ChrRacesID;

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
    public sealed record ChrRaceXChrModelHotfix1110 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrRacesID")]
        public byte? ChrRacesID;

        [DBFieldName("ChrModelID")]
        public int? ChrModelID;

        [DBFieldName("Sex")]
        public sbyte? Sex;

        [DBFieldName("AllowedTransmogSlots")]
        public int? AllowedTransmogSlots;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
