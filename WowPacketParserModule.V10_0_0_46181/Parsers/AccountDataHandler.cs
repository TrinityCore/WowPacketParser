using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.CMSG_GET_ACCOUNT_CHARACTER_LIST)]
        public static void HandleGetAccountCharacterList(Packet packet)
        {
            packet.ReadUInt32("UnkInt32");

            packet.ResetBitReader();
            packet.ReadBit("UnkBit");
        }
    }
}
