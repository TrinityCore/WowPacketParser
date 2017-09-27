using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V4_3_4_15595.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var isQueued = packet.ReadBit("Queued");
            var hasAccountInfo = packet.ReadBit("Has account info");

            if (isQueued)
                packet.ReadBit("UnkBit");

            if (hasAccountInfo)
            {
                packet.ReadInt32("Billing Time Remaining");
                packet.ReadByteE<ClientType>("Player Expansion");
                packet.ReadInt32("Unknown UInt32");
                packet.ReadByteE<ClientType>("Account Expansion");
                packet.ReadInt32("Billing Time Rested");
                packet.ReadByteE<BillingFlag>("Billing Flags");
            }

            packet.ReadByteE<ResponseCode>("Auth Code");

            if (isQueued)
                packet.ReadInt32("Queue Position");
        }
   
    }
}
