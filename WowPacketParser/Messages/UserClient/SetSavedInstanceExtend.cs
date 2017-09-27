using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetSavedInstanceExtend
    {
        public int MapID;
        public bool Extend;
        public uint DifficultyID;

        [Parser(Opcode.CMSG_SET_SAVED_INSTANCE_EXTEND, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSetSavedInstanceExtend(Packet packet)
        {
            packet.ReadInt32<MapId>("Map Id");
            packet.ReadInt32E<MapDifficulty>("Difficulty");
            packet.ReadBool("Extended");
        }

        [Parser(Opcode.CMSG_SET_SAVED_INSTANCE_EXTEND, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSetSavedInstanceExtend602(Packet packet)
        {
            packet.ReadInt32<MapId>("MapID");
            packet.ReadInt32<DifficultyId>("DifficultyID");
            packet.ReadBit("Extended");
        }
    }
}
