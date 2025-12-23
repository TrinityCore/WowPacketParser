using System;
using System.Text.Json;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Protos.V5_5_0_61735
{
    public static class BattlenetProtoHandler
    {
        [Parser(Opcode.SMSG_BATTLENET_NOTIFICATION)]
        public static void HandleBattlenetNotification(Packet packet)
        {
            V10_0_0_46181.Protos.V10_2_0_52649.BattlenetProtoHandler.HandleBattlenetNotification(packet);
        }

        [Parser(Opcode.SMSG_BATTLENET_RESPONSE)]
        public static void HandleBattlenetResponse(Packet packet)
        {
            V10_0_0_46181.Protos.V10_2_0_52649.BattlenetProtoHandler.HandleBattlenetResponse(packet);
        }

        [Parser(Opcode.CMSG_BATTLENET_REQUEST)]
        public static void HandleBattlenetRequest(Packet packet)
        {
            V10_0_0_46181.Protos.V10_2_0_52649.BattlenetProtoHandler.HandleBattlenetRequest(packet);
        }
    }
}
