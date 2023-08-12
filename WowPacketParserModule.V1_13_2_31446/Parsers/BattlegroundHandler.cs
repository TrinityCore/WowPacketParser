using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.SMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldList(Packet packet)
        {
            packet.ReadPackedGuid128("BattlemasterGuid");
            packet.ReadInt32("UnkID"); // some kind of serverside verification id its only being used in CMSG_BATTLEMASTER_JOIN again
            packet.ReadInt32("BattlemasterListID");
            packet.ReadByte("MinLevel");
            packet.ReadByte("MaxLevel");
            var battlefieldsCount = packet.ReadUInt32("BattlefieldsCount");
            for (var i = 0; i < battlefieldsCount; ++i)
                packet.ReadInt32("Battlefield", i);

            packet.ResetBitReader();
            packet.ReadBit("PvpAnywhere");
            packet.ReadBit("HasRandomWinToday");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN)]
        public static void HandleBattlemasterJoin(Packet packet)
        {
            V6_0_2_19033.Parsers.BattlegroundHandler.ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadByte("Roles");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("BlacklistMap", i);

            packet.ReadPackedGuid128("BattlemasterGuid");
            packet.ReadInt32("UnkID"); // reused from SMSG_BATTLEFIELD_LIST
            packet.ReadInt32("BattlefieldIndexSpecific");
            packet.ReadBit("JoinAsGroup");
        }
    }
}
