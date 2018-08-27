using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class VoidStorageHandler
    {
        [Parser(Opcode.CMSG_VOID_STORAGE_TRANSFER)]
        public static void HandleVoidStorageTransfer(Packet packet)
        {
            packet.ReadPackedGuid128("Npc");
            var int48 = packet.ReadInt32("DepositsCount");
            var int32 = packet.ReadInt32("WithdrawalsCount");

            for (int i = 0; i < int48; i++)
                packet.ReadPackedGuid128("Deposits", i);

            for (int i = 0; i < int32; i++)
                packet.ReadPackedGuid128("Withdrawals", i);
        }
    }
}
