using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct CancelChannelling
    {
        public int ChannelSpell;

        [Parser(Opcode.CMSG_CANCEL_CHANNELLING)]
        public static void HandleRemovedSpell2(Packet packet)
        {
            packet.ReadUInt32<SpellId>("Spell ID");
        }
    }
}
