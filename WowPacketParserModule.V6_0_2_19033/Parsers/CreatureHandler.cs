using WowPacketParser.Enums;
using WowPacketParser.Loading;
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

            CreatureTemplate creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };

            Bit hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            packet.ResetBitReader();
            uint titleLen = packet.ReadBits(11);
            uint titleAltLen = packet.ReadBits(11);
            uint cursorNameLen = packet.ReadBits(6);
            creature.RacialLeader = packet.ReadBit("Leader");

            var stringLens = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.ReadBits(11);
                stringLens[i][1] = (int)packet.ReadBits(11);
            }

            for (var i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                {
                    string name = packet.ReadCString("Name");
                    if (i == 0)
                        creature.Name = name;
                }
                if (stringLens[i][1] > 1)
                {
                    string nameAlt = packet.ReadCString("NameAlt");
                    if (i == 0)
                        creature.FemaleName = nameAlt;
                }
            }

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2");

            creature.Type = packet.ReadInt32E<CreatureType>("CreatureType");
            creature.Family = (CreatureFamily) packet.ReadInt32<CreatureFamilyId>("CreatureFamily");
            creature.Rank = packet.ReadInt32E<CreatureRank>("Classification");

            creature.KillCredits = new uint?[2];
            for (int i = 0; i < 2; ++i)
                creature.KillCredits[i] = packet.ReadUInt32("ProxyCreatureID", i);

            creature.ModelIDs = new uint?[4];
            for (var i = 0; i < 4; ++i)
                creature.ModelIDs[i] = packet.ReadUInt32("CreatureDisplayID", i);

            creature.HealthModifier = packet.ReadSingle("HpMulti");
            creature.ManaModifier = packet.ReadSingle("EnergyMulti");

            uint questItems = packet.ReadUInt32("QuestItems");
            creature.MovementID = packet.ReadUInt32("CreatureMovementInfoID");
            creature.HealthScalingExpansion = packet.ReadUInt32E<ClientType>("HealthScalingExpansion");
            creature.RequiredExpansion = packet.ReadUInt32E<ClientType>("RequiredExpansion");

            if (titleLen > 1)
                creature.SubName = packet.ReadCString("Title");

            if (titleAltLen > 1)
                creature.TitleAlt = packet.ReadCString("TitleAlt");

            if (cursorNameLen > 1)
                creature.IconName = packet.ReadCString("CursorName");

            for (uint i = 0; i < questItems; ++i)
            {
                CreatureTemplateQuestItem questItem = new CreatureTemplateQuestItem
                {
                    CreatureEntry = (uint)entry.Key,
                    Idx = i,
                    ItemId = packet.ReadUInt32<ItemId>("QuestItem", i)
                };

                Storage.CreatureTemplateQuestItems.Add(questItem, packet.TimeSpan);
            }

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.CreatureTemplates.Add(creature, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CreatureTemplateLocale localesCreature = new CreatureTemplateLocale
                {
                    ID = (uint)entry.Key,
                    Name = creature.Name,
                    NameAlt = creature.FemaleName,
                    Title = creature.SubName,
                    TitleAlt = creature.TitleAlt
                };

                Storage.LocalesCreatures.Add(localesCreature, packet.TimeSpan);
            }

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
