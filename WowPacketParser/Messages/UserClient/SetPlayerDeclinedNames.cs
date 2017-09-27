using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetPlayerDeclinedNames
    {
        public ulong Player;
        public string[/*5*/] DeclinedName;

        [Parser(Opcode.CMSG_SET_PLAYER_DECLINED_NAMES)]
        public static void HandleSetPlayerDeclinedNames(Packet packet)
        {
            packet.ReadPackedGuid128("Player");

            var count = new int[5];
            for (var i = 0; i < 5; ++i)
                count[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < 5; ++i)
                packet.ReadWoWString("DeclinedName", count[i], i);
        }
    }
}
