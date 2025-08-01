using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;


namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class PetHandler
    {
        public static void ReadPetFlags(Packet packet, params object[] idx)
        {
            packet.ReadByteE<CommandState>("CommandState");
            packet.ReadByte("Flag");
            packet.ReadByteE<ReactState>("ReactState");
        }

        [Parser(Opcode.SMSG_PET_NEWLY_TAMED)]
        public static void HandlePetNewlyTamed(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ResetBitReader();
            packet.ReadBit("SuppressLevelUpAnim");
        }

        [Parser(Opcode.SMSG_PET_MODE)]
        public static void HandlePetMode(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            ReadPetFlags(packet, "PetModeAndOrders");
        }

        [Parser(Opcode.SMSG_PET_STABLE_RESULT)]
        public static void HandlePetStableResult(Packet packet)
        {
            packet.ReadUInt32E<PetStableResult>("Result");
        }
    }
}
