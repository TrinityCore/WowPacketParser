using WowPacketParser.Misc;

namespace WowPacketParserModule.V6_1_0_19678.Parsers
{
    public static class AreaTriggerHandler
    {
        public static void ReadAreaTriggerSpline(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("TimeToTarget", indexes);
            packet.ReadInt32("ElapsedTimeForMovement", indexes);
            var int8 = packet.ReadInt32("VerticesCount", indexes);

            for (var i = 0; i < int8; ++i)
                packet.ReadVector3("Points", indexes, i);
        }
    }
}
