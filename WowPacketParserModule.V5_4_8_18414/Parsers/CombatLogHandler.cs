using System;
using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using SpellHitInfo = WowPacketParser.Enums.SpellHitInfo;

namespace WowPacketParser.V5_4_8_18414.Parsers
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
                packet.WriteLine("["+ i+ "] Unknown: {0}", unk1 - unk2);

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
            //ReadPeriodicAuraLog(ref packet);
            packet.AsHex();
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELLNONMELEEDAMAGELOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            ReadSpellNonMeleeDamageLog(ref packet);
        }

        [Parser(Opcode.SMSG_SPELLHEALLOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            //ReadSpellHealLog(ref packet);
            packet.AsHex();
            packet.ReadToEnd();
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
            packet.ReadToEnd();
        }

        // Unknown opcode name(s)
        private static void ReadSpellRemoveLog(ref Packet packet, int index = -1)
        {
            packet.ReadPackedGuid("Target GUID", index);
            packet.ReadPackedGuid("Caster GUID", index); // Can be 0
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell", index); // Can be 0
            var debug = packet.ReadBoolean("Debug Output", index);
            var count = packet.ReadInt32("Count", index);

            for (int i = 0; i < count; i++)
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell", index, i);
                packet.ReadByte("Unknown Byte/Bool", index, i);
            }

            if (debug)
            {
                packet.ReadInt32("Unk int32");
                packet.ReadInt32("Unk int32");
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
                            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Interrupted Spell ID", index, i, j);
                            break;
                        }
                        case SpellEffect.DurabilityDamage:
                        {
                            packet.ReadPackedGuid("Target GUID", index, i, j);
                            packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item", index, i, j);
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
            var guid = new byte[8];
            var guid2 = new byte[8];
            guid2[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var unk92 = packet.ReadBit("unk92", index);
            guid[0] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            var unk20 = packet.ReadBit("unk20", index);
            var unk128 = packet.ReadBit("unk128", index);
            guid2[1] = packet.ReadBit();
            var unk68 = packet.ReadBit("unk68", index);
            var unk3ch = false;
            var unk28h = false;
            var unk24h = false;
            var unk30h = false;
            var unk2ch = false;
            var unk20h = false;
            var unk1ch = false;
            var unk34h = false;
            var unk38h = false;
            var unk40h = false;
            if (unk68)
            {
                unk3ch = !packet.ReadBit("!unk3ch", index);
                unk28h = !packet.ReadBit("!unk28h", index);
                unk24h = !packet.ReadBit("!unk24h", index);
                unk30h = !packet.ReadBit("!unk30h", index);
                unk2ch = !packet.ReadBit("!unk2ch", index);
                unk20h = !packet.ReadBit("!unk20h", index);
                unk1ch = !packet.ReadBit("!unk1ch", index);
                unk34h = !packet.ReadBit("!unk34h", index);
                unk38h = !packet.ReadBit("!unk38h", index);
                unk40h = !packet.ReadBit("!unk40h", index);
            }
            guid2[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            var unk112 = 0u;
            if (unk128)
                unk112 = packet.ReadBits("unk112", 21, index);

            guid2[4] = packet.ReadBit();

            packet.ReadInt32("unk140", index);

            if (unk68)
            {
                if (unk30h)
                    packet.ReadSingle("unk30h", index);
                if (unk20h)
                    packet.ReadSingle("unk20h", index);
                if (unk3ch)
                    packet.ReadSingle("unk3ch", index);
                if (unk2ch)
                    packet.ReadSingle("unk2ch", index);
                if (unk34h)
                    packet.ReadSingle("unk34h", index);
                if (unk24h)
                    packet.ReadSingle("unk24h", index);
                if (unk1ch)
                    packet.ReadSingle("unk1ch", index);
                if (unk40h)
                    packet.ReadSingle("unk40h", index);
                if (unk28h)
                    packet.ReadSingle("unk28h", index);
                if (unk38h)
                    packet.ReadSingle("unk38h", index);
            }

            packet.ParseBitStream(guid, 1);
            packet.ReadInt32("unk16", index);
            packet.ParseBitStream(guid2, 3);
            packet.ParseBitStream(guid, 0);
            packet.ParseBitStream(guid2, 6);
            packet.ParseBitStream(guid2, 4);
            packet.ParseBitStream(guid, 7);
            packet.ReadInt32("unk136", index);
            packet.ReadInt32("unk96", index);

            if (unk128)
            {
                packet.ReadInt32("unk100", index);
                for (var i = 0; i < unk112; i++)
                {
                    packet.ReadInt32("unk116", index, i);
                    packet.ReadInt32("unk120", index, i);
                }
                packet.ReadInt32("unk108", index);
                packet.ReadInt32("unk104", index);
            }
            packet.ParseBitStream(guid, 5);
            packet.ParseBitStream(guid2, 5);
            packet.ParseBitStream(guid, 3);
            packet.ParseBitStream(guid, 2);
            packet.ParseBitStream(guid2, 2);
            packet.ParseBitStream(guid, 6);
            packet.ParseBitStream(guid2, 0);
            packet.ParseBitStream(guid, 4);
            packet.ReadInt32("unk24", index);
            packet.ReadByte("unk132", index);
            packet.ParseBitStream(guid2, 7);
            packet.ReadInt32("unk72", index);
            packet.ParseBitStream(guid2, 1);
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", index);

            packet.WriteGuid("Caster", guid, index);
            packet.WriteGuid("Target", guid2, index);
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

            if (packet.ReadBoolean("Debug output", index))
            {
                packet.ReadSingle("Unk float", index);
                packet.ReadSingle("Unk float 2", index);
            }
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
            var debug = packet.ReadBoolean("Debug output", index);

            var count = packet.ReadUInt32("Target Count", index);
            for (var i = 0; i < count; ++i)
            {
                packet.ReadGuid("Target GUID", index);
                packet.ReadEnum<SpellMissType>("Miss Info", TypeCode.Byte, index);
                if (debug)
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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596))
                packet.ReadInt32("Resisted Damage");
        }
    }
}
