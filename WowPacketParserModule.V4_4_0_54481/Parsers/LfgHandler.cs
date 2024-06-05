using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class LfgHandler
    {
        public static void ReadLFGListBlacklistEntry(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("ActivityID", indexes);
            packet.ReadInt32("Reason", indexes);
        }

        [Parser(Opcode.CMSG_DF_GET_SYSTEM_INFO)]
        public static void HandleLFGLockInfoRequest(Packet packet)
        {
            packet.ReadBit("Player");
            if (packet.ReadBit())
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_LFG_LIST_UPDATE_BLACKLIST)]
        public static void HandleLFGListUpdateBlacklist(Packet packet)
        {
            var count = packet.ReadInt32("BlacklistEntryCount");
            for (int i = 0; i < count; i++)
                ReadLFGListBlacklistEntry(packet, i, "ListBlacklistEntry");
        }

        [Parser(Opcode.CMSG_LFG_LIST_GET_STATUS)]
        [Parser(Opcode.CMSG_REQUEST_LFG_LIST_BLACKLIST)]
        [Parser(Opcode.CMSG_DF_GET_JOIN_STATUS)]
        public static void HandleLfgZero(Packet packet)
        {
        }
    }
}
