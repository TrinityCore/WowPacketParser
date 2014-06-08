using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class CorpseHandler
    {
        [Parser(Opcode.CMSG_CORPSE_MAP_POSITION_QUERY)]
        public static void HandleCorpseMapPositionQuery(Packet packet)
        {
            packet.ReadInt32("Low GUID");
            packet.ReadPackedGuid("Guid");
        }

        [Parser(Opcode.CMSG_RECLAIM_CORPSE)]
        public static void HandleReclaimCorpse(Packet packet)
        {
            packet.ReadPackedGuid("Corpse GUID");
        }

        [Parser(Opcode.SMSG_CORPSE_QUERY)]
        public static void HandleCorpseQuery(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                var guid = new byte[8];

                guid[4] = packet.ReadBit();
                guid[2] = packet.ReadBit();
                guid[5] = packet.ReadBit();
                guid[3] = packet.ReadBit();
                guid[1] = packet.ReadBit();
                guid[6] = packet.ReadBit();
                guid[0] = packet.ReadBit();

                var CorpseFound = packet.ReadBit("CorpseFound");

                guid[7] = packet.ReadBit();

                packet.ReadXORByte(guid, 3);
                packet.ReadXORByte(guid, 2);
                packet.ReadXORByte(guid, 1);

                packet.ReadUInt32("MapId");
                packet.ReadSingle("X");

                packet.ReadXORByte(guid, 6);
                packet.ReadXORByte(guid, 4);
                packet.ReadXORByte(guid, 5);

                packet.ReadUInt32("CorpseMapId");

                packet.ReadXORByte(guid, 7);

                packet.ReadSingle("Z");

                packet.ReadXORByte(guid, 0);

                packet.ReadSingle("Y");

                packet.WriteGuid("Corpse Guid", guid);
            } else
            {
                packet.WriteLine("              : CMSG_MOUNTSPECIAL_ANIM");
                packet.Opcode = (int)Opcode.CMSG_MOUNTSPECIAL_ANIM;
            }
        }

        [Parser(Opcode.SMSG_CORPSE_RECLAIM_DELAY)]
        public static void HandleCorpseReclaimDelay(Packet packet)
        {
            var hasNoTime = packet.ReadBit();
            if (!hasNoTime)
                packet.ReadInt32("Delay");
        }
    }
}
