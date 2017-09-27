using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetDungeonDifficulty
    {
        public uint DifficultyID;

        [Parser(Opcode.CMSG_SET_DUNGEON_DIFFICULTY)]
        public static void HandleSetDifficulty(Packet packet)
        {
            packet.ReadInt32E<MapDifficulty>("Difficulty");
        }
    }
}
