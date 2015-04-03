using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CreatureHandler
    {
        [Parser(Opcode.CMSG_QUERY_CREATURE)]
        public static void HandleCreatureQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_CREATURE_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");

            var creature = new UnitTemplate();
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            packet.ResetBitReader();
            var bits4 = packet.ReadBits(11);
            var bits16 = packet.ReadBits(11);
            var bits28 = packet.ReadBits(6);
            creature.RacialLeader = packet.ReadBit("Leader");

            var stringLens = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.ReadBits(11);
                stringLens[i][1] = (int)packet.ReadBits(11);
            }

            for (var i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    creature.Name = packet.ReadCString("Name");
                if (stringLens[i][1] > 1)
                    creature.FemaleName = packet.ReadCString("NameAlt");
            }

            //for (var i = 0; i < 2; ++i)
            //{
            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2");
            //}

            creature.Type = packet.ReadInt32E<CreatureType>("CreatureType");
            creature.Family = packet.ReadInt32E<CreatureFamily>("CreatureFamily");
            creature.Rank = packet.ReadInt32E<CreatureRank>("Classification");

            creature.KillCredits = new uint[2];
            for (var i = 0; i < 2; ++i)
                creature.KillCredits[i] = packet.ReadUInt32("ProxyCreatureID", i);

            creature.DisplayIds = new uint[4];
            for (var i = 0; i < 4; ++i)
                creature.DisplayIds[i] = packet.ReadUInt32("CreatureDisplayID", i);

            creature.Modifier1 = packet.ReadSingle("HpMulti");
            creature.Modifier2 = packet.ReadSingle("EnergyMulti");

            creature.QuestItems = new uint[6];
            var questItems = packet.ReadInt32("QuestItems");
            creature.MovementId = packet.ReadUInt32("CreatureMovementInfoID");
            creature.Expansion = packet.ReadUInt32E<ClientType>("RequiredExpansion");
            packet.ReadInt32("FlagQuest");

            if (bits4 > 1)
                creature.SubName = packet.ReadCString("Title");

            if (bits16 > 1)
                packet.ReadCString("TitleAlt");

            if (bits28 > 1)
                creature.IconName = packet.ReadCString("CursorName");

            for (var i = 0; i < questItems; ++i)
                creature.QuestItems[i] = (uint)packet.ReadInt32<ItemId>("Quest Item", i);

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.UnitTemplates.Add((uint)entry.Key, creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                Name = creature.Name
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }
    }
}