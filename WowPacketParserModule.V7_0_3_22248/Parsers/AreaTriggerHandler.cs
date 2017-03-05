using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class AreaTriggerHandler
    {
        public static void ReadAreaTriggerSpline(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("TimeToTarget", indexes);
            packet.Translator.ReadInt32("ElapsedTimeForMovement", indexes);

            packet.Translator.ResetBitReader();

            var verticesCount = packet.Translator.ReadBits("VerticesCount", 16, indexes);

            for (var i = 0; i < verticesCount; ++i)
                packet.Translator.ReadVector3("Points", indexes, i);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_PATH)]
        public static void HandleAreaTriggerRePath(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TriggerGUID");
            ReadAreaTriggerSpline(packet);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_SHAPE)]
        public static void HandleAreaTriggerReShape(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TriggerGUID");

            packet.Translator.ResetBitReader();
            var hasAreaTriggerSpline = packet.Translator.ReadBit("HasAreaTriggerSpline");
            var hasAreaTriggerUnkType = packet.Translator.ReadBit("HasAreaTriggerUnkType");

            if (hasAreaTriggerSpline)
                ReadAreaTriggerSpline(packet);

            if (hasAreaTriggerUnkType)
            {
                packet.Translator.ResetBitReader();
                var unk1 = packet.Translator.ReadBit("AreaTriggerUnk1");
                var hasCenter = packet.Translator.ReadBit("HasCenter");
                packet.Translator.ReadBit("Unk bit 703 1");
                packet.Translator.ReadBit("Unk bit 703 2");

                packet.Translator.ReadUInt32("Unk UInt 1");
                packet.Translator.ReadInt32("Unk Int 1");
                packet.Translator.ReadUInt32("Unk UInt 2");
                packet.Translator.ReadSingle("Radius");
                packet.Translator.ReadSingle("BlendFromRadius");
                packet.Translator.ReadSingle("InitialAngel");
                packet.Translator.ReadSingle("ZOffset");

                if (unk1)
                    packet.Translator.ReadPackedGuid128("AreaTriggerUnkGUID");

                if (hasCenter)
                    packet.Translator.ReadVector3("Center");
            }
        }
    }
}
