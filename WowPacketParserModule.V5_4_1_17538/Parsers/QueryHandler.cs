using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_1_17538;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {

            var byte20 = packet.ReadByte("byte20");
            packet.ReadUInt32("Realm Id");

            var bits22 = packet.ReadBits(8);
            packet.ReadBit();
            var bits278 = packet.ReadBits(8);

            packet.ReadWoWString("Realmname (without white char)", bits278);
            packet.ReadWoWString("Realmname", bits22);
        }
		
		[HasSniffData]
        [Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var creature = new UnitTemplate();

            var lenS3 = (int)packet.ReadBits(11);
            var qItemCount = packet.ReadBits(22);
            var lenS4 = (int)packet.ReadBits(6);
            creature.RacialLeader = packet.ReadBit("Racial Leader");

			var stringLens = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.ReadBits(11);
                stringLens[i][1] = (int)packet.ReadBits(11);
            }
            
			var lenS5 = (int)packet.ReadBits(11);

            packet.ResetBitReader();

			creature.Family = packet.ReadEnum<CreatureFamily>("Family", TypeCode.Int32);
			
            creature.Expansion = packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);
			
			//packet.ReadCString("string5");
			
            creature.Type = packet.ReadEnum<CreatureType>("Type", TypeCode.Int32);
			
			if (lenS5 > 1)
                creature.SubName = packet.ReadCString("Sub Name");
			
			creature.DisplayIds = new uint[4];
            creature.DisplayIds[0] = packet.ReadUInt32("Display ID 0");
            creature.DisplayIds[3] = packet.ReadUInt32("Display ID 3");
			
			creature.QuestItems = new uint[qItemCount];
            for (var i = 0; i < qItemCount; ++i)
                creature.QuestItems[i] = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Quest Item", i);

            var name = new string[4];
            for (var i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    packet.ReadCString("Female Name", i);
                if (stringLens[i][1] > 1)
                    name[i] = packet.ReadCString("Name", i);
            }
            creature.Name = name[0];

            if (lenS4 > 1)
			    creature.IconName = packet.ReadCString("Icon Name");
			
            creature.TypeFlags = packet.ReadEnum<CreatureTypeFlag>("Type Flags", TypeCode.UInt32);
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum
            
			creature.Modifier1 = packet.ReadSingle("Modifier 1");
            
            creature.Rank = packet.ReadEnum<CreatureRank>("Rank", TypeCode.Int32);
			
            creature.KillCredits = new uint[2];
            for (var i = 0; i < 2; ++i)
                creature.KillCredits[i] = packet.ReadUInt32("Kill Credit", i);

            creature.Modifier2 = packet.ReadSingle("Modifier 2");
            
            creature.MovementId = packet.ReadUInt32("Movement ID");
			
            creature.DisplayIds[1] = packet.ReadUInt32("Display ID 1");
            creature.DisplayIds[2] = packet.ReadUInt32("Display ID 2");

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
