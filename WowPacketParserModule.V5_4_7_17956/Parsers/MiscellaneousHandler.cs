using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.V5_4_7_17956.Enums;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_17956.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.CMSG_LOAD_SCREEN, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_LOAD_SCREEN)]
        public static void HandleClientEnterWorld547(Packet packet)
        {
            var mapId = packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map Id");
            packet.ReadBit("Loading");

            CoreParsers.MovementHandler.CurrentMapId = (uint)mapId;

            packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

        [Parser(Opcode.CMSG_PING, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_PING)]
        public static void HandleClientPing547(Packet packet)
        {
            packet.ReadUInt32("Latency");
            packet.ReadUInt32("Ping");
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESP, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_TIME_SYNC_RESP)]
        public static void HandleTimeSyncResp547(Packet packet)
        {
            packet.ReadUInt32("Counter");
            packet.ReadUInt32("Client Ticks");
        }

        [Parser(Opcode.CMSG_SET_SELECTION, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection547(Packet packet)
        {
            var guid = packet.StartBitStream(3, 5, 6, 7, 2, 4, 1, 0);
            packet.ParseBitStream(guid, 5, 0, 4, 3, 1, 7, 2, 6);

            packet.WriteGuid("Target Guid", guid);
        }

        [Parser(Opcode.SMSG_CLIENTCACHE_VERSION, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_CLIENTCACHE_VERSION)]
        public static void HandleClientCacheVersion547(Packet packet)
        {
            packet.ReadUInt32("Version");
        }

        [Parser(Opcode.SMSG_PONG, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_PONG)]
        public static void HandleServerPong547(Packet packet)
        {
            packet.ReadUInt32("Ping");
        }

        [Parser(Opcode.SMSG_SERVER_TIMEZONE, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_SERVER_TIMEZONE)]
        public static void HandleServerTimezone547(Packet packet)
        {
            var Location2Lenght = packet.ReadBits(7);
            var Location1Lenght = packet.ReadBits(7);

            packet.ReadWoWString("Timezone Location1", Location1Lenght);
            packet.ReadWoWString("Timezone Location2", Location2Lenght);
        }
    }
}
