using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_COMBAT_LOG_MULTIPLE)]
        public static void HandleCombatLogMultiple(Packet packet)
        {
            var count = packet.Translator.ReadInt32("Count");
            var unk1 = packet.Translator.ReadInt32();

            for (var i = 0; i < count; i++)
            {
                var unk2 = packet.Translator.ReadInt32();
                packet.AddValue("Unknown", unk1 - unk2, i);

                var opcode = Opcodes.GetOpcode(packet.Translator.ReadInt32(), Direction.ServerToClient);
                packet.AddValue("Opcode", opcode);
                switch (opcode)
                {
                    case Opcode.SMSG_SPELL_HEAL_LOG:
                    {
                        ReadSpellHealLog(packet, i);
                        break;
                    }
                    case Opcode.SMSG_SPELL_ENERGIZE_LOG:
                    {
                        ReadSpellEnergizeLog(packet, i);
                        break;
                    }
                    case Opcode.SMSG_SPELL_PERIODIC_AURA_LOG:
                    {
                        ReadPeriodicAuraLog(packet, i); // sub_5EEE10
                        break;
                    }
                    case Opcode.SMSG_SPELL_EXECUTE_LOG:
                    {
                        ReadSpellLogExecute(packet, i);
                        break;
                    }
                    case Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG:
                    {
                        ReadSpellNonMeleeDamageLog(packet, i);
                        break;
                    }
                    case Opcode.SMSG_SPELL_MISS_LOG:
                    {
                        ReadSpellMissLog(packet, i);
                        break;
                    }
                    case Opcode.SMSG_SPELL_STEAL_LOG:
                    case Opcode.SMSG_SPELL_DISPELL_LOG:
                    case Opcode.SMSG_SPELL_BREAK_LOG:
                    {
                        ReadSpellRemoveLog(packet, i);
                        break;
                    }
                    default:
                        throw new InvalidDataException("Unknown Spell Log Type/Opcode: " + opcode);
                }
            }
        }

        [Parser(Opcode.SMSG_SPELL_STEAL_LOG)]
        [Parser(Opcode.SMSG_SPELL_DISPELL_LOG)]
        [Parser(Opcode.SMSG_SPELL_BREAK_LOG)]
        public static void HandleSpellRemoveLog(Packet packet)
        {
            ReadSpellRemoveLog(packet);
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            ReadPeriodicAuraLog(packet);
        }

        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            ReadSpellNonMeleeDamageLog(packet);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            ReadSpellHealLog(packet);
        }

        [Parser(Opcode.SMSG_SPELL_ENERGIZE_LOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            ReadSpellEnergizeLog(packet);
        }

        [Parser(Opcode.SMSG_SPELL_MISS_LOG)]
        public static void HandleSpellLogMiss(Packet packet)
        {
            ReadSpellMissLog(packet);
        }

        [Parser(Opcode.SMSG_SPELL_EXECUTE_LOG)]
        public static void HandleSpellLogExecute(Packet packet)
        {
            ReadSpellLogExecute(packet);
        }

        // Unknown opcode name(s)
        private static void ReadSpellRemoveLog(Packet packet, object index = null)
        {
            packet.Translator.ReadPackedGuid("Target GUID", index);
            packet.Translator.ReadPackedGuid("Caster GUID", index); // Can be 0
            packet.Translator.ReadInt32<SpellId>("Spell", index); // Can be 0
            var debug = packet.Translator.ReadBool("Debug Output", index);
            var count = packet.Translator.ReadInt32("Count", index);

            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadInt32<SpellId>("Spell", index, i);
                packet.Translator.ReadByte("Unknown Byte/Bool", index, i);
            }

            if (debug)
            {
                packet.Translator.ReadInt32("Unk int32");
                packet.Translator.ReadInt32("Unk int32");
            }
        }

        private static void ReadSpellLogExecute(Packet packet, object index = null)
        {
            packet.Translator.ReadPackedGuid("Caster GUID", index);
            packet.Translator.ReadInt32<SpellId>("Spell ID", index);
            var count = packet.Translator.ReadInt32("Count", index); // v47

            for (int i = 0; i < count; i++)
            {
                var type = packet.Translator.ReadInt32E<SpellEffect>("Spell Effect", index, i);
                var count2 = packet.Translator.ReadInt32("Count", index, i);
                for (int j = 0; j < count2; j++)
                {
                    switch (type)
                    {
                        case SpellEffect.PowerDrain:
                        case SpellEffect.PowerBurn:
                        {
                            packet.Translator.ReadPackedGuid("Target GUID", index, i, j);
                            packet.Translator.ReadInt32("Power taken", index, i, j);
                            packet.Translator.ReadInt32("Power type", index, i, j);
                            packet.Translator.ReadSingle("Multiplier", index, i, j);
                            break;
                        }
                        case SpellEffect.AddExtraAttacks:
                        {
                            packet.Translator.ReadPackedGuid("Target GUID", index, i, j);
                            packet.Translator.ReadInt32("Amount", index, i, j);
                            break;
                        }
                        case SpellEffect.InterruptCast:
                        {
                            packet.Translator.ReadPackedGuid("Target GUID", index, i, j);
                            packet.Translator.ReadInt32<SpellId>("Interrupted Spell ID", index, i, j);
                            break;
                        }
                        case SpellEffect.DurabilityDamage:
                        {
                            packet.Translator.ReadPackedGuid("Target GUID", index, i, j);
                            packet.Translator.ReadInt32<ItemId>("Item", index, i, j);
                            packet.Translator.ReadInt32("Slot", index, i, j);
                            break;
                        }
                        case SpellEffect.OpenLock:
                        {
                            packet.Translator.ReadPackedGuid("Target", i, j);
                            break;
                        }
                        case SpellEffect.CreateItem:
                        case SpellEffect.CreateRandomItem:
                        case SpellEffect.CreateItem2:
                        {
                            packet.Translator.ReadInt32<ItemId>("Created Item", index, i, j);
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
                            var guid = packet.Translator.ReadPackedGuid("Summoned GUID", index, i, j);

                            WoWObject obj;
                            if (Storage.Objects.TryGetValue(guid, out obj))
                                obj.ForceTemporarySpawn = true;

                            break;
                        }
                        case SpellEffect.FeedPet:
                        {
                            packet.Translator.ReadInt32("Unknown Int32", index, i, j);
                            break;
                        }
                        case SpellEffect.DismissPet:
                        {
                            packet.Translator.ReadPackedGuid("GUID", index, i, j);
                            break;
                        }
                        case SpellEffect.Resurrect:
                        case SpellEffect.ResurrectNew:
                        case SpellEffect.RessurectAOE:
                        {
                            packet.Translator.ReadPackedGuid("GUID", index, i, j);
                            break;
                        }
                        default:
                            throw new InvalidDataException("Unknown Spell Effect: " + type);
                    }
                }
            }
        }

        private static void ReadPeriodicAuraLog(Packet packet, object index = null)
        {
            packet.Translator.ReadPackedGuid("Target GUID", index);
            packet.Translator.ReadPackedGuid("Caster GUID", index);
            packet.Translator.ReadInt32<SpellId>("Spell ID", index);
            var count = packet.Translator.ReadInt32("Count", index);

            for (var i = 0; i < count; i++)
            {
                var aura = packet.Translator.ReadUInt32E<AuraType>("Aura Type", index);
                switch (aura)
                {
                    case AuraType.PeriodicDamage:
                    case AuraType.PeriodicDamagePercent:
                    {
                        packet.Translator.ReadUInt32("Damage", index);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                            packet.Translator.ReadUInt32("Over damage", index);

                        packet.Translator.ReadUInt32("Spell Proto", index);
                        packet.Translator.ReadUInt32("Absorb", index);
                        packet.Translator.ReadUInt32("Resist", index);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                            packet.Translator.ReadByte("Critical", index);

                        break;
                    }
                    case AuraType.PeriodicHeal:
                    case AuraType.ObsModHealth:
                    {
                        packet.Translator.ReadUInt32("Damage", index);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                            packet.Translator.ReadUInt32("Over damage", index);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                            // no idea when this was added exactly
                            packet.Translator.ReadUInt32("Absorb", index);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                            packet.Translator.ReadByte("Critical", index);

                        break;
                    }
                    case AuraType.ObsModPower:
                    case AuraType.PeriodicEnergize:
                    {
                        packet.Translator.ReadInt32E<PowerType>("Power type", index);
                        packet.Translator.ReadUInt32("Amount", index);
                        break;
                    }
                    case AuraType.PeriodicManaLeech:
                    {
                        packet.Translator.ReadInt32E<PowerType>("Power type", index);
                        packet.Translator.ReadUInt32("Amount", index);
                        packet.Translator.ReadSingle("Gain multiplier", index);
                        break;
                    }
                }
            }
        }

        private static void ReadSpellNonMeleeDamageLog(Packet packet, object index = null)
        {
            packet.Translator.ReadPackedGuid("Target GUID", index);
            packet.Translator.ReadPackedGuid("Caster GUID", index);
            packet.Translator.ReadUInt32<SpellId>("Spell ID", index);
            packet.Translator.ReadUInt32("Damage", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.Translator.ReadInt32("Overkill", index);

            packet.Translator.ReadByte("SchoolMask", index);
            packet.Translator.ReadUInt32("Absorb", index);
            packet.Translator.ReadUInt32("Resist", index);
            packet.Translator.ReadBool("Show spellname in log", index);
            packet.Translator.ReadByte("Unk byte", index);
            packet.Translator.ReadUInt32("Blocked", index);
            var type = packet.Translator.ReadInt32E<SpellHitType>("HitType", index);

            if (packet.Translator.ReadBool("Debug output", index))
            {
                if (!type.HasAnyFlag(SpellHitType.SPELL_HIT_TYPE_UNK4))
                {
                    if (type.HasAnyFlag(SpellHitType.SPELL_HIT_TYPE_UNK1))
                    {
                        packet.Translator.ReadSingle("Unk float 1 1");
                        packet.Translator.ReadSingle("Unk float 1 2");
                    }

                    if (type.HasAnyFlag(SpellHitType.SPELL_HIT_TYPE_UNK3))
                    {
                        packet.Translator.ReadSingle("Unk float 3 1");
                        packet.Translator.ReadSingle("Unk float 3 2");
                    }

                    if (type.HasAnyFlag(SpellHitType.SPELL_HIT_TYPE_UNK6))
                    {
                        packet.Translator.ReadSingle("Unk float 6 1");
                        packet.Translator.ReadSingle("Unk float 6 2");
                        packet.Translator.ReadSingle("Unk float 6 3");
                        packet.Translator.ReadSingle("Unk float 6 4");
                        packet.Translator.ReadSingle("Unk float 6 5");
                        packet.Translator.ReadSingle("Unk float 6 6");
                    }
                }
            }
        }

        private static void ReadSpellHealLog(Packet packet, object index = null)
        {
            packet.Translator.ReadPackedGuid("Target GUID", index);
            packet.Translator.ReadPackedGuid("Caster GUID", index);
            packet.Translator.ReadUInt32<SpellId>("Spell ID", index);
            packet.Translator.ReadUInt32("Damage", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.Translator.ReadUInt32("Overheal", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183)) // no idea when this was added exactly
                packet.Translator.ReadUInt32("Absorb", index);

            packet.Translator.ReadBool("Critical", index);

            if (packet.Translator.ReadBool("Debug output", index))
            {
                packet.Translator.ReadSingle("Unk float", index);
                packet.Translator.ReadSingle("Unk float 2", index);
            }
        }

        private static void ReadSpellEnergizeLog(Packet packet, object index = null)
        {
            packet.Translator.ReadPackedGuid("Target GUID", index);
            packet.Translator.ReadPackedGuid("Caster GUID", index);
            packet.Translator.ReadUInt32<SpellId>("Spell ID", index);
            packet.Translator.ReadUInt32E<PowerType>("Power type", index);
            packet.Translator.ReadInt32("Amount", index);
        }

        private static void ReadSpellMissLog(Packet packet, object index = null)
        {
            packet.Translator.ReadUInt32<SpellId>("Spell ID", index);
            packet.Translator.ReadGuid("Caster GUID", index);
            var debug = packet.Translator.ReadBool("Debug output", index);

            var count = packet.Translator.ReadUInt32("Target Count", index);
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadGuid("Target GUID", index);
                packet.Translator.ReadByteE<SpellMissType>("Miss Info", index);
                if (debug)
                {
                    packet.Translator.ReadSingle("Unk float");
                    packet.Translator.ReadSingle("Unk float");
                }
            }
        }

        [Parser(Opcode.SMSG_SPELL_DAMAGE_SHIELD)]
        public static void ReadSpellDamageShield(Packet packet)
        {
            packet.Translator.ReadGuid("Victim");
            packet.Translator.ReadGuid("Caster");
            packet.Translator.ReadUInt32<SpellId>("Spell Id");
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadInt32("Overkill");
            packet.Translator.ReadInt32("SpellSchoolMask");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596))
                packet.Translator.ReadInt32("Resisted Damage");
        }
    }
}
