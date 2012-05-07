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
            var opcode = packet.ReadEnum<WardenServerOpcode>("Warden Server Opcode", TypeCode.Byte);

            packet.SetPosition(0);

            switch (opcode)
            {
                case WardenServerOpcode.ModuleInfo:
                {
                    packet.ReadByte();

                    var md5 = packet.ReadBytes(16);
                    packet.WriteLine("Module MD5: " + Utilities.ByteArrayToHexString(md5));

                    var rc4 = packet.ReadBytes(16);
                    packet.WriteLine("Module RC4: " + Utilities.ByteArrayToHexString(rc4));

                    packet.ReadUInt32("Module Length");
                    break;
                }
                case WardenServerOpcode.ModuleChunk:
                {
                    packet.ReadByte();

                    var length = packet.ReadUInt16("Chunk Length");

                    var chunk = packet.ReadBytes(length);
                    packet.WriteLine("Module Chunk: " + Utilities.ByteArrayToHexString(chunk));
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
                        packet.WriteLine("String: " + str);
                    }

                    // var rest = (int)(packet.GetLength() - packet.GetPosition());
                    break;
                }
                case WardenServerOpcode.Data:
                {
                    while (packet.CanRead())
                    {
                        packet.ReadByte();

                        var length = packet.ReadUInt16("Data Length");

                        packet.ReadInt32("Data Checksum");

                        var data = packet.ReadBytes(length);
                        packet.WriteLine("Data: " + Utilities.ByteArrayToHexString(data));
                    }
                    break;
                }
                case WardenServerOpcode.Seed:
                {
                    packet.ReadByte();

                    var seed = packet.ReadBytes(16);
                    packet.WriteLine("Seed: " + Utilities.ByteArrayToHexString(seed));
                    break;
                }
            }
        }

        [Parser(Opcode.CMSG_WARDEN_DATA)]
        public static void HandleClientWardenData(Packet packet)
        {
            var opcode = packet.ReadEnum<WardenClientOpcode>("Warden Client Opcode", TypeCode.Byte);

            switch (opcode)
            {
                case WardenClientOpcode.CheatCheckResults:
                {
                    var length = packet.ReadUInt16("Check Result Length");

                    packet.ReadInt32("Check Result Checksum");

                    var result = packet.ReadBytes(length);
                    packet.WriteLine("Check Results: " + Utilities.ByteArrayToHexString(result));

                    break;
                }
                case WardenClientOpcode.TransformedSeed:
                {
                    var sha1 = packet.ReadBytes(20);
                    packet.WriteLine("SHA1 Seed: " + Utilities.ByteArrayToHexString(sha1));
                    break;
                }
            }
        }

        [Parser(Opcode.SMSG_CHECK_FOR_BOTS)]
        public static void HandleCheckForBots(Packet packet)
        {
            ReadCheatCheckDecryptionBlock(ref packet);
        }

        [Parser(Opcode.CMSG_BOT_DETECTED)]
        public static void HandleBotDetected(Packet packet)
        {
            packet.ReadBoolean("Glider 1 Detected");
            packet.ReadBoolean("Glider 2 Detected");
            packet.ReadBoolean("Inner Space Detected");
            packet.ReadBytes(20); // Hash
        }

        [Parser(Opcode.CMSG_BOT_DETECTED2)]
        public static void HandleBotDetected2(Packet packet)
        {
            packet.ReadInt32("Unk Int32");
        }

        public static void ReadCheatCheckDecryptionBlock(ref Packet packet)
        {
            var arc4 = packet.ReadBytes(16);
            packet.WriteLine("ARC4 Key: " + Utilities.ByteArrayToHexString(arc4));
        }
    }
}
