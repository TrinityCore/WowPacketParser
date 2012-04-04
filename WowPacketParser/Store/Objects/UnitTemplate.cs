using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public class UnitTemplate
    {
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
    }

    // TODO: Use the existing UnitTemplate +
    // attributes specifying the DB field names
    public struct UnitTemplateDb
    {
        // ReSharper disable InconsistentNaming, NotAccessedField.Global
        public string name;
        public string subname;
        public string IconName;
        public uint type_flags;
        public uint type;
        public int family;
        public uint rank;
        public uint KillCredit1;
        public uint KillCredit2;
        public uint PetSpellDataId;
        public uint modelid1;
        public uint modelid2;
        public uint modelid3;
        public uint modelid4;
        public float Health_mod;
        public float Mana_mod;
        public uint RacialLeader;
        public uint questItem1;
        public uint questItem2;
        public uint questItem3;
        public uint questItem4;
        public uint questItem5;
        public uint questItem6;
        public uint movementId;
        // ReSharper restore InconsistentNaming, NotAccessedField.Global
    }
}
