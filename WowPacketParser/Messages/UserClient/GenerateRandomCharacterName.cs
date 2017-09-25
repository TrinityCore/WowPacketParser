using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct GenerateRandomCharacterName
    {
        public byte Sex;
        public byte Race;

        [Parser(Opcode.CMSG_GENERATE_RANDOM_CHARACTER_NAME)]
        public static void HandleGenerateRandomCharacterNameQuery(Packet packet)
        {
            packet.ReadByteE<Race>("Race");
            packet.ReadByteE<Gender>("Sex");
        }
    }
}
