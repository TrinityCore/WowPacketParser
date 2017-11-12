using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.GuildBank
{
    public unsafe struct TextQuery
    {
        public int Tab;

        [Parser(Opcode.CMSG_GUILD_BANK_TEXT_QUERY, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleQueryGuildBankText(Packet packet)
        {
            packet.ReadUInt32("Tab Id");
        }
    }
}
