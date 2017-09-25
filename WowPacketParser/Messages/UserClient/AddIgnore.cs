using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct AddIgnore
    {
        public string Name;

        [Parser(Opcode.CMSG_ADD_IGNORE)]
        public static void HandleAddIgnoreOrMute(Packet packet)
        {
            var bits9 = packet.ReadBits(9);
            packet.ReadWoWString("Name", bits9);
        }
    }
}
