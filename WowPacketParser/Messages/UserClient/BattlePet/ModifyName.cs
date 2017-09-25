using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.BattlePet
{
    public unsafe struct ModifyName
    {
        public string Name;
        public DeclinedBattlePetNames? DeclinedNames; // Optional
        public ulong BattlePetGUID;

        [Parser(Opcode.CMSG_BATTLE_PET_MODIFY_NAME, ClientVersionBuild.Zero, ClientVersionBuild.V7_2_0_23826)]
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

        [Parser(Opcode.CMSG_BATTLE_PET_MODIFY_NAME, ClientVersionBuild.V7_2_0_23826)]
        public static void HandleBattlePetModifyName720(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");

            packet.ResetBitReader();

            var nameLen = packet.ReadBits(7);
            var hasDeclinedNames = packet.ReadBit("HasDeclinedNames");

            if (hasDeclinedNames)
            {
                var declinedNamesLen = new uint[5];
                for (int i = 0; i < 5; i++)
                    declinedNamesLen[i] = packet.ReadBits(7);

                for (int i = 0; i < 5; i++)
                    packet.ReadWoWString("DeclinedNames", declinedNamesLen[i]);
            }

            packet.ReadWoWString("Name", nameLen);
        }
    }
}
