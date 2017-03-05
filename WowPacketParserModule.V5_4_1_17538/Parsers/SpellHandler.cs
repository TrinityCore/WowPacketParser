using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_LEARNED_SPELLS)]
        public static void HandleLearnedSpells(Packet packet)
        {
            packet.Translator.ReadBit("SuppressMessaging");

            var count = packet.Translator.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32<SpellId>("Spell ID", i);
        }

        [Parser(Opcode.CMSG_LEARN_TALENT)]
        public static void HandleLearnTalents(Packet packet)
        {
            var talentCount = packet.Translator.ReadBits("Learned Talent Count", 23);

            for (int i = 0; i < talentCount; i++)
                packet.Translator.ReadUInt16("Talent Id", i);
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA)]
        public static void ReadTalentInfo510(Packet packet)
        {
            var specCount = packet.Translator.ReadBits("Spec Group count", 19);

            var spentTalents = new uint[specCount];

            for (var i = 0; i < specCount; ++i)
                spentTalents[i] = packet.Translator.ReadBits("Spec Talent Count", 23, i);

            for (var i = 0; i < specCount; ++i)
            {
                for (var j = 0; j < 6; ++j)
                    packet.Translator.ReadUInt16("Glyph", i, j);

                for (var j = 0; j < spentTalents[i]; ++j)
                    packet.Translator.ReadUInt16("Talent Id", i, j);

                packet.Translator.ReadUInt32("Spec Id", i);
            }

            packet.Translator.ReadByte("Active Spec Group");
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

            var bit151 = !packet.Translator.ReadBit();
            var bit198 = !packet.Translator.ReadBit();
            var bit194 = !packet.Translator.ReadBit();

            guid2[5] = packet.Translator.ReadBit();

            var bits140 = (int)packet.Translator.ReadBits(21);

            guid2[7] = packet.Translator.ReadBit();

            var bits54 = (int)packet.Translator.ReadBits(25);

            guid1[7] = packet.Translator.ReadBit();

            var bit180 = !packet.Translator.ReadBit();
            var bits184 = (int)packet.Translator.ReadBits(20);

            guid2[0] = packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            var hasPowerData = packet.Translator.ReadBit();

            var PowerTypeCount = 0u;
            if (hasPowerData)
            {
                powerGUID[7] = packet.Translator.ReadBit();
                powerGUID[3] = packet.Translator.ReadBit();
                powerGUID[1] = packet.Translator.ReadBit();
                powerGUID[5] = packet.Translator.ReadBit();
                powerGUID[4] = packet.Translator.ReadBit();
                powerGUID[2] = packet.Translator.ReadBit();
                powerGUID[0] = packet.Translator.ReadBit();
                PowerTypeCount = packet.Translator.ReadBits(21);
                powerGUID[6] = packet.Translator.ReadBit();
            }

            var bit1AC = !packet.Translator.ReadBit();
            var bit164 = !packet.Translator.ReadBit();
            var bit150 = !packet.Translator.ReadBit();

            guid8 = new byte[bits184][];
            for (var i = 0; i < bits184; ++i)
            {
                guid8[i] = new byte[8];
                packet.Translator.StartBitStream(guid8[i], 3, 1, 7, 6, 2, 4, 5, 0);
            }

            var bit170 = !packet.Translator.ReadBit();

            guid1[2] = packet.Translator.ReadBit();

            var bits34 = (int)packet.Translator.ReadBits(24);

            for (var i = 0; i < bits54; ++i)
            {
                if (packet.Translator.ReadBits("bits22[0]", 4, i) == 11)
                    packet.Translator.ReadBits("bits22[1]", 4, i);
            }

            guid1[3] = packet.Translator.ReadBit();

            guid9 = new byte[bits184][];
            for (var i = 0; i < bits34; ++i)
            {
                guid9[i] = new byte[8];
                packet.Translator.StartBitStream(guid9[i], 6, 1, 5, 2, 7, 0, 4, 3);
            }

            guid2[6] = packet.Translator.ReadBit();
            var bits2C = (int)packet.Translator.ReadBits(13);
            guid1[4] = packet.Translator.ReadBit();
            var bit1A8 = !packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            var bit17C = packet.Translator.ReadBit();
            var bit98 = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid7[4] = packet.Translator.ReadBit();
            guid7[5] = packet.Translator.ReadBit();
            guid7[6] = packet.Translator.ReadBit();
            guid7[7] = packet.Translator.ReadBit();
            guid7[3] = packet.Translator.ReadBit();
            guid7[1] = packet.Translator.ReadBit();
            guid7[2] = packet.Translator.ReadBit();
            guid7[0] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            var bits154 = (int)packet.Translator.ReadBits(3);

            var bitB8 = packet.Translator.ReadBit();
            if (bitB8)
            {
                guid6[4] = packet.Translator.ReadBit();
                guid6[1] = packet.Translator.ReadBit();
                guid6[6] = packet.Translator.ReadBit();
                guid6[2] = packet.Translator.ReadBit();
                guid6[3] = packet.Translator.ReadBit();
                guid6[0] = packet.Translator.ReadBit();
                guid6[7] = packet.Translator.ReadBit();
                guid6[5] = packet.Translator.ReadBit();
            }

            if (bit98)
            {
                guid5[4] = packet.Translator.ReadBit();
                guid5[5] = packet.Translator.ReadBit();
                guid5[6] = packet.Translator.ReadBit();
                guid5[0] = packet.Translator.ReadBit();
                guid5[7] = packet.Translator.ReadBit();
                guid5[1] = packet.Translator.ReadBit();
                guid5[2] = packet.Translator.ReadBit();
                guid5[3] = packet.Translator.ReadBit();
            }

            guid2[1] = packet.Translator.ReadBit();
            var bit16C = !packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            var bitC0 = !packet.Translator.ReadBit();

            var bitsC0 = 0u;
            if (bitC0)
                bitsC0 = packet.Translator.ReadBits(7);

            var bit68 = !packet.Translator.ReadBit();
            var bits44 = (int)packet.Translator.ReadBits(24);

            guid10 = new byte[bits44][];
            for (var i = 0; i < bits44; ++i)
            {
                guid10[i] = new byte[8];
                packet.Translator.StartBitStream(guid10[i], 4, 1, 7, 5, 3, 6, 2, 0);
            }

            guid1[6] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            guid3[7] = packet.Translator.ReadBit();
            guid3[5] = packet.Translator.ReadBit();
            guid3[3] = packet.Translator.ReadBit();
            guid3[6] = packet.Translator.ReadBit();
            guid3[1] = packet.Translator.ReadBit();
            guid3[4] = packet.Translator.ReadBit();
            guid3[0] = packet.Translator.ReadBit();
            guid3[2] = packet.Translator.ReadBit();

            var bits68 = 0u;
            if (bit68)
                bits68 = packet.Translator.ReadBits(20);

            packet.Translator.ReadBit(); // fake bit

            guid4[0] = packet.Translator.ReadBit();
            guid4[1] = packet.Translator.ReadBit();
            guid4[2] = packet.Translator.ReadBit();
            guid4[5] = packet.Translator.ReadBit();
            guid4[4] = packet.Translator.ReadBit();
            guid4[7] = packet.Translator.ReadBit();
            guid4[3] = packet.Translator.ReadBit();
            guid4[6] = packet.Translator.ReadBit();
            var bit168 = !packet.Translator.ReadBit();

            guid2[3] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 1);

            if (bit98)
            {
                packet.Translator.ReadXORByte(guid5, 1);
                packet.Translator.ReadXORByte(guid5, 3);
                packet.Translator.ReadSingle("Float90");
                packet.Translator.ReadXORByte(guid5, 7);
                packet.Translator.ReadXORByte(guid5, 5);
                packet.Translator.ReadSingle("Float88");
                packet.Translator.ReadXORByte(guid5, 2);
                packet.Translator.ReadXORByte(guid5, 0);
                packet.Translator.ReadXORByte(guid5, 4);
                packet.Translator.ReadSingle("Float8C");
                packet.Translator.ReadXORByte(guid5, 6);
                packet.Translator.WriteGuid("Guid10", guid5);
            }

            for (var i = 0; i < bits44; ++i)
            {
                packet.Translator.ParseBitStream(guid10[i], 7, 1, 6, 4, 5, 2, 0, 3);
                packet.Translator.WriteGuid("Guid9", guid10[9]);
            }

            for (var i = 0; i < bits184; ++i)
            {
                packet.Translator.ReadXORByte(guid8[i], 5);
                packet.Translator.ReadXORByte(guid8[i], 2);

                packet.Translator.ReadSingle("Float188");
                packet.Translator.ReadSingle("Float188");

                packet.Translator.ReadXORByte(guid8[i], 7);

                packet.Translator.ReadSingle("Float188");

                packet.Translator.ReadXORByte(guid8[i], 3);
                packet.Translator.ReadXORByte(guid8[i], 1);
                packet.Translator.ReadXORByte(guid8[i], 6);
                packet.Translator.ReadXORByte(guid8[i], 4);
                packet.Translator.ReadXORByte(guid8[i], 0);
                packet.Translator.WriteGuid("Guid31", guid8[i]);
            }

            if (bit170)
                packet.Translator.ReadByte("Byte170");

            packet.Translator.ReadXORByte(guid3, 4);
            packet.Translator.ReadXORByte(guid3, 2);
            packet.Translator.ReadXORByte(guid3, 5);
            packet.Translator.ReadXORByte(guid3, 6);
            packet.Translator.ReadXORByte(guid3, 3);
            packet.Translator.ReadXORByte(guid3, 1);
            packet.Translator.ReadXORByte(guid3, 7);
            packet.Translator.ReadXORByte(guid3, 0);

            for (var i = 0; i < bits34; ++i)
            {
                packet.Translator.ParseBitStream(guid9[i], 2, 4, 5, 3, 6, 7, 1);
                packet.Translator.WriteGuid("Guid7", guid9[i]);
            }

            if (bitB8)
            {
                packet.Translator.ReadSingle("FloatA8");
                packet.Translator.ReadXORByte(guid6, 0);
                packet.Translator.ReadSingle("FloatB0");
                packet.Translator.ReadXORByte(guid6, 7);
                packet.Translator.ReadXORByte(guid6, 4);
                packet.Translator.ReadXORByte(guid6, 1);
                packet.Translator.ReadXORByte(guid6, 5);
                packet.Translator.ReadXORByte(guid6, 3);
                packet.Translator.ReadXORByte(guid6, 2);
                packet.Translator.ReadSingle("FloatAC");
                packet.Translator.ReadXORByte(guid6, 6);
                packet.Translator.WriteGuid("Guid14", guid6);
            }

            if (bit164)
                packet.Translator.ReadInt32("Int164");

            if (bit151)
                packet.Translator.ReadByte("Byte151");

            packet.Translator.ReadXORByte(guid7, 3);
            packet.Translator.ReadXORByte(guid7, 7);
            packet.Translator.ReadXORByte(guid7, 0);
            packet.Translator.ReadXORByte(guid7, 2);
            packet.Translator.ReadXORByte(guid7, 1);
            packet.Translator.ReadXORByte(guid7, 6);
            packet.Translator.ReadXORByte(guid7, 4);
            packet.Translator.ReadXORByte(guid7, 5);

            if (hasPowerData)
            {
                packet.Translator.ReadInt32("Int1B8");
                packet.Translator.ReadXORByte(powerGUID, 7);
                packet.Translator.ReadXORByte(powerGUID, 2);
                for (var i = 0; i < PowerTypeCount; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int1BC");
                packet.Translator.ReadInt32("Int1C0");

                packet.Translator.ReadXORByte(powerGUID, 6);
                packet.Translator.ReadXORByte(powerGUID, 4);
                packet.Translator.ReadXORByte(powerGUID, 5);
                packet.Translator.ReadXORByte(powerGUID, 0);
                packet.Translator.ReadXORByte(powerGUID, 1);
                packet.Translator.ReadXORByte(powerGUID, 3);

                packet.Translator.WriteGuid("Guid36", powerGUID);
            }

            packet.Translator.ReadXORByte(guid1, 2);
            if (bit150)
                packet.Translator.ReadByte("Byte150");
            packet.Translator.ReadXORByte(guid4, 1);
            packet.Translator.ReadXORByte(guid4, 7);
            packet.Translator.ReadXORByte(guid4, 6);
            packet.Translator.ReadXORByte(guid4, 5);
            packet.Translator.ReadXORByte(guid4, 2);
            packet.Translator.ReadXORByte(guid4, 3);
            packet.Translator.ReadXORByte(guid4, 0);
            packet.Translator.ReadXORByte(guid4, 4);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadWoWString("StringC0", bitsC0);
            for (var i = 0; i < bits140; ++i)
            {
                packet.Translator.ReadByte("ByteED", i);
                packet.Translator.ReadInt32("IntED", i);
            }

            packet.Translator.ReadInt32<SpellId>("Spell ID");
            if (bit1AC)
                packet.Translator.ReadByte("Byte1AC");
            packet.Translator.ReadByte("Byte20");
            for (var i = 0; i < bits154; ++i)
                packet.Translator.ReadByte("Byte158", i);

            packet.Translator.ReadXORByte(guid1, 6);

            if (bit16C)
                packet.Translator.ReadInt32("Int16C");

            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 4);

            if (bit168)
                packet.Translator.ReadSingle("Float168");

            if (bit17C)
            {
                packet.Translator.ReadInt32("Int174");
                packet.Translator.ReadInt32("Int178");
            }

            packet.Translator.ReadInt32("Int30");

            packet.Translator.ReadXORByte(guid1, 3);

            if (bit194)
                packet.Translator.ReadInt32("Int194");

            if (bit180)
                packet.Translator.ReadByte("Byte180");

            if (bit198)
                packet.Translator.ReadInt32("Int198");

            packet.Translator.ReadXORByte(guid2, 7);

            if (bit1A8)
                packet.Translator.ReadInt32("Int1A8");

            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadInt32("Int28");
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 2);

            packet.Translator.WriteGuid("Guid2", guid1);
            packet.Translator.WriteGuid("Guid3", guid2);
            packet.Translator.WriteGuid("GuidE", guid3);
            packet.Translator.WriteGuid("GuidF", guid4);
            packet.Translator.WriteGuid("Guid34", guid7);
        }

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.Translator.ReadBit("InitialLogin");
            var count = packet.Translator.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadUInt32<SpellId>("Spell ID", i);;
            }
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            var guid6 = new byte[8];

            var bit272 = false;
            var bit332 = false;
            var bit340 = false;
            var bit344 = false;
            var bit376 = false;
            var bit380 = false;
            var bit408 = false;

            var bits120 = 0u;
            var bits392 = 0u;

            var bit24 = !packet.Translator.ReadBit();
            var bit120 = !packet.Translator.ReadBit();
            var bit80 = packet.Translator.ReadBit();
            var hasSpellId = !packet.Translator.ReadBit();
            var hasCastCount = !packet.Translator.ReadBit();
            var hasTargetMask = !packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit
            var bit48 = packet.Translator.ReadBit();
            var archeologyCounter = packet.Translator.ReadBits(2);
            var bit416 = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit
            var bit28 = !packet.Translator.ReadBit();
            var bit112 = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit

            for (var i = 0; i < archeologyCounter; ++i)
                packet.Translator.ReadBits("archeologyType", 2, i);

            if (bit120)
                bits120 = packet.Translator.ReadBits("bits120", 7);

            if (bit112)
                packet.Translator.StartBitStream(guid1, 3, 5, 1, 7, 0, 6, 2, 4);

            if (bit416)
            {
                var bit388 = packet.Translator.ReadBit();
                guid2[4] = packet.Translator.ReadBit();
                var bit264 = !packet.Translator.ReadBit();
                guid2[0] = packet.Translator.ReadBit();
                guid2[1] = packet.Translator.ReadBit();
                bit380 = packet.Translator.ReadBit();

                packet.Translator.ReadBit(); // fake
                guid2[6] = packet.Translator.ReadBit();

                if (bit264)
                    packet.Translator.ReadBits(30);

                packet.Translator.ReadBit(); // fake
                var bit412 = packet.Translator.ReadBit();
                bit344 = packet.Translator.ReadBit();

                if (bit344)
                {
                    guid3[6] = packet.Translator.ReadBit();
                    guid3[0] = packet.Translator.ReadBit();
                    guid3[7] = packet.Translator.ReadBit();
                    bit340 = packet.Translator.ReadBit("bit340");
                    guid3[3] = packet.Translator.ReadBit();
                    guid3[2] = packet.Translator.ReadBit();
                    guid3[1] = packet.Translator.ReadBit();
                    guid3[5] = packet.Translator.ReadBit();
                    guid3[4] = packet.Translator.ReadBit();
                    bit332 = packet.Translator.ReadBit("bit332");
                }
                bit272 = !packet.Translator.ReadBit();
                guid2[7] = packet.Translator.ReadBit();

                if (bit380)
                    bit376 = packet.Translator.ReadBit("bit376");

                var bit268 = !packet.Translator.ReadBit();
                packet.Translator.ReadBit("bit389");
                packet.Translator.ReadBit(); // fake
                guid2[2] = packet.Translator.ReadBit();

                if (bit268)
                    packet.Translator.ReadBits(13);

                bit408 = !packet.Translator.ReadBit();
                guid2[5] = packet.Translator.ReadBit();
                bits392 = packet.Translator.ReadBits(22);
                guid2[3] = packet.Translator.ReadBit();

            }

            if (bit80)
            {
                guid4[2] = packet.Translator.ReadBit();
                guid4[6] = packet.Translator.ReadBit();
                guid4[4] = packet.Translator.ReadBit();
                guid4[7] = packet.Translator.ReadBit();
                guid4[0] = packet.Translator.ReadBit();
                guid4[1] = packet.Translator.ReadBit();
                guid4[3] = packet.Translator.ReadBit();
                guid4[5] = packet.Translator.ReadBit();
            }

            packet.Translator.StartBitStream(guid5, 0, 4, 3, 1, 6, 5, 7, 2);
            packet.Translator.StartBitStream(guid6, 3, 4, 2, 0, 7, 6, 5, 1);


            if (bit28)
                packet.Translator.ReadBits(5);

            if (hasTargetMask)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            packet.Translator.ResetBitReader(); //?

            for (var i = 0; i < archeologyCounter; ++i)
            {
                packet.Translator.ReadUInt32("unk1", i);
                packet.Translator.ReadUInt32("unk2", i);
            }

            packet.Translator.ParseBitStream(guid6, 4, 3, 5, 6, 0, 7, 2, 1);

            if (bit416)
            {
                packet.Translator.ReadXORByte(guid2, 3);

                if (bit344)
                {
                    packet.Translator.ReadXORByte(guid3, 2);
                    packet.Translator.ReadSingle("float1");
                    packet.Translator.ParseBitStream(guid3, 3, 5, 6);
                    packet.Translator.ReadSingle("float2");
                    packet.Translator.ReadUInt32("unk3");
                    packet.Translator.ParseBitStream(guid3, 7, 1);

                    if (bit340)
                        packet.Translator.ReadUInt32("unk4");

                    packet.Translator.ReadSingle("float3");

                    if (bit332)
                        packet.Translator.ReadUInt32("unk5");

                    packet.Translator.ReadXORByte(guid3, 0);
                    packet.Translator.ReadSingle("float4");
                    packet.Translator.ReadXORByte(guid3, 4);
                }
                packet.Translator.ParseBitStream(guid2, 7, 1);

                if (bit408)
                    packet.Translator.ReadUInt32("unk6");

                packet.Translator.ReadSingle("float5");

                if (bit272)
                    packet.Translator.ReadUInt32("unk7");

                packet.Translator.ReadSingle("float6");

                if (bit380)
                {
                    packet.Translator.ReadSingle("float7");
                    if (bit376)
                    {
                        packet.Translator.ReadSingle("float8");
                        packet.Translator.ReadSingle("float9");
                        packet.Translator.ReadSingle("float10");
                    }
                    packet.Translator.ReadUInt32("unk8");
                }
                packet.Translator.ReadSingle("float11");
                packet.Translator.ParseBitStream(guid2, 0, 6);

                for (var i = 0; i < bits392; ++i)
                    packet.Translator.ReadUInt32("unk8", i);

                packet.Translator.ReadSingle("float11x");
                packet.Translator.ReadXORByte(guid2, 2);
                packet.Translator.ReadSingle("float12");
                packet.Translator.ReadSingle("float13");

                packet.Translator.ParseBitStream(guid2, 4, 5);
            }

            if (bit112)
            {
                packet.Translator.ReadSingle("float14");
                packet.Translator.ParseBitStream(guid1, 1, 3, 7, 6, 0, 2);
                packet.Translator.ReadSingle("float15");
                packet.Translator.ReadXORByte(guid1, 5);
                packet.Translator.ReadSingle("float16");
                packet.Translator.ReadXORByte(guid1, 4);
            }

            if (bit80)
            {
                packet.Translator.ReadSingle("float17");
                packet.Translator.ParseBitStream(guid4, 5, 7, 0, 4);
                packet.Translator.ReadSingle("float18");
                packet.Translator.ParseBitStream(guid4, 2, 1, 3);
                packet.Translator.ReadSingle("float19");
                packet.Translator.ReadXORByte(guid4, 6);
            }

            packet.Translator.ParseBitStream(guid5, 7, 3, 2, 0, 4, 6, 5, 1);

            if (bit24)
                packet.Translator.ReadUInt32("unk9");

            if (hasSpellId)
                packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (bit120)
                packet.Translator.ReadWoWString("Text", bits120);

            if (hasCastCount)
                packet.Translator.ReadByte("Cast Count");
        }
    }
}
