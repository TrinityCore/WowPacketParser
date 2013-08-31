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
                var name = new string[8];
                for (var i = 0; i < 4; ++i)
                {
                    if (stringLens[i][0] > 1)
                        packet.ReadCString("Female Name", i);
                    if (stringLens[i][1] > 1)
                        name[i] = packet.ReadCString("Name", i);
                }
                creature.Name = name[0];

                creature.Modifier1 = packet.ReadSingle("Modifier 1");
                if (lenS3 > 1)
                    creature.SubName = packet.ReadCString("Sub Name");

                creature.Rank = packet.ReadEnum<CreatureRank>("Rank", TypeCode.Int32);

                creature.QuestItems = new uint[qItemCount];
                for (var i = 0; i < qItemCount; ++i)
                    creature.QuestItems[i] = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Quest Item", i);

                creature.Type = packet.ReadEnum<CreatureType>("Type", TypeCode.Int32);
                creature.KillCredits = new uint[2];
                for (var i = 0; i < 2; ++i)
                    creature.KillCredits[i] = packet.ReadUInt32("Kill Credit", i);
                creature.Family = packet.ReadEnum<CreatureFamily>("Family", TypeCode.Int32);
                if (lenS4 > 1)
                    creature.IconName = packet.ReadCString("Icon Name");

                creature.DisplayIds = new uint[4];
                creature.DisplayIds[1] = packet.ReadUInt32("Display ID 1");
                creature.DisplayIds[0] = packet.ReadUInt32("Display ID 0");
                creature.MovementId = packet.ReadUInt32("Movement ID");
                creature.DisplayIds[3] = packet.ReadUInt32("Display ID 3");

                creature.TypeFlags = packet.ReadEnum<CreatureTypeFlag>("Type Flags", TypeCode.UInt32);
                creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

                if (lenS5 > 1)
                    packet.ReadCString("string5");
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

        [Parser(Opcode.CMSG_QUERY_PLAYER_NAME)]
        public static void HandlePlayerQueryName(Packet packet)
        {
            var guid = new byte[8];

            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var bit16 = packet.ReadBit("bit16");
            guid[6] = packet.ReadBit();
            var bit24 = packet.ReadBit("bit24");

            packet.ParseBitStream(guid, 6, 0, 2, 3, 4, 5, 7, 1);

            if (bit24)
                packet.ReadUInt32("unk28");

            if (bit16)
                packet.ReadUInt32("unk20");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            UInt32 realmId = packet.ReadUInt32();
            var byte20 = packet.ReadByte("byte20");

            var bits22 = packet.ReadBits(8);
            var bits278 = packet.ReadBits(8);
            packet.ReadBit();

            packet.ReadWoWString("Realmname", bits278);
            packet.ReadWoWString("Realmname2", bits22);
        }

        [Parser(Opcode.SMSG_NAME_QUERY_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var guid = new byte[8];

            var bit16 = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            if (bit16)
            {
                for (var i = 0; i < 5; ++i)
                {
                    var counter = packet.ReadBits(7);
                }
            }
            var bits32 = packet.ReadBits(6);
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var bit83 = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            packet.ReadXORByte(guid, 1);
            packet.ReadWoWString("Name: ", bits32);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);

            packet.ReadByte("Race");
            packet.ReadByte("unk81");
            packet.ReadByte("Gender");

            if (bit16)
            {
                for (var i = 0; i < 5; ++i)
                {
                    packet.ReadCString("Declined Name");
                }
            }
            packet.ReadByte("Class");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadUInt32("unk84");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }
    }
}
