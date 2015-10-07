using System;
using System.Net;
using WowPacketParser.Enums.Battlenet;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.BattleNet.V37165.Enums;

namespace WowPacketParserModule.BattleNet.V37165.Parsers
{
    public static class WoWRealm
    {
        [BattlenetParser(WoWRealmClientCommand.ListSubscribeRequest)]
        [BattlenetParser(WoWRealmClientCommand.ListUnsubscribe)]
        [BattlenetParser(WoWRealmServerCommand.ListComplete)]
        [BattlenetParser(WoWRealmServerCommand.ToonLoggedOut)]
        public static void HandleEmpty(BattlenetPacket packet)
        {
        }

        [BattlenetParser(WoWRealmClientCommand.JoinRequestV2)]
        public static void HandleJoinRequestV2(BattlenetPacket packet)
        {
            packet.Read<uint>("ClientSalt", 32);
            packet.Read<uint>(20);
            packet.Read<byte>("Region", 8);
            packet.Read<short>(12);
            packet.Read<byte>("Site", 8);
            packet.Read<uint>("Realm", 32);
        }

        [BattlenetParser(WoWRealmClientCommand.MultiLogonRequestV2)]
        public static void HandleMultiLogonRequestV2(BattlenetPacket packet)
        {
            packet.Read<uint>("ClientSalt", 32);
        }

        [BattlenetParser(WoWRealmServerCommand.ListSubscribeResponse)]
        public static void HandleListSubscribeResponse(BattlenetPacket packet)
        {
            if (packet.Read<bool>(1))
            {
                packet.Read<WowAuthResult>("Failure", 8);
                return;
            }

            var charNumberCount = packet.Read<int>(7);
            for (var i = 0; i < charNumberCount; ++i)
            {
                packet.Read<byte>("Region", 8, "ToonCountEntry", i);
                packet.Read<short>(12);
                packet.Read<byte>("Site", 8, "ToonCountEntry", i);
                packet.Read<uint>("Realm", 32, "ToonCountEntry", i);
                packet.Read<short>("Count", 16, "ToonCountEntry", i);
            }
        }

        [BattlenetParser(WoWRealmServerCommand.ListUpdate)]
        public static void HandleListUpdate(BattlenetPacket packet)
        {
            if (packet.Read<bool>(1))
            {
                packet.Read<uint>("Category", 32);
                packet.ReadSingle("Population");
                packet.Read<byte>("StateFlags", 8);
                packet.Read<uint>(19);
                packet.Stream.AddValue("Type", packet.Read<uint>(32) + int.MinValue);
                packet.ReadString("Name", packet.Read<int>(10));
                if (packet.Read<bool>(1))
                {
                    packet.ReadString("Version", packet.Read<int>(5), "PrivilegedData");
                    packet.Read<uint>("ConfigId", 32, "PrivilegedData");

                    var ip = packet.ReadBytes(4);
                    var port = packet.ReadBytes(2);

                    Array.Reverse(port);

                    packet.Stream.AddValue("Address", new IPEndPoint(new IPAddress(ip), BitConverter.ToUInt16(port, 0)), "PrivilegedData");
                }

                packet.Read<RealmInfoFlags>("InfoFlags", 8);
            }

            packet.Read<byte>("Region", 8);
            packet.Read<short>(12);
            packet.Read<byte>("Site", 8);
            packet.Read<uint>("Realm", 32);
        }

        [BattlenetParser(WoWRealmServerCommand.ToonReady)]
        public static void HandleToonReady(BattlenetPacket packet)
        {
            packet.Read<byte>("Region", 8, "Name");
            packet.ReadFourCC("ProgramId", "Name");
            packet.Read<uint>("Realm", 32, "Name");
            packet.ReadString("Name", packet.Read<int>(7) + 2, "Name");
            packet.Read<uint>(21);
            packet.Read<ulong>("Id", 64, "ProfileAddress");
            packet.Read<uint>("Label", 32, "ProfileAddress");
            packet.Read<uint>(21);
            packet.Read<ulong>("Id", 64, "Handle");
            packet.Read<uint>("Realm", 32, "Handle");
            packet.Read<byte>("Region", 8, "Handle");
            packet.ReadFourCC("ProgramId", "Handle");
        }

        [BattlenetParser(WoWRealmServerCommand.JoinResponseV2)]
        public static void HandleJoinResponse(BattlenetPacket packet)
        {
            if (packet.Read<bool>(1))
            {
                packet.Read<WowAuthResult>("Failure", 8);
                return;
            }

            packet.Read<uint>("ServerSalt", 32);
            var count = packet.Read<uint>(5);
            for (var i = 0; i < count; ++i)
            {
                var ip = packet.ReadBytes(4);
                var port = packet.ReadBytes(2);

                Array.Reverse(port);

                packet.Stream.AddValue("Address", new IPEndPoint(new IPAddress(ip), BitConverter.ToUInt16(port, 0)), "v4", i);
            }

            count = packet.Read<uint>(5);
            for (var i = 0; i < count; ++i)
            {
                var ip = packet.ReadBytes(16);
                var port = packet.ReadBytes(2);

                Array.Reverse(port);

                packet.Stream.AddValue("Address", new IPEndPoint(new IPAddress(ip), BitConverter.ToUInt16(port, 0)), "v6", i);
            }
        }
    }
}
