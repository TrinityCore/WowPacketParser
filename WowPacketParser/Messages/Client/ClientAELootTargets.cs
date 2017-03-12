using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAELootTargets
    {
        public uint Count;

        [Parser(Opcode.SMSG_AE_LOOT_TARGETS)]
        public static void HandleClientAELootTargets(Packet packet)
        {
            packet.ReadUInt32("Count");
        }
    }
}
