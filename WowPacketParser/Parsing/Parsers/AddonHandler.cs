using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class AddonHandler
    {
        public static void ReadClientAddonsList(Packet packet)
        {
            var decompCount = packet.ReadInt32();
            if (decompCount == 0)
                return;

            var newPacket = packet.Inflate(decompCount, false);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
            {
                var count = newPacket.ReadInt32("Addons Count");
                Messages.ClientAddonInfo._addonCount = count;

                for (var i = 0; i < count; i++)
                {
                    newPacket.ReadCString("Name", i);
                    newPacket.ReadBool("Uses public key", i);
                    newPacket.ReadInt32("Public key CRC", i);
                    newPacket.ReadInt32("URL file CRC", i);
                }

                newPacket.ReadTime("Time");
            }
            else
            {
                int count = 0;

                while (newPacket.Position != newPacket.Length)
                {
                    newPacket.ReadCString("Name");
                    newPacket.ReadBool("Enabled");
                    newPacket.ReadInt32("CRC");
                    newPacket.ReadInt32("Unk Int32");

                    count++;
                }

                Messages.ClientAddonInfo._addonCount = count;
            }

            newPacket.ClosePacket(false);
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

        [Parser(Opcode.CMSG_CHAT_UNREGISTER_ALL_ADDON_PREFIXES)]
        public static void HandleAddonNull(Packet packet)
        {
        }
    }
}
