using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Battlenet;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class BattlenetHandler
    {
        [BattlenetParser(BattlenetOpcode.ClientInformationRequest, BattlenetChannel.Auth, Direction.BNClientToServer)]
        [BattlenetParser(BattlenetOpcode.ClientInformationRequestOld, BattlenetChannel.Auth, Direction.BNClientToServer)]
        public static void HandleInformationRequest(BattlenetPacket packet)
        {
            packet.Stream.WriteLine(string.Format("Program: {0}", packet.ReadFourCC()));
            packet.Stream.WriteLine(string.Format("Platform: {0}", packet.ReadFourCC()));
            packet.Stream.WriteLine(string.Format("Locale: {0}", packet.ReadFourCC()));

            var components = packet.Read<int>(6);
            for (var i = 0; i < components; ++i)
            {
                packet.Stream.WriteLine(string.Format("[{0}] Program: {1}", i, packet.ReadFourCC()));
                packet.Stream.WriteLine(string.Format("[{0}] Platform: {1}", i, packet.ReadFourCC()));
                packet.Stream.WriteLine(string.Format("[{0}] Build: {1}", i, packet.Read<uint>(32)));
            }

            if (packet.Read<bool>(1))
                packet.Stream.WriteLine(string.Format("Login: {0}", packet.ReadString(packet.Read<int>(9) + 3)));
        }

        [BattlenetParser(BattlenetOpcode.ServerProofRequest, BattlenetChannel.Auth, Direction.BNServerToClient)]
        public static void HandleServerProofRequest(BattlenetPacket packet)
        {
            var modules = packet.Read<byte>(3);
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
                //switch (id.Substring(0, 2))
                //{
                //}

                packet.Stream.WriteLine(string.Format("[{0}] Data: {1}", i, Utilities.ByteArrayToHexString(data)));
            }
        }

        [BattlenetParser(BattlenetOpcode.ClientProofResponse, BattlenetChannel.Auth, Direction.BNClientToServer)]
        public static void HandleProofResponse(BattlenetPacket packet)
        {
            var modules = packet.Read<byte>(3);
            for (var i = 0; i < modules; ++i)
            {
                var dataSize = packet.Read<int>(10);
                var data = packet.ReadBytes(dataSize);
                packet.Stream.WriteLine(string.Format("[{0}] Data size: {1}", i, dataSize));
                packet.Stream.WriteLine(string.Format("[{0}] Data: {1}", i, Utilities.ByteArrayToHexString(data)));
            }
        }

        [BattlenetParser(BattlenetOpcode.ServerComplete, BattlenetChannel.Auth, Direction.BNServerToClient)]
        public static void HandleComplete(BattlenetPacket packet)
        {
            var failed = packet.Read<bool>(1);

            if (failed)
            {
                if (packet.Read<bool>(1)) // has module
                {
                    var dataSize = packet.Read<int>(10);
                    var data = packet.ReadBytes(dataSize);

                    packet.Stream.WriteLine(string.Format("Type: {0}", packet.ReadString(4)));
                    packet.Stream.WriteLine(string.Format("Region: {0}", packet.ReadFourCC()));
                    packet.Stream.WriteLine(string.Format("ModuleId: {0}", Utilities.ByteArrayToHexString(packet.ReadBytes(32))));
                    packet.Stream.WriteLine(string.Format("Data size: {0}", dataSize));
                    //switch (id.Substring(0, 2))
                    //{
                    //}

                    packet.Stream.WriteLine(string.Format("Data: {1}", Utilities.ByteArrayToHexString(data)));

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
                    //switch (id.Substring(0, 2))
                    //{
                    //}

                    packet.Stream.WriteLine(string.Format("[{0}] Data: {1}", i, Utilities.ByteArrayToHexString(data)));
                }

                packet.Stream.WriteLine(string.Format("Ping timeout: {0}", packet.Read<uint>(32) + int.MinValue));

                var hasOptionalData = packet.Read<bool>(1);
                if (hasOptionalData)
                {
                    var hasConnectionInfo = packet.Read<bool>(1);
                    if (hasConnectionInfo)
                    {
                        packet.Stream.WriteLine(string.Format("Threshold: {0}", packet.Read<uint>(32)));
                        packet.Stream.WriteLine(string.Format("Rate: {0}", packet.Read<uint>(32)));
                    }
                }

                packet.Stream.WriteLine(string.Format("Unk bool: {0}", packet.Read<bool>(1)));
                packet.Stream.WriteLine(string.Format("First Name: {0}", packet.ReadString(packet.Read<int>(8))));
                packet.Stream.WriteLine(string.Format("Last Name: {0}", packet.ReadString(packet.Read<int>(8))));

                packet.Stream.WriteLine(string.Format("Account Id: {0}", packet.Read<uint>(32)));

                packet.Stream.WriteLine(string.Format("Unk8: {0}", packet.Read<byte>(8)));
                packet.Stream.WriteLine(string.Format("Unk64: {0}", packet.Read<ulong>(64)));
                packet.Stream.WriteLine(string.Format("Unk8: {0}", packet.Read<byte>(8)));

                packet.Stream.WriteLine(string.Format("Account name: {0}", packet.ReadString(packet.Read<int>(5) + 1)));

                packet.Stream.WriteLine(string.Format("Unk64: {0}", packet.Read<ulong>(64)));
                packet.Stream.WriteLine(string.Format("Unk32: {0}", packet.Read<uint>(32)));
                packet.Stream.WriteLine(string.Format("Unk8: {0}", packet.Read<byte>(8)));
            }
        }





        [BattlenetParser(BattlenetOpcode.ClientPing, BattlenetChannel.Creep, Direction.BNClientToServer)]
        [BattlenetParser(BattlenetOpcode.ServerPong, BattlenetChannel.Creep, Direction.BNServerToClient)]
        public static void HandlePong(BattlenetPacket packet)
        {
        }
    }
}
