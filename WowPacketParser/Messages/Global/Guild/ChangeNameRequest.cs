using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct ChangeNameRequest
    {
        public string NewName;

        [Parser(Opcode.CMSG_GUILD_CHANGE_NAME_REQUEST)]
        public static void HandleGuildNameChange(Packet packet)
        {
            packet.ReadWoWString("New Name", (int)packet.ReadBits(8));
        }
    }
}
