using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_spell_list")]
    public sealed record CreatureSpellList : IDataModel
    {
        [DBFieldName("Id", true)]
        public int Id;

        [DBFieldName("Position", true)]
        public int Position;

        [DBFieldName("SpellId")]
        public int SpellId;

        [DBFieldName("Flags")]
        public int Flags;

        [DBFieldName("CombatCondition")]
        public int CombatCondition = -1;

        [DBFieldName("TargetId")]
        public int TargetId = 0;

        [DBFieldName("ScriptId")]
        public int ScriptId = 0;

        [DBFieldName("Availability")]
        public int Availability = 100;

        [DBFieldName("Probability")]
        public int Probability = 1;

        [DBFieldName("InitialMin")]
        public int InitialMin = 0;

        [DBFieldName("InitialMax")]
        public int InitialMax = 0;

        [DBFieldName("RepeatMin")]
        public int RepeatMin = 0;

        [DBFieldName("RepeatMax")]
        public int RepeatMax = 0;

        [DBFieldName("Comments")]
        public string Comments = "";

        public static int ConvertDifficultyToIdx(uint? difficultyId)
        {
            switch (difficultyId)
            {
                case null: return 4;
                default: return 5 + (int)difficultyId - 1;
                case 0: return 5; // open world
            }
        }
    }
}
