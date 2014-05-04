using System;
using System.Collections.Generic;
using System.Net;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Battlenet;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class BattlenetHandler
    {
        private static List<string> ModulesWaitingForData = new List<string>(16);

        [BattlenetParser(BattlenetOpcode.ClientInformationRequest, BattlenetChannel.Auth, Direction.BNClientToServer)]
        [BattlenetParser(BattlenetOpcode.ClientInformationRequestOld, BattlenetChannel.Auth, Direction.BNClientToServer)]
        public static void HandleInformationRequest(BattlenetPacket packet)
        {
            packet.ReadFourCC("Program");
            packet.ReadFourCC("Platform");
            packet.ReadFourCC("Locale");

            var components = packet.Read<int>(6);
            for (var i = 0; i < components; ++i)
            {
                packet.ReadFourCC("Program", i);
                packet.ReadFourCC("Platform", i);
                packet.Read<uint>("Build", 32, i);
            }

            if (packet.Read<bool>(1))
                packet.ReadString("Login", packet.Read<int>(9) + 3);
        }

        [BattlenetParser(BattlenetOpcode.ServerProofRequest, BattlenetChannel.Auth, Direction.BNServerToClient)]
        public static void HandleServerProofRequest(BattlenetPacket packet)
        {
            var modules = packet.Read<byte>(3);
            var module = new BattlenetModuleHandler(packet);
            for (var i = 0; i < modules; ++i)
            {
                packet.ReadString("Type", 4, i);
                packet.ReadFourCC("Region", i);
                var id = Utilities.ByteArrayToHexString(packet.ReadBytes(32));
                packet.Stream.WriteLine(string.Format("[{0}] ModuleId: {1}", i, id));
                var dataSize = packet.Read<int>("Data size", 10, i);
                var data = packet.ReadBytes(dataSize);

                var result = module.HandleData(id, data, i);
                if (!result.HasValue)
                    packet.Stream.WriteLine(string.Format("[{0}] Data: {1}", i, Utilities.ByteArrayToHexString(data)));
                else if (!result.Value)
                    ModulesWaitingForData.Add(id);
            }
        }

        [BattlenetParser(BattlenetOpcode.ClientProofResponse, BattlenetChannel.Auth, Direction.BNClientToServer)]
        public static void HandleProofResponse(BattlenetPacket packet)
        {
            var modules = packet.Read<byte>(3);
            var module = new BattlenetModuleHandler(packet);
            for (var i = 0; i < modules; ++i)
            {
                var dataSize = packet.Read<int>("Data size", 10, i);
                var data = packet.ReadBytes(dataSize);
                if (!module.HandleData(ModulesWaitingForData[i], data, i).HasValue)
                    packet.Stream.WriteLine(string.Format("[{0}] Data: {1}", i, Utilities.ByteArrayToHexString(data)));
            }

            ModulesWaitingForData.Clear();
        }

        [BattlenetParser(BattlenetOpcode.ServerComplete, BattlenetChannel.Auth, Direction.BNServerToClient)]
        public static void HandleComplete(BattlenetPacket packet)
        {
            var failed = packet.Read<bool>(1);
            if (failed)
            {
                if (packet.Read<bool>(1)) // has module
                {
                    var type = packet.ReadString(4);
                    var region = packet.ReadFourCC();
                    var id = Utilities.ByteArrayToHexString(packet.ReadBytes(32));
                    var dataSize = packet.Read<int>(10);
                    var data = packet.ReadBytes(dataSize);
                    var module = new BattlenetModuleHandler(packet);

                    packet.Stream.WriteLine(string.Format("Type: {0}", type));
                    packet.Stream.WriteLine(string.Format("Region: {0}", region));
                    packet.Stream.WriteLine(string.Format("ModuleId: {0}", id));
                    packet.Stream.WriteLine(string.Format("Data size: {0}", dataSize));
                    if (!module.HandleData(id, data).HasValue)
                        packet.Stream.WriteLine(string.Format("Data: {0}", Utilities.ByteArrayToHexString(data)));
                }

                var errorType = packet.Read<byte>(2);
                if (errorType == 1)
                {
                    packet.Stream.WriteLine(string.Format("Result: {0}", packet.Read<ushort>(16)));
                    packet.Stream.WriteLine(string.Format("Unk: {0}", packet.Read<uint>(32)));
                }
            }
            else
            {
                var modules = packet.Read<byte>(3);
                var module = new BattlenetModuleHandler(packet);
                for (var i = 0; i < modules; ++i)
                {
                    var type = packet.ReadString(4);
                    var region = packet.ReadFourCC();
                    var id = Utilities.ByteArrayToHexString(packet.ReadBytes(32));
                    var dataSize = packet.Read<int>(10);
                    var data = packet.ReadBytes(dataSize);

                    packet.Stream.WriteLine(string.Format("[{0}] Type: {1}", i, type));
                    packet.Stream.WriteLine(string.Format("[{0}] Region: {1}", i, region));
                    packet.Stream.WriteLine(string.Format("[{0}] ModuleId: {1}", i, id));
                    packet.Stream.WriteLine(string.Format("[{0}] Data size: {1}", i, dataSize));
                    if (!module.HandleData(id, data, i).HasValue)
                        packet.Stream.WriteLine(string.Format("[{0}] Data: {1}", i, Utilities.ByteArrayToHexString(data)));
                }

                packet.Stream.WriteLine(string.Format("Ping timeout: {0}", packet.Read<uint>(32) + int.MinValue));

                var hasOptionalData = packet.Read<bool>(1);
                if (hasOptionalData)
                {
                    var hasConnectionInfo = packet.Read<bool>(1);
                    if (hasConnectionInfo)
                    {
                        packet.Read<uint>("Threshold", 32);
                        packet.Read<uint>("Rate", 32);
                    }
                }

                packet.Read<bool>("Unk bool", 1);
                packet.ReadString("First name", packet.Read<int>(8));
                packet.ReadString("Last name", packet.Read<int>(8));

                packet.Read<uint>("Account id", 32);

                packet.Read<byte>("Unk8", 8);
                packet.Read<ulong>("Unk64", 64);
                packet.Read<byte>("Unk8", 8);

                packet.ReadString("Account name", packet.Read<int>(5) + 1);

                packet.Read<ulong>("Unk64", 64);
                packet.Read<uint>("Unk32", 32);
                packet.Read<byte>("Unk8", 8);
            }
        }

        [BattlenetParser(BattlenetOpcode.ClientPing, BattlenetChannel.Creep, Direction.BNClientToServer)]
        [BattlenetParser(BattlenetOpcode.ServerPong, BattlenetChannel.Creep, Direction.BNServerToClient)]
        public static void HandlePong(BattlenetPacket packet)
        {
        }

        [BattlenetParser(BattlenetOpcode.ServerJoinResponse, BattlenetChannel.WoW, Direction.BNServerToClient)]
        public static void HandleJoinResponse(BattlenetPacket packet)
        {
            if (packet.Read<bool>("Failed", 1))
            {
                packet.Read<byte>("Error code", 8);
                return;
            }

            packet.Read<uint>("Server salt", 32);
            var count = packet.Read<uint>("IPv4 count", 5);
            for (var i = 0; i < count; ++i)
            {
                var ip = packet.ReadBytes(4);
                var port = packet.ReadBytes(2);

                Array.Reverse(port);

                packet.Stream.WriteLine("[{0}] IP address: {1}", i, new IPAddress(ip));
                packet.Stream.WriteLine("[{0}] Port: {1}", i, BitConverter.ToUInt16(port, 0));
            }

            count = packet.Read<uint>("IPv6 count", 5);
            for (var i = 0; i < count; ++i)
            {
                var ip = packet.ReadBytes(16);
                var port = packet.ReadBytes(2);

                Array.Reverse(port);

                packet.Stream.WriteLine("[{0}] IP address: {1}", i, new IPAddress(ip));
                packet.Stream.WriteLine("[{0}] Port: {1}", i, BitConverter.ToUInt16(port, 0));
            }
        }
    }
}
