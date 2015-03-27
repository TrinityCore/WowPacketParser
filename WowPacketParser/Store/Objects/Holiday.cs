using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("holidays")]
    public sealed class HolidayData
    {
        [DBFieldName("Duration", 10)]
        public uint[] Duration;

        [DBFieldName("Date", 16)]
        public uint[] Date;

        [DBFieldName("Region")]
        public uint Region;

        [DBFieldName("Looping")]
        public uint Looping;

        [DBFieldName("CalendarFlags", 10)]
        public uint[] CalendarFlags;

        [DBFieldName("HolidayNameID")]
        public uint HolidayNameID;

        [DBFieldName("HolidayDescriptionID")]
        public uint HolidayDescriptionID;

        [DBFieldName("TextureFilename")]
        public string TextureFilename;

        [DBFieldName("Priority")]
        public uint Priority;

        [DBFieldName("CalendarFilterType")]
        public uint CalendarFilterType;

        [DBFieldName("Flags")]
        public uint Flags;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
