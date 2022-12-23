using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class ActionBarHandler
    {
        [Parser(Opcode.CMSG_SET_ACTION_BUTTON)]
        public static void HandleActionButton(Packet packet)
        {
            var data = packet.ReadInt32();
            packet.AddValue("Type", (ActionButtonType)((data & 0xFF000000) >> 24));
            packet.AddValue("ID", data & 0x00FFFFFF);
            packet.ReadByte("Button");
        }
    }
}
