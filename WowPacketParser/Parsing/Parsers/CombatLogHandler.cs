using System;
using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_COMBAT_LOG_MULTIPLE)]
        public static void HandleCombatLogMultiple(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            packet.ReadUInt32("Timestamp");

            packet.StoreBeginList("CombatLogPackets");
            for (var i = 0; i < count; i++)
            {
                packet.ReadUInt32("Timestamp", i);
                var opcode = packet.ReadInt32();
                packet.Store("Opcode", Opcodes.GetOpcode(opcode), i);
                packet.ReadSubPacket(opcode, "Packet", i);
            }
            packet.StoreEndList();
        }

        [Parser(Opcode.SMSG_SPELLSTEALLOG)]
        [Parser(Opcode.SMSG_SPELLDISPELLOG)]
        [Parser(Opcode.SMSG_SPELLBREAKLOG)]
        public static void HandleSpellRemoveLog(Packet packet)
        {
            ReadSpellRemoveLog(ref packet);
        }

        [Parser(Opcode.SMSG_PERIODICAURALOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            ReadPeriodicAuraLog(ref packet);
        }

        [Parser(Opcode.SMSG_SPELLNONMELEEDAMAGELOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            ReadSpellNonMeleeDamageLog(ref packet);
        }

        [Parser(Opcode.SMSG_SPELLHEALLOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            ReadSpellHealLog(ref packet);
        }

        [Parser(Opcode.SMSG_SPELLENERGIZELOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            ReadSpellEnergizeLog(ref packet);
        }

        [Parser(Opcode.SMSG_SPELLLOGMISS)]
        public static void HandleSpellLogMiss(Packet packet)
        {
            ReadSpellMissLog(ref packet);
        }

        [Parser(Opcode.SMSG_SPELLLOGEXECUTE)]
        public static void HandleSpellLogExecute(Packet packet)
        {
            ReadSpellLogExecute(ref packet);
        }

        // Unknown opcode name(s)
        private static void ReadSpellRemoveLog(ref Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
            packet.ReadPackedGuid("Caster GUID"); // Can be 0
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell"); // Can be 0
            var debug = packet.ReadBoolean("Debug Output");
            var count = packet.ReadInt32("Count");

            packet.StoreBeginList("RemovedAuras");
            for (int i = 0; i < count; i++)
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell", i);
                packet.ReadByte("Unknown Byte/Bool", i);
            }
            packet.StoreEndList();

            if (debug)
            {
                packet.ReadInt32("Unk int32");
                packet.ReadInt32("Unk int32");
            }
        }

        private static void ReadSpellLogExecute(ref Packet packet)
        {
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            var count = packet.ReadInt32("Count"); // v47

            packet.StoreBeginList("SpellEffects");
            for (int i = 0; i < count; i++)
            {
                var type = packet.ReadEnum<SpellEffect>("Spell Effect", TypeCode.Int32, i);
                var count2 = packet.ReadInt32("Count", i);
                packet.StoreBeginList("Targets", i);
                for (int j = 0; j < count2; j++)
                {
                    switch (type)
                    {
                        case SpellEffect.PowerDrain:
                        case SpellEffect.PowerBurn:
                        {
                            packet.ReadPackedGuid("Target GUID", i, j);
                            packet.ReadInt32("Unknown Int32 1", i, j);
                            packet.ReadInt32("Unknown Int32 2", i, j);
                            packet.ReadSingle("Unknown Float", i, j);
                            break;
                        }
                        case SpellEffect.AddExtraAttacks:
                        {
                            packet.ReadPackedGuid("Target GUID", i, j);
                            packet.ReadInt32("Unknown Int32", i, j);
                            break;
                        }
                        case SpellEffect.InterruptCast:
                        {
                            packet.ReadPackedGuid("Target GUID", i, j);
                            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Interrupted Spell ID", i, j);
                            break;
                        }
                        case SpellEffect.DurabilityDamage:
                        {
                            packet.ReadPackedGuid("Target GUID", i, j);
                            packet.ReadInt32("Unknown Int32 1", i, j);
                            packet.ReadInt32("Unknown Int32 2", i, j);
                            break;
                        }
                        case SpellEffect.OpenLock:
                        {
                            packet.ReadPackedGuid("Target", i, j);
                            break;
                        }
                        case SpellEffect.CreateItem:
                        case SpellEffect.CreateRandomItem:
                        case SpellEffect.CreateItem2:
                        {
                            packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Created Item", i, j);
                            break;
                        }
                        case SpellEffect.Summon:
                        case SpellEffect.TransDoor:
                        case SpellEffect.SummonPet:
                        case SpellEffect.SummonObjectWild:
                        case SpellEffect.CreateHouse:
                        case SpellEffect.Duel:
                        case SpellEffect.SummonObjectSlot1:
                        case SpellEffect.SummonObjectSlot2:
                        case SpellEffect.SummonObjectSlot3:
                        case SpellEffect.SummonObjectSlot4:
                        case SpellEffect.Unk171:
                        {
                            packet.ReadPackedGuid("Summoned GUID", i, j);
                            break;
                        }
                        case SpellEffect.FeedPet:
                        {
                            packet.ReadInt32("Unknown Int32", i, j);
                            break;
                        }
                        case SpellEffect.DismissPet:
                        {
                            packet.ReadPackedGuid("GUID", i, j);
                            break;
                        }
                        case SpellEffect.Resurrect:
                        case SpellEffect.ResurrectNew:
                        case SpellEffect.RessurectAOE:
                        {
                            packet.ReadPackedGuid("GUID", i, j);
                            break;
                        }
                        default:
                            throw new InvalidDataException("Unknown Spell Effect: " + type);
                    }
                }
                packet.StoreEndList();
            }
            packet.StoreEndList();
        }

        private static void ReadPeriodicAuraLog(ref Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            var count = packet.ReadInt32("Count");

            packet.StoreBeginList("PeriodicAuras");
            for (var i = 0; i < count; i++)
            {
                var aura = packet.ReadEnum<AuraType>("Aura Type", TypeCode.UInt32, i);
                switch (aura)
                {
                    case AuraType.PeriodicDamage:
                    case AuraType.PeriodicDamagePercent:
                    {
                        packet.ReadUInt32("Damage", i);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                            packet.ReadUInt32("Over damage", i);

                        packet.ReadUInt32("Spell Proto", i);
                        packet.ReadUInt32("Absorb", i);
                        packet.ReadUInt32("Resist", i);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                            packet.ReadByte("Critical", i);

                        break;
                    }
                    case AuraType.PeriodicHeal:
                    case AuraType.ObsModHealth:
                    {
                        packet.ReadUInt32("Damage", i);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                            packet.ReadUInt32("Over damage", i);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                            // no idea when this was added exactly
                            packet.ReadUInt32("Absorb", i);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                            packet.ReadByte("Critical", i);

                        break;
                    }
                    case AuraType.ObsModPower:
                    case AuraType.PeriodicEnergize:
                    {
                        packet.ReadEnum<PowerType>("Power type", TypeCode.Int32, i);
                        packet.ReadUInt32("Amount", i);
                        break;
                    }
                    case AuraType.PeriodicManaLeech:
                    {
                        packet.ReadEnum<PowerType>("Power type", TypeCode.Int32, i);
                        packet.ReadUInt32("Amount", i);
                        packet.ReadSingle("Gain multiplier", i);
                        break;
                    }
                }
            }
            packet.StoreEndList();
        }

        private static void ReadSpellNonMeleeDamageLog(ref Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadUInt32("Damage");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.ReadInt32("Overkill");

            packet.ReadByte("SchoolMask");
            packet.ReadUInt32("Absorb");
            packet.ReadUInt32("Resist");
            packet.ReadBoolean("Show spellname in log");
            packet.ReadByte("Unk byte");
            packet.ReadUInt32("Blocked");
            var type = packet.ReadEnum<SpellHitType>("HitType", TypeCode.Int32);

            if (packet.ReadBoolean("Debug output"))
            {
                if (!type.HasAnyFlag(SpellHitType.SPELL_HIT_TYPE_UNK4))
                {
                    if (type.HasAnyFlag(SpellHitType.SPELL_HIT_TYPE_UNK1))
                    {
                        packet.ReadSingle("Unk float 1 1");
                        packet.ReadSingle("Unk float 1 2");
                    }

                    if (type.HasAnyFlag(SpellHitType.SPELL_HIT_TYPE_UNK3))
                    {
                        packet.ReadSingle("Unk float 3 1");
                        packet.ReadSingle("Unk float 3 2");
                    }

                    if (type.HasAnyFlag(SpellHitType.SPELL_HIT_TYPE_UNK6))
                    {
                        packet.ReadSingle("Unk float 6 1");
                        packet.ReadSingle("Unk float 6 2");
                        packet.ReadSingle("Unk float 6 3");
                        packet.ReadSingle("Unk float 6 4");
                        packet.ReadSingle("Unk float 6 5");
                        packet.ReadSingle("Unk float 6 6");
                    }
                }
            }
        }

        private static void ReadSpellHealLog(ref Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadUInt32("Damage");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.ReadUInt32("Overheal");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183)) // no idea when this was added exactly
                packet.ReadUInt32("Absorb");

            packet.ReadBoolean("Critical");
            if (packet.ReadBoolean("Debug output"))
            {
                packet.ReadSingle("Unk float");
                packet.ReadSingle("Unk float 2");
            }
        }

        private static void ReadSpellEnergizeLog(ref Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadEnum<PowerType>("Power type", TypeCode.UInt32);
            packet.ReadUInt32("Amount");
        }

        private static void ReadSpellMissLog(ref Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadGuid("Caster GUID");
            var debug = packet.ReadBoolean("Debug output");

            var count = packet.ReadUInt32("Target Count");

            packet.StoreBeginList("Targets");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadGuid("Target GUID", i);
                packet.ReadEnum<SpellMissType>("Miss Info", TypeCode.Byte, i);
                if (debug)
                {
                    packet.ReadSingle("Unk float 1", i);
                    packet.ReadSingle("Unk float 2", i);
                }
            }
            packet.StoreEndList();
        }

        [Parser(Opcode.SMSG_SPELLDAMAGESHIELD)]
        public static void ReadSpellDamageShield(Packet packet)
        {
            packet.ReadGuid("Victim");
            packet.ReadGuid("Caster");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell Id");
            packet.ReadInt32("Damage");
            packet.ReadInt32("Overkill");
            packet.ReadInt32("SpellSchoolMask");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596))
                packet.ReadInt32("Resisted Damage");
        }
    }
}
