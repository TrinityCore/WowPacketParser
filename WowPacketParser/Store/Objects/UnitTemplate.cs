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
}
