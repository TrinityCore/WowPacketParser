using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.DF
{
    public unsafe struct GetSystemInfo
    {
        public byte PartyIndex;
        public bool Player;

        [Parser(Opcode.CMSG_DUNGEON_FINDER_GET_SYSTEM_INFO, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleDungeonFinderGetSystemInfo(Packet packet)
        {
            packet.ReadBit("Unk boolean");
        }


        [Parser(Opcode.CMSG_DUNGEON_FINDER_GET_SYSTEM_INFO, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleDungeonFinderGetSystemInfo540(Packet packet)
        {
            packet.ReadByte("Unk Byte");
            packet.ReadBit("Unk boolean");
        }
    }
}
