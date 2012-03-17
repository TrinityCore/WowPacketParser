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
            packet.ReadInt32("Unk1");

            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Unk2", i);
                var opcode = Opcodes.GetOpcode(packet.ReadInt32());
                packet.WriteLine("Opcode: " + opcode);
                switch (opcode)
                {
                    case Opcode.SMSG_SPELLHEALLOG:
                    {
                        ReadSpellHealLog(ref packet, i);
                        break;
                    }
                    case Opcode.SMSG_SPELLENERGIZELOG:
                    {
                        ReadSpellEnergizeLog(ref packet, i);
                        break;
                    }
                    case Opcode.SMSG_PERIODICAURALOG:
                    {
                        ReadPeriodicAuraLog(ref packet, i); // sub_5EEE10
                        break;
                    }
                    case Opcode.SMSG_SPELLLOGEXECUTE:
                    {
                        ReadSpellLogExecute(ref packet, i);
                        break;
                    }
                    case Opcode.SMSG_SPELLNONMELEEDAMAGELOG:
                    {
                        ReadSpellNonMeleeDamageLog(ref packet, i);
                        break;
                    }
                    case Opcode.SMSG_SPELLLOGMISS:
                    {
                        ReadSpellMissLog(ref packet, i);
                        break;
                    }
                    case Opcode.SMSG_SPELLSTEALLOG:
                    case Opcode.SMSG_SPELLDISPELLOG:
                    case Opcode.SMSG_SPELLBREAKLOG:
                    {
                        ReadSpellRemoveLog(ref packet, i);
                        break;
                    }
                    default:
                        throw new InvalidDataException("Unknown Spell Log Type/Opcode: " + opcode);
                }
            }
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
        private static void ReadSpellRemoveLog(ref Packet packet, int index = -1)
        {
            packet.ReadPackedGuid("Target GUID", index);
            packet.ReadPackedGuid("Caster GUID", index); // Can be 0
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell", index); // Can be 0
            packet.ReadByte("Unknown Byte/Bool", index);
            var count = packet.ReadInt32("Count", index);

            for (int i = 0; i < count; i++)
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell", index, i);
                packet.ReadByte("Unknown Byte/Bool", index, i);
            }
        }

        private static void ReadSpellLogExecute(ref Packet packet, int index = -1)
        {
            packet.ReadPackedGuid("Caster GUID", index);
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", index);
            var count = packet.ReadInt32("Count", index); // v47

            for (int i = 0; i < count; i++)
            {
                var type = packet.ReadEnum<SpellEffect>("Spell Effect", TypeCode.Int32, index, i);
                var count2 = packet.ReadInt32("Count", index, i);
                for (int j = 0; j < count2; j++)
                {
                    switch (type)
                    {
                        case SpellEffect.PowerDrain:
                        case SpellEffect.PowerBurn:
                        {
                            packet.ReadPackedGuid("Target GUID", index, i, j);
                            packet.ReadInt32("Unknown Int32", index, i, j);
                            packet.ReadInt32("Unknown Int32", index, i, j);
                            packet.ReadSingle("Unknown Float", index, i, j);
                            break;
                        }
                        case SpellEffect.AddExtraAttacks:
                        {
                            packet.ReadPackedGuid("Target GUID", index, i, j);
                            packet.ReadInt32("Unknown Int32", index, i, j);
                            break;
                        }
                        case SpellEffect.DurabilityDamage:
                        {
                            packet.ReadPackedGuid("Target GUID", index, i, j);
                            packet.ReadInt32("Unknown Int32", index, i, j);
                            packet.ReadInt32("Unknown Int32", index, i, j);
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
                            packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Created Item", index, i, j);
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
                        case SpellEffect.Unk171:
                        {
                            packet.ReadPackedGuid("Summoned GUID", index, i, j);
                            break;
                        }
                        case SpellEffect.FeedPet:
                        {
                            packet.ReadInt32("Unknown Int32", index, i, j);
                            break;
                        }
                        case SpellEffect.DismissPet:
                        {
                            packet.ReadPackedGuid("GUID", index, i, j);
                            break;
                        }
                        case SpellEffect.Resurrect:
                        case SpellEffect.ResurrectNew:
                        case SpellEffect.RessurectAOE:
                        {
                            packet.ReadPackedGuid("GUID", index, i, j);
                            break;
                        }
                        default:
                            throw new InvalidDataException("Unknown Spell Effect: " + type);
                    }
                }
            }
        }

        private static void ReadPeriodicAuraLog(ref Packet packet, int index = -1)
        {
            packet.ReadPackedGuid("Target GUID", index);
            packet.ReadPackedGuid("Caster GUID", index);
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", index);
            var count = packet.ReadInt32("Count", index);

            for (var i = 0; i < count; i++)
            {
                var aura = packet.ReadEnum<AuraType>("Aura Type", TypeCode.UInt32, index);
                switch (aura)
                {
                    case AuraType.PeriodicDamage:
                    case AuraType.PeriodicDamagePercent:
                    {
                        packet.ReadUInt32("Damage", index);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                            packet.ReadUInt32("Over damage", index);

                        packet.ReadUInt32("Spell Proto", index);
                        packet.ReadUInt32("Absorb", index);
                        packet.ReadUInt32("Resist", index);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                            packet.ReadByte("Critical", index);

                        break;
                    }
                    case AuraType.PeriodicHeal:
                    case AuraType.ObsModHealth:
                    {
                        packet.ReadUInt32("Damage", index);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                            packet.ReadUInt32("Over damage", index);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                            // no idea when this was added exactly
                            packet.ReadUInt32("Absorb", index);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                            packet.ReadByte("Critical", index);

                        break;
                    }
                    case AuraType.ObsModPower:
                    case AuraType.PeriodicEnergize:
                    {
                        packet.ReadEnum<PowerType>("Power type", TypeCode.Int32, index);
                        packet.ReadUInt32("Amount", index);
                        break;
                    }
                    case AuraType.PeriodicManaLeech:
                    {
                        packet.ReadEnum<PowerType>("Power type", TypeCode.Int32, index);
                        packet.ReadUInt32("Amount", index);
                        packet.ReadSingle("Gain multiplier", index);
                        break;
                    }
                }
            }
        }

        private static void ReadSpellNonMeleeDamageLog(ref Packet packet, int index = -1)
        {
            packet.ReadPackedGuid("Target GUID", index);
            packet.ReadPackedGuid("Caster GUID", index);
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", index);
            packet.ReadUInt32("Damage", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.ReadInt32("Overkill", index);

            packet.ReadByte("SchoolMask", index);
            packet.ReadUInt32("Absorb", index);
            packet.ReadUInt32("Resist", index);
            packet.ReadBoolean("Show spellname in log", index);
            packet.ReadByte("Unk byte", index);
            packet.ReadUInt32("Blocked", index);
            var type = packet.ReadEnum<SpellHitType>("HitType", TypeCode.Int32, index);
            var debug = packet.ReadBoolean("Debug output", index);
            if (debug)
            {
                if (!type.HasAnyFlag(SpellHitType.SPELL_HIT_TYPE_UNK4))
                {
                    if (type.HasAnyFlag(SpellHitType.SPELL_HIT_TYPE_UNK1))
                    {
                        packet.ReadSingle("Unk float");
                        packet.ReadSingle("Unk float");
                    }

                    if (type.HasAnyFlag(SpellHitType.SPELL_HIT_TYPE_UNK3))
                    {
                        packet.ReadSingle("Unk float");
                        packet.ReadSingle("Unk float");
                    }

                    if (type.HasAnyFlag(SpellHitType.SPELL_HIT_TYPE_UNK6))
                    {
                        packet.ReadSingle("Unk float");
                        packet.ReadSingle("Unk float");
                        packet.ReadSingle("Unk float");
                        packet.ReadSingle("Unk float");
                        packet.ReadSingle("Unk float");
                        packet.ReadSingle("Unk float");
                    }
                }
            }
        }

        private static void ReadSpellHealLog(ref Packet packet, int index = -1)
        {
            packet.ReadPackedGuid("Target GUID", index);
            packet.ReadPackedGuid("Caster GUID", index);
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", index);
            packet.ReadUInt32("Damage", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.ReadUInt32("Overheal", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183)) // no idea when this was added exactly
                packet.ReadUInt32("Absorb", index);

            packet.ReadBoolean("Critical", index);
            packet.ReadBoolean("Debug output", index);
        }

        private static void ReadSpellEnergizeLog(ref Packet packet, int index = -1)
        {
            packet.ReadPackedGuid("Target GUID", index);
            packet.ReadPackedGuid("Caster GUID", index);
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", index);
            packet.ReadEnum<PowerType>("Power type", TypeCode.UInt32, index);
            packet.ReadUInt32("Amount", index);
        }

        private static void ReadSpellMissLog(ref Packet packet, int index = -1)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", index);
            packet.ReadGuid("Caster GUID", index);
            var unkbool = packet.ReadBoolean("Unk bool", index);

            var count = packet.ReadUInt32("Target Count", index);
            for (var i = 0; i < count; ++i)
            {
                packet.ReadGuid("Target GUID", index);
                packet.ReadEnum<SpellMissType>("Miss Info", TypeCode.Byte, index);
                if (unkbool)
                {
                    packet.ReadSingle("Unk float");
                    packet.ReadSingle("Unk float");
                }
            }
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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545)) // not verified
                packet.ReadInt32("Unknown Int32");
        }
    }
}
