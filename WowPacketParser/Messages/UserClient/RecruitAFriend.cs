using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct RecruitAFriend
    {
        public string Email;
        public string Text;
        public string Name;

        [Parser(Opcode.CMSG_RECRUIT_A_FRIEND)]
        public static void HandleRecruitAFriend(Packet packet)
        {
            var bits16 = packet.ReadBits(7);
            var bits273 = packet.ReadBits(9);
            var bits338 = packet.ReadBits(10);

            packet.ReadWoWString("Name", bits16);
            packet.ReadWoWString("Email", bits273);
            packet.ReadWoWString("Text", bits338);
        }
    }
}
