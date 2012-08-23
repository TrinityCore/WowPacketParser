using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template")]
    public class UnitTemplate
    {
        [DBFieldName("name")]
        public string Name;

        [DBFieldName("subname")]
        public string SubName;

        [DBFieldName("IconName")]
        public string IconName;

        [DBFieldName("type_flags")]
        public CreatureTypeFlag TypeFlags;

        [DBFieldName("type_flags2")]
        public uint TypeFlags2;

        [DBFieldName("type")]
        public CreatureType Type;

        [DBFieldName("family")]
        public CreatureFamily Family;

        [DBFieldName("rank")]
        public CreatureRank Rank;

        [DBFieldName("KillCredit1")]
        public uint KillCredit1;

        [DBFieldName("KillCredit2")]
        public uint KillCredit2;

        public int UnkInt; // pre 3.1

        //[DBFieldName("PetSpellDataId")]
        public uint PetSpellData;

        [DBFieldName("modelid", Count = 4)]
        public uint[] DisplayIds;

        [DBFieldName("Health_mod")]
        public float Modifier1;

        [DBFieldName("Mana_mod")]
        public float Modifier2;

        [DBFieldName("RacialLeader")]
        public bool RacialLeader;

        [DBFieldName("questItem", Count = 6)]
        public uint[] QuestItems;

        [DBFieldName("movementId")]
        public uint MovementId;

        [DBFieldName("exp_unk")]
        public ClientType Expansion;

        [DBFieldName("WDBVerified")]
        public int WDBVerified;
    }
}
