using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class ActionBarHandler
    {
        [Parser(Opcode.CMSG_SET_ACTION_BUTTON)]
        public static void HandleSetActionButton(Packet packet)
        {
            short action = packet.ReadInt16();
            short type = packet.ReadInt16();

            packet.AddValue("Action ", action);
            packet.AddValue("Type ", type);
            packet.ReadByte("Slot Id");
        }
    }
}
