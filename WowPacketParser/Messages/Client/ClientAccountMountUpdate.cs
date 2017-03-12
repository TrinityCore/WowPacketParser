using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAccountMountUpdate
    {
        public List<int> MountSpellIDs;
        public List<bool> MountIsFavorite;
        public bool IsFullUpdate;

        [Parser(Opcode.SMSG_ACCOUNT_MOUNT_UPDATE, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V7_0_3_22248)]
        public static void HandleAccountMountUpdate6(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            var int32 = packet.ReadInt32("MountSpellIDsCount");
            var int16 = packet.ReadInt32("MountIsFavoriteCount");

            for (int i = 0; i < int32; i++)
                packet.ReadInt32("MountSpellIDs", i);

            packet.ResetBitReader();

            for (int i = 0; i < int16; i++)
                packet.ReadBit("MountIsFavorite", i);
        }

        [Parser(Opcode.SMSG_ACCOUNT_MOUNT_UPDATE, ClientVersionBuild.V7_0_3_22248)]
        public static void HandleAccountMountUpdate7(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            var mountSpellIDsCount = packet.ReadInt32("MountSpellIDsCount");

            for (int i = 0; i < mountSpellIDsCount; i++)
            {
                packet.ReadInt32("MountSpellIDs", i);

                packet.ResetBitReader();
                packet.ReadBits("MountIsFavorite", 2, i);
            }
        }
    }
}
