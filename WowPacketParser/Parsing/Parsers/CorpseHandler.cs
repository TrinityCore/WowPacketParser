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

            if (!packet.ReadBoolean("Corpse Found"))
                return;

            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadVector3("Corpse Position");
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Corpse Map ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_2_10482))
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

        [Parser(Opcode.SMSG_CORPSE_NOT_IN_INSTANCE)]
        public static void HandleCorpseNull(Packet packet)
        {
        }
    }
}
