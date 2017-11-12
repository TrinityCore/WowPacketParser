using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.GuildBank
{
    public unsafe struct SetTabText
    {
        public int Tab;
        public string TabText;

        [Parser(Opcode.CMSG_GUILD_BANK_SET_TAB_TEXT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildSetBankText(Packet packet)
        {
            packet.ReadByte("Tab Id");
            packet.ReadCString("Tab Text");
        }
    }
}
