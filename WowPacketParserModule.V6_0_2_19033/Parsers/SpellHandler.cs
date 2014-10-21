using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParser.V6_0_2_19033.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_LEARNED_SPELL)]
        public static void HandleLearnSpell(Packet packet)
        {
            var count = packet.ReadUInt32("Spell Count");

            for (var i = 0; i < count; ++i)
                packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID", i);
            packet.ReadBit("Unk Bits");
        }

        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.ReadBit("Unk Bit");
            var count = packet.ReadUInt32("Spell Count");

            var spells = new List<uint>((int)count);
            for (var i = 0; i < count; i++)
            {
                var spellId = packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID", i);
                spells.Add((uint)spellId);
            }
            // Fix me
            /*
            var startSpell = new StartSpell { Spells = spells };

            WoWObject character;
            if (Storage.Objects.TryGetValue(CoreParsers.SessionHandler.LoginGuid, out character))
            {
                var player = character as Player;
                if (player != null && player.FirstLogin)
                    Storage.StartSpells.Add(new Tuple<Race, Class>(player.Race, player.Class), startSpell, packet.TimeSpan);
            }*/
        }

        [Parser(Opcode.SMSG_SPELL_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.ReadUInt32("Spell Count");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Cooldown", i);
                packet.ReadInt32("Category Cooldown", i);
            }
        }

        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifierFlat(Packet packet)
        {
            var modCount = packet.ReadUInt32("Modifier type count");
            var modTypeCount = new uint[modCount];

            for (var j = 0; j < modCount; ++j)
            {
                packet.ReadEnum<SpellModOp>("Spell Mod", TypeCode.Byte, j);

                modTypeCount[j] = packet.ReadUInt32("Count", j);
            }

            for (var j = 0; j < modCount; ++j)
            {
                for (var i = 0; i < modTypeCount[j]; ++i)
                {
                    packet.ReadSingle("Amount", j, i);
                    packet.ReadByte("Spell Mask bitpos", j, i);
                }
            }
        }

        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifierPct(Packet packet)
        {
            var modCount = packet.ReadUInt32("Modifier type count");
            var modTypeCount = new uint[modCount];

            for (var j = 0; j < modCount; ++j)
            {
                packet.ReadEnum<SpellModOp>("Spell Mod", TypeCode.Byte, j);

                modTypeCount[j] = packet.ReadUInt32("Count", j);
            }

            for (var j = 0; j < modCount; ++j)
            {
                for (var i = 0; i < modTypeCount[j]; ++i)
                {
                    packet.ReadSingle("Amount", j, i);
                    packet.ReadByte("Spell Mask bitpos", j, i);
                }
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            packet.ReadBit("bit16");
            packet.ReadPackedGuid128("Guid");
            var count = packet.ReadUInt32("AuraCount");
            
            var auras = new List<Aura>();
            for (var i = 0; i < count; ++i)
            {
                var aura = new Aura();

                packet.ReadByte("Slot", i);

                packet.ResetBitReader();
                var hasAura = packet.ReadBit("HasAura");
                if (hasAura)
                {
                    var id = packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID", i);
                    aura.AuraFlags = packet.ReadEnum<AuraFlagMoP>("Flags", TypeCode.Byte, i);
                    packet.ReadInt32("Effect Mask", i);
                    packet.ResetBitReader();
                    aura.Level = packet.ReadUInt16("Caster Level", i);
                    aura.Charges = packet.ReadByte("Charges", i);

                    var int72 = packet.ReadUInt32("Int56 Count");
                    var effectCount = packet.ReadUInt32("Effect Count");

                    for (var j = 0; j < int72; ++j)
                        packet.ReadSingle("Float15", i, j);

                    for (var j = 0; j < effectCount; ++j)
                        packet.ReadSingle("Effect Value", i, j);

                    var hasCasterGUID = packet.ReadBit("hasCasterGUID");
                    var hasDuration = packet.ReadBit("hasDuration");
                    var hasMaxDuration = packet.ReadBit("hasMaxDuration");

                    if (hasCasterGUID)
                        packet.ReadPackedGuid128("Caster Guid");

                    if (hasDuration)
                        aura.Duration = packet.ReadInt32("Duration", i);
                    else
                        aura.Duration = 0;

                    if (hasMaxDuration)
                        aura.MaxDuration = packet.ReadInt32("Max Duration", i);
                    else
                        aura.MaxDuration = 0;

                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }
            }
            // To-Do: Fix me
            /*
            if (Storage.Objects.ContainsKey(GUID))
            {
                var unit = Storage.Objects[GUID].Item1 as Unit;
                if (unit != null)
                {
                    // If this is the first packet that sends auras
                    // (hopefully at spawn time) add it to the "Auras" field,
                    // if not create another row of auras in AddedAuras
                    // (similar to ChangedUpdateFields)

                    if (unit.Auras == null)
                        unit.Auras = auras;
                    else
                        unit.AddedAuras.Add(auras);
                }
            }*/
        }
    }
}
