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
            if (packet.GetDirection() == Direction.ClientToServer)
                return;

            var found = packet.ReadBoolean("Corpse Found");

            if (!found)
                return;

            Console.WriteLine("Map ID: " + Extensions.MapLine(packet.ReadInt32()));

            packet.ReadVector3("Corpse Position");

            Console.WriteLine("Corpse Map ID: " + Extensions.MapLine(packet.ReadInt32()));

            packet.ReadInt32("Corpse Low GUID");
        }

        [Parser(Opcode.CMSG_CORPSE_MAP_POSITION_QUERY)]
        public static void HandleCorpseMapPositionQuery(Packet packet)
        {
            var lowGuid = packet.ReadInt32();
            Console.WriteLine("Low GUID: " + lowGuid);
        }

        [Parser(Opcode.SMSG_CORPSE_MAP_POSITION_QUERY_RESPONSE)]
        public static void HandleCorpseMapPositionResponse(Packet packet)
        {
            var unkCoords = packet.ReadVector3();
            Console.WriteLine("Unk Vector3: " + unkCoords);

            var unkSingle = packet.ReadSingle();
            Console.WriteLine("Unk Single: " + unkSingle);
        }

        [Parser(Opcode.SMSG_CORPSE_RECLAIM_DELAY)]
        public static void HandleCorpseReclaimDelay(Packet packet)
        {
            var delay = packet.ReadInt32();
            Console.WriteLine("Delay: " + delay);
        }

        [Parser(Opcode.CMSG_RECLAIM_CORPSE)]
        public static void HandleReclaimCorpse(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("Corpse GUID: " + guid);
        }
    }
}
