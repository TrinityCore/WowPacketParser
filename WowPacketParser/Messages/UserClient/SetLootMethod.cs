using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetLootMethod
    {
        public ulong Master;
        public int Threshold;
        public byte Method;
        public byte PartyIndex;

        [Parser(Opcode.CMSG_SET_LOOT_METHOD, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleLootMethod(Packet packet)
        {
            packet.ReadUInt32E<LootMethod>("Loot Method");
            packet.ReadGuid("Master GUID");
            packet.ReadUInt32E<ItemQuality>("Loot Threshold");
        }

        [Parser(Opcode.CMSG_SET_LOOT_METHOD, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleLootMethod602(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadByteE<LootMethod>("Method");
            packet.ReadPackedGuid128("Master");
            packet.ReadInt32E<ItemQuality>("Threshold");
        }
    }
}
