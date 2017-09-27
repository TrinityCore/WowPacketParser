using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetRaidDifficulty
    {
        public int DifficultyID;

        [Parser(Opcode.CMSG_SET_RAID_DIFFICULTY, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSetRaidDifficulty(Packet packet)
        {
            packet.ReadInt32E<MapDifficulty>("Difficulty");
        }

        [Parser(Opcode.CMSG_SET_RAID_DIFFICULTY, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSetRaidDifficulty602(Packet packet)
        {
            packet.ReadInt32<DifficultyId>("DifficultyID");
            packet.ReadByte("Force");
        }
    }
}
