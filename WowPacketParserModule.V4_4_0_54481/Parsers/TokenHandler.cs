using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class TokenHandler
    {
        [Parser(Opcode.SMSG_COMMERCE_TOKEN_UPDATE)]
        public static void HandleCommerceTokenUpdate(Packet packet)
        {
            var count1 = packet.ReadInt32("DistributionCount1");
            var count2 = packet.ReadInt32("DistributionCount2");

            for (int i = 0; i < count1; i++)
                packet.ReadInt64("DistributionID", i);

            for (int i = 0; i < count2; i++)
                packet.ReadInt64("DistributionID", i);
        }
    }
}
