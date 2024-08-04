using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class AreaTriggerHandler
    {
        public static void ReadAreaTriggerMovementScript(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("SpellScriptID", indexes);
            packet.ReadVector3("Center", indexes);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_PATH)]
        public static void HandleAreaTriggerReShape(Packet packet)
        {
            packet.ReadPackedGuid128("TriggerGUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
                packet.ReadPackedGuid128("Unused_1100");

            packet.ResetBitReader();
            var hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline");
            var hasAreaTriggerOrbit = packet.ReadBit("HasAreaTriggerOrbit");
            var hasAreaTriggerMovementScript = packet.ReadBit("HasAreaTriggerMovementScript");

            if (hasAreaTriggerSpline)
                V7_0_3_22248.Parsers.AreaTriggerHandler.ReadAreaTriggerSpline(null, packet, "Spline");

            if (hasAreaTriggerMovementScript)
                ReadAreaTriggerMovementScript(packet, "MovementScript");

            if (hasAreaTriggerOrbit)
                V7_0_3_22248.Parsers.AreaTriggerHandler.ReadAreaTriggerOrbit(null, packet, "Orbit");
        }
    }
}
