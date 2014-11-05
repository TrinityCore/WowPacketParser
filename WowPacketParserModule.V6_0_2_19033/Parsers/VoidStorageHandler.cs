using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class VoidStorageHandler
    {
        [Parser(Opcode.CMSG_VOID_STORAGE_QUERY)]
        public static void HandleVoidStorageQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_CONTENTS)]
        public static void HandleVoidStorageContents(Packet packet)
        {
            var count = packet.ReadBits(8);
            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadPackedGuid128("Creator", i);
                packet.ReadUInt32("Slot", i);

                packet.ReadUInt32("ItemID", i);
                packet.ReadUInt32("RandomPropertiesSeed", i);
                packet.ReadUInt32("RandomPropertiesID", i);
                packet.ResetBitReader();
                var hasBonuses = packet.ReadBit("HasItemBonus", i);
                var hasModifications = packet.ReadBit("HasModifications", i);
                if (hasBonuses)
                {
                    packet.ReadByte("Context", i);

                    var bonusCount = packet.ReadUInt32();
                    for (var j = 0; j < bonusCount; ++j)
                        packet.ReadUInt32("BonusListID", i, j);
                }

                if (hasModifications)
                {
                    var modificationCount = packet.ReadUInt32() / 4;
                    for (var j = 0; j < modificationCount; ++j)
                        packet.ReadUInt32("Modification", i, j);
                }
            }
        }
    }
}
