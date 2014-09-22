﻿using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class AddonHandler
    {

        [Parser(Opcode.SMSG_ADDON_INFO)]
        public static void HandleServerAddonsList(Packet packet)
        {
            var bits20 = packet.ReadBits(23);

            var usePublicKey = new bool[bits20];
            var bit3 = new bool[bits20];
            var bit1 = new bool[bits20];
            var bits0 = new uint[bits20];

            for (var i = 0; i < bits20; i++)
            {
                usePublicKey[i] = packet.ReadBit();
                bit3[i] = packet.ReadBit();
                bit1[i] = packet.ReadBit();

                if (bit3[i])
                    bits0[i] = packet.ReadBits(8);
            }

            var bits10 = (int)packet.ReadBits(18);

            for (var i = 0; i < bits20; i++)
            {
                if (usePublicKey[i])
                {
                    packet.ReadBytes("Name MD5", 256);
                }

                if (bit1[i])
                {
                    packet.ReadByte("Byte24", i);
                    packet.ReadInt32("Int24", i);
                }

                if (bit3[i])
                    packet.ReadWoWString("Addon URL File", bits0[i], i);

                packet.ReadByte("Addon State", i);
            }

            for (var i = 0; i < bits10; i++)
            {
                packet.ReadInt32("Int14", i);

                for (var j = 0; j < 4; j++)
                {
                    packet.ReadInt32("IntED", i, j);
                    packet.ReadInt32("Int14", i, j);
                }

                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
            }
        }
    }
}
