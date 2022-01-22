using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template_addon")]
    public sealed record CreatureTemplateAddon : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("path_id")]
        public uint? PathID;

        [DBFieldName("mount")]
        public uint? MountID;

        [DBFieldName("bytes1")]
        public uint? Bytes1;

        [DBFieldName("bytes2")]
        public uint? Bytes2;

        [DBFieldName("emote")]
        public uint? Emote;

        [DBFieldName("aiAnimKit", TargetedDatabase.Legion)]
        public ushort? AIAnimKit;

        [DBFieldName("movementAnimKit", TargetedDatabase.Legion)]
        public ushort? MovementAnimKit;

        [DBFieldName("meleeAnimKit", TargetedDatabase.Legion)]
        public ushort? MeleeAnimKit;

        // visibilityDistanceType exists in all database versions but because UnitFlags2 to detect the value from sniff doesn't exist in earlier client version
        // we pretend the field doesn't exist
        [DBFieldName("visibilityDistanceType", TargetedDatabase.WarlordsOfDraenor)]
        public byte? VisibilityDistanceType;

        [DBFieldName("auras")]
        public string Auras;

        public string CommentAuras;
    }
}
