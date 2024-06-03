using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class VoidStorageHandler
    {
        public static void ReadVoidItem(Packet packet, params object[] index)
        {
            packet.ReadPackedGuid128("Guid", index);
            packet.ReadPackedGuid128("Creator", index);
            packet.ReadUInt32("Slot", index);

            Substructures.ItemHandler.ReadItemInstance(packet, index);
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_CONTENTS)]
        public static void HandleVoidStorageContents(Packet packet)
        {
            var count = packet.ReadBits(8);
            for (var i = 0; i < count; ++i)
                ReadVoidItem(packet, i);
        }
    }
}
