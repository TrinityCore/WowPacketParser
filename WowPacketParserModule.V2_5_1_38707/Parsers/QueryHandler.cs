using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V2_5_1_38835.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleQueryPlayerNameResponse(Packet packet)
        {
            var hasData = packet.ReadByte("HasData");

            packet.ReadPackedGuid128("Player Guid");

            if (hasData == 0)
            {
                packet.ReadBit("IsDeleted");
                var nameLen = (int)packet.ReadBits(6);

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("Name Declined", count[i], i);

                packet.ReadPackedGuid128("AccountID");
                packet.ReadPackedGuid128("BnetAccountID");
                packet.ReadPackedGuid128("Player Guid");

                packet.ReadUInt64("GuildClubMemberID");
                packet.ReadUInt32("VirtualRealmAddress");

                packet.ReadByteE<Race>("Race");
                packet.ReadByteE<Gender>("Gender");
                packet.ReadByteE<Class>("Class");
                packet.ReadByte("Level");
                packet.ReadByte("UnkBCC");

                packet.ReadWoWString("Name", nameLen);
            }
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
            packet.ReadBit("Civilian");
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
                    string name = packet.ReadDynamicString("Name", stringLens[i][0], i);
                    if (i == 0)
                        creature.Name = name;
                }
                if (stringLens[i][1] > 1)
                {
                    string nameAlt = packet.ReadDynamicString("NameAlt", stringLens[i][1], i);
                    if (i == 0)
                        creature.FemaleName = nameAlt;
                }
            }
            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2");

            creature.Type = packet.ReadInt32E<CreatureType>("CreatureType");
            creature.Family = packet.ReadInt32E<CreatureFamily>("CreatureFamily");
            creature.Rank = packet.ReadInt32E<CreatureRank>("Classification");
            creature.PetSpellDataID = packet.ReadUInt32("PetSpellDataId");

            creature.KillCredits = new uint?[2];
            for (int i = 0; i < 2; ++i)
                creature.KillCredits[i] = (uint)packet.ReadInt32("ProxyCreatureID", i);

            var displayTotalCount = packet.ReadUInt32("DisplayIdCount");
            packet.ReadSingle("TotalProbability");

            for (uint i = 0; i < displayTotalCount; ++i)
            {
                packet.ReadInt32("DisplayId" + i, i);
                packet.ReadSingle("DisplayScale" + i, i);
                packet.ReadSingle("DisplayProbability" + i, i);
            }

            packet.ReadSingle("HpMulti");
            packet.ReadSingle("EnergyMulti");
            uint questItems = packet.ReadUInt32("QuestItems");
            creature.MovementID = (uint)packet.ReadInt32("CreatureMovementInfoID");
            creature.HealthScalingExpansion = packet.ReadInt32E<ClientType>("HealthScalingExpansion");
            creature.RequiredExpansion = packet.ReadInt32E<ClientType>("RequiredExpansion");
            creature.VignetteID = (uint)packet.ReadInt32("VignetteID");
            creature.UnitClass = (uint)packet.ReadInt32E<Class>("UnitClass");
            packet.ReadInt32("CreatureDifficultyID");
            creature.WidgetSetID = packet.ReadInt32("WidgetSetID");
            creature.WidgetSetUnitConditionID = packet.ReadInt32("WidgetSetUnitConditionID");

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
                    ItemId = (uint)packet.ReadInt32<ItemId>("QuestItem", i)
                };

                Storage.CreatureTemplateQuestItems.Add(questItem, packet.TimeSpan);
            }

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.CreatureTemplates.Add(creature.Entry.Value, creature, packet.TimeSpan);

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
                ObjectType = StoreNameType.Unit,
                ID = entry.Key,
                Name = creature.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUERY_PET_NAME_RESPONSE)]
        public static void HandlePetNameQueryResponse(Packet packet)
        {
            packet.ReadPackedGuid128("PetID");

            var hasData = packet.ReadBit("HasData");
            if (!hasData)
                return;

            var len = packet.ReadBits(8);
            packet.ReadBit("HasDeclined");

            const int maxDeclinedNameCases = 5;
            var declinedNameLen = new int[maxDeclinedNameCases];
            for (var i = 0; i < maxDeclinedNameCases; ++i)
                declinedNameLen[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < maxDeclinedNameCases; ++i)
                packet.ReadWoWString("DeclinedNames", declinedNameLen[i], i);

            packet.ReadTime64("Timestamp");
            packet.ReadWoWString("Petname", len);
        }
    }
}