using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("taxi_path_node")]
    public sealed record TaxiPathNodeHotfix1000: IDataModel
    {
        [DBFieldName("LocX")]
        public float? LocX;

        [DBFieldName("LocY")]
        public float? LocY;

        [DBFieldName("LocZ")]
        public float? LocZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PathID")]
        public ushort? PathID;

        [DBFieldName("NodeIndex")]
        public int? NodeIndex;

        [DBFieldName("ContinentID")]
        public ushort? ContinentID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("Delay")]
        public uint? Delay;

        [DBFieldName("ArrivalEventID")]
        public int? ArrivalEventID;

        [DBFieldName("DepartureEventID")]
        public int? DepartureEventID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("taxi_path_node")]
    public sealed record TaxiPathNodeHotfix1017 : IDataModel
    {
        [DBFieldName("LocX")]
        public float? LocX;

        [DBFieldName("LocY")]
        public float? LocY;

        [DBFieldName("LocZ")]
        public float? LocZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PathID")]
        public ushort? PathID;

        [DBFieldName("NodeIndex")]
        public int? NodeIndex;

        [DBFieldName("ContinentID")]
        public ushort? ContinentID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("Delay")]
        public uint? Delay;

        [DBFieldName("ArrivalEventID")]
        public int? ArrivalEventID;

        [DBFieldName("DepartureEventID")]
        public int? DepartureEventID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("taxi_path_node")]
    public sealed record TaxiPathNodeHotfix340: IDataModel
    {
        [DBFieldName("LocX")]
        public float? LocX;

        [DBFieldName("LocY")]
        public float? LocY;

        [DBFieldName("LocZ")]
        public float? LocZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PathID")]
        public ushort? PathID;

        [DBFieldName("NodeIndex")]
        public int? NodeIndex;

        [DBFieldName("ContinentID")]
        public ushort? ContinentID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("Delay")]
        public uint? Delay;

        [DBFieldName("ArrivalEventID")]
        public uint? ArrivalEventID;

        [DBFieldName("DepartureEventID")]
        public uint? DepartureEventID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("taxi_path_node")]
    public sealed record TaxiPathNodeHotfix343: IDataModel
    {
        [DBFieldName("LocX")]
        public float? LocX;

        [DBFieldName("LocY")]
        public float? LocY;

        [DBFieldName("LocZ")]
        public float? LocZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PathID")]
        public ushort? PathID;

        [DBFieldName("NodeIndex")]
        public int? NodeIndex;

        [DBFieldName("ContinentID")]
        public ushort? ContinentID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("Delay")]
        public uint? Delay;

        [DBFieldName("ArrivalEventID")]
        public uint? ArrivalEventID;

        [DBFieldName("DepartureEventID")]
        public uint? DepartureEventID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
