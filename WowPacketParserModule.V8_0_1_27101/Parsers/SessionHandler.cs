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

            packet.ReadBytes_Sanitize("Digest", 32);
            packet.ReadBytes_Sanitize("SessionKey", sessionKeyLength);
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.ReadUInt32_Sanitize("VirtualRealmAddress");

            var state = packet.ReadByte("LookupState");
            if (state == 0)
            {
                packet.ResetBitReader();

                packet.ReadBit("IsLocal");
                packet.ReadBit("Unk bit");

                var bits2 = packet.ReadBits(9);
                var bits258 = packet.ReadBits(9);
                packet.ReadBit();

                packet.ReadWoWString_Sanitize("RealmNameActual", bits2);
                packet.ReadWoWString_Sanitize("RealmNameNormalized", bits258);
            }
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
            packet.ReadBytes_Sanitize("EncryptionKey (RSA encrypted)", 256);
            packet.ResetBitReader();
            packet.ReadBit("Enabled");
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V8_2_0_30898)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.ReadBytes_Sanitize("Where (RSA encrypted)", 256);

            AddressType type = packet.ReadByteE<AddressType>("Type");
            switch (type)
            {
                case AddressType.IPv4:
                    packet.ReadIPAddress_Sanitize("Address");
                    break;
                case AddressType.IPv6:
                    packet.ReadIPv6Address_Sanitize("Address");
                    break;
                case AddressType.NamedSocket:
                    packet.ReadWoWString_Sanitize("Address", 128);
                    break;
                default:
                    break;
            }

            packet.ReadUInt16("Port");
            packet.ReadUInt32E<ConnectToSerial>("Serial");
            packet.ReadByte("Con");
            packet.ReadUInt64_Sanitize("Key");
        }
    }
}
