using WowPacketParser.Enums;
using WowPacketParser.Messages.Player;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.Query
{
    public unsafe struct PlayerName
    {
        public ulong Player;
        public GuidLookupHint Hint;

        [Parser(Opcode.CMSG_QUERY_PLAYER_NAME, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_1_17538)]
        public static void HandlePlayerQueryName(Packet packet)
        {
            var guid = new byte[8];

            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var bit16 = packet.ReadBit("bit16");
            guid[6] = packet.ReadBit();
            var bit24 = packet.ReadBit("bit24");

            packet.ParseBitStream(guid, 6, 0, 2, 3, 4, 5, 7, 1);

            if (bit24)
                packet.ReadUInt32("unk28");

            if (bit16)
                packet.ReadUInt32("unk20");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUERY_PLAYER_NAME, ClientVersionBuild.V5_4_1_17538, ClientVersionBuild.V6_0_2_19033)]
        public static void HandlePlayerQueryName541(Packet packet)
        {
            var guid = new byte[8];

            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var bit20 = packet.ReadBit("bit20");
            guid[0] = packet.ReadBit();
            var bit28 = packet.ReadBit("bit28");
            guid[4] = packet.ReadBit();

            packet.ParseBitStream(guid, 4, 6, 7, 1, 2, 5, 0, 3);

            if (bit20)
                packet.ReadUInt32("unk20");

            if (bit28)
                packet.ReadUInt32("unk28");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUERY_PLAYER_NAME, ClientVersionBuild.V6_0_2_19033)]
        public static void HandlePlayerQueryName602(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            if (!ClientVersion.RemovedInVersion(ClientVersionBuild.V6_1_0_19678))
                return;

            var bit4 = packet.ReadBit();
            var bit12 = packet.ReadBit();

            if (bit4)
                packet.ReadInt32("VirtualRealmAddress");

            if (bit12)
                packet.ReadInt32("NativeRealmAddress");
        }
    }
}
