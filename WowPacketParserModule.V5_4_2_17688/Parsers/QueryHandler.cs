using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_2_17688;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_2_17688.Parsers
{
    public static class QueryHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            //var entry = packet.ReadEntry("Entry");
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var creature = new UnitTemplate();

            creature.RacialLeader = packet.ReadBit("Racial Leader");
            var iconLenS = (int)packet.ReadBits(6);
            var unkLens = (int)packet.ReadBits(11);
            var qItemCount = packet.ReadBits(22);

            var stringLens = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.ReadBits(11);
                stringLens[i][1] = (int)packet.ReadBits(11);
            }

            var subLenS = (int)packet.ReadBits(11);

            packet.ResetBitReader();

            creature.DisplayIds = new uint[4];
            creature.DisplayIds[2] = packet.ReadUInt32("Display ID 2");
            creature.KillCredits = new uint[2];
            creature.KillCredits[1] = packet.ReadUInt32("KillCredit 2");
            creature.Type = packet.ReadEnum<CreatureType>("Type", TypeCode.Int32);

            var name = new string[4];
            for (var i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    packet.ReadCString("Female Name", i);
                if (stringLens[i][1] > 1)
                    name[i] = packet.ReadCString("Name", i);
            }
            creature.Name = name[0];

            creature.Modifier2 = packet.ReadSingle("Modifier 2");
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum
            creature.TypeFlags = packet.ReadEnum<CreatureTypeFlag>("Type Flags", TypeCode.UInt32);
            creature.Family = packet.ReadEnum<CreatureFamily>("Family", TypeCode.Int32);
            creature.KillCredits[0] = packet.ReadUInt32("KillCredit 1");
            creature.DisplayIds[3] = packet.ReadUInt32("Display ID 3");

            creature.QuestItems = new uint[qItemCount];
            for (var i = 0; i < qItemCount; ++i)
                creature.QuestItems[i] = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Quest Item", i);

            creature.Modifier1 = packet.ReadSingle("Modifier 1");
            creature.MovementId = packet.ReadUInt32("Movement ID");
            creature.Expansion = packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);

            if (iconLenS > 1)
                creature.IconName = packet.ReadCString("Icon Name");

            creature.DisplayIds[1] = packet.ReadUInt32("Display ID 1");
            creature.DisplayIds[0] = packet.ReadUInt32("Display ID 0");
            creature.Rank = packet.ReadEnum<CreatureRank>("Rank", TypeCode.Int32);

            if (subLenS > 1)
                creature.SubName = packet.ReadCString("Sub Name");

            var entry = packet.ReadEntry("Entry");

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.UnitTemplates.Add((uint)entry.Key, creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                Name = creature.Name,
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }
        
        [HasSniffData]
        [Parser(Opcode.SMSG_NPC_TEXT_UPDATE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcText();
            
            var hasData = packet.ReadBit("hasData");
            var entry = packet.ReadEntry("TextID");
            if (entry.Value) // Can be masked
                return;

            if (!hasData)
                return; // nothing to do

            var size = packet.ReadInt32("Size");

            npcText.Probabilities = new float[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = packet.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                packet.ReadInt32("Unknown Id", i);
        } 
    }
}
