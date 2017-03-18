using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientAddFriend
    {
        public string Notes;
        public string Name;

        [Parser(Opcode.CMSG_ADD_FRIEND, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAddFriend(Packet packet)
        {
            packet.ReadCString("Name");
            packet.ReadCString("Note");
        }

        [Parser(Opcode.CMSG_ADD_FRIEND, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAddFriend602(Packet packet)
        {
            var bits16 = packet.ReadBits(9);
            var bits10 = packet.ReadBits(10);

            packet.ReadWoWString("Name", bits16);
            packet.ReadWoWString("Notes", bits10);
        }
    }
}
