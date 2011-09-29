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
                packet.ReadGuid("[" + i + "] Player GUID");
                Console.WriteLine("[" + i + "] Position: " + packet.ReadVector2());
            }

            var count2 = packet.ReadInt32("Count2");

            for (var i = 0; i < count2; i++)
            {
                packet.ReadGuid("[" + i + "] Player GUID");
                Console.WriteLine("[" + i + "] Position: " + packet.ReadVector2());
            }
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE)]
        public static void HandleBattlefieldMgrStateChanged(Packet packet)
        {
            packet.ReadEnum<BattlegroundStatus>("Old status", TypeCode.UInt32);
            packet.ReadEnum<BattlegroundStatus>("New status", TypeCode.UInt32);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTRY_INVITE)]
        public static void HandleBattlefieldMgrInviteSend(Packet packet)
        {
            packet.ReadInt32("Unk int32 1");
            packet.ReadInt32("Unk int32 2");
            packet.ReadTime("Invite lasts until");
        }
    }
}
