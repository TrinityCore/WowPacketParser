using WowPacketParser.Enums;
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

        [DBFieldName("StandState")]
        public byte? StandState;

        [DBFieldName("AnimTier")]
        public byte? AnimTier;

        [DBFieldName("VisFlags")]
        public byte? VisFlags;

        [DBFieldName("SheathState")]
        public byte? SheathState;

        [DBFieldName("PvpFlags")]
        public byte? PvpFlags;

        [DBFieldName("emote")]
        public uint? Emote;

        [DBFieldName("aiAnimKit", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public ushort? AIAnimKit;

        [DBFieldName("movementAnimKit", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public ushort? MovementAnimKit;

        [DBFieldName("meleeAnimKit", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public ushort? MeleeAnimKit;

        // visibilityDistanceType exists in all database versions but because UnitFlags2 to detect the value from sniff doesn't exist in earlier client version
        // we pretend the field doesn't exist
        [DBFieldName("visibilityDistanceType", TargetedDatabaseFlag.SinceWarlordsOfDraenor | TargetedDatabaseFlag.WotlkClassic)]
        public byte? VisibilityDistanceType;

        [DBFieldName("auras")]
        public string Auras;
    }
}
