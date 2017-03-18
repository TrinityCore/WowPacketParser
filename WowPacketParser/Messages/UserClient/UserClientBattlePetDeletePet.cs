using WowPacketParser.Misc;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlePetDeletePet
    {
        public ulong BattlePetGUID;

        public static void HandleBattlePetDeletePet(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
        }
    }
}
