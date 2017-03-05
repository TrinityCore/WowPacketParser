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
            var count = packet.Translator.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadInt32("Type", i);
                packet.Translator.ReadUInt64("Guid", i);
                packet.Translator.ReadUInt32("SetID", i);
                uint ignoreMask = packet.Translator.ReadUInt32("IgnoreMask");

                for (var j = 0; j < NumSlots; j++)
                {
                    bool ignore = (ignoreMask & (1 << j)) != 0;
                    packet.Translator.ReadPackedGuid128("Pieces" + (ignore ? " (Ignored)" : ""), i, j);
                    packet.Translator.ReadInt32("Appearances", i);
                }

                for (var j = 0; j < 2; j++)
                    packet.Translator.ReadInt32("Enchants", i);

                packet.Translator.ResetBitReader();
                var setNameLen = packet.Translator.ReadBits(8);
                var setIconLen = packet.Translator.ReadBits(9);

                packet.Translator.ReadWoWString("SetName", setNameLen, i);
                packet.Translator.ReadWoWString("SetIcon", setIconLen, i);
            }
        }
    }
}
