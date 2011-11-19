using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public class UnitTemplate
    {
        public uint Entry;

        public string Name;

        public string SubName;

        public string IconName;

        public CreatureTypeFlag TypeFlags;

        public uint TypeFlags2;

        public CreatureType Type;

        public CreatureFamily Family;

        public CreatureRank Rank;

        public uint KillCredit1;

        public uint KillCredit2;

        public int UnkInt;

        public uint PetSpellData;

        public uint[] DisplayIds;

        public float Modifier1;

        public float Modifier2;

        public bool RacialLeader;

        public uint[] QuestItems;

        public uint MovementId;

        public ClientType Expansion;

        // TypeFlags2? UnkInt?
        public string[] DatabaseStructure = {
                                                "entry", "KillCredit1", "KillCredit2", "modelid1", "modelid2", "modelid3",
                                                "modelid4", "name", "subname", "IconName", "exp", "rank", "family", "type",
                                                "type_flags", "PetSpellDataId", "Health_mod", "Mana_mod", "RacialLeader",
                                                "questItem1", "questItem2", "questItem3", "questItem4", "questItem5",
                                                "questItem6", "movementId"
                                            };
    }
}
