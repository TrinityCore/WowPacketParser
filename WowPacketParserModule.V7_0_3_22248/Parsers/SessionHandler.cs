using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_QUERY_TIME_RESPONSE)]
        public static void HandleQueryTimeResponse(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503) &&
                ClientVersion.Expansion != ClientType.Classic)
                packet.ReadTime64("CurrentTime");
            else
                packet.ReadTime("CurrentTime");
        }

        [Parser(Opcode.SMSG_ENTER_ENCRYPTED_MODE)]
        [Parser(Opcode.CMSG_ENTER_ENCRYPTED_MODE_ACK)]
        public static void HandleSessionZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_LOGOUT_COMPLETE, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleLogoutComplete(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_LOGOUT_REQUEST, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleLogoutRequest(Packet packet)
        {
            packet.ReadBit("IdleLogout");
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.ReadPackedGuid128("Guid");
            packet.ReadSingle("FarClip");
            packet.Holder.PlayerLogin = new() { PlayerGuid = guid };
        }
    }
}
