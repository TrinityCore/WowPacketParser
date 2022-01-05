using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template_spell")]
    public sealed record CreatureTemplateSpell : IDataModel
    {
        [DBFieldName("CreatureID", true)]
        public uint? CreatureID;

        [DBFieldName("Index", true)]
        public byte? Index;

        [DBFieldName("Spell")]
        public uint? Spell;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
