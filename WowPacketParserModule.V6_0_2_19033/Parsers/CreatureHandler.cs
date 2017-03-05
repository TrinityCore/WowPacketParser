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
            packet.Translator.ReadInt32("Entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_CREATURE_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Entry");

            CreatureTemplate creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };

            Bit hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            packet.Translator.ResetBitReader();
            uint titleLen = packet.Translator.ReadBits(11);
            uint titleAltLen = packet.Translator.ReadBits(11);
            uint cursorNameLen = packet.Translator.ReadBits(6);
            creature.RacialLeader = packet.Translator.ReadBit("Leader");

            var stringLens = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.Translator.ReadBits(11);
                stringLens[i][1] = (int)packet.Translator.ReadBits(11);
            }

            for (var i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    creature.Name = packet.Translator.ReadCString("Name");
                if (stringLens[i][1] > 1)
                    creature.FemaleName = packet.Translator.ReadCString("NameAlt");
            }

            creature.TypeFlags = packet.Translator.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.Translator.ReadUInt32("Creature Type Flags 2");

            creature.Type = packet.Translator.ReadInt32E<CreatureType>("CreatureType");
            creature.Family = packet.Translator.ReadInt32E<CreatureFamily>("CreatureFamily");
            creature.Rank = packet.Translator.ReadInt32E<CreatureRank>("Classification");

            creature.KillCredits = new uint?[2];
            for (int i = 0; i < 2; ++i)
                creature.KillCredits[i] = packet.Translator.ReadUInt32("ProxyCreatureID", i);

            creature.ModelIDs = new uint?[4];
            for (int i = 0; i < 4; ++i)
                creature.ModelIDs[i] = packet.Translator.ReadUInt32("CreatureDisplayID", i);

            creature.HealthModifier = packet.Translator.ReadSingle("HpMulti");
            creature.ManaModifier = packet.Translator.ReadSingle("EnergyMulti");

            uint questItems = packet.Translator.ReadUInt32("QuestItems");
            creature.MovementID = packet.Translator.ReadUInt32("CreatureMovementInfoID");
            creature.HealthScalingExpansion = packet.Translator.ReadUInt32E<ClientType>("HealthScalingExpansion");
            creature.RequiredExpansion = packet.Translator.ReadUInt32E<ClientType>("RequiredExpansion");

            if (titleLen > 1)
                creature.SubName = packet.Translator.ReadCString("Title");

            if (titleAltLen > 1)
                packet.Translator.ReadCString("TitleAlt");

            if (cursorNameLen > 1)
                creature.IconName = packet.Translator.ReadCString("CursorName");

            for (uint i = 0; i < questItems; ++i)
            {
                CreatureTemplateQuestItem questItem = new CreatureTemplateQuestItem
                {
                    CreatureEntry = (uint)entry.Key,
                    Idx = i,
                    ItemId = packet.Translator.ReadUInt32<ItemId>("QuestItem", i)
                };

                Storage.CreatureTemplateQuestItems.Add(questItem, packet.TimeSpan);
            }

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.CreatureTemplates.Add(creature, packet.TimeSpan);

            ObjectName objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                ID = entry.Key,
                Name = creature.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
        }
    }
}
