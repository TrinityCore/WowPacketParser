using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class WardenHandler
    {
        [Parser(Opcode.SMSG_WARDEN_DATA)]
        public static void HandleServerWardenData(Packet packet)
        {
            var opcode = packet.ReadByteE<WardenServerOpcode>("Warden Server Opcode");

            packet.SetPosition(0);

            switch (opcode)
            {
                case WardenServerOpcode.ModuleInfo:
                {
                    packet.ReadByte();
                    packet.ReadBytes("Module MD5", 16);
                    packet.ReadBytes("Module RC4", 16);
                    packet.ReadUInt32("Module Length");
                    break;
                }
                case WardenServerOpcode.ModuleChunk:
                {
                    packet.ReadByte();

                    var length = packet.ReadUInt16("Chunk Length");
                    packet.ReadBytes("Module Chunk", length);
                    break;
                }
                case WardenServerOpcode.CheatChecks:
                {
                    packet.ReadByte();

                    byte length;
                    while ((length = packet.ReadByte()) != 0)
                    {
                        packet.ReadBytes("String", length);
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
                        packet.ReadBytes("Data", length);
                    }
                    break;
                }
                case WardenServerOpcode.Seed:
                {
                    packet.ReadByte();
                    packet.ReadBytes("Seed", 16);
                    break;
                }
            }
        }

        [Parser(Opcode.CMSG_WARDEN_DATA)]
        public static void HandleClientWardenData(Packet packet)
        {
            var opcode = packet.ReadByteE<WardenClientOpcode>("Warden Client Opcode");

            switch (opcode)
            {
                case WardenClientOpcode.CheatCheckResults:
                {
                    var length = packet.ReadUInt16("Check Result Length");
                    packet.ReadInt32("Check Result Checksum");
                    packet.ReadBytes("Check Results", length);

                    break;
                }
                case WardenClientOpcode.TransformedSeed:
                {
                    packet.ReadBytes("SHA1 Seed", 20);
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
            packet.ReadBool("Glider 1 Detected");
            packet.ReadBool("Glider 2 Detected");
            packet.ReadBool("Inner Space Detected");
            packet.ReadBytes(20); // Hash
        }

        [Parser(Opcode.CMSG_BOT_DETECTED2)]
        public static void HandleBotDetected2(Packet packet)
        {
            packet.ReadInt32("Unk Int32");
        }

        public static void ReadCheatCheckDecryptionBlock(Packet packet)
        {
            packet.ReadBytes("ARC4 Key", 16);
        }
    }
}
