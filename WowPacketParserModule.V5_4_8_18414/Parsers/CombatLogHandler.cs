using System;
using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using SpellHitInfo = WowPacketParser.Enums.SpellHitInfo;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_COMBAT_LOG_MULTIPLE)]
        public static void HandleCombatLogMultiple(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_PERIODICAURALOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELLBREAKLOG)]
        public static void HandleSpellBreakLog(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELLDAMAGESHIELD)]
        public static void HandleSpellDamageShield(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELLDISPELLOG)]
        public static void HandleSpellDispellLog(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELLENERGIZELOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELLHEALLOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELLLOGEXECUTE)]
        public static void HandleSpellLogExecute(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELLLOGMISS)]
        public static void HandleSpellLogMiss(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELLNONMELEEDAMAGELOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            ReadSpellNonMeleeDamageLog(ref packet);
        }

        [Parser(Opcode.SMSG_SPELLSTEALLOG)]
        public static void HandleSpellStealLog(Packet packet)
        {
            packet.ReadToEnd();
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
    }
}
