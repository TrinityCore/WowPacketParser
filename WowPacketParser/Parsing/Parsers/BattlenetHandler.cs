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
        private static readonly List<string> ModulesWaitingForData = new List<string>(16);

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
                var id = Utilities.ByteArrayToHexString(packet.ReadBytes("ModuleId", 32, i));
                var dataSize = packet.Read<int>("Data size", 10, i);
                var data = packet.ReadBytes(dataSize);

                var result = module.HandleData(id, data, i);
                if (!result.HasValue)
                    packet.Stream.AddValue("Data", data, i);
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
                    packet.Stream.AddValue("Data", data, i);
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
                    packet.ReadString("Type", 4);
                    packet.ReadFourCC("Region");
                    var id = Utilities.ByteArrayToHexString(packet.ReadBytes("ModuleId", 32));
                    var dataSize = packet.Read<int>("Data size", 10);
                    var data = packet.ReadBytes(dataSize);
                    var module = new BattlenetModuleHandler(packet);

                    if (!module.HandleData(id, data).HasValue)
                        packet.Stream.AddValue("Data", data);
                }

                var errorType = packet.Read<byte>(2);
                if (errorType == 1)
                {
                    packet.Read<ushort>("Result", 16);
                    packet.Read<uint>("Unk", 32);
                }
            }
            else
            {
                var modules = packet.Read<byte>(3);
                var module = new BattlenetModuleHandler(packet);
                for (var i = 0; i < modules; ++i)
                {
                    packet.ReadString("Type", 4);
                    packet.ReadFourCC("Region");
                    var id = Utilities.ByteArrayToHexString(packet.ReadBytes("ModuleId", 32));
                    var dataSize = packet.Read<int>("Data size", 10);
                    var data = packet.ReadBytes(dataSize);

                    if (!module.HandleData(id, data, i).HasValue)
                        packet.Stream.AddValue("Data", data, i);
                }

                packet.Stream.AddValue("Ping timeout", packet.Read<uint>(32) + int.MinValue);

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

                packet.ReadString("First name", packet.Read<int>(8));
                packet.ReadString("Last name", packet.Read<int>(8));

                packet.Read<uint>("Account id", 32);

                packet.Read<byte>("Unk8", 8);
                packet.Read<ulong>("Unk64", 64);
                packet.Read<byte>("Unk8", 8);

                packet.ReadString("Account name", packet.Read<int>(5) + 1);

                packet.Read<ulong>("Unk64", 64);
                packet.Read<uint>("Unk32", 32);
                if (packet.Read<bool>("Unk1", 1))
                    packet.ReadBytes("Data", packet.Read<int>(10));
            }
        }

        [BattlenetParser(BattlenetOpcode.ClientPing, BattlenetChannel.Connection, Direction.BNClientToServer)]
        [BattlenetParser(BattlenetOpcode.ServerPong, BattlenetChannel.Connection, Direction.BNServerToClient)]
        [BattlenetParser(BattlenetOpcode.ClientEnableEncyption, BattlenetChannel.Connection, Direction.BNClientToServer)]
        [BattlenetParser(BattlenetOpcode.ClientRealmUpdateSubscribe, BattlenetChannel.WoW, Direction.BNClientToServer)]
        [BattlenetParser(BattlenetOpcode.ServerRealmUpdateEnd, BattlenetChannel.WoW, Direction.BNServerToClient)]
        public static void HandleEmpty(BattlenetPacket packet)
        {
        }

        [BattlenetParser(BattlenetOpcode.ServerRealmUpdateBegin, BattlenetChannel.WoW, Direction.BNServerToClient)]
        public static void HandleRealmUpdateBegin(BattlenetPacket packet)
        {
            if (packet.Read<bool>("Failed", 1))
            {
                packet.Read<byte>("Error code", 8);
                return;
            }

            var charNumberCount = packet.Read<int>(7);  // number of character count entries, lel
            for (var i = 0; i < charNumberCount; ++i)
            {
                packet.Read<byte>("Region", 8);
                packet.Read<short>("Unk2", 12);
                packet.Read<byte>("Battlegroup", 8);
                packet.Read<uint>("Battlegroup index", 32);
                packet.Read<short>("Character count", 16);
            }
        }

        [BattlenetParser(BattlenetOpcode.ServerRealmUpdate, BattlenetChannel.WoW, Direction.BNServerToClient)]
        public static void HandleServerRealmUpdate(BattlenetPacket packet)
        {
            if (!packet.Read<bool>(1))
                return;

            packet.Read<uint>("Timezone", 32);
            packet.ReadSingle("Population");
            packet.Read<byte>("Lock", 8);
            packet.Read<uint>("Unk", 19);
            packet.Stream.AddValue("Type", packet.Read<uint>(32) + int.MinValue);
            packet.ReadString("Name", packet.Read<int>(10));
            if (packet.Read<bool>("Has version", 1))
            {
                packet.ReadString("Version", packet.Read<int>(5));
                packet.Read<uint>("RealmId4", 32);

                var ip = packet.ReadBytes(4);
                var port = packet.ReadBytes(2);

                Array.Reverse(port);

                packet.Stream.AddValue("IP address", new IPAddress(ip));
                packet.Stream.AddValue("Port", BitConverter.ToUInt16(port, 0));
            }

            packet.Read<byte>("Flags", 8);
            packet.Read<byte>("Region", 8);
            packet.Read<short>("Unk2", 12);
            packet.Read<byte>("Battlegroup", 8);
            packet.Read<uint>("Battlegroup index", 32);
        }

        [BattlenetParser(BattlenetOpcode.ClientJoinRequest, BattlenetChannel.WoW, Direction.BNClientToServer)]
        public static void HandleJoinRequest(BattlenetPacket packet)
        {
            packet.Read<uint>("Client seed", 32);
            packet.Read<uint>("Checksum?", 20);
            packet.Read<byte>("Region", 8);
            packet.Read<short>("Unk2", 12);
            packet.Read<byte>("Battlegroup", 8);
            packet.Read<uint>("Battlegroup index", 32);
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

                packet.Stream.AddValue("IP address", new IPAddress(ip), i);
                packet.Stream.AddValue("Port", BitConverter.ToUInt16(port, 0), i);
            }

            count = packet.Read<uint>("IPv6 count", 5);
            for (var i = 0; i < count; ++i)
            {
                var ip = packet.ReadBytes(16);
                var port = packet.ReadBytes(2);

                Array.Reverse(port);

                packet.Stream.AddValue("IP address", new IPAddress(ip), i);
                packet.Stream.AddValue("Port", BitConverter.ToUInt16(port, 0), i);
            }
        }
    }
}
