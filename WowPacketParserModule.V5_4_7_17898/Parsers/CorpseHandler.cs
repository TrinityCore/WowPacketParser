using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class CorpseHandler
    {
        [Parser(Opcode.SMSG_CORPSE_RECLAIM_DELAY)]
        public static void HandleCorpseReclaimDelay(Packet packet)
        {
            var hasDelay = !packet.ReadBit("hasDelay");
            if (hasDelay)
                packet.ReadInt32("Delay");
        }

        [Parser(Opcode.CMSG_RECLAIM_CORPSE)]
        public static void HandleReclaimCorpse(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 3, 4, 0, 2, 6, 1, 7);
            packet.ParseBitStream(guid, 5, 4, 3, 1, 7, 0, 2, 6);

            packet.WriteGuid("Corpse GUID", guid);
        }

        [Parser(Opcode.CMSG_CORPSE_QUERY)]
        public static void HandleCorpseQuery(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_CORPSE_QUERY_RESPONSE)]
        public static void HandleCorpseQueryResponse(Packet packet)
        {
            var pos = new Vector3();
            var guid = new byte[8];

            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            packet.ReadBit("Corpse Found");
            guid[7] = packet.ReadBit();

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadInt32<MapId>("Map ID");
            pos.X = packet.ReadSingle();
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32<MapId>("Corpse Map ID");
            packet.ReadXORByte(guid, 7);
            pos.Z = packet.ReadSingle();
            packet.ReadXORByte(guid, 0);
            pos.Y = packet.ReadSingle();

            packet.AddValue("Position", pos);
            packet.WriteGuid("Corpse Low GUID", guid);
        }

        [Parser(Opcode.SMSG_DEATH_RELEASE_LOC)]
        public static void HandleDeathReleaseLoc(Packet packet)
        {
            var pos = new Vector3();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            packet.ReadInt32<MapId>("Map Id");

            packet.AddValue("Position", pos);
        }
    }
}
