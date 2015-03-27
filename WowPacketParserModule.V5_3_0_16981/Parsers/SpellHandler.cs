using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class SpellHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[1] = packet.ReadBit();
            var hasPowerData = packet.ReadBit();

            uint bits3C = 0;
            if (hasPowerData)
            {
                packet.StartBitStream(guid2, 1, 5, 6);
                bits3C = packet.ReadBits(21);
                packet.StartBitStream(guid2, 2, 3, 7, 0, 4);
            }

            var bits4 = packet.ReadBits(24);
            guid[6] = packet.ReadBit();

            var hasAura = new bool[bits4];
            var hasCasterGUID = new bool[bits4];
            var hasDuration = new bool[bits4];
            var hasMaxDuration = new bool[bits4];
            var effectCount = new uint[bits4];
            var casterGUID = new byte[bits4][];
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
                        packet.StartBitStream(casterGUID[i], 5, 7, 4, 1, 6, 0, 3, 2);
                    }
                    hasDuration[i] = packet.ReadBit();
                }
            }
            packet.StartBitStream(guid, 2, 3, 7, 4);
            packet.ReadBit("Is AURA_UPDATE_ALL");
            packet.StartBitStream(guid, 0, 5);
            packet.ResetBitReader();

            var auras = new List<Aura>();
            for (var i = 0; i < bits4; ++i)
            {
                if (hasAura[i])
                {
                    var aura = new Aura();
                    if (hasCasterGUID[i])
                    {
                        packet.ParseBitStream(casterGUID[i], 2, 7, 6, 1, 4, 0, 5, 3);
                        packet.WriteGuid("Caster GUID", casterGUID[i], i);
                        aura.CasterGuid = new WowGuid64(BitConverter.ToUInt64(casterGUID[i], 0));
                    }
                    else
                        aura.CasterGuid = new WowGuid64();

                    aura.Charges = packet.ReadByte("Charges", i);

                    aura.Duration = hasDuration[i] ? packet.ReadInt32("Duration", i) : 0;

                    aura.SpellId = packet.ReadUInt32("Spell Id", i);
                    aura.AuraFlags = packet.ReadByteE<AuraFlagMoP>("Flags", i);

                    for (var j = 0; j < effectCount[i]; ++j)
                        packet.ReadSingle("Effect Value", i, j);
                    packet.ReadInt32("Effect Mask", i);

                    aura.MaxDuration = hasMaxDuration[i] ? packet.ReadInt32("Max Duration", i) : 0;

                    aura.Level = packet.ReadUInt16("Caster Level", i);
                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }
                packet.ReadByte("Slot", i);
            }

            if (hasPowerData)
            {
                packet.ReadXORBytes(guid2, 7, 0);
                for (var i = 0; i < bits3C; ++i)
                {
                    packet.ReadUInt32E<PowerType>("Power Type", i);
                    packet.ReadInt32("Power Value", i);
                }
                packet.ReadXORBytes(guid2, 2, 5);
                packet.ReadInt32("Attack power");
                packet.ReadInt32("Spell power");
                packet.ReadXORBytes(guid2, 6, 4, 3, 1);
                packet.ReadInt32("Current Health");
                packet.WriteGuid("PowerUnitGUID", guid2);
            }
            packet.ParseBitStream(guid, 0, 5, 7, 2, 1, 4, 3, 6);
            packet.WriteGuid("Guid", guid);

            var GUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
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
            }
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            var counter = packet.ReadBits(2);
            var unkBit = !packet.ReadBit();
            for (var i = 0; i < counter; ++i)
                packet.ReadBits("unk value0", 2, i);

            var hasCastCount = !packet.ReadBit();
            packet.ReadBit("Fake bit? Has TargetGUID"); // TargetGUID
            var hasbit1C = !packet.ReadBit();
            var hasMovment = packet.ReadBit();
            var hasbit78 = !packet.ReadBit();
            var hasbitF8 = !packet.ReadBit();
            var hasGUID2 = packet.ReadBit();
            var hasbitFC = !packet.ReadBit();
            var hasbit18 = !packet.ReadBit();
            var hasGUID3 = packet.ReadBit();
            packet.ReadBit("Fake bit? Has GUID0"); // GUID0
            var hasSpellId = !packet.ReadBit();

            var guid2 = new byte[8];
            var guid3 = new byte[8];

            var guid0 = packet.StartBitStream(0, 5, 1, 7, 4, 3, 6, 2);
            if (hasGUID3)
                guid3 = packet.StartBitStream(2, 5, 3, 7, 4, 1, 0, 6);

            if (hasGUID2)
                guid2 = packet.StartBitStream(6, 2, 4, 7, 3, 5, 0, 1);

            var targetGUID = packet.StartBitStream(3, 0, 2, 7, 6, 4, 1, 5);

            if (unkBit)
                packet.ReadBitsE<CastFlag>("Cast Flags", 20);

            if (hasbit1C)
                packet.ReadBits("hasbit1C", 5);

            uint len78 = 0;
            if (hasbit78)
                len78 = packet.ReadBits("hasbit78", 7);
            packet.ResetBitReader();

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadInt32("unk value1", i);
                packet.ReadInt32("unk value2", i);
            }

            if (hasGUID3)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(guid3, 7, 5, 3);
                pos.X = packet.ReadSingle();
                packet.ReadXORBytes(guid3, 0, 2, 1, 4, 6);
                pos.Z = packet.ReadSingle();
                pos.Y = packet.ReadSingle();
                packet.WriteGuid("GUID3", guid3);
                packet.AddValue("Position", pos);
            }

            packet.ParseBitStream(targetGUID, 2, 0, 5, 6, 7, 3, 4, 1);
            packet.WriteGuid("Target GUID", targetGUID);

            if (hasGUID2)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(guid2, 5, 7);
                pos.Y = packet.ReadSingle();
                pos.X = packet.ReadSingle();
                packet.ReadXORBytes(guid2, 3, 1);
                pos.Z = packet.ReadSingle();
                packet.ReadXORBytes(guid2, 2, 6, 4, 0);
                packet.WriteGuid("GUID2", guid2);
                packet.AddValue("Position", pos);
            }

            packet.ParseBitStream(guid0, 7, 2, 6, 4, 1, 0, 3, 5);
            packet.WriteGuid("GUID0", guid0);

            if (hasbit78)
                packet.ReadWoWString("String", (int)len78);

            if (hasCastCount)
                packet.ReadByte("Cast Count");

            if (hasbit18)
                packet.ReadInt32("Int18");

            if (hasMovment)
                MovementHandler.ReadClientMovementBlock(packet);

            if (hasSpellId)
                packet.ReadInt32("SpellId");

            if (hasbitF8)
                packet.ReadSingle("FloatF8");

            if (hasbitFC)
                packet.ReadSingle("FloatFC");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            //var CasterGUID1 = new byte[8]; // 14
            var casterGUID2 = new byte[8]; // 112-119
            var guid3 = new byte[8]; // 24-31
            var guid4 = new byte[8]; // 16-23
            //var guid5 = new byte[8]; // 98
            var guid6 = new byte[8]; // 416-423
            var destinationTransportGUID = new byte[8]; // 168-175
            var sourceTransportGUID = new byte[8]; // 136-143
            //var guid9 = new byte[8]; // 18
            var targetGUID = new byte[8];
            var powerUnitGUID = new byte[8];

            var bits52 = packet.ReadBits(24);
            var casterGUID1 = new byte[bits52][];
            for (var i = 0; i < bits52; ++i)
            {
                casterGUID1[i] = new byte[8];
                packet.StartBitStream(casterGUID1[i], 2, 5, 4, 7, 6, 0, 3, 1);
            }

            packet.ReadBit("bit28");
            var bit106 = !packet.ReadBit();
            packet.ReadBit("bit30");

            packet.StartBitStream(casterGUID2, 5, 4, 7, 1, 0, 6, 3, 2);
            packet.StartBitStream(guid4, 5, 6);
            guid3[2] = packet.ReadBit();
            var bit372 = packet.ReadBit();
            packet.StartBitStream(targetGUID, 0, 3, 1, 5, 6, 2, 7, 4);

            var hasPowerData = packet.ReadBit("Has Power Data"); // bit432
            uint powerTypeCount = 0;
            if (hasPowerData)
            {
                packet.StartBitStream(powerUnitGUID, 6, 7, 3, 5, 0, 4, 2, 1);
                powerTypeCount = packet.ReadBits("Power Type Count", 21);
            }
            packet.StartBitStream(guid3, 6, 0);
            var bit102 = !packet.ReadBit();
            var bit101 = !packet.ReadBit();
            guid4[0] = packet.ReadBit();
            var bits84 = packet.ReadBits(25);
            guid3[7] = packet.ReadBit();
            var hasTargetFlags = !packet.ReadBit();
            var bit368 = !packet.ReadBit();
            var hasRuneStateBefore = !packet.ReadBit();
            guid4[4] = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            var bit91 = !packet.ReadBit();
            var ExtraTargetsCount = packet.ReadBits("Extra Targets Count", 20);

            var ExtraTargetsGUID = new byte[ExtraTargetsCount][];
            for (var i = 0; i < ExtraTargetsCount; i++)
            {
                ExtraTargetsGUID[i] = new byte[8];
                packet.StartBitStream(ExtraTargetsGUID[i], 0, 5, 2, 7, 6, 4, 3, 1);

            }
            packet.ReadBit("bit104");
            var bit90 = !packet.ReadBit();

            for (var i = 0; i < bits84; ++i)
            {
                if (packet.ReadBits("bits22[0]", 4, i) == 11)
                    packet.ReadBits("bits22[1]", 4, i);
            }

            packet.StartBitStream(guid6, 2, 1, 7, 0, 6, 3, 5, 4);
            guid4[7] = packet.ReadBit();
            var HasDestinationData = packet.ReadBit();
            var hasSourceData = packet.ReadBit();
            guid4[2] = packet.ReadBit();

            if (HasDestinationData)
                packet.StartBitStream(destinationTransportGUID, 3, 7, 1, 0, 5, 6, 4, 2);

            guid4[3] = packet.ReadBit();
            var bit89 = !packet.ReadBit();

            if (hasSourceData)
                packet.StartBitStream(sourceTransportGUID, 5, 4, 3, 2, 0, 6, 7, 1);

            var bit48 = packet.ReadBit();

            if (hasTargetFlags)
                packet.ReadBitsE<TargetFlag>("Target Flags", 20);

            var hasRuneStateAfter = !packet.ReadBit();

            int bits48 = 0;
            if (!bit48)
                bits48 = (int)packet.ReadBits("bits48", 7);

            var hasPredictedType = !packet.ReadBit("bit428");
            guid3[3] = packet.ReadBit();
            var bits68 = packet.ReadBits("bits68", 24);
            guid3[1] = packet.ReadBit();

            var guid9 = new byte[bits68][];
            for (var i = 0; i < bits68; ++i)
            {
                guid9[i] = new byte[8];
                packet.StartBitStream(guid9[i], 3, 1, 2, 7, 5, 6, 4, 0);
            }

            var bit384 = !packet.ReadBit("bit384");
            guid3[5] = packet.ReadBit();
            guid4[1] = packet.ReadBit();
            var bits320 = packet.ReadBits("bits320", 21);
            var RuneCooldownCount = packet.ReadBits("Rune Cooldown Count", 3);
            packet.ReadBits("bits11", 12);

            if (HasDestinationData)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(destinationTransportGUID, 0, 1);
                pos.Z = packet.ReadSingle();
                packet.ReadXORBytes(destinationTransportGUID, 5, 3);
                pos.Y = packet.ReadSingle();
                packet.ReadXORByte(destinationTransportGUID, 2);
                pos.X = packet.ReadSingle();
                packet.ReadXORBytes(destinationTransportGUID, 6, 7, 4);
                packet.WriteGuid("Destination Transport GUID", destinationTransportGUID);
                packet.AddValue("Destination Position", pos);
            }
            packet.ParseBitStream(targetGUID, 1, 0, 5, 2, 3, 4, 7, 6);
            packet.ReadXORByte(guid4, 3);
            packet.WriteGuid("TargetGUID", targetGUID);
            if (bit372)
            {
                packet.ReadUInt32("unk95");
                packet.ReadUInt32("unk94");
            }

            for (var i = 0; i < bits68; ++i)
            {
                packet.ParseBitStream(guid9[i], 1, 4, 5, 6, 2, 7, 3, 0);
                packet.WriteGuid("GUID9", guid9[i], i);
            }

            for (var i = 0; i < bits52; ++i)
            {
                packet.ParseBitStream(casterGUID1[i], 0, 4, 3, 2, 7, 1, 5, 6);
                packet.WriteGuid("CasterGUID1", casterGUID1[i], i);
            }

            packet.ParseBitStream(casterGUID2, 4, 5, 7, 0, 1, 3, 2, 6);
            packet.WriteGuid("CasterGUID2", casterGUID2);

            for (var i = 0; i < bits320; ++i)
            {
                packet.ReadByteE<PowerType>("Power Type", i);
                packet.ReadInt32("Power Value", i);
            }

            if (hasPowerData)
            {
                packet.ReadXORByte(powerUnitGUID, 0);
                packet.ReadInt32("Current Health");
                packet.ReadXORByte(powerUnitGUID, 2);
                packet.ReadInt32("Spell power");
                packet.ReadXORByte(powerUnitGUID, 5);
                packet.ReadInt32("Attack power");
                packet.ReadXORByte(powerUnitGUID, 1);

                for (var i = 0; i < powerTypeCount; ++i)
                {
                    packet.ReadUInt32E<PowerType>("Power Type", i);
                    packet.ReadInt32("Power Value", i);
                }
                packet.ReadXORBytes(powerUnitGUID, 6, 7, 4, 3);
                packet.WriteGuid("PowerUnitGUID", powerUnitGUID);
            }

            if (bit89)
                packet.ReadUInt32("unk89");

            packet.ParseBitStream(guid6, 1, 7, 4, 3, 5, 2, 0, 6);
            packet.WriteGuid("GUID6", guid6);
            packet.ReadXORBytes(guid3, 3, 4);

            if (hasSourceData)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(sourceTransportGUID, 2, 4, 1);
                pos.Z = packet.ReadSingle();
                packet.ReadXORBytes(sourceTransportGUID, 0, 5, 3);
                pos.X = packet.ReadSingle();
                pos.Y = packet.ReadSingle();
                packet.ReadXORBytes(sourceTransportGUID, 7, 6);
                packet.WriteGuid("Source Transport GUID", sourceTransportGUID);
                packet.AddValue("Source Position", pos);
            }

            for (var i = 0; i < RuneCooldownCount; ++i)
                packet.ReadByte("Rune Cooldown Passed", i);

            for (var i = 0; i < ExtraTargetsCount; ++i)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(ExtraTargetsGUID[i], 4, 2);
                pos.X = packet.ReadSingle();
                packet.ReadXORBytes(ExtraTargetsGUID[i], 5, 7, 0);
                pos.Y = packet.ReadSingle();
                packet.ReadXORBytes(ExtraTargetsGUID[i], 1, 3, 6);
                pos.Z = packet.ReadSingle();
                packet.WriteGuid("Extra Target GUID", ExtraTargetsGUID[i], i);
                packet.AddValue("Position", pos, i);
            }
            packet.ReadXORByte(guid4, 2);

            if (hasRuneStateBefore)
                packet.ReadByte("Rune State Before");

            if (bit90)
                packet.ReadSingle("float90");

            packet.ReadInt32E<CastFlag>("Cast Flags");
            packet.ReadXORByte(guid3, 2);
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadXORByte(guid4, 0);
            packet.ReadXORByte(guid3, 5);

            if (hasPredictedType)
                packet.ReadByte("Predicted Type");

            packet.ReadXORBytes(guid3, 0, 6, 1);
            packet.ReadXORByte(guid4, 1);

            if (hasRuneStateAfter)
                packet.ReadByte("Rune State After");

            if (bit101)
                packet.ReadUInt32("unk101");

            packet.ReadXORByte(guid4, 4);

            if (bit368)
                packet.ReadByte("byte368");

            if (bit384)
                packet.ReadByte("byte384");

            packet.ReadWoWString("String48:", bits48);

            packet.ReadXORByte(guid4, 7);
            packet.ReadByte("Cast Count");

            if (bit102)
                packet.ReadUInt32("unk102");

            packet.ReadXORByte(guid4, 6);

            if (bit106)
                packet.ReadUInt32("Heal");

            packet.ReadUInt32("Cast time");
            packet.ReadXORByte(guid4, 5);

            if (bit91)
                packet.ReadUInt32("unk91");

            packet.ReadXORByte(guid3, 7);
            packet.WriteGuid("GUID3", guid3);
            packet.WriteGuid("GUID4", guid4);
        }

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            var count = packet.ReadBits("Spell Count", 22);
            packet.ReadBit("InitialLogin");

            var spells = new List<uint>((int)count);
            for (var i = 0; i < count; i++)
            {
                var spellId = packet.ReadUInt32<SpellId>("Spell ID", i);
                spells.Add(spellId);
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

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];
            var SourceTransportGUID = new byte[8];
            byte[][] guid4;
            byte[][] guid5;
            var DestinationTransportGUID = new byte[8];
            byte[][] guid7;
            var guid8 = new byte[8];
            var guid9 = new byte[8];
            var guid10 = new byte[8];
            var guid11 = new byte[8];

            uint powerCount = 0;

            guid10[5] = packet.ReadBit();
            var counter = packet.ReadBits(24);
            var hasPredictedType = !packet.ReadBit();
            var bit404 = !packet.ReadBit();
            packet.ReadBit("field_A8");
            guid11[6] = packet.ReadBit();
            var bit432 = !packet.ReadBit();
            var counter3 = packet.ReadBits(20);
            packet.StartBitStream(guid, 2, 1, 0, 4, 5, 6, 3, 7);
            packet.StartBitStream(guid10, 0, 1, 3);

            guid7 = new byte[counter][];
            for (var i = 0; i < counter; ++i)
            {
                guid7[i] = new byte[8];
                packet.StartBitStream(guid7[i], 5, 6, 4, 7, 1, 2, 3, 0);
            }

            var hasCastSchoolImmunities = !packet.ReadBit();
            guid11[5] = packet.ReadBit();
            var hasRuneStateBefore = !packet.ReadBit();
            var powerLeftSelf = packet.ReadBits(21);
            var bit408 = !packet.ReadBit();
            var counter2 = packet.ReadBits(24);
            var hasPowerData = packet.ReadBit();
            packet.StartBitStream(guid10, 6, 2);
            guid11[7] = packet.ReadBit();
            var RuneCooldownCount = packet.ReadBits(3);
            var hasTargetFlags = !packet.ReadBit();
            var hasSourceData = packet.ReadBit();

            if (hasPowerData)
            {
                packet.StartBitStream(guid2, 7, 4, 5, 0, 2, 6, 3, 1);
                powerCount = packet.ReadBits(21);
            }

            var bit412 = !packet.ReadBit();

            if (hasSourceData)
                packet.StartBitStream(SourceTransportGUID, 6, 3, 0, 1, 4, 5, 2, 7);

            guid11[1] = packet.ReadBit();

            guid4 = new byte[counter2][];
            for (var i = 0; i < counter2; ++i)
            {
                guid4[i] = new byte[8];
                packet.StartBitStream(guid4[i], 6, 4, 1, 7, 5, 2, 3, 0);
            }

            guid5 = new byte[counter3][];
            for (var i = 0; i < counter3; ++i)
            {
                guid5[i] = new byte[8];
                packet.StartBitStream(guid5[i], 7, 6, 5, 0, 4, 3, 1, 2);
            }
            var bit416 = !packet.ReadBit();
            guid11[0] = packet.ReadBit();
            packet.ReadBits("int5C", 12);
            packet.StartBitStream(guid10, 7, 4);
            var hasDestinationData = packet.ReadBit();

            if (hasDestinationData)
                packet.StartBitStream(DestinationTransportGUID, 0, 2, 7, 6, 1, 4, 3, 5);

            var hasRuneStateAfter = !packet.ReadBit();
            var hasCastImmunities = !packet.ReadBit();
            guid11[3] = packet.ReadBit();
            var bit420 = packet.ReadBit();
            var hasPredictedSpellId = !packet.ReadBit();
            guid11[4] = packet.ReadBit();
            var unkflag27 = packet.ReadBit();

            int bits7 = 0;
            if (!unkflag27)
                bits7 = (int)packet.ReadBits(7);

            var counter4 = packet.ReadBits(25);
            guid11[2] = packet.ReadBit();

            for (var i = 0; i < counter4; ++i)
            {
                var bits136 = packet.ReadBits("bits136", 4);

                if (bits136 == 11)
                    packet.ReadBits("bits140", 4);
            }

            packet.ReadBit("unk464");
            packet.StartBitStream(guid8, 7, 3, 6, 4, 2, 5, 0, 1);
            packet.ReadBit("unk160");
            packet.StartBitStream(guid9, 3, 5, 2, 1, 4, 0, 6, 7);

            if (hasTargetFlags)
                packet.ReadBitsE<TargetFlag>("Target Flags", 20);

            if (hasSourceData)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(SourceTransportGUID, 3, 7, 4);
                pos.X = packet.ReadSingle();
                packet.ReadXORBytes(SourceTransportGUID, 6, 5, 2);
                pos.Z = packet.ReadSingle();
                pos.Y = packet.ReadSingle();
                packet.ReadXORBytes(SourceTransportGUID, 0, 1);
                packet.WriteGuid("Source Transport GUID", SourceTransportGUID);
                packet.AddValue("Source Position", pos);
            }


            for (var i = 0; i < counter3; ++i)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(guid5[i], 4, 0, 7, 6, 2, 1);
                pos.X = packet.ReadSingle();
                packet.ReadXORBytes(guid5[i], 5, 3);
                pos.Y = packet.ReadSingle();
                pos.Z = packet.ReadSingle();
                packet.WriteGuid("GUID5", guid5[i], i);
                packet.AddValue("Position", pos);
            }

            for (var i = 0; i < counter; ++i)
            {
                packet.ParseBitStream(guid7[i], 2, 1, 5, 3, 7, 0, 4, 6);
                packet.WriteGuid("GUID7", guid7[i], i);
            }

            if (hasDestinationData)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(DestinationTransportGUID, 1, 4);
                pos.Z = packet.ReadSingle();
                packet.ReadXORBytes(DestinationTransportGUID, 0, 7, 3, 5);
                pos.Y = packet.ReadSingle();
                packet.ReadXORByte(DestinationTransportGUID, 6);
                pos.X = packet.ReadSingle();
                packet.ReadXORByte(DestinationTransportGUID, 2);
                packet.WriteGuid("Destination Transport GUID", DestinationTransportGUID);
                packet.AddValue("Destination Position", pos);
            }
            if (bit408)
                packet.ReadSingle("float198");

            if (bit416)
                packet.ReadByte("unk.416");

            packet.ParseBitStream(guid8, 5, 0, 2, 7, 6, 3, 4, 1);
            packet.WriteGuid("GUID8", guid8);

            for (var i = 0; i < counter2; ++i)
            {
                packet.ParseBitStream(guid4[i], 6, 3, 1, 0, 2, 4, 7, 5);
                packet.WriteGuid("GUID4", guid4[i], i);
            }

            if (hasPowerData)
            {
                packet.ReadXORByte(guid2, 3);
                packet.ReadUInt32("Spell Power");
                packet.ReadXORBytes(guid2, 6, 4, 7, 0);
                packet.ReadUInt32("Attack Power");
                packet.ReadXORByte(guid2, 2);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadUInt32("uint32 60");
                    packet.ReadUInt32("uint32 48");
                }

                packet.ReadUInt32("Current Health");
                packet.ReadXORBytes(guid2, 1, 5);
                packet.WriteGuid("GUID2", guid2);
            }

            packet.ParseBitStream(guid9, 2, 3, 4, 7, 5, 1, 6, 0);
            packet.WriteGuid("GUID9", guid9);

            packet.ReadByte("Cast Count");

            packet.ParseBitStream(guid, 0, 1, 4, 5, 7, 6, 2, 3);
            packet.WriteGuid("GUID", guid);

            packet.ReadXORByte(guid10, 7);
            if (hasPredictedType)
                packet.ReadByte("Predicted Type");

            if (hasPredictedSpellId)
                packet.ReadUInt32("Predicted Spell Id");

            packet.ReadXORByte(guid10, 3);

            packet.ReadBytes("Bytes", bits7);

            if (bit404)
                packet.ReadUInt32("uint32 404");

            if (hasCastImmunities)
                packet.ReadUInt32("Cast Immunities");

            packet.ReadXORByte(guid10, 2);
            packet.ReadXORByte(guid11, 0);

            if (bit420)
            {
                packet.ReadUInt32("uint32 424");
                packet.ReadUInt32("uint32 428");
            }

            if (bit412)
                packet.ReadUInt32("uint32 412");

            packet.ReadXORByte(guid11, 2);
            packet.ReadXORBytes(guid10, 0, 1);
            packet.ReadXORByte(guid11, 3);

            for (var i = 0; i < RuneCooldownCount; ++i)
                packet.ReadByte("Rune Cooldown Passed", i);

            if (hasRuneStateAfter)
                packet.ReadByte("Rune State After");

            packet.ReadXORByte(guid11, 1);
            packet.ReadXORByte(guid10, 4);
            packet.ReadInt32<SpellId>("Spell ID");

            if (hasRuneStateBefore)
                packet.ReadByte("Rune State Before");

            packet.ReadXORByte(guid10, 6);
            packet.ReadXORByte(guid11, 5);
            packet.ReadUInt32("uint32 88"); // field_58
            packet.ReadXORByte(guid11, 4);
            packet.ReadUInt32("uint32 96"); // field_60

            for (var i = 0; i < powerLeftSelf; ++i)
            {
                packet.ReadInt32("Power Value", i);
                packet.ReadByteE<PowerType>("Power Type", i);
            }
            packet.ReadXORByte(guid10, 5);
            packet.ReadXORByte(guid11, 6);

            if (hasCastSchoolImmunities)
                packet.ReadUInt32("Cast School Immunities");

            packet.ReadXORByte(guid11, 7);

            if (bit432)
                packet.ReadByte("unk432");

            packet.WriteGuid("GUID10", guid10);
            packet.WriteGuid("GUID11", guid11);
        }

        [Parser(Opcode.SMSG_UNLEARNED_SPELLS)]
        public static void HandleRemovedSpell2(Packet packet)
        {
            var count = packet.ReadBits(22);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt32<SpellId>("Spell ID", i);
        }
    }
}
