﻿using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WoWPacketParser.Proto;
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
            PacketQueryCreatureResponse response = packet.Holder.QueryCreatureResponse = new PacketQueryCreatureResponse();
            var entry = packet.ReadEntry("Entry");

            CreatureTemplate creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };
            response.Entry = (uint) entry.Key;

            Bit hasData = packet.ReadBit();
            response.HasData = hasData;
            if (!hasData)
                return; // nothing to do

            packet.ResetBitReader();
            uint titleLen = packet.ReadBits(11);
            uint titleAltLen = packet.ReadBits(11);
            uint cursorNameLen = packet.ReadBits(6);
            creature.RacialLeader = response.Leader = packet.ReadBit("Leader");

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
                        creature.Name = response.Name = name;
                }
                if (stringLens[i][1] > 1)
                {
                    string nameAlt = packet.ReadCString("NameAlt");
                    if (i == 0)
                        creature.FemaleName = response.NameAlt = nameAlt;
                }
            }

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            response.TypeFlags = (uint?)creature.TypeFlags ?? 0;
            creature.TypeFlags2 = response.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2");

            creature.Type = packet.ReadInt32E<CreatureType>("CreatureType");
            response.Type = (int?)creature.Type ?? 0;
            creature.Family = (CreatureFamily) packet.ReadInt32<CreatureFamilyId>("CreatureFamily");
            response.Family = (int?)creature.Family ?? 0;
            creature.Rank = packet.ReadInt32E<CreatureRank>("Classification");
            response.Rank = (int?)creature.Rank ?? 0;

            creature.KillCredits = new uint?[2];
            for (int i = 0; i < 2; ++i)
            {
                creature.KillCredits[i] = packet.ReadUInt32("ProxyCreatureID", i);
                response.KillCredits.Add(creature.KillCredits[i] ?? 0);
            }

            creature.ModelIDs = new uint?[4];
            for (var i = 0; i < 4; ++i)
            {
                creature.ModelIDs[i] = packet.ReadUInt32("CreatureDisplayID", i);
                response.Models.Add(creature.ModelIDs[i] ?? 0);
            }

            creature.HealthModifier = response.HpMod = packet.ReadSingle("HpMulti");
            creature.ManaModifier = response.ManaMod = packet.ReadSingle("EnergyMulti");

            uint questItems = packet.ReadUInt32("QuestItems");
            creature.MovementID = response.MovementId =  packet.ReadUInt32("CreatureMovementInfoID");
            creature.HealthScalingExpansion = packet.ReadUInt32E<ClientType>("HealthScalingExpansion");
            response.HpScalingExp = (uint?) creature.HealthScalingExpansion ?? 0;
            creature.RequiredExpansion = packet.ReadUInt32E<ClientType>("RequiredExpansion");
            response.Expansion = (uint?) creature.RequiredExpansion ?? 0;

            if (titleLen > 1)
                creature.SubName = response.Title = packet.ReadCString("Title");

            if (titleAltLen > 1)
                creature.TitleAlt = response.TitleAlt =  packet.ReadCString("TitleAlt");

            if (cursorNameLen > 1)
                creature.IconName = response.IconName = packet.ReadCString("CursorName");

            for (uint i = 0; i < questItems; ++i)
            {
                CreatureTemplateQuestItem questItem = new CreatureTemplateQuestItem
                {
                    CreatureEntry = (uint)entry.Key,
                    Idx = i,
                    ItemId = packet.ReadUInt32<ItemId>("QuestItem", i)
                };

                Storage.CreatureTemplateQuestItems.Add(questItem, packet.TimeSpan);
                response.QuestItems.Add(questItem.ItemId ?? 0);
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
    }
}
