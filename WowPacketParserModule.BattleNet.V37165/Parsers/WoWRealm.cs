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
            packet.Read<uint>("ClientSalt", 0, 32);
            packet.ReadSkip(20);
            packet.Read<byte>("Region", 0, 8);
            packet.ReadSkip(12);
            packet.Read<byte>("Site", 0, 8);
            packet.Read<uint>("Realm", 0, 32);
        }

        [BattlenetParser(WoWRealmClientCommand.MultiLogonRequestV2)]
        public static void HandleMultiLogonRequestV2(BattlenetPacket packet)
        {
            packet.Read<uint>("ClientSalt", 0, 32);
        }

        [BattlenetParser(WoWRealmServerCommand.ListSubscribeResponse)]
        public static void HandleListSubscribeResponse(BattlenetPacket packet)
        {
            if (packet.ReadBoolean())
            {
                packet.Read<WowAuthResult>("Failure", 0, 8);
                return;
            }

            var charNumberCount = packet.Read<int>(0, 7);
            for (var i = 0; i < charNumberCount; ++i)
            {
                packet.Read<byte>("Region", 0, 8, "ToonCountEntry", i);
                packet.ReadSkip(12);
                packet.Read<byte>("Site", 0, 8, "ToonCountEntry", i);
                packet.Read<uint>("Realm", 0, 32, "ToonCountEntry", i);
                packet.Read<short>("Count", 0, 16, "ToonCountEntry", i);
            }
        }

        [BattlenetParser(WoWRealmServerCommand.ListUpdate)]
        public static void HandleListUpdate(BattlenetPacket packet)
        {
            if (packet.ReadBoolean())
            {
                packet.Read<uint>("Category", 0, 32);
                packet.ReadSingle("Population");
                packet.Read<byte>("StateFlags", 0, 8);
                packet.ReadSkip(19);
                packet.Read<uint>("Type", int.MinValue, 32);
                packet.ReadString("Name", 0, 10);
                if (packet.ReadBoolean())
                {
                    packet.ReadString("Version", 0, 5, "PrivilegedData");
                    packet.Read<uint>("ConfigId", 0, 32, "PrivilegedData");

                    var ip = packet.ReadBytes(4);
                    var port = packet.ReadBytes(2);

                    Array.Reverse(port);

                    packet.Stream.AddValue("Address", new IPEndPoint(new IPAddress(ip), BitConverter.ToUInt16(port, 0)), "PrivilegedData");
                }

                packet.Read<RealmInfoFlags>("InfoFlags", 0, 8);
            }

            packet.Read<byte>("Region", 0, 8);
            packet.ReadSkip(12);
            packet.Read<byte>("Site", 0, 8);
            packet.Read<uint>("Realm", 0, 32);
        }

        [BattlenetParser(WoWRealmServerCommand.ToonReady)]
        public static void HandleToonReady(BattlenetPacket packet)
        {
            packet.Read<byte>("Region", 0, 8, "Name");
            packet.ReadFourCC("ProgramId", "Name");
            packet.Read<uint>("Realm", 0, 32, "Name");
            packet.ReadString("Name", 2, 7, "Name");
            packet.ReadSkip(21);
            packet.Read<ulong>("Id", 0, 64, "ProfileAddress");
            packet.Read<uint>("Label", 0, 32, "ProfileAddress");
            packet.Read<ulong>("Id", 0, 64, "Handle");
            packet.Read<uint>("Realm", 0, 32, "Handle");
            packet.Read<byte>("Region", 0, 8, "Handle");
            packet.ReadFourCC("ProgramId", "Handle");
        }

        [BattlenetParser(WoWRealmServerCommand.JoinResponseV2)]
        public static void HandleJoinResponse(BattlenetPacket packet)
        {
            if (packet.ReadBoolean())
            {
                packet.Read<WowAuthResult>("Failure", 0, 8);
                return;
            }

            packet.Read<uint>("ServerSalt", 0, 32);
            var count = packet.Read<uint>(0, 5);
            for (var i = 0; i < count; ++i)
            {
                var ip = packet.ReadBytes(4);
                var port = packet.ReadBytes(2);

                Array.Reverse(port);

                packet.Stream.AddValue("Address", new IPEndPoint(new IPAddress(ip), BitConverter.ToUInt16(port, 0)), "v4", i);
            }

            count = packet.Read<uint>(0, 5);
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
