using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("npc_spellclick_spells")]
    public sealed class NpcSpellClick
    {
        [DBFieldName("spell_id")]
        public uint SpellId;

        [DBFieldName("cast_flags")]
        public uint CastFlags;

        [DBFieldName("user_type")]
        public uint UserType;


        public WowGuid CasterGUID;
        public WowGuid TargetGUID;
    }
}
