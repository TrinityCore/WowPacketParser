using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("npc_spellclick_spells")]
    public sealed record NpcSpellClick : IDataModel
    {
        [DBFieldName("npc_entry", true)]
        public uint? Entry;

        [DBFieldName("spell_id", true)]
        public uint? SpellID;

        [DBFieldName("cast_flags")]
        public uint? CastFlags;

        [DBFieldName("user_type")]
        public uint? UserType;


        public WowGuid CasterGUID;
        public WowGuid TargetGUID;
    }
}
