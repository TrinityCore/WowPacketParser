﻿using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("points_of_interest")]
    public sealed record PointsOfInterest : IDataModel
    {
        [DBFieldName("ID", true, true)]
        public object ID;

        [DBFieldName("PositionX")]
        public float? PositionX;

        [DBFieldName("PositionY")]
        public float? PositionY;

        [DBFieldName("PositionZ", TargetedDatabase.Shadowlands)]
        public float? PositionZ;

        [DBFieldName("Icon")]
        public GossipPOIIcon? Icon;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("Importance")]
        public uint? Importance;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Unknown905", TargetedDatabase.Shadowlands)]
        public int? Unknown905;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
