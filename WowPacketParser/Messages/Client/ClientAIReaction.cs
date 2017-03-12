using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAIReaction
    {
        public ulong UnitGUID;
        public int Reaction;

        [Parser(Opcode.SMSG_AI_REACTION, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleAIReaction(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32E<AIReaction>("Reaction");
        }

        [Parser(Opcode.SMSG_AI_REACTION, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleAIReaction547(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 1, 4, 3, 6, 5, 7, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadInt32E<AIReaction>("Reaction");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_AI_REACTION, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAIReaction548(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 7, 0, 4, 6, 2, 3, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32E<AIReaction>("Reaction");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_AI_REACTION, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAIReaction602(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32E<AIReaction>("Reaction");
        }
    }
}
