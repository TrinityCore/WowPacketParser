using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Parsing.Parsers;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct LoadingScreenNotify
    {
        public int MapID;
        public bool Showing;

        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)] // Also named CMSG_LOADING_SCREEN_NOTIFY
        public static void HandleClientEnterWorld(Packet packet)
        {
            packet.ReadBit("Showing");
            var mapId = packet.ReadUInt32<MapId>("MapID");
            MovementHandler.CurrentMapId = mapId;

            if (mapId < 1000) // Getting some weird results in a couple of packets
                packet.AddSniffData(StoreNameType.Map, (int)mapId, "LOAD_SCREEN");
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleClientEnterWorld434(Packet packet)
        {
            var mapId = packet.ReadUInt32<MapId>("MapID");
            packet.ReadBit("Showing");
            MovementHandler.CurrentMapId = mapId;

            if (mapId < 1000) // Getting some weird results in a couple of packets
                packet.AddSniffData(StoreNameType.Map, (int)mapId, "LOAD_SCREEN");
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleClientEnterWorld540(Packet packet)
        {
            uint mapId = packet.ReadUInt32<MapId>("MapID");
            packet.ReadBit("Showing");
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientEnterWorld548(Packet packet)
        {
            var mapId = packet.ReadInt32<MapId>("MapID");
            packet.ReadBit("Showing");

            packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientEnterWorld602(Packet packet)
        {
            var mapId = packet.ReadInt32<MapId>("MapID");
            packet.ReadBit("Showing");

            packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

    }
}
