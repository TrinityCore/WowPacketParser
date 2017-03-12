using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliAreaTriggerSpline
    {
        public uint TimeToTarget;
        public uint ElapsedTimeForMovement;
        public List<Vector3> Points;

        public static void ReadAreaTriggerSpline6(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("TimeToTarget", indexes);
            packet.ReadInt32("ElapsedTimeForMovement", indexes);
            var int8 = packet.ReadInt32("VerticesCount", indexes);

            for (var i = 0; i < int8; ++i)
                packet.ReadVector3("Points", indexes, i);
        }

        public static void ReadAreaTriggerSpline7(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("TimeToTarget", indexes);
            packet.ReadInt32("ElapsedTimeForMovement", indexes);

            packet.ResetBitReader();

            var verticesCount = packet.ReadBits("VerticesCount", 16, indexes);

            for (var i = 0; i < verticesCount; ++i)
                packet.ReadVector3("Points", indexes, i);
        }
    }
}
