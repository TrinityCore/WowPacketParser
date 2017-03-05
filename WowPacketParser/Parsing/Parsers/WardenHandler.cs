using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class WardenHandler
    {
        [Parser(Opcode.SMSG_WARDEN_DATA)]
        public static void HandleServerWardenData(Packet packet)
        {
            var opcode = packet.Translator.ReadByteE<WardenServerOpcode>("Warden Server Opcode");

            packet.SetPosition(0);

            switch (opcode)
            {
                case WardenServerOpcode.ModuleInfo:
                {
                    packet.Translator.ReadByte();
                    packet.Translator.ReadBytes("Module MD5", 16);
                    packet.Translator.ReadBytes("Module RC4", 16);
                    packet.Translator.ReadUInt32("Module Length");
                    break;
                }
                case WardenServerOpcode.ModuleChunk:
                {
                    packet.Translator.ReadByte();

                    var length = packet.Translator.ReadUInt16("Chunk Length");
                    packet.Translator.ReadBytes("Module Chunk", length);
                    break;
                }
                case WardenServerOpcode.CheatChecks:
                {
                    packet.Translator.ReadByte();

                    byte length;
                    while ((length = packet.Translator.ReadByte()) != 0)
                    {
                        packet.Translator.ReadBytes("String", length);
                    }

                    // var rest = (int)(packet.GetLength() - packet.GetPosition());
                    break;
                }
                case WardenServerOpcode.Data:
                {
                    while (packet.CanRead())
                    {
                        packet.Translator.ReadByte();

                        var length = packet.Translator.ReadUInt16("Data Length");
                        packet.Translator.ReadInt32("Data Checksum");
                        packet.Translator.ReadBytes("Data", length);
                    }
                    break;
                }
                case WardenServerOpcode.Seed:
                {
                    packet.Translator.ReadByte();
                    packet.Translator.ReadBytes("Seed", 16);
                    break;
                }
            }
        }

        [Parser(Opcode.CMSG_WARDEN_DATA)]
        public static void HandleClientWardenData(Packet packet)
        {
            var opcode = packet.Translator.ReadByteE<WardenClientOpcode>("Warden Client Opcode");

            switch (opcode)
            {
                case WardenClientOpcode.CheatCheckResults:
                {
                    var length = packet.Translator.ReadUInt16("Check Result Length");
                    packet.Translator.ReadInt32("Check Result Checksum");
                    packet.Translator.ReadBytes("Check Results", length);

                    break;
                }
                case WardenClientOpcode.TransformedSeed:
                {
                    packet.Translator.ReadBytes("SHA1 Seed", 20);
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
            packet.Translator.ReadBool("Glider 1 Detected");
            packet.Translator.ReadBool("Glider 2 Detected");
            packet.Translator.ReadBool("Inner Space Detected");
            packet.Translator.ReadBytes(20); // Hash
        }

        [Parser(Opcode.CMSG_BOT_DETECTED2)]
        public static void HandleBotDetected2(Packet packet)
        {
            packet.Translator.ReadInt32("Unk Int32");
        }

        public static void ReadCheatCheckDecryptionBlock(Packet packet)
        {
            packet.Translator.ReadBytes("ARC4 Key", 16);
        }
    }
}
