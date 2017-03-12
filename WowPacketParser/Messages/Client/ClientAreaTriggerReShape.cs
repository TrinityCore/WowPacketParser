using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAreaTriggerReShape
    {
        public CliAreaTriggerPolygon AreaTriggerPolygon;
        public ulong TriggerGUID;

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_SHAPE)]
        public static void HandleAreaTriggerReShape7(Packet packet)
        {
            packet.ReadPackedGuid128("TriggerGUID");

            packet.ResetBitReader();
            var hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline");
            var hasAreaTriggerUnkType = packet.ReadBit("HasAreaTriggerUnkType");

            if (hasAreaTriggerSpline)
                CliAreaTriggerSpline.ReadAreaTriggerSpline7(packet);

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
