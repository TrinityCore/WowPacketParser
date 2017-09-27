using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct ShowTradeSkill
    {
        public ulong PlayerGUID;
        public int SkillLineID;
        public int SpellID;

        [Parser(Opcode.CMSG_SHOW_TRADE_SKILL)]
        public static void HandleShowTradeSkill(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("SkillLineID");
        }
    }
}
