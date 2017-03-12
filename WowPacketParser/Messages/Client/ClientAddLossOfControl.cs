using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAddLossOfControl
    {
        public uint DurationRemaining;
        public ulong Caster;
        public uint Duration;
        public uint LockoutSchoolMask;
        public int SpellID;
        public int Type;
        public int Mechanic;

        [Parser(Opcode.SMSG_ADD_LOSS_OF_CONTROL, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAddLossOfControl602(Packet packet)
        {
            packet.ReadBits("Mechanic", 8);
            packet.ReadBits("Type", 8);

            packet.ReadInt32<SpellId>("SpellID");

            packet.ReadPackedGuid128("Caster");

            packet.ReadInt32("Duration");
            packet.ReadInt32("DurationRemaining");
            packet.ReadInt32("LockoutSchoolMask");
        }
    }
}
