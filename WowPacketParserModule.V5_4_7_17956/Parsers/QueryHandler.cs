using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_7_17956.Enums;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_7_17956.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_CREATURE_QUERY, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_CREATURE_QUERY, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleCreatureQuery(Packet packet)
        {
            packet.ReadUInt32("Entry");
        }

        [Parser(Opcode.CMSG_NAME_QUERY, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_NAME_QUERY, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleNameQuery(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.ReadBit();

            var bit20 = packet.ReadBit();

            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            var bit28 = packet.ReadBit();

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);

            if (bit28)
                packet.ReadUInt32("Unk 1");

            if (bit20)
                packet.ReadUInt32("Unk 1");

            packet.WriteGuid("Target Guid", guid);
        }

        [Parser(Opcode.CMSG_NPC_TEXT_QUERY, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_NPC_TEXT_QUERY, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.ReadUInt32("Text Id");

            var guid = packet.StartBitStream(0, 1, 2, 6, 4, 3, 7, 5);
            packet.ParseBitStream(guid, 3, 1, 4, 6, 2, 0, 5, 7);

            packet.WriteGuid("NPC Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");

            var hasData = packet.ReadBit("hasData");

            if (!hasData)
                return; // nothing to do

            var creature = new UnitTemplate();

            creature.RacialLeader = packet.ReadBit("Racial Leader");

            uint lengthIconName = packet.ReadBits(6) ^ 1;

            var lengthName = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                lengthName[i] = new int[2];
                lengthName[i][0] = (int)packet.ReadBits("Name length female", 11);
                lengthName[i][1] = (int)packet.ReadBits("Name length male", 11);
            }

            uint qItemCount = packet.ReadBits("itemCount", 22);
            uint lengthSubname = packet.ReadBits("Subname length", 11);

            packet.ReadBits(11); // Unk String length. Needs reading somewhere?

            creature.Modifier1 = packet.ReadSingle("Modifier Mana");

            var name = new string[8];
            for (var i = 0; i < 4; ++i)
            {
                if (lengthName[i][1] > 1)
                    packet.ReadCString("Male Name", i);

                if (lengthName[i][0] > 1)
                    name[i] = packet.ReadCString("Female name", i);
            }
            creature.Name = name[0];

            creature.Modifier2 = packet.ReadSingle("Modifier Health");

            creature.KillCredits = new uint[2];
            creature.KillCredits[1] = packet.ReadUInt32("Kill Credit 2");

            creature.DisplayIds = new uint[4];
            creature.DisplayIds[1] = packet.ReadUInt32("Display Id 2");

            creature.QuestItems = new uint[qItemCount];
            for (var i = 0; i < qItemCount; ++i)
                creature.QuestItems[i] = (uint)packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Quest Item", i);

            creature.Type = packet.ReadEnum<CreatureType>("Type", TypeCode.Int32);

            if (lengthIconName > 1)
                creature.IconName = packet.ReadCString("Icon Name");

            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2");

            creature.TypeFlags = packet.ReadEnum<CreatureTypeFlag>("Type Flags", TypeCode.UInt32);

            creature.KillCredits[0] = packet.ReadUInt32("Kill Credit 1");

            creature.Family = packet.ReadEnum<CreatureFamily>("Family", TypeCode.Int32);

            creature.MovementId = packet.ReadUInt32("Movement Id");

            creature.Expansion = packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);

            creature.DisplayIds[0] = packet.ReadUInt32("Display Id 1");
            creature.DisplayIds[2] = packet.ReadUInt32("Display Id 3");

            creature.Rank = packet.ReadEnum<CreatureRank>("Rank", TypeCode.Int32);

            if (lengthSubname > 1)
                creature.SubName = packet.ReadCString("Sub Name");

            creature.DisplayIds[3] = packet.ReadUInt32("Display Id 4");

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.UnitTemplates.Add((uint)entry.Key, creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                Name = creature.Name,
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }
    }
}
