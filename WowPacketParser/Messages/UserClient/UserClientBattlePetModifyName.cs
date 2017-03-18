using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlePetModifyName
    {
        public string Name;
        public DeclinedBattlePetNames? DeclinedNames; // Optional
        public ulong BattlePetGUID;

        [Parser(Opcode.CMSG_BATTLE_PET_MODIFY_NAME)]
        public static void HandleBattlePetModifyName(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");

            packet.ResetBitReader();

            var bits342 = packet.ReadBits(7);
            var bit341 = packet.ReadBit("HasDeclinedNames");

            packet.ReadWoWString("Name", bits342);

            if (bit341)
            {
                var bits97 = new uint[5];
                for (int i = 0; i < 5; i++)
                    bits97[i] = packet.ReadBits(7);

                for (int i = 0; i < 5; i++)
                    packet.ReadWoWString("DeclinedNames", bits97[i]);
            }
        }
    }
}
