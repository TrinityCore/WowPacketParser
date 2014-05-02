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
        [BattlenetParser(BattlenetOpcode.ServerProofRequest, BattlenetChannel.Auth, Direction.BNServerToClient)]
        public static void HandleServerProofRequest(BattlenetPacket packet)
        {
            var modules = packet.Read<byte>(3);
            for (var i = 0; i < modules; ++i)
            {
                var type = packet.ReadString(4);
                var region = packet.ReadFourCC();
                var id = packet.ReadBytes(32);
                var dataSize = packet.Read<int>(10);
                var data = packet.ReadBytes(dataSize);

                packet.Stream.WriteLine(string.Format("[{0}] Type: {1}", i, type));
                packet.Stream.WriteLine(string.Format("[{0}] Region: {1}", i, region));
                packet.Stream.WriteLine(string.Format("[{0}] ModuleId: {1}", i, Utilities.ByteArrayToHexString(id)));
                packet.Stream.WriteLine(string.Format("[{0}] Data size: {1}", i, dataSize));
                packet.Stream.WriteLine(string.Format("[{0}] Data: {1}", i, Utilities.ByteArrayToHexString(data)));
            }
        }
    }
}
