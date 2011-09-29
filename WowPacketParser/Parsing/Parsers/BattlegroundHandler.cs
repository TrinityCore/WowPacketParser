using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.MSG_BATTLEGROUND_PLAYER_POSITIONS)]
        public static void HandleBattlegrounPlayerPositions(Packet packet)
        {
            if (packet.GetDirection() == Direction.ClientToServer)
                return;

            var count1 = packet.ReadInt32("Count1");

            for (var i = 0; i < count1; i++)
            {
                packet.ReadInt64("[" + i + "] Player GUID");
                Console.WriteLine("[" + i + "] Position: " + packet.ReadVector2());
            }

            var count2 = packet.ReadInt32("Count2");

            for (var i = 0; i < count2; i++)
            {
                packet.ReadInt64("[" + i + "] Player GUID");
                Console.WriteLine("[" + i + "] Position: " + packet.ReadVector2());
            }
        }
    }
}
