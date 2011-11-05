using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CorpseHandler
    {
        [Parser(Opcode.MSG_CORPSE_QUERY)]
        public static void HandleCorpseQuery(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            var found = packet.ReadBoolean("Corpse Found");

            if (!found)
                return;

            Console.WriteLine("Map ID: " + StoreGetters.GetName(StoreNameType.Map, packet.ReadInt32()));

            packet.ReadVector3("Corpse Position");

            Console.WriteLine("Corpse Map ID: " + StoreGetters.GetName(StoreNameType.Map, packet.ReadInt32()));

            packet.ReadInt32("Corpse Low GUID");
        }

        [Parser(Opcode.CMSG_CORPSE_MAP_POSITION_QUERY)]
        public static void HandleCorpseMapPositionQuery(Packet packet)
        {
            packet.ReadInt32("Low GUID");
        }

        [Parser(Opcode.SMSG_CORPSE_MAP_POSITION_QUERY_RESPONSE)]
        public static void HandleCorpseMapPositionResponse(Packet packet)
        {
            packet.ReadVector3("Unk Vector3");

            packet.ReadSingle("Unk Single");
        }

        [Parser(Opcode.SMSG_CORPSE_RECLAIM_DELAY)]
        public static void HandleCorpseReclaimDelay(Packet packet)
        {
           packet.ReadInt32("Delay");
        }

        [Parser(Opcode.CMSG_RECLAIM_CORPSE)]
        public static void HandleReclaimCorpse(Packet packet)
        {
            packet.ReadGuid("Corpse GUID");
        }
    }
}
