using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetAssistantLeader
    {
        public bool Set;
        public byte PartyIndex;
        public ulong Target;

        [Parser(Opcode.CMSG_SET_ASSISTANT_LEADER, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleGroupAssistantLeader(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBool("Promote"); // False = demote
        }

        [Parser(Opcode.CMSG_SET_ASSISTANT_LEADER, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleGroupAssistantLeader547(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadBool("Promote");
            guid[1] = packet.ReadBit();
            var bit11 = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }
    }
}
