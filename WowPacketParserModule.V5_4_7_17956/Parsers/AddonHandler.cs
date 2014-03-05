using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17956.Parsers
{
    public static class AddonHandler
    {
        [Parser(Opcode.SMSG_ADDON_INFO, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_ADDON_INFO)]
        public static void HandleServerAddonInfo547(Packet packet)
        {
            var AddonsCount = packet.ReadBits("Addons Count", 23);
            uint[,] AddonsInfo = new uint[AddonsCount, 4];

            for (var i = 0; i < AddonsCount; ++i)
            {
                AddonsInfo[i, 0] = packet.ReadBit("Use CRC", i);
                AddonsInfo[i, 2] = packet.ReadBit("Has URL", i);
                AddonsInfo[i, 1] = packet.ReadBit("Has Public Key", i);

                if (AddonsInfo[i, 2] == 1)
                    AddonsInfo[i, 3] = packet.ReadBits(8);
                else
                    AddonsInfo[i, 3] = 0;
            }

            var BannedAddonsCount = packet.ReadBits("Banned Addons Count",18);

            for (var i = 0; i < AddonsCount; ++i)
            {
                if (AddonsInfo[i, 1] == 1)
                    packet.ReadBytes(256); // the bytes order isn't 1,2,3,4.. they are mangled.

                if (AddonsInfo[i, 0] == 1)
                {
                    packet.ReadUInt32("CRC Summ", i);
                    packet.ReadByte("Unk Byte1", i);
                }

                packet.ReadByte("Addon State", i);

                if (AddonsInfo[i, 2] == 1 && AddonsInfo[i, 3] > 0)
                    packet.ReadWoWString("URL path", AddonsInfo[i, 3], i);
            }

            for (var i = 0; i < BannedAddonsCount; ++i)
            {
                var NameMD5 = new byte[16];
                var VersionMD5 = new byte[16];

                for (uint j = 0; j < 16; j += 4)
                {
                    Array.Copy(packet.ReadBytes(4), 0, NameMD5, j, 4);
                    Array.Copy(packet.ReadBytes(4), 0, VersionMD5, j, 4);
                }

                packet.ReadUInt32("ID", i);
                packet.ReadUInt32("Timestamp", i);
                packet.ReadUInt32("Banned", i);
            }
        }
    }
}
