using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.V5_4_8_18414.Enums;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class UnkHandler
    {
        [Parser(Opcode.CMSG_UNK_0002)]
        public static void HandleUnk0002(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadBit("unk");
            }
            else
            {
                packet.WriteLine("              : SMSG_UNK_0002");
                packet.ReadInt32("unk");
            }
        }
    }
}
