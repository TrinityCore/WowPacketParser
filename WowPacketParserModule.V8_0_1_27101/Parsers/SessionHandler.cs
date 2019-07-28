using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_BATTLENET_UPDATE_SESSION_KEY)]
        public static void HandleBattlenetUpdateSessionKey(Packet packet)
        {
            var sessionKeyLength = (int) packet.ReadBits(7);

            packet.ReadBytes("Digest", 32);
            packet.ReadBytes("SessionKey", sessionKeyLength);
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.ReadUInt32("VirtualRealmAddress");

            var state = packet.ReadByte("LookupState");
            if (state == 0)
            {
                packet.ResetBitReader();

                packet.ReadBit("IsLocal");
                packet.ReadBit("Unk bit");

                var bits2 = packet.ReadBits(9);
                var bits258 = packet.ReadBits(9);
                packet.ReadBit();

                packet.ReadWoWString("RealmNameActual", bits2);
                packet.ReadWoWString("RealmNameNormalized", bits258);
            }
        }

        [Parser(Opcode.SMSG_BATTLENET_SET_SESSION_STATE, ClientVersionBuild.V8_2_0_30898)]
        public static void HandleBattlenetSetSessionState(Packet packet)
        {
            packet.ReadBits("State", 2); // TODO: enum
            packet.ReadBit("SuppressNotification");
        }

        [Parser(Opcode.SMSG_ENABLE_ENCRYPTION, ClientVersionBuild.V8_2_0_30898)]
        public static void HandleEnableEncryption(Packet packet)
        {
            packet.ReadBytes("EncryptionKey (RSA encrypted)", 256);
            packet.ResetBitReader();
            packet.ReadBit("Enabled");
        }
    }
}
