using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAddonInfo
    {
        public List<BannedAddonInfo> BannedAddons;
        public List<AddonInfo> Addons;

        public static int _addonCount = -1;

        [Parser(Opcode.SMSG_ADDON_INFO, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleServerAddonsList(Packet packet)
        {
            // This packet requires _addonCount from CMSG_AUTH_SESSION to be parsed.
            if (_addonCount == -1)
            {
                packet.AddValue("Error", "CMSG_AUTH_SESSION was not received - cannot successfully parse this packet.");
                packet.ReadToEnd();
                return;
            }

            for (var i = 0; i < _addonCount; i++)
            {
                packet.ReadByte("Addon State", i);

                var sendCrc = packet.ReadBool("Use CRC", i);

                if (sendCrc)
                {
                    var usePublicKey = packet.ReadBool("Use Public Key", i);

                    if (usePublicKey)
                        packet.ReadBytes("Name MD5", 256);

                    packet.ReadInt32("Unk Int32", i);
                }

                if (packet.ReadBool("Use URL File", i))
                    packet.ReadCString("Addon URL File", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
            {
                var bannedCount = packet.ReadInt32("Banned Addons Count");

                for (var i = 0; i < bannedCount; i++)
                {
                    packet.ReadInt32("ID", i);
                    packet.ReadBytes("Name MD5", 16);
                    packet.ReadBytes("Version MD5", 16);
                    packet.ReadTime("Time", i);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3a_11723))
                        packet.ReadInt32("Is banned", i);
                }
            }
        }

        [Parser(Opcode.SMSG_ADDON_INFO, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleServerAddonsList540(Packet packet)
        {
            var bits20 = (int)packet.ReadBits(23);

            var bit3 = new bool[bits20];
            var usePublicKey = new bool[bits20];
            var bits0 = new uint[bits20];
            var bit1 = new bool[bits20];

            for (var i = 0; i < bits20; i++)
            {
                bit3[i] = packet.ReadBit();
                usePublicKey[i] = packet.ReadBit();

                if (bit3[i])
                    bits0[i] = packet.ReadBits(8);

                bit1[i] = packet.ReadBit();
            }

            var bits10 = (int)packet.ReadBits(18);

            for (var i = 0; i < bits20; i++)
            {
                if (bit3[i])
                    packet.ReadWoWString("Addon URL File", bits0[i], i);

                if (usePublicKey[i])
                {
                    packet.ReadBytes("Name MD5", 256, i);
                }

                if (bit1[i])
                {
                    packet.ReadByte("Byte24", i);
                    packet.ReadInt32("Int24", i);
                }

                packet.ReadByte("Addon State", i);
            }

            for (var i = 0; i < bits10; i++)
            {
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("IntED", i);

                for (var j = 0; j < 4; j++)
                {
                    packet.ReadInt32("IntED", i, j);
                    packet.ReadInt32("Int14", i, j);
                }

                packet.ReadInt32("IntED", i);
            }
        }

        [Parser(Opcode.SMSG_ADDON_INFO, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleServerAddonsList542(Packet packet)
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

        [Parser(Opcode.SMSG_ADDON_INFO, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleServerAddonsList547(Packet packet)
        {
            var bits20 = packet.ReadBits(23);

            var usePublicKey = new bool[bits20];
            var hasURL = new bool[bits20];
            var bit1 = new bool[bits20];
            var urlLang = new uint[bits20];

            for (var i = 0; i < bits20; i++)
            {
                bit1[i] = packet.ReadBit();
                hasURL[i] = packet.ReadBit();
                usePublicKey[i] = packet.ReadBit();

                if (hasURL[i])
                    urlLang[i] = packet.ReadBits(8);
            }

            var bits10 = (int)packet.ReadBits(18);

            for (var i = 0; i < bits20; i++)
            {
                if (usePublicKey[i])
                    packet.ReadBytes("Name MD5", 256, i);

                if (bit1[i])
                {
                    packet.ReadInt32("Int24", i);
                    packet.ReadByte("Byte24", i);
                }

                if (hasURL[i])
                    packet.ReadWoWString("Addon URL File", urlLang[i], i);

                packet.ReadByte("Addon State", i);
            }

            for (var i = 0; i < bits10; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    packet.ReadInt32("IntED", i, j);
                    packet.ReadInt32("Int14", i, j);
                }

                packet.ReadInt32("Int14", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
            }
        }

        [Parser(Opcode.SMSG_ADDON_INFO, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleServerAddonsList602(Packet packet)
        {
            var int4 = packet.ReadInt32("AddonInfo");
            var int8 = packet.ReadInt32("BannedAddonInfo");

            for (var i = 0; i < int4; i++)
            {
                packet.ReadByte("Status", i);

                var bit1 = packet.ReadBit("InfoProvided", i);
                var bit2 = packet.ReadBit("KeyProvided", i);
                var bit3 = packet.ReadBit("UrlProvided", i);

                packet.ResetBitReader();

                if (bit3)
                {
                    var urlLang = packet.ReadBits(8);
                    packet.ReadWoWString("Url", urlLang, i);
                }

                if (bit1)
                {
                    packet.ReadByte("KeyVersion", i);
                    packet.ReadInt32("Revision", i);
                }

                if (bit2)
                    packet.ReadBytes("KeyData", 256, i);
            }

            for (var i = 0; i < int8; i++)
            {
                packet.ReadInt32("Id", i);

                for (var j = 0; j < 4; j++)
                {
                    packet.ReadInt32("NameMD5", i, j);
                    packet.ReadInt32("VersionMD5", i, j);
                }

                packet.ReadPackedTime("LastModified", i);
                packet.ReadInt32("Flags", i);
            }
        }
    }
}
