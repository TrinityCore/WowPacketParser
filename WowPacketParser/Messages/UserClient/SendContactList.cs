using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SendContactList
    {
        public uint Flags;

        [Parser(Opcode.CMSG_SEND_CONTACT_LIST)]
        public static void HandleSendContactList(Packet packet)
        {
            packet.ReadInt32("Flags");
        }
    }
}
