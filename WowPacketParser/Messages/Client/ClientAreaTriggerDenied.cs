using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAreaTriggerDenied
    {
        public bool Entered;
        public int AreaTriggerID;

        [Parser(Opcode.SMSG_AREA_TRIGGER_DENIED, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAreaTriggerDenied(Packet packet)
        {
            packet.ReadInt32("AreaTriggerID");
            packet.ReadBit("Entered");
        }
    }
}
