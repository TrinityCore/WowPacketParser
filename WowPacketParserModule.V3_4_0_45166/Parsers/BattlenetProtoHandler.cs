using System;
using System.Text.Json;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Protos.V3_4_0_45166
{
    public static class BattlenetProtoHandler
    {
        [Parser(Opcode.CMSG_BATTLENET_REQUEST, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlenetRequest(Packet packet)
        {
            V10_0_0_46181.Protos.V10_2_0_52649.BattlenetProtoHandler.HandleBattlenetRequest(packet);
        }

        [Parser(Opcode.SMSG_BATTLENET_NOTIFICATION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlenetNotification(Packet packet)
        {
            V10_0_0_46181.Protos.V10_2_0_52649.BattlenetProtoHandler.HandleBattlenetNotification(packet);
        }

        [Parser(Opcode.SMSG_BATTLENET_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlenetResponse(Packet packet)
        {
            V10_0_0_46181.Protos.V10_2_0_52649.BattlenetProtoHandler.HandleBattlenetResponse(packet);
        }
    }
}
