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

namespace WowPacketParser.V5_4_1_17538.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_LEARNED_SPELL)]
        public static void HandleLearnSpell(Packet packet)
        {
            packet.ReadBit("Unk Bits");

            var count = packet.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.CMSG_LEARN_TALENT)]
        public static void HandleLearnTalents(Packet packet)
        {
            var talentCount = packet.ReadBits("Learned Talent Count", 23);

            for (int i = 0; i < talentCount; i++)
                packet.ReadUInt16("Talent Id", i);
        }

        [Parser(Opcode.SMSG_TALENTS_INFO)]
        public static void ReadTalentInfo510(Packet packet)
        {
            var specCount = packet.ReadBits("Spec Group count", 19);

            var spentTalents = new uint[specCount];

            for (var i = 0; i < specCount; ++i)
                spentTalents[i] = packet.ReadBits("Spec Talent Count", 23, i);

            for (var i = 0; i < specCount; ++i)
            {
                for (var j = 0; j < 6; ++j)
                    packet.ReadUInt16("Glyph", i, j);

                for (var j = 0; j < spentTalents[i]; ++j)
                    packet.ReadUInt16("Talent Id", i, j);

                packet.ReadUInt32("Spec Id", i);
            }

            packet.ReadByte("Active Spec Group");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            var guid6 = new byte[8];
            var guid7 = new byte[8];
            var powerGUID = new byte[8];
            byte[][] guid8;
            byte[][] guid9;
            byte[][] guid10;

            var bit151 = !packet.ReadBit();
            var bit198 = !packet.ReadBit();
            var bit194 = !packet.ReadBit();

            guid2[5] = packet.ReadBit();

            var bits140 = (int)packet.ReadBits(21);

            guid2[7] = packet.ReadBit();

            var bits54 = (int)packet.ReadBits(25);

            guid1[7] = packet.ReadBit();

            var bit180 = !packet.ReadBit();
            var bits184 = (int)packet.ReadBits(20);

            guid2[0] = packet.ReadBit();

            packet.ReadBit(); // fake bit

            var hasPowerData = packet.ReadBit();

            var PowerTypeCount = 0u;
            if (hasPowerData)
            {
                powerGUID[7] = packet.ReadBit();
                powerGUID[3] = packet.ReadBit();
                powerGUID[1] = packet.ReadBit();
                powerGUID[5] = packet.ReadBit();
                powerGUID[4] = packet.ReadBit();
                powerGUID[2] = packet.ReadBit();
                powerGUID[0] = packet.ReadBit();
                PowerTypeCount = packet.ReadBits(21);
                powerGUID[6] = packet.ReadBit();
            }

            var bit1AC = !packet.ReadBit();
            var bit164 = !packet.ReadBit();
            var bit150 = !packet.ReadBit();
            
            guid8 = new byte[bits184][];
            for (var i = 0; i < bits184; ++i)
            {
                guid8[i] = new byte[8];
                packet.StartBitStream(guid8[i], 3, 1, 7, 6, 2, 4, 5, 0);
            }

            var bit170 = !packet.ReadBit();

            guid1[2] = packet.ReadBit();

            var bits34 = (int)packet.ReadBits(24);
            
            for (var i = 0; i < bits54; ++i)
            {
                if (packet.ReadBits("bits22[0]", 4, i) == 11)
                    packet.ReadBits("bits22[1]", 4, i);
            }

            guid1[3] = packet.ReadBit();

            guid9 = new byte[bits184][];
            for (var i = 0; i < bits34; ++i)
            {
                guid9[i] = new byte[8];
                packet.StartBitStream(guid9[i], 6, 1, 5, 2, 7, 0, 4, 3);
            }

            guid2[6] = packet.ReadBit();
            var bits2C = (int)packet.ReadBits(13);
            guid1[4] = packet.ReadBit();
            var bit1A8 = !packet.ReadBit();
            guid1[0] = packet.ReadBit();
            var bit17C = packet.ReadBit();
            var bit98 = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid7[4] = packet.ReadBit();
            guid7[5] = packet.ReadBit();
            guid7[6] = packet.ReadBit();
            guid7[7] = packet.ReadBit();
            guid7[3] = packet.ReadBit();
            guid7[1] = packet.ReadBit();
            guid7[2] = packet.ReadBit();
            guid7[0] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            var bits154 = (int)packet.ReadBits(3);

            var bitB8 = packet.ReadBit();
            if (bitB8)
            {
                guid6[4] = packet.ReadBit();
                guid6[1] = packet.ReadBit();
                guid6[6] = packet.ReadBit();
                guid6[2] = packet.ReadBit();
                guid6[3] = packet.ReadBit();
                guid6[0] = packet.ReadBit();
                guid6[7] = packet.ReadBit();
                guid6[5] = packet.ReadBit();
            }

            if (bit98)
            {
                guid5[4] = packet.ReadBit();
                guid5[5] = packet.ReadBit();
                guid5[6] = packet.ReadBit();
                guid5[0] = packet.ReadBit();
                guid5[7] = packet.ReadBit();
                guid5[1] = packet.ReadBit();
                guid5[2] = packet.ReadBit();
                guid5[3] = packet.ReadBit();
            }

            guid2[1] = packet.ReadBit();
            var bit16C = !packet.ReadBit();
            guid2[4] = packet.ReadBit();
            var bitC0 = !packet.ReadBit();

            var bitsC0 = 0u;
            if (bitC0)
                bitsC0 = packet.ReadBits(7);

            var bit68 = !packet.ReadBit();
            var bits44 = (int)packet.ReadBits(24);
            
            guid10 = new byte[bits44][];
            for (var i = 0; i < bits44; ++i)
            {
                guid10[i] = new byte[8];
                packet.StartBitStream(guid10[i], 4, 1, 7, 5, 3, 6, 2, 0);
            }

            guid1[6] = packet.ReadBit();
            guid1[5] = packet.ReadBit();

            packet.ReadBit(); // fake bit

            guid3[7] = packet.ReadBit();
            guid3[5] = packet.ReadBit();
            guid3[3] = packet.ReadBit();
            guid3[6] = packet.ReadBit();
            guid3[1] = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            guid3[2] = packet.ReadBit();

            var bits68 = 0u;
            if (bit68)
                bits68 = packet.ReadBits(20);

            packet.ReadBit(); // fake bit

            guid4[0] = packet.ReadBit();
            guid4[1] = packet.ReadBit();
            guid4[2] = packet.ReadBit();
            guid4[5] = packet.ReadBit();
            guid4[4] = packet.ReadBit();
            guid4[7] = packet.ReadBit();
            guid4[3] = packet.ReadBit();
            guid4[6] = packet.ReadBit();
            var bit168 = !packet.ReadBit();

            guid2[3] = packet.ReadBit();

            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 1);

            if (bit98)
            {
                packet.ReadXORByte(guid5, 1);
                packet.ReadXORByte(guid5, 3);
                packet.ReadSingle("Float90");
                packet.ReadXORByte(guid5, 7);
                packet.ReadXORByte(guid5, 5);
                packet.ReadSingle("Float88");
                packet.ReadXORByte(guid5, 2);
                packet.ReadXORByte(guid5, 0);
                packet.ReadXORByte(guid5, 4);
                packet.ReadSingle("Float8C");
                packet.ReadXORByte(guid5, 6);
                packet.WriteGuid("Guid10", guid5);
            }
            
            for (var i = 0; i < bits44; ++i)
            {
                packet.ParseBitStream(guid10[i], 7, 1, 6, 4, 5, 2, 0, 3);
                packet.WriteGuid("Guid9", guid10[9]);
            }
            
            for (var i = 0; i < bits184; ++i)
            {
                packet.ReadXORByte(guid8[i], 5);
                packet.ReadXORByte(guid8[i], 2);

                packet.ReadSingle("Float188");
                packet.ReadSingle("Float188");

                packet.ReadXORByte(guid8[i], 7);

                packet.ReadSingle("Float188");

                packet.ReadXORByte(guid8[i], 3);
                packet.ReadXORByte(guid8[i], 1);
                packet.ReadXORByte(guid8[i], 6);
                packet.ReadXORByte(guid8[i], 4);
                packet.ReadXORByte(guid8[i], 0);
                packet.WriteGuid("Guid31", guid8[i]);
            }

            if (bit170)
                packet.ReadByte("Byte170");

            packet.ReadXORByte(guid3, 4);
            packet.ReadXORByte(guid3, 2);
            packet.ReadXORByte(guid3, 5);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid3, 3);
            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid3, 0);
            
            for (var i = 0; i < bits34; ++i)
            {
                packet.ParseBitStream(guid9[i], 2, 4, 5, 3, 6, 7, 1);
                packet.WriteGuid("Guid7", guid9[i]);
            }

            if (bitB8)
            {
                packet.ReadSingle("FloatA8");
                packet.ReadXORByte(guid6, 0);
                packet.ReadSingle("FloatB0");
                packet.ReadXORByte(guid6, 7);
                packet.ReadXORByte(guid6, 4);
                packet.ReadXORByte(guid6, 1);
                packet.ReadXORByte(guid6, 5);
                packet.ReadXORByte(guid6, 3);
                packet.ReadXORByte(guid6, 2);
                packet.ReadSingle("FloatAC");
                packet.ReadXORByte(guid6, 6);
                packet.WriteGuid("Guid14", guid6);
            }

            if (bit164)
                packet.ReadInt32("Int164");

            if (bit151)
                packet.ReadByte("Byte151");

            packet.ReadXORByte(guid7, 3);
            packet.ReadXORByte(guid7, 7);
            packet.ReadXORByte(guid7, 0);
            packet.ReadXORByte(guid7, 2);
            packet.ReadXORByte(guid7, 1);
            packet.ReadXORByte(guid7, 6);
            packet.ReadXORByte(guid7, 4);
            packet.ReadXORByte(guid7, 5);

            if (hasPowerData)
            {
                packet.ReadInt32("Int1B8");
                packet.ReadXORByte(powerGUID, 7);
                packet.ReadXORByte(powerGUID, 2);
                for (var i = 0; i < PowerTypeCount; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int1BC");
                packet.ReadInt32("Int1C0");

                packet.ReadXORByte(powerGUID, 6);
                packet.ReadXORByte(powerGUID, 4);
                packet.ReadXORByte(powerGUID, 5);
                packet.ReadXORByte(powerGUID, 0);
                packet.ReadXORByte(powerGUID, 1);
                packet.ReadXORByte(powerGUID, 3);

                packet.WriteGuid("Guid36", powerGUID);
            }

            packet.ReadXORByte(guid1, 2);
            if (bit150)
                packet.ReadByte("Byte150");
            packet.ReadXORByte(guid4, 1);
            packet.ReadXORByte(guid4, 7);
            packet.ReadXORByte(guid4, 6);
            packet.ReadXORByte(guid4, 5);
            packet.ReadXORByte(guid4, 2);
            packet.ReadXORByte(guid4, 3);
            packet.ReadXORByte(guid4, 0);
            packet.ReadXORByte(guid4, 4);
            packet.ReadXORByte(guid2, 1);
            packet.ReadWoWString("StringC0", bitsC0);
            for (var i = 0; i < bits140; ++i)
            {
                packet.ReadByte("ByteED", i);
                packet.ReadInt32("IntED", i);
            }

            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            if (bit1AC)
                packet.ReadByte("Byte1AC");
            packet.ReadByte("Byte20");
            for (var i = 0; i < bits154; ++i)
                packet.ReadByte("Byte158", i);

            packet.ReadXORByte(guid1, 6);

            if (bit16C)
                packet.ReadInt32("Int16C");

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 4);

            if (bit168)
                packet.ReadSingle("Float168");

            if (bit17C)
            {
                packet.ReadInt32("Int174");
                packet.ReadInt32("Int178");
            }

            packet.ReadInt32("Int30");

            packet.ReadXORByte(guid1, 3);

            if (bit194)
                packet.ReadInt32("Int194");

            if (bit180)
                packet.ReadByte("Byte180");

            if (bit198)
                packet.ReadInt32("Int198");

            packet.ReadXORByte(guid2, 7);

            if (bit1A8)
                packet.ReadInt32("Int1A8");

            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadInt32("Int28");
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 2);

            packet.WriteGuid("Guid2", guid1);
            packet.WriteGuid("Guid3", guid2);
            packet.WriteGuid("GuidE", guid3);
            packet.WriteGuid("GuidF", guid4);
            packet.WriteGuid("Guid34", guid7);
        }

        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.ReadBit("Unk Bit");
            var count = packet.ReadBits("Spell Count", 22);

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
    }
}
