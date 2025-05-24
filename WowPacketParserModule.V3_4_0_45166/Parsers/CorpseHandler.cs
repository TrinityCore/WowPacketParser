using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class CorpseHandler
    {
        [Parser(Opcode.SMSG_CORPSE_RECLAIM_DELAY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCorpseReclaimDelay(Packet packet)
        {
            packet.ReadInt32("Delay");
        }

        [Parser(Opcode.SMSG_CORPSE_LOCATION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCorpseLocation(Packet packet)
        {
            packet.ReadBit("Valid");
            packet.ReadPackedGuid128("Player");
            packet.ReadInt32("ActualMapID");
            packet.ReadInt32("MapID");
            packet.ReadPackedGuid128("Transport");
            packet.ReadVector3("Position");
        }

        [Parser(Opcode.SMSG_CORPSE_TRANSPORT_QUERY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCorpseTransportQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
            packet.ReadVector3("Position");
            packet.ReadSingle("Facing");
        }

        [Parser(Opcode.CMSG_QUERY_CORPSE_LOCATION_FROM_CLIENT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleQueryCorpseLocationFromClient(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
        }

        [Parser(Opcode.CMSG_QUERY_CORPSE_TRANSPORT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleQueryCorpseTransport(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
            packet.ReadPackedGuid128("Transport");
        }

        [Parser(Opcode.CMSG_RECLAIM_CORPSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleReclaimCorpse(Packet packet)
        {
            packet.ReadPackedGuid128("CorpseGUID");
        }
    }
}
