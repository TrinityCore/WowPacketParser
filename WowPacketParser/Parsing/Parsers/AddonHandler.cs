using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class AddonHandler
    {
        private static int _addonCount = -1;

        public static void ReadClientAddonsList(Packet packet)
        {
            var decompCount = packet.Translator.ReadInt32();
            if (decompCount == 0)
                return;

            var newPacket = packet.Inflate(decompCount, false);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
            {
                var count = newPacket.Translator.ReadInt32("Addons Count");
                _addonCount = count;

                for (var i = 0; i < count; i++)
                {
                    newPacket.Translator.ReadCString("Name", i);
                    newPacket.Translator.ReadBool("Uses public key", i);
                    newPacket.Translator.ReadInt32("Public key CRC", i);
                    newPacket.Translator.ReadInt32("URL file CRC", i);
                }

                newPacket.Translator.ReadTime("Time");
            }
            else
            {
                int count = 0;

                while (newPacket.Position != newPacket.Length)
                {
                    newPacket.Translator.ReadCString("Name");
                    newPacket.Translator.ReadBool("Enabled");
                    newPacket.Translator.ReadInt32("CRC");
                    newPacket.Translator.ReadInt32("Unk Int32");

                    count++;
                }

                _addonCount = count;
            }

            newPacket.ClosePacket(false);
        }

        [Parser(Opcode.SMSG_ADDON_INFO)]
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
                packet.Translator.ReadByte("Addon State", i);

                var sendCrc = packet.Translator.ReadBool("Use CRC", i);

                if (sendCrc)
                {
                    var usePublicKey = packet.Translator.ReadBool("Use Public Key", i);

                    if (usePublicKey)
                        packet.Translator.ReadBytes("Name MD5", 256);

                    packet.Translator.ReadInt32("Unk Int32", i);
                }

                if (packet.Translator.ReadBool("Use URL File", i))
                    packet.Translator.ReadCString("Addon URL File", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
            {
                var bannedCount = packet.Translator.ReadInt32("Banned Addons Count");

                for (var i = 0; i < bannedCount; i++)
                {
                    packet.Translator.ReadInt32("ID", i);
                    packet.Translator.ReadBytes("Name MD5", 16);
                    packet.Translator.ReadBytes("Version MD5", 16);
                    packet.Translator.ReadTime("Time", i);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3a_11723))
                        packet.Translator.ReadInt32("Is banned", i);
                }
            }
        }

        // Changed on 4.3.0, bitshifted
        [Parser(Opcode.CMSG_ADDON_REGISTERED_PREFIXES, ClientVersionBuild.V4_1_0_13914, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleAddonPrefixes(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadCString("Addon", i);
        }

        [Parser(Opcode.CMSG_ADDON_REGISTERED_PREFIXES, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleAddonPrefixes434(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 25);
            var lengths = new int[count];
            for (var i = 0; i < count; ++i)
                lengths[i] = (int)packet.Translator.ReadBits(5);

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadWoWString("Addon", lengths[i], i);
        }

        [Parser(Opcode.CMSG_CHAT_UNREGISTER_ALL_ADDON_PREFIXES)]
        public static void HandleAddonNull(Packet packet)
        {
        }
    }
}
