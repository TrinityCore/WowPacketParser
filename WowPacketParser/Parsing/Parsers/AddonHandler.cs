using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class AddonHandler
    {
        private static int _addonCount = -1;

        public static void ReadClientAddonsList(Packet packet, int size = -1)
        {
            var decompCount = packet.ReadInt32();
            if (size == -1)
            {
                packet.Inflate(decompCount);
            }
            else
            {
                packet.Inflate(decompCount, size);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
            {
                var count = packet.ReadInt32("Addons Count");
                _addonCount = count;

                for (var i = 0; i < count; i++)
                {
                    packet.ReadCString("Name", i);
                    packet.ReadBoolean("Enabled", i);
                    packet.ReadInt32("CRC", i);
                    packet.ReadInt32("Unk Int32", i);
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

        [Parser(Opcode.SMSG_ADDON_INFO)]
        public static void HandleServerAddonsList(Packet packet)
        {
            // This packet requires _addonCount from CMSG_AUTH_SESSION to be parsed.
            if (_addonCount == -1)
            {
                packet.StoreOutputText("CMSG_AUTH_SESSION was not received - cannot successfully parse this packet.");
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
                        packet.ReadChars("Public Key", 256, i);
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
                    packet.Store("Unk Hash 1", Utilities.ByteArrayToHexString(unkStr2), i);

                    var unkStr3 = packet.ReadBytes(16);
                    packet.Store("Unk Hash 2", Utilities.ByteArrayToHexString(unkStr3), i);

                    packet.ReadInt32("Unk Int32 3", i);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3a_11723))
                        packet.ReadInt32("Unk Int32 4", i);
                }
            }
        }

        // Changed on 4.3.2, bitshiffted
        [Parser(Opcode.CMSG_ADDON_REGISTERED_PREFIXES, ClientVersionBuild.V4_1_0_13914, ClientVersionBuild.V4_3_2_15211)]
        public static void HandleAddonPrefixes(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadCString("Addon", i);
        }

        [Parser(Opcode.CMSG_ADDON_REGISTERED_PREFIXES, ClientVersionBuild.V4_3_4_15595)]
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
