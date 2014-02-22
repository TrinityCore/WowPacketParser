using WowPacketParser.Enums;
using WowPacketParser.Misc;
using System;

namespace WowPacketParser.Parsing.Parsers
{
    public static class AddonHandler
    {
        private static int _addonCount = -1;

        public static void ReadClientAddonsList(ref Packet packet)
        {
            var decompCount = packet.ReadInt32();
            packet = packet.Inflate(decompCount, false);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
            {
                var count = packet.ReadInt32("Addons Count");
                _addonCount = count;

                for (var i = 0; i < count; i++)
                {
                    packet.ReadCString("Name", i);
                    packet.ReadBoolean("Uses public key", i);
                    packet.ReadInt32("Public key CRC", i);
                    packet.ReadInt32("URL file CRC", i);
                }

                packet.ReadTime("Time");
            }
            else
            {
                int count = 0;

                while (packet.Position != packet.Length)
                {
                    packet.ReadCString("Name");
                    packet.ReadBoolean("Enabled");
                    packet.ReadInt32("CRC");
                    packet.ReadInt32("Unk Int32");

                    count++;
                }

                _addonCount = count;
            }
        }

        [Parser(Opcode.SMSG_ADDON_INFO, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_7_17930)]
        public static void HandleServerAddonsList(Packet packet)
        {
            // This packet requires _addonCount from CMSG_AUTH_SESSION to be parsed.
            if (_addonCount == -1)
            {
                packet.WriteLine("CMSG_AUTH_SESSION was not received - cannot successfully parse this packet.");
                packet.ReadToEnd();
                return;
            }

            for (var i = 0; i < _addonCount; i++)
            {
                packet.ReadByte("Addon State", i);

                var sendCrc = packet.ReadBoolean("Use CRC", i);

                if (sendCrc)
                {
                    var usePublicKey = packet.ReadBoolean("Use Public Key", i);

                    if (usePublicKey)
                    {
                        var pubKey = packet.ReadBytes(256);
                        packet.WriteLine("[{0}] Name MD5: {1}", i, Utilities.ByteArrayToHexString(pubKey));
                    }

                    packet.ReadInt32("Unk Int32", i);
                }

                if (packet.ReadBoolean("Use URL File", i))
                    packet.ReadCString("Addon URL File", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
            {
                var bannedCount = packet.ReadInt32("Banned Addons Count");

                for (var i = 0; i < bannedCount; i++)
                {
                    packet.ReadInt32("ID", i);

                    var unkStr2 = packet.ReadBytes(16);
                    packet.WriteLine("[{0}] Name MD5: {1}", i, Utilities.ByteArrayToHexString(unkStr2));

                    var unkStr3 = packet.ReadBytes(16);
                    packet.WriteLine("[{0}] Version MD5: {1}", i, Utilities.ByteArrayToHexString(unkStr3));

                    packet.ReadTime("Time", i);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3a_11723))
                        packet.ReadInt32("Is banned", i);
                }
            }
        }

        [Parser(Opcode.SMSG_ADDON_INFO, ClientVersionBuild.V5_4_7_17930)]
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
                {
                    AddonsInfo[i, 3] = packet.ReadBits(8);
                }
                else
                {
                    AddonsInfo[i, 3] = 0;
                }
            }

            var BannedAddonsCount = packet.ReadBits("Banned Addons Count",18);

            for (var i = 0; i < AddonsCount; ++i)
            {
                if (AddonsInfo[i, 1] == 1)
                {
                    packet.ReadBytes(256);
                    // the bytes order isn't 1,2,3,4.. they are mangled
                }

                if (AddonsInfo[i, 0] == 1)
                {
                    packet.ReadUInt32("CRC Summ", i);
                    packet.ReadByte("Unk Byte1", i);
                }

                packet.ReadByte("Addon State", i);

                if (AddonsInfo[i, 2] == 1 && AddonsInfo[i, 3] > 0)
                {
                    packet.ReadWoWString("URL path", AddonsInfo[i, 3], i);
                }
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

        // Changed on 4.3.0, bitshifted
        [Parser(Opcode.CMSG_ADDON_REGISTERED_PREFIXES, ClientVersionBuild.V4_1_0_13914, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleAddonPrefixes(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadCString("Addon", i);
        }

        [Parser(Opcode.CMSG_ADDON_REGISTERED_PREFIXES, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleAddonPrefixes434(Packet packet)
        {
            var count = packet.ReadBits("Count", 25);
            var lengths = new int[count];
            for (var i = 0; i < count; ++i)
                lengths[i] = (int)packet.ReadBits(5);

            for (var i = 0; i < count; ++i)
                packet.ReadWoWString("Addon", lengths[i], i);
        }

        [Parser(Opcode.CMSG_UNREGISTER_ALL_ADDON_PREFIXES)]
        public static void HandleAddonNull(Packet packet)
        {
        }
    }
}
