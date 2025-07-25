﻿using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class QueryHandler
    {
        public static PacketQueryPlayerNameResponse ReadNameCacheLookupResult(Packet packet, params object[] idx)
        {
            var response = new PacketQueryPlayerNameResponse();
            packet.ResetBitReader();
            packet.ReadByte("Result", idx);
            packet.ReadPackedGuid128("Player", idx);
            var hasPlayerGuidLookupData = packet.ReadBit("HasPlayerGuidLookupData", idx);
            var hasThingy = packet.ReadBit("HasNameCacheUnused920", idx);

            if (hasPlayerGuidLookupData)
            {
                var data = CharacterHandler.ReadPlayerGuidLookupData(packet, idx, "PlayerGuidLookupData");
                response.Race = (uint)data.Race;
                response.Gender = (uint)data.Gender;
                response.Class = (uint)data.Class;
                response.Level = data.Level;
                response.HasData = true;
            }

            if (hasThingy)
            {
                packet.ResetBitReader();
                packet.ReadUInt32("Unused1", idx, "NameCacheUnused920");
                packet.ReadPackedGuid128("Unused2", idx, "NameCacheUnused920");
                var length = packet.ReadBits(7);
                packet.ReadWoWString("Unused3", length, idx, "NameCacheUnused920");
            }

            return response;
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAMES_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var response = packet.Holder.QueryPlayerNameResponse = new();

            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
                response.Responses.Add(ReadNameCacheLookupResult(packet, i));
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
            response.Entry = (uint)entry.Key;

            Bit hasData = packet.ReadBit();
            response.HasData = hasData;
            if (!hasData)
                return; // nothing to do

            packet.ResetBitReader();
            uint titleLen = packet.ReadBits(11);
            uint titleAltLen = packet.ReadBits(11);
            uint cursorNameLen = packet.ReadBits(6);
            creature.Civilian = packet.ReadBit("Civilian");
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
                    string name = packet.ReadDynamicString("Name", stringLens[i][0], i);
                    if (i == 0)
                        creature.Name = response.Name = name;
                }
                if (stringLens[i][1] > 1)
                {
                    string nameAlt = packet.ReadDynamicString("NameAlt", stringLens[i][1], i);
                    if (i == 0)
                        creature.FemaleName = response.NameAlt = nameAlt;
                }
            }

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            response.TypeFlags = (uint?)creature.TypeFlags ?? 0;
            creature.TypeFlags2 = response.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2");
            packet.ReadUInt32("Creature Type Flags 3");

            creature.Type = packet.ReadByteE<CreatureType>("CreatureType");
            creature.Family = packet.ReadInt32E<CreatureFamily>("CreatureFamily");
            creature.Rank = packet.ReadSByteE<CreatureRank>("Classification");
            creature.PetSpellDataID = packet.ReadUInt32("PetSpellDataId");
            response.Type = (int?)creature.Type ?? 0;
            response.Family = (int?)creature.Family ?? 0;
            response.Rank = (int?)creature.Rank ?? 0;

            creature.KillCredits = new uint?[2];
            for (int i = 0; i < 2; ++i)
            {
                creature.KillCredits[i] = (uint)packet.ReadInt32("ProxyCreatureID", i);
                response.KillCredits.Add(creature.KillCredits[i] ?? 0);
            }

            var displayIdCount = packet.ReadUInt32("DisplayIdCount");
            packet.ReadSingle("TotalProbability");

            for (var i = 0; i < displayIdCount; ++i)
            {
                CreatureTemplateModel model = new CreatureTemplateModel
                {
                    CreatureID = (uint)entry.Key,
                    Idx = (uint)i
                };

                model.CreatureDisplayID = (uint)packet.ReadInt32("CreatureDisplayID", i);
                model.DisplayScale = packet.ReadSingle("DisplayScale", i);
                model.Probability = packet.ReadSingle("Probability", i);

                response.Models.Add(model.CreatureDisplayID ?? 0);
                Storage.CreatureTemplateModels.Add(model, packet.TimeSpan);
            }

            creature.HealthModifier = response.HpMod = packet.ReadSingle("HpMulti");
            creature.ManaModifier = response.ManaMod = packet.ReadSingle("EnergyMulti");
            uint questItems = packet.ReadUInt32("QuestItems");
            uint questCurrencies = packet.ReadUInt32("QuestCurrencies");
            creature.MovementID = response.MovementId = (uint)packet.ReadInt32("CreatureMovementInfoID");
            creature.HealthScalingExpansion = packet.ReadInt32E<ClientType>("HealthScalingExpansion");
            response.HpScalingExp = (uint?)creature.HealthScalingExpansion ?? 0;
            creature.RequiredExpansion = packet.ReadInt32E<ClientType>("RequiredExpansion");
            response.Expansion = (uint?)creature.RequiredExpansion ?? 0;
            creature.VignetteID = (uint)packet.ReadInt32("VignetteID");
            creature.UnitClass = (uint)packet.ReadInt32E<Class>("UnitClass");
            creature.CreatureDifficultyID = packet.ReadInt32("CreatureDifficultyID");
            creature.WidgetSetID = packet.ReadInt32("WidgetSetID");
            creature.WidgetSetUnitConditionID = packet.ReadInt32("WidgetSetUnitConditionID");

            if (titleLen > 1)
                creature.SubName = response.Title = packet.ReadCString("Title");

            if (titleAltLen > 1)
                creature.TitleAlt = response.TitleAlt = packet.ReadCString("TitleAlt");

            if (cursorNameLen > 1)
                creature.IconName = response.IconName = packet.ReadCString("CursorName");

            for (uint i = 0; i < questItems; ++i)
            {
                CreatureTemplateQuestItem questItem = new CreatureTemplateQuestItem
                {
                    CreatureEntry = (uint)entry.Key,
                    Idx = i,
                    ItemId = (uint)packet.ReadInt32<ItemId>("QuestItem", i)
                };

                questItem.DifficultyID = WowPacketParser.Parsing.Parsers.MovementHandler.CurrentDifficultyID;

                Storage.CreatureTemplateQuestItems.Add(questItem, packet.TimeSpan);
                response.QuestItems.Add(questItem.ItemId ?? 0);
            }

            for (uint i = 0; i < questCurrencies; ++i)
            {
                CreatureTemplateQuestCurrency questCurrency = new CreatureTemplateQuestCurrency
                {
                    CreatureId = (uint)entry.Key,
                    CurrencyId = packet.ReadInt32<CurrencyId>("QuestCurrency", i)
                };

                Storage.CreatureTemplateQuestCurrencies.Add(questCurrency, packet.TimeSpan);
                response.QuestCurrencies.Add(questCurrency.CurrencyId ?? 0);
            }

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

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

            Storage.CreatureTemplates.Add(creature.Entry.Value, creature, packet.TimeSpan);

            CreatureTemplateDifficultyWDB creatureTemplateDifficultyWDB = new CreatureTemplateDifficultyWDB
            {
                Entry = creature.Entry,
                DifficultyID = WowPacketParser.Parsing.Parsers.MovementHandler.CurrentDifficultyID,
                HealthScalingExpansion = creature.HealthScalingExpansion,
                HealthModifier = creature.HealthModifier,
                ManaModifier = creature.ManaModifier,
                CreatureDifficultyID = creature.CreatureDifficultyID,
                TypeFlags = creature.TypeFlags,
                TypeFlags2 = creature.TypeFlags2
            };
            creatureTemplateDifficultyWDB = WowPacketParser.SQL.SQLDatabase.CheckCreatureTemplateDifficultyWDBFallbacks(creatureTemplateDifficultyWDB, creatureTemplateDifficultyWDB.DifficultyID);
            Storage.CreatureTemplateDifficultiesWDB.Add(creatureTemplateDifficultyWDB);

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
