using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class AreaTriggerHandler
    {
        public static void ReadAreaTriggerSpline(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("TimeToTarget", indexes);
            packet.ReadInt32("ElapsedTimeForMovement", indexes);

            packet.ResetBitReader();

            var verticesCount = packet.ReadBits("VerticesCount", 16, indexes);

            for (var i = 0; i < verticesCount; ++i)
                packet.ReadVector3("Points", indexes, i);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_PATH)]
        public static void HandleAreaTriggerRePath(Packet packet)
        {
            packet.ReadPackedGuid128("TriggerGUID");
            ReadAreaTriggerSpline(packet);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_SHAPE)]
        public static void HandleAreaTriggerReShape(Packet packet)
        {
            packet.ReadPackedGuid128("TriggerGUID");

            packet.ResetBitReader();
            var hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline");
            var hasAreaTriggerUnkType = packet.ReadBit("HasAreaTriggerUnkType");

            if (hasAreaTriggerSpline)
                ReadAreaTriggerSpline(packet);

            if (hasAreaTriggerUnkType)
            {
                packet.ResetBitReader();
                var unk1 = packet.ReadBit("AreaTriggerUnk1");
                var hasCenter = packet.ReadBit("HasCenter");
                packet.ReadBit("Unk bit 703 1");
                packet.ReadBit("Unk bit 703 2");

                packet.ReadUInt32("Unk UInt 1");
                packet.ReadInt32("Unk Int 1");
                packet.ReadUInt32("Unk UInt 2");
                packet.ReadSingle("Radius");
                packet.ReadSingle("BlendFromRadius");
                packet.ReadSingle("InitialAngel");
                packet.ReadSingle("ZOffset");

                if (unk1)
                    packet.ReadPackedGuid128("AreaTriggerUnkGUID");

                if (hasCenter)
                    packet.ReadVector3("Center");
            }
        }
    }
}
