using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class CorpseHandler
    {
        [Parser(Opcode.SMSG_CORPSE_LOCATION)]
        public static void HandleCorpseLocation(Packet packet)
        {
            packet.ReadBit("Valid");
            packet.ReadPackedGuid128("Player");
            packet.ReadInt32("ActualMapID");
            packet.ReadInt32("MapID");
            packet.ReadPackedGuid128("Transport");
            packet.ReadVector3("Position");
        }

        [Parser(Opcode.SMSG_CORPSE_TRANSPORT_QUERY)]
        public static void HandleCorpseTransportQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
            packet.ReadVector3("Position");
            packet.ReadSingle("Facing");
        }

        [Parser(Opcode.SMSG_CORPSE_RECLAIM_DELAY)]
        public static void HandleCorpseReclaimDelay(Packet packet)
        {
            packet.ReadInt32("Delay");
        }

        [Parser(Opcode.CMSG_RECLAIM_CORPSE)]
        public static void HandleReclaimCorpse(Packet packet)
        {
            packet.ReadPackedGuid128("CorpseGUID");
        }
    }
}
