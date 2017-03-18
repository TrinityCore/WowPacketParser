using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlePetSetFlags
    {
        public ulong BattlePetGUID;
        public uint Flags;
        public clibattlepetsetflagcontroltype ControlType;



        [Parser(Opcode.CMSG_BATTLE_PET_SET_FLAGS)]
        public static void HandleBattlePetSetFlags(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
            packet.ReadInt32("Flags");
            packet.ReadBits("ControlType", 2);
        }
    }
}
