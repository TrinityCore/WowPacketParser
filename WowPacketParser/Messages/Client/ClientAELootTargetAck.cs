using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAELootTargetAck // Name not confirmed
    {
        [Parser(Opcode.SMSG_AE_LOOT_TARGET_ACK)]
        public static void HandleLootZero(Packet packet)
        {
        }
    }
}
