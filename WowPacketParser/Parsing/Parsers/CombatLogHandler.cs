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
            var count = packet.ReadInt32("Count");
            var unk1 = packet.ReadInt32();

            for (var i = 0; i < count; i++)
            {
                var unk2 = packet.ReadInt32();
                packet.AddValue("Unknown", unk1 - unk2, i);

                var opcode = Opcodes.GetOpcode(packet.ReadInt32(), Direction.ServerToClient);
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
            packet.ReadPackedGuid("Target GUID", index);
            packet.ReadPackedGuid("Caster GUID", index); // Can be 0
            packet.ReadInt32<SpellId>("Spell", index); // Can be 0
            var debug = packet.ReadBool("Debug Output", index);
            var count = packet.ReadInt32("Count", index);

            for (int i = 0; i < count; i++)
            {
                packet.ReadInt32<SpellId>("Spell", index, i);
                packet.ReadByte("Unknown Byte/Bool", index, i);
            }

            if (debug)
            {
                packet.ReadInt32("Unk int32");
                packet.ReadInt32("Unk int32");
            }
        }

        private static void ReadSpellLogExecute(Packet packet, object index = null)
        {
            packet.ReadPackedGuid("Caster GUID", index);
            packet.ReadInt32<SpellId>("Spell ID", index);
            var count = packet.ReadInt32("Count", index); // v47

            for (int i = 0; i < count; i++)
            {
                var type = packet.ReadInt32E<SpellEffect>("Spell Effect", index, i);
                var count2 = packet.ReadInt32("Count", index, i);
                for (int j = 0; j < count2; j++)
                {
                    switch (type)
                    {
                        case SpellEffect.PowerDrain:
                        case SpellEffect.PowerBurn:
                        {
                            packet.ReadPackedGuid("Target GUID", index, i, j);
                            packet.ReadInt32("Power taken", index, i, j);
                            packet.ReadInt32("Power type", index, i, j);
                            packet.ReadSingle("Multiplier", index, i, j);
                            break;
                        }
                        case SpellEffect.AddExtraAttacks:
                        {
                            packet.ReadPackedGuid("Target GUID", index, i, j);
                            packet.ReadInt32("Amount", index, i, j);
                            break;
                        }
                        case SpellEffect.InterruptCast:
                        {
                            packet.ReadPackedGuid("Target GUID", index, i, j);
                            packet.ReadInt32<SpellId>("Interrupted Spell ID", index, i, j);
                            break;
                        }
                        case SpellEffect.DurabilityDamage:
                        {
                            packet.ReadPackedGuid("Target GUID", index, i, j);
                            packet.ReadInt32<ItemId>("Item", index, i, j);
                            packet.ReadInt32("Slot", index, i, j);
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
                            packet.ReadInt32<ItemId>("Created Item", index, i, j);
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
                            var guid = packet.ReadPackedGuid("Summoned GUID", index, i, j);

                            WoWObject obj;
                            if (Storage.Objects.TryGetValue(guid, out obj))
                                obj.ForceTemporarySpawn = true;

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

        private static void ReadPeriodicAuraLog(Packet packet, object index = null)
        {
            packet.ReadPackedGuid("Target GUID", index);
            packet.ReadPackedGuid("Caster GUID", index);
            packet.ReadInt32<SpellId>("Spell ID", index);
            var count = packet.ReadInt32("Count", index);

            for (var i = 0; i < count; i++)
            {
                var aura = packet.ReadUInt32E<AuraType>("Aura Type", index);
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
                        packet.ReadInt32E<PowerType>("Power type", index);
                        packet.ReadUInt32("Amount", index);
                        break;
                    }
                    case AuraType.PeriodicManaLeech:
                    {
                        packet.ReadInt32E<PowerType>("Power type", index);
                        packet.ReadUInt32("Amount", index);
                        packet.ReadSingle("Gain multiplier", index);
                        break;
                    }
                }
            }
        }

        private static void ReadSpellNonMeleeDamageLog(Packet packet, object index = null)
        {
            packet.ReadPackedGuid("Target GUID", index);
            packet.ReadPackedGuid("Caster GUID", index);
            packet.ReadUInt32<SpellId>("Spell ID", index);
            packet.ReadUInt32("Damage", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.ReadInt32("Overkill", index);

            packet.ReadByte("SchoolMask", index);
            packet.ReadUInt32("Absorb", index);
            packet.ReadUInt32("Resist", index);
            packet.ReadBool("Show spellname in log", index);
            packet.ReadByte("Unk byte", index);
            packet.ReadUInt32("Blocked", index);
            var type = packet.ReadInt32E<SpellHitType>("HitType", index);

            if (packet.ReadBool("Debug output", index))
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

        private static void ReadSpellHealLog(Packet packet, object index = null)
        {
            packet.ReadPackedGuid("Target GUID", index);
            packet.ReadPackedGuid("Caster GUID", index);
            packet.ReadUInt32<SpellId>("Spell ID", index);
            packet.ReadUInt32("Damage", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.ReadUInt32("Overheal", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183)) // no idea when this was added exactly
                packet.ReadUInt32("Absorb", index);

            packet.ReadBool("Critical", index);

            if (packet.ReadBool("Debug output", index))
            {
                packet.ReadSingle("Unk float", index);
                packet.ReadSingle("Unk float 2", index);
            }
        }

        private static void ReadSpellEnergizeLog(Packet packet, object index = null)
        {
            packet.ReadPackedGuid("Target GUID", index);
            packet.ReadPackedGuid("Caster GUID", index);
            packet.ReadUInt32<SpellId>("Spell ID", index);
            packet.ReadUInt32E<PowerType>("Power type", index);
            packet.ReadInt32("Amount", index);
        }

        private static void ReadSpellMissLog(Packet packet, object index = null)
        {
            packet.ReadUInt32<SpellId>("Spell ID", index);
            packet.ReadGuid("Caster GUID", index);
            var debug = packet.ReadBool("Debug output", index);

            var count = packet.ReadUInt32("Target Count", index);
            for (var i = 0; i < count; ++i)
            {
                packet.ReadGuid("Target GUID", index);
                packet.ReadByteE<SpellMissType>("Miss Info", index);
                if (debug)
                {
                    packet.ReadSingle("Unk float");
                    packet.ReadSingle("Unk float");
                }
            }
        }

        [Parser(Opcode.SMSG_SPELL_DAMAGE_SHIELD)]
        public static void ReadSpellDamageShield(Packet packet)
        {
            packet.ReadGuid("Victim");
            packet.ReadGuid("Caster");
            packet.ReadUInt32<SpellId>("Spell Id");
            packet.ReadInt32("Damage");
            packet.ReadInt32("Overkill");
            packet.ReadInt32("SpellSchoolMask");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596))
                packet.ReadInt32("Resisted Damage");
        }
    }
}
