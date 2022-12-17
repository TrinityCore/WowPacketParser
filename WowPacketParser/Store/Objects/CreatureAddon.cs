﻿using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_addon")]
    public sealed record CreatureAddon : IDataModel
    {
        [DBFieldName("guid", true, noQuotes: true)]
        public string GUID;

        [DBFieldName("path_id")]
        public uint? PathID;

        [DBFieldName("mount")]
        public uint? Mount;

        [DBFieldName("bytes1")]
        public uint? Bytes1;

        [DBFieldName("bytes2")]
        public uint? Bytes2;

        [DBFieldName("emote")]
        public uint? Emote;

        [DBFieldName("aiAnimKit", TargetedDatabaseFlag.SinceLegion)]
        public ushort? AIAnimKit;

        [DBFieldName("movementAnimKit", TargetedDatabaseFlag.SinceLegion)]
        public ushort? MovementAnimKit;

        [DBFieldName("meleeAnimKit", TargetedDatabaseFlag.SinceLegion)]
        public ushort? MeleeAnimKit;

        // visibilityDistanceType exists in all database versions but because UnitFlags2 to detect the value from sniff doesn't exist in earlier client version
        // we pretend the field doesn't exist
        [DBFieldName("visibilityDistanceType", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public byte? VisibilityDistanceType;

        [DBFieldName("auras")]
        public string Auras;
    }
}
