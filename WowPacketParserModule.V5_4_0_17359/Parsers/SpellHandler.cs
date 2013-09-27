using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;

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

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);
            for (var i = 0; i < count; i++)
            {
                packet.ReadByte("Unk Byte", i);
                packet.ReadInt32("Unk Int32", i);
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = new byte[8];
            var powerGUID = new byte[8];

            packet.ReadBit(); // fake bit?
            
            packet.StartBitStream(guid, 6, 1, 0);

            var bits4 = (int)packet.ReadBits(24);
            
            packet.StartBitStream(guid, 2, 4);

            var hasPowerData = packet.ReadBit();

            var bits34 = 0u;
            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 7, 0, 6);
                bits34 = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 3, 1, 2, 4, 5);
            }

            packet.StartBitStream(guid, 7, 3, 5);

            var hasAura = new bool[bits4];
            var hasCasterGUID = new bool[bits4];
            var hasDuration = new bool[bits4];
            var hasMaxDuration = new bool[bits4];
            var effectCount = new uint[bits4];
            var casterGUID = new byte[bits4][];
            var bitsEC = new uint[bits4];

            for (var i = 0; i < bits4; ++i)
            {
                hasAura[i] = packet.ReadBit();

                if (hasAura[i])
                {
                    hasMaxDuration[i] = packet.ReadBit();
                    effectCount[i] = packet.ReadBits(22);
                    hasCasterGUID[i] = packet.ReadBit();
                    if (hasCasterGUID[i])
                    {
                        casterGUID[i] = new byte[8];
                        packet.StartBitStream(casterGUID[i], 3, 0, 2, 6, 5, 7, 4, 1);
                    }

                    hasDuration[i] = packet.ReadBit();
                    bitsEC[i] = packet.ReadBits(22);
                }
            }

            var auras = new List<Aura>();
            for (var i = 0; i < bits4; ++i)
            {
                if (hasAura[i])
                {
                    var aura = new Aura();

                    if (hasDuration[i])
                        aura.Duration = packet.ReadInt32("Duration", i);
                    else
                        aura.Duration = 0;

                    if (hasCasterGUID[i])
                    {
                        packet.ParseBitStream(casterGUID[i], 0, 7, 5, 6, 1, 3, 2, 4);
                        packet.WriteGuid("Caster GUID", casterGUID[i], i);
                        aura.CasterGuid = new Guid(BitConverter.ToUInt64(casterGUID[i], 0));
                    }
                    else
                        aura.CasterGuid = new Guid();

                    aura.AuraFlags = packet.ReadEnum<AuraFlagMoP>("Flags", TypeCode.Byte, i);

                    for (var j = 0; j < effectCount[i]; ++j)
                        packet.ReadSingle("Effect Value", i, j);

                    aura.SpellId = packet.ReadUInt32("Spell Id", i);

                    if (hasMaxDuration[i])
                        aura.MaxDuration = packet.ReadInt32("Max Duration", i);
                    else
                        aura.MaxDuration = 0;

                    for (var j = 0; j < bitsEC[i]; ++j)
                        packet.ReadSingle("FloatEA");

                    aura.Charges = packet.ReadByte("Charges", i);
                    packet.ReadInt32("Effect Mask", i);
                    aura.Level = packet.ReadUInt16("Caster Level", i);
                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }

                packet.ReadByte("Slot", i);
            }

            if (hasPowerData)
            {
                packet.ReadXORByte(powerGUID, 7);
                packet.ReadXORByte(powerGUID, 4);
                packet.ReadXORByte(powerGUID, 5);
                packet.ReadXORByte(powerGUID, 1);
                packet.ReadXORByte(powerGUID, 6);
                for (var i = 0; i < bits34; ++i)
                {
                    packet.ReadInt32("Value", i);
                    packet.ReadEnum<PowerType>("Power type", TypeCode.Int32, i); // Actually powertype for class
                }

                packet.ReadInt32("Attack power");
                packet.ReadInt32("Spell power");
                packet.ReadXORByte(powerGUID, 3);
                packet.ReadInt32("Current health");
                packet.ReadXORByte(powerGUID, 0);
                packet.ReadXORByte(powerGUID, 2);
                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ParseBitStream(guid, 0, 4, 3, 7, 5, 6, 2, 1);

            packet.WriteGuid("Guid", guid);

            var GUID = new Guid(BitConverter.ToUInt64(guid, 0));
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
                    else if (unit.AddedAuras == null)
                        unit.AddedAuras = new List<List<Aura>> { auras };
                    else
                        unit.AddedAuras.Add(auras);
                }
            }
        }
    }
}
