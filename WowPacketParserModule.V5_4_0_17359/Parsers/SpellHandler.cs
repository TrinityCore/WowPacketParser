using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            var count = packet.ReadBits("Spell Count", 22);
            packet.ReadBit("Unk Bit");

            var spells = new List<uint>((int)count);
            for (var i = 0; i < count; i++)
            {
                var spellId = packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", i);
                spells.Add((uint)spellId);
            }

            var startSpell = new StartSpell { Spells = spells };

            WoWObject character;
            if (Storage.Objects.TryGetValue(CoreParsers.SessionHandler.LoginGuid, out character))
            {
                var player = character as Player;
                if (player != null && player.FirstLogin)
                    Storage.StartSpells.Add(new Tuple<Race, Class>(player.Race, player.Class), startSpell, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_LEARNED_SPELL)]
        public static void HandleLearnSpell(Packet packet)
        {
            packet.ReadBits("Unk Bits", 22);

            var count = packet.ReadBit("Spell Count");

            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.SMSG_REMOVED_SPELL)]
        public static void HandleRemovedSpell(Packet packet)
        {
            var count = packet.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.SMSG_SPELL_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Category Cooldown");
                packet.ReadInt32("Cooldown");
            }
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadByte("Unk Int8");
                packet.ReadInt32("Unk Int32");
            }
        }
    }
}
