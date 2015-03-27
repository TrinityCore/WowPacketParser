using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("mount")]
    public sealed class Mount
    {
        [DBFieldName("MountTypeId")]
        public uint MountTypeId;
        [DBFieldName("DisplayId")]
        public uint DisplayId;
        [DBFieldName("Flags")]
        public uint Flags;
        [DBFieldName("Name")]
        public string Name;
        [DBFieldName("Description")]
        public string Description;
        [DBFieldName("SourceDescription")]
        public string SourceDescription;
        [DBFieldName("Source")]
        public uint Source;
        [DBFieldName("SpellId")]
        public uint SpellId;
        [DBFieldName("PlayerConditionId")]
        public uint PlayerConditionId;
        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
