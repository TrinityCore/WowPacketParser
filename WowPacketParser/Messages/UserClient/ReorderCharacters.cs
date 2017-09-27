using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct ReorderCharacters
    {
        //public List<UserClientReorderEntry> Entries;

        [Parser(Opcode.CMSG_REORDER_CHARACTERS, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_7_17898)] // 4.3.4
        public static void HandleReorderCharacters(Packet packet)
        {
            var count = packet.ReadBits("Count", 10);

            var guids = new byte[count][];

            for (int i = 0; i < count; ++i)
                guids[i] = packet.StartBitStream(1, 4, 5, 3, 0, 7, 6, 2);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guids[i], 6);
                packet.ReadXORByte(guids[i], 5);
                packet.ReadXORByte(guids[i], 1);
                packet.ReadXORByte(guids[i], 4);
                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 3);
                packet.ReadByte("Slot", i);
                packet.ReadXORByte(guids[i], 2);
                packet.ReadXORByte(guids[i], 7);

                packet.WriteGuid("Character Guid", guids[i], i);
            }
        }


        [Parser(Opcode.CMSG_REORDER_CHARACTERS, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleReorderCharacters547(Packet packet)
        {
            var count = packet.ReadBits("Count", 9);

            var guids = new byte[count][];

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 3, 7, 4, 1, 2, 5, 0, 6);
            }

            for (int i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guids[i], 4);
                packet.ReadXORByte(guids[i], 7);
                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 2);
                packet.ReadByte("Slot", i);
                packet.ReadXORByte(guids[i], 6);
                packet.ReadXORByte(guids[i], 3);
                packet.ReadXORByte(guids[i], 1);
                packet.ReadXORByte(guids[i], 5);

                packet.WriteGuid("Character Guid", guids[i], i);
            }
        }

        [Parser(Opcode.CMSG_REORDER_CHARACTERS)]
        public static void HandleReorderCharacters602(Packet packet)
        {
            var count = packet.ReadBits("CharactersCount", 9);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("PlayerGUID");
                packet.ReadByte("NewPosition", i);
            }
        }

    }
}
