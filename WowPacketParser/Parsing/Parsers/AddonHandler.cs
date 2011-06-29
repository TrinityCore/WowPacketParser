using System;
using System.Text;
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

            var count = packet.ReadInt32();
            _addonCount = count;
            Console.WriteLine("Addons Count: " + count);

            for (var i = 0; i < count; i++)
            {
                var name = packet.ReadCString();
                Console.WriteLine("Name: " + name);

                var enabled = packet.ReadBoolean();
                Console.WriteLine("Enabled: " + enabled);

                var crcSum = packet.ReadInt32();
                Console.WriteLine("CRC: " + crcSum);

                var unk1 = packet.ReadInt32();
                Console.WriteLine("Unk Int32 1: " + unk1);
            }

            var unk2 = packet.ReadTime();
            Console.WriteLine("Current Time: " + unk2);
        }

        [Parser(Opcode.SMSG_ADDON_INFO)]
        public static void HandleServerAddonsList(Packet packet)
        {
            for (var i = 0; i < _addonCount; i++)
            {
                var state = packet.ReadByte();
                Console.WriteLine("Addon State: " + state);

                var sendCrc = packet.ReadBoolean();
                Console.WriteLine("Use CRC: " + sendCrc);

                if (sendCrc)
                {
                    var usePublicKey = packet.ReadBoolean();
                    Console.WriteLine("Use Public Key: " + usePublicKey);

                    if (usePublicKey)
                    {
                        var pubKey = packet.ReadChars(256);
                        Console.Write("Public Key: ");

                        foreach (var t in pubKey)
                            Console.Write(t);
                    }

                    var crc = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 1: " + crc);
                }

                var unkByte2 = packet.ReadBoolean();
                Console.WriteLine("Use URL File: " + unkByte2);

                if (!unkByte2)
                    continue;

                var unkStr = packet.ReadCString();
                Console.WriteLine("Addon URL File: " + unkStr);
            }

            var bannedCount = packet.ReadInt32();
            Console.WriteLine("Banned Addons Count: " + bannedCount);

            for (var i = 0; i < bannedCount; i++)
            {
                var unk1 = packet.ReadInt32();
                Console.WriteLine("ID: " + unk1);

                var unkStr2 = packet.ReadBytes(16);
                Console.WriteLine("Unk Hash 1: " + Utilities.ByteArrayToHexString(unkStr2));

                var unkStr3 = packet.ReadBytes(16);
                Console.WriteLine("Unk Hash 2: " + Utilities.ByteArrayToHexString(unkStr3));

                var unk2 = packet.ReadInt32();
                Console.WriteLine("Unk Int32 3: " + unk2);

                var unk3 = packet.ReadInt32();
                Console.WriteLine("Unk Int32 4: " + unk3);
            }
        }
    }
}
