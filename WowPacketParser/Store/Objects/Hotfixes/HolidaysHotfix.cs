using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("holidays")]
    public sealed record HolidaysHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Region")]
        public ushort? Region;

        [DBFieldName("Looping")]
        public byte? Looping;

        [DBFieldName("HolidayNameID")]
        public uint? HolidayNameID;

        [DBFieldName("HolidayDescriptionID")]
        public uint? HolidayDescriptionID;

        [DBFieldName("Priority")]
        public byte? Priority;

        [DBFieldName("CalendarFilterType")]
        public sbyte? CalendarFilterType;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("Duration", 10)]
        public ushort?[] Duration;

        [DBFieldName("Date", 26)]
        public uint?[] Date;

        [DBFieldName("CalendarFlags", 10)]
        public byte?[] CalendarFlags;

        [DBFieldName("TextureFileDataID", 3)]
        public int?[] TextureFileDataID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("holidays")]
    public sealed record HolidaysHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Region")]
        public ushort? Region;

        [DBFieldName("Looping")]
        public byte? Looping;

        [DBFieldName("HolidayNameID")]
        public uint? HolidayNameID;

        [DBFieldName("HolidayDescriptionID")]
        public uint? HolidayDescriptionID;

        [DBFieldName("Priority")]
        public byte? Priority;

        [DBFieldName("CalendarFilterType")]
        public sbyte? CalendarFilterType;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("WorldStateExpressionID")]
        public uint? WorldStateExpressionID;

        [DBFieldName("Duration", 10)]
        public ushort?[] Duration;

        [DBFieldName("Date", 16)]
        public uint?[] Date;

        [DBFieldName("CalendarFlags", 10)]
        public byte?[] CalendarFlags;

        [DBFieldName("TextureFileDataID", 3)]
        public int?[] TextureFileDataID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
