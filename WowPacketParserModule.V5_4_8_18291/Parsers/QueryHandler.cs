using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_8_18291;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class QueryHandler
    {
         [HasSniffData]
[Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");

            var hasData = packet.ReadBit("hasData");

            if (!hasData)
                return; // nothing to do

            var creature = new UnitTemplate();

            uint lengthSubname = packet.ReadBits("Subname length", 11);
            uint qItemCount = packet.ReadBits("itemCount", 22);

            packet.ReadBits(11); // Unk String length. Needs reading somewhere?
			
            var lengthName = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                lengthName[i] = new int[2];
                lengthName[i][0] = (int)packet.ReadBits("Name length female", 11);
                lengthName[i][1] = (int)packet.ReadBits("Name length male", 11);
            }

            creature.RacialLeader = packet.ReadBit("Racial Leader");

            uint lengthIconName = packet.ReadBits(6) ^ 1;

            creature.KillCredits = new uint[2];
            creature.KillCredits[0] = packet.ReadUInt32("Kill Credit 1");
			
            creature.DisplayIds = new uint[4];
            creature.DisplayIds[3] = packet.ReadUInt32("Display Id 4");
            creature.DisplayIds[1] = packet.ReadUInt32("Display Id 2");
			
            creature.Expansion = packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);
			
            creature.Type = packet.ReadEnum<CreatureType>("Type", TypeCode.Int32);
			
            creature.Modifier2 = packet.ReadSingle("Modifier Health");
			
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2");
			
            creature.TypeFlags = packet.ReadEnum<CreatureTypeFlag>("Type Flags", TypeCode.UInt32);
			
            creature.Rank = packet.ReadEnum<CreatureRank>("Rank", TypeCode.Int32);
            creature.MovementId = packet.ReadUInt32("Movement Id");

            var name = new string[8];
            for (var i = 0; i < 4; ++i)
            {
                if (lengthName[i][1] > 1)
                    packet.ReadCString("Male Name", i);

                if (lengthName[i][0] > 1)
                    name[i] = packet.ReadCString("Female name", i);
            }
            creature.Name = name[0];
			
            if (lengthSubname > 1)
                creature.SubName = packet.ReadCString("Sub Name");
				
			
            creature.DisplayIds[0] = packet.ReadUInt32("Display Id 1");	
				
            creature.DisplayIds[2] = packet.ReadUInt32("Display Id 3");
			
            if (lengthIconName > 1)
                creature.IconName = packet.ReadCString("Icon Name");
			
            creature.QuestItems = new uint[qItemCount];
            for (var i = 0; i < qItemCount; ++i)
                creature.QuestItems[i] = (uint)packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Quest Item", i);			
			
            creature.KillCredits[1] = packet.ReadUInt32("Kill Credit 2");

            creature.Modifier1 = packet.ReadSingle("Modifier Mana");

            creature.Family = packet.ReadEnum<CreatureFamily>("Family", TypeCode.Int32);

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
