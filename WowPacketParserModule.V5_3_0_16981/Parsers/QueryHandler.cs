using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_CREATURE_QUERY)]
        public static void HandleCreatureQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            if (entry.Value)
                return;

            var creature = new UnitTemplate();

            var hasUnk1 = packet.ReadBit("Has unk 1");
            var stringLens = new int[4][];
            int lenS3 = 0;
            int lenS4 = 0;
            int lenS5 = 0;
            uint qItemCount = 0;

            if (hasUnk1)
            {
                lenS3 = (int)packet.ReadBits(11);
                creature.RacialLeader = packet.ReadBit("Racial Leader");

                stringLens = new int[4][];
                for (var i = 0; i < 4; i++)
                {
                    stringLens[i] = new int[2];
                    stringLens[i][0] = (int)packet.ReadBits(11);
                    stringLens[i][1] = (int)packet.ReadBits(11);
                }

                lenS4 = (int)packet.ReadBits(6);
                lenS5 = (int)packet.ReadBits(11);
                qItemCount = packet.ReadBits(22);
            }

            packet.ResetBitReader();

            if (hasUnk1)
            {
                for (var i = 0; i < 4; ++i)
                {
                    if (stringLens[i][0] > 1)
                        packet.ReadWoWString("string1", stringLens[i][0], i);
                    if (stringLens[i][1] > 1)
                        packet.ReadWoWString("string2", stringLens[i][1], i);
                }
                creature.Modifier1 = packet.ReadSingle("Modifier 1");
                if (lenS3 > 1)
                    creature.SubName = packet.ReadWoWString("Sub Name", lenS3);

                creature.Rank = packet.ReadEnum<CreatureRank>("Rank", TypeCode.Int32);

                creature.QuestItems = new uint[qItemCount];
                for (var i = 0; i < qItemCount; ++i)
                    creature.QuestItems[i] = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Quest Item", i);

                creature.Type = packet.ReadEnum<CreatureType>("Type", TypeCode.Int32);
                packet.ReadInt32("int27");
                packet.ReadInt32("int28");
                packet.ReadInt32("int13");
                if (lenS4 > 1)
                    packet.ReadWoWString("string4", lenS4);

                creature.DisplayIds = new uint[4];
                creature.DisplayIds[1] = packet.ReadUInt32("Display ID 1");
                creature.DisplayIds[0] = packet.ReadUInt32("Display ID 0");
                creature.Family = packet.ReadEnum<CreatureFamily>("Family", TypeCode.Int32);
                creature.DisplayIds[3] = packet.ReadUInt32("Display ID 3");

                creature.KillCredits = new uint[2];
                for (var i = 0; i < 2; ++i)
                    creature.KillCredits[i] = packet.ReadUInt32("Kill Credit", i);

                if (lenS5 > 1)
                    packet.ReadWoWString("string5", lenS5);
                creature.DisplayIds[2] = packet.ReadUInt32("Display ID 2");
                creature.Modifier2 = packet.ReadSingle("Modifier 2");
                creature.Expansion = packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);
            }

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.UnitTemplates.Add((uint)entry.Key, creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                Name = creature.Name,
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_NPC_TEXT_QUERY)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");

            var GUID = new byte[8];
            GUID = packet.StartBitStream(7, 1, 4, 3, 0, 2, 6, 5);
            packet.ParseBitStream(GUID, 4, 5, 6, 7, 1, 0, 2, 3);
            packet.WriteGuid("GUID", GUID);
        }

    }
}
