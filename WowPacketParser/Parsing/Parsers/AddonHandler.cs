using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class AddonHandler
    {
        private static int _addonCount;

        public static void ReadClientAddonsList(Packet packet)
        {
            var decompCount = packet.ReadInt32();
            packet = packet.Inflate(decompCount);

            var count = packet.ReadInt32("Addons Count");
            _addonCount = count;

            for (var i = 0; i < count; i++)
            {
                packet.ReadCString("Name");

                packet.ReadBoolean("Enabled");

                packet.ReadInt32("CRC");

                packet.ReadInt32("Unk Int32");
            }

            packet.ReadTime("Current Time");
        }

        [Parser(Opcode.SMSG_ADDON_INFO)]
        public static void HandleServerAddonsList(Packet packet)
        {
            for (var i = 0; i < _addonCount; i++)
            {
                packet.ReadByte("Addon State");

                var sendCrc = packet.ReadBoolean("Use CRC");

                if (sendCrc)
                {
                    var usePublicKey = packet.ReadBoolean("Use Public Key");

                    if (usePublicKey)
                    {
                        var pubKey = packet.ReadChars(256);
                        Console.Write("Public Key: ");

                        foreach (var t in pubKey)
                            Console.Write(t);
                    }

                    packet.ReadInt32("Unk Int32");
                }

                var unkByte2 = packet.ReadBoolean("Use URL File");

                if (!unkByte2)
                    continue;

                packet.ReadCString("Addon URL File");
            }

            var bannedCount = packet.ReadInt32("Banned Addons Count");

            for (var i = 0; i < bannedCount; i++)
            {
                packet.ReadInt32("ID");

                var unkStr2 = packet.ReadBytes(16);
                Console.WriteLine("Unk Hash 1: " + Utilities.ByteArrayToHexString(unkStr2));

                var unkStr3 = packet.ReadBytes(16);
                Console.WriteLine("Unk Hash 2: " + Utilities.ByteArrayToHexString(unkStr3));

                packet.ReadInt32("Unk Int32 3");

                packet.ReadInt32("Unk Int32 4");
            }
        }
    }
}
