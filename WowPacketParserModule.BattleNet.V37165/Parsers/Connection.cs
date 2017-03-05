using System;
using System.Net;
using WowPacketParser.Enums.Battlenet;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.BattleNet.V37165.Enums;

namespace WowPacketParserModule.BattleNet.V37165.Parsers
{
    public static class Connection
    {
        [BattlenetParser(ConnectionClientCommand.Ping)]
        [BattlenetParser(ConnectionClientCommand.EnableEncyption)]
        [BattlenetParser(ConnectionClientCommand.LogoutRequest)]
        [BattlenetParser(ConnectionServerCommand.Pong)]
        public static void HandleEmpty(BattlenetPacket packet)
        {
        }

        [BattlenetParser(ConnectionClientCommand.DisconnectRequest)]
        public static void HandleDisconnectRequest(BattlenetPacket packet)
        {
            packet.Read<ushort>("Error", 0, 16);
            packet.Read<uint>("Timeout", 0, 32);
        }

        [BattlenetParser(ConnectionClientCommand.ConnectionClosing)]
        public static void HandleConnectionClosing(BattlenetPacket packet)
        {
            var packets = packet.Read<int>(0, 6);
            for (var i = 0; i < packets; ++i)
            {
                packet.ReadFourCC("Command", i);
                packet.Read<uint>("Time", 0, 32, i);
                packet.Read<ushort>("Size", 0, 16, i);
                packet.ReadFourCC("Layer", 0, 16, i);
                packet.Read<ushort>("Offset", 0, 16, i);
            }

            packet.Read<ClosingReason>("Reason", 0, 4);
            packet.ReadByteArray("BadData", 0, 8);

            if (packet.ReadBoolean())
            {
                packet.Read<ushort>("Command", 0, 6);
                if (packet.ReadBoolean())
                    packet.Read<ushort>("Channel", 0, 4);
            }

            packet.Read<uint>("Now", 0, 32);
        }

        [BattlenetParser(ConnectionServerCommand.Boom)]
        public static void HandleBoom(BattlenetPacket packet)
        {
            packet.Read<ushort>("Error", 0, 16);
        }

        public static void ReadRegulatorInfo(BattlenetPacket packet, string fieldName)
        {
            if (packet.ReadBoolean())
            {
                packet.Read<uint>("Threshold", 0, 32, fieldName);
                packet.Read<uint>("Rate", 0, 32, fieldName);
            }
        }

        [BattlenetParser(ConnectionServerCommand.RegulatorUpdate)]
        public static void HandleRegulatorUpdate(BattlenetPacket packet)
        {
            ReadRegulatorInfo(packet, "Info");
        }

        [BattlenetParser(ConnectionServerCommand.ServerVersion)]
        public static void HandleServerVersion(BattlenetPacket packet)
        {
            packet.Read<uint>("Version", 0, 32);
        }

        [BattlenetParser(ConnectionServerCommand.STUNServers)]
        public static void HandleSTUNServers(BattlenetPacket packet)
        {
            var ip = packet.ReadBytes(4);
            var port = packet.ReadBytes(2);
            Array.Reverse(port);

            packet.Stream.AddValue("Server1", new IPEndPoint(new IPAddress(ip), BitConverter.ToUInt16(port, 0)));

            ip = packet.ReadBytes(4);
            port = packet.ReadBytes(2);
            Array.Reverse(port);

            packet.Stream.AddValue("Server2", new IPEndPoint(new IPAddress(ip), BitConverter.ToUInt16(port, 0)));
        }
    }
}
