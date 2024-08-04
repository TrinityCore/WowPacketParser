using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class BankHandler
    {
        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        public static void HandleBankerActivate(Packet packet)
        {
            packet.ReadPackedGuid128("Banker");
            packet.ReadInt32E<PlayerInteractionType>("InteractionType");
        }
    }
}
