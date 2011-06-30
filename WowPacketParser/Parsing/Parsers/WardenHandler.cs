using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class WardenHandler
    {
        [Parser(Opcode.SMSG_WARDEN_DATA)]
        public static void HandleServerWardenData(Packet packet)
        {
            var opcode = (WardenServerOpcode)packet.ReadByte();
            Console.WriteLine("Warden Server Opcode: " + opcode);
            packet.SetPosition(0);

            switch (opcode)
            {
                case WardenServerOpcode.ModuleInfo:
                {
                    packet.ReadByte();

                    var md5 = packet.ReadBytes(16);
                    Console.WriteLine("Module MD5: " + Utilities.ByteArrayToHexString(md5));

                    var rc4 = packet.ReadBytes(16);
                    Console.WriteLine("Module RC4: " + Utilities.ByteArrayToHexString(rc4));

                    var length = packet.ReadInt32();
                    Console.WriteLine("Module Length: " + length);
                    break;
                }
                case WardenServerOpcode.ModuleChunk:
                {
                    packet.ReadByte();

                    var length = packet.ReadInt16();
                    Console.WriteLine("Chunk Length: " + length);

                    var chunk = packet.ReadBytes(length);
                    Console.WriteLine("Module Chunk: " + Utilities.ByteArrayToHexString(chunk));
                    break;
                }
                case WardenServerOpcode.CheatChecks:
                {
                    packet.ReadByte();

                    byte length;
                    while ((length = packet.ReadByte()) != 0)
                    {
                        var strBytes = packet.ReadBytes(length);
                        var str = Encoding.ASCII.GetString(strBytes);
                        Console.WriteLine("String: " + str);
                    }

                    // var rest = (int)(packet.GetLength() - packet.GetPosition());
                    break;
                }
                case WardenServerOpcode.Data:
                {
                    while (!packet.IsRead())
                    {
                        packet.ReadByte();

                        var length = packet.ReadInt16();
                        Console.WriteLine("Data Length: " + length);

                        var checksum = packet.ReadInt32();
                        Console.WriteLine("Data Checksum: " + checksum);

                        var data = packet.ReadBytes(length);
                        Console.WriteLine("Data: " + Utilities.ByteArrayToHexString(data));
                    }
                    break;
                }
                case WardenServerOpcode.Seed:
                {
                    packet.ReadByte();

                    var seed = packet.ReadBytes(16);
                    Console.WriteLine("Seed: " + Utilities.ByteArrayToHexString(seed));
                    break;
                }
            }
        }

        [Parser(Opcode.CMSG_WARDEN_DATA)]
        public static void HandleClientWardenData(Packet packet)
        {
            var opcode = (WardenClientOpcode)packet.ReadByte();
            Console.WriteLine("Warden Client Opcode: " + opcode);

            switch (opcode)
            {
                case WardenClientOpcode.CheatCheckResults:
                {
                    var length = packet.ReadInt16();
                    Console.WriteLine("Check Result Length: " + length);

                    var checksum = packet.ReadInt32();
                    Console.WriteLine("Check Result Checksum: " + checksum);

                    var result = packet.ReadBytes(length);
                    Console.WriteLine("Check Results: " + Utilities.ByteArrayToHexString(result));
                    break;
                }
                case WardenClientOpcode.TransformedSeed:
                {
                    var sha1 = packet.ReadBytes(20);
                    Console.WriteLine("SHA1 Seed: " + Utilities.ByteArrayToHexString(sha1));
                    break;
                }
            }
        }

        [Parser(Opcode.SMSG_CHECK_FOR_BOTS)]
        public static void HandleCheckForBots(Packet packet)
        {
            ReadCheatCheckDecryptionBlock(packet);
        }

        [Parser(Opcode.CMSG_BOT_DETECTED)]
        public static void HandleBotDetected(Packet packet)
        {
            var glider1 = packet.ReadBoolean();
            Console.WriteLine("Glider 1 Detected: " + glider1);

            var glider2 = packet.ReadBoolean();
            Console.WriteLine("Glider 2 Detected: " + glider2);

            var innerSpace = packet.ReadBoolean();
            Console.WriteLine("Inner Space Detected: " + innerSpace);

            packet.ReadBytes(20); // Hash
        }

        [Parser(Opcode.CMSG_BOT_DETECTED2)]
        public static void HandleBotDetected2(Packet packet)
        {
            var useless = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + useless);
        }

        public static void ReadCheatCheckDecryptionBlock(Packet packet)
        {
            var arc4 = packet.ReadBytes(16);
            Console.WriteLine("ARC4 Key: " + Utilities.ByteArrayToHexString(arc4));
        }
    }
}
