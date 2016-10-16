using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class EquipmentSetHandler
    {
        private const int NumSlots = 19;

        [Parser(Opcode.SMSG_LOAD_EQUIPMENT_SET)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Type", i);
                packet.ReadUInt64("Guid", i);
                packet.ReadUInt32("SetID", i);
                uint ignoreMask = packet.ReadUInt32("IgnoreMask");

                for (var j = 0; j < NumSlots; j++)
                {
                    bool ignore = (ignoreMask & (1 << j)) != 0;
                    packet.ReadPackedGuid128("Pieces" + (ignore ? " (Ignored)" : ""), i, j);
                    packet.ReadInt32("Appearances", i);
                }

                for (var j = 0; j < 2; j++)
                    packet.ReadInt32("Enchants", i);

                packet.ResetBitReader();
                var setNameLen = packet.ReadBits(8);
                var setIconLen = packet.ReadBits(9);

                packet.ReadWoWString("SetName", setNameLen, i);
                packet.ReadWoWString("SetIcon", setIconLen, i);
            }
        }
    }
}
