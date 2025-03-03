using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_UPDATE_BNET_SESSION_KEY)]
        public static void HandleUpdateBnetSessionKey(Packet packet)
        {
            var sessionKeyLength = (int) packet.ReadBits(7);

            packet.ReadBytes("Digest", 32);
            packet.ReadBytes("SessionKey", sessionKeyLength);
        }

        public static void ReadVirtualRealmNameInfo(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadBit("IsLocal", indexes);
            packet.ReadBit("IsHiddenFromPlayers", indexes);

            var bitsCount = 8;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724) && ClientVersion.RemovedInVersion(ClientVersionBuild.V8_1_5_29683))
                bitsCount = 9;

            var actualNameLength = packet.ReadBits(bitsCount);
            var normalizedNameLength = packet.ReadBits(bitsCount);
            packet.ReadWoWString("RealmNameActual", actualNameLength, indexes);
            packet.ReadWoWString("RealmNameNormalized", normalizedNameLength, indexes);
        }

        public static void ReadVirtualRealmInfo(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("RealmAddress", indexes);
            ReadVirtualRealmNameInfo(packet, indexes, "RealmNameInfo");
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE, ClientVersionBuild.V8_1_0_28724, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.ReadUInt32("VirtualRealmAddress");

            var state = packet.ReadByte("LookupState");
            if (state == 0)
                ReadVirtualRealmNameInfo(packet, "NameInfo");
        }

        [Parser(Opcode.SMSG_BATTLE_NET_CONNECTION_STATUS, ClientVersionBuild.V8_2_0_30898)]
        public static void HandleBattleNetConnectionStatus(Packet packet)
        {
            packet.ReadBits("State", 2); // TODO: enum
            packet.ReadBit("SuppressNotification");
        }

        [Parser(Opcode.SMSG_ENTER_ENCRYPTED_MODE, ClientVersionBuild.V8_2_0_30898)]
        public static void HandleEnterEncryptedMode(Packet packet)
        {
            packet.ReadBytes("EncryptionKey (RSA encrypted)", 256);
            packet.ResetBitReader();
            packet.ReadBit("Enabled");
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V8_2_0_30898)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.ReadBytes("Where (RSA encrypted)", 256);

            AddressType type = packet.ReadByteE<AddressType>("Type");
            switch (type)
            {
                case AddressType.IPv4:
                    packet.ReadIPAddress("Address");
                    break;
                case AddressType.IPv6:
                    packet.ReadIPv6Address("Address");
                    break;
                case AddressType.NamedSocket:
                    packet.ReadWoWString("Address", 128);
                    break;
                default:
                    break;
            }

            packet.ReadUInt16("Port");
            packet.ReadUInt32E<ConnectToSerial>("Serial");
            packet.ReadByte("Con");
            packet.ReadUInt64("Key");
        }
    }
}
