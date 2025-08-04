using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class UpdateHandler
    {
        [Parser(Opcode.SMSG_MAP_OBJ_EVENTS)]
        public static void HandleMapObjEvents(Packet packet)
        {
            packet.ReadInt32("UniqueID");
            packet.ReadInt32("DataSize");

            var count = packet.ReadByte("Unk1");
            for (var i = 0; i < count; i++)
            {
                var byte20 = packet.ReadByte("Unk2", i);
                packet.ReadInt32(byte20 == 1 ? "Unk3" : "Unk4", i);
            }
        }
    }
}
