using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class BattlegroundHandler
    {
        // Hack to read BG Guids (didn't wanted to touch Guid class till 100% sure this is real GUID as it seems to be)
        private static bool ReadBgGuid(UInt64 guid)
        {
            var bgGuid = new WowPacketParser.Misc.Guid(guid);
            bool isBg = bgGuid.GetHighType() == HighGuidType.Unknown3;
            int low = (int)bgGuid.GetLow();
            if (isBg)
            {
                var bgEntry = new BgEntry(low);
                Console.WriteLine("BG GUID: Full: 0x" + bgGuid.Full.ToString("X8") + " BG Data: " + bgEntry);
            }
            else
                Console.WriteLine("BG GUID: " + bgGuid);
            return low != 0;
        }

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

        [Parser(Opcode.CMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldListClient(Packet packet)
        {
            Console.WriteLine("BGType: " + Extensions.BattlegroundLine(packet.ReadInt32()));
            packet.ReadBoolean("From UI");
            packet.ReadByte("Unk Byte (BattlefieldList)");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldListServer(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBoolean("From UI");
            Console.WriteLine("BGType: " + Extensions.BattlegroundLine(packet.ReadInt32()));
            packet.ReadByte("Min Level");
            packet.ReadByte("Max Level");
            packet.ReadBoolean("Has Win");
            packet.ReadInt32("Winner Honor Reward");
            packet.ReadInt32("Winner Arena Reward");
            packet.ReadInt32("Losser Honor Reward");

            var random = packet.ReadBoolean("Is random");
            if (random)
            {
                packet.ReadByte("Random Has Win");
                packet.ReadInt32("Random Winner Honor Reward");
                packet.ReadInt32("Random Winner Arena Reward");
                packet.ReadInt32("Random Losser Honor Reward");
            }

            var count = packet.ReadUInt32("BG Instance count");
            for (var i = 0; i < count; i++)
                packet.ReadUInt32("[" + i + "] Instance ID");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_PORT)]
        public static void HandleBattlefieldPort(Packet packet)
        {
            ReadBgGuid(packet.ReadUInt64());
            packet.ReadBoolean("Join BG");
        }

        [Parser(Opcode.CMSG_LEAVE_BATTLEFIELD)]
        public static void HandleBattlefieldLeave(Packet packet)
        {
            ReadBgGuid(packet.ReadUInt64());
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_STATUS)]
        public static void HandleBattlefieldStatusClient(Packet packet)
        {
            // Added here to have all BG related opcodes in same file
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS)]
        public static void HandleBattlefieldStatusServer(Packet packet)
        {
            packet.ReadUInt32("Queue Slot");

             // Hack - Sometimes found incomplete data... Old versions?
            if (!ReadBgGuid(packet.ReadUInt64()))
            {
                packet.SetPosition(packet.GetLength());
                return;
            }

            packet.ReadByte("Min Level");
            packet.ReadByte("Max Level");
            packet.ReadUInt32("Client Instance ID");
            packet.ReadBoolean("Rated");
            var status = packet.ReadEnum<BattlegroundStatus>("Status", TypeCode.UInt32);
            switch (status)
            {
                case BattlegroundStatus.WaitQueue:
                    packet.ReadUInt32("Average Wait Time");
                    packet.ReadUInt32("Time in queue");
                    break;
                case BattlegroundStatus.WaitJoin:
                    Console.WriteLine("Map ID: " + Extensions.MapLine(packet.ReadInt32()));
                    ReadBgGuid(packet.ReadUInt64());
                    packet.ReadUInt32("Time left");
                    break;
                case BattlegroundStatus.InProgress:
                    Console.WriteLine("Map ID: " + Extensions.MapLine(packet.ReadInt32()));
                    ReadBgGuid(packet.ReadUInt64());
                    packet.ReadUInt32("Auto Leave Time");
                    packet.ReadUInt32("Time in BG");
                    packet.ReadByte("Is BG?");
                    break;
            }
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_HELLO)]
        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUERY)]
        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUEUE)]
        [Parser(Opcode.CMSG_REPORT_PVP_AFK)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED)]
        public static void HandleBattlemasterHello(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN)]
        public static void HandleBattlemasterJoin(Packet packet)
        {
            packet.ReadGuid("GUID");
            Console.WriteLine("BGType: " + Extensions.BattlegroundLine(packet.ReadInt32()));
            packet.ReadUInt32("Instance Id");
            packet.ReadBoolean("As group");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA)]
        public static void HandleBattlemasterJoinArena(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Slot");
            packet.ReadBoolean("As group");
            packet.ReadBoolean("Rated");
        }

        [Parser(Opcode.CMSG_REPORT_PVP_AFK)]
        public static void HandleReportPvPAFK(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        // Reading 11 Bytes in random order. Only 1st Byte seems to have data (Only seen '5')
        [Parser(Opcode.SMSG_REPORT_PVP_AFK_RESULT)]
        public static void HandleReportPvPAFKResult(Packet packet)
        {
            packet.ReadByte("Unk Byte (afkResult)");
            packet.ReadUInt64("Unk Uint64 (afkResult)");
            packet.ReadUInt16("Unk Uint16 (afkResult)");
        }

        [Parser(Opcode.SMSG_GROUP_JOINED_BATTLEGROUND)]
        public static void HandleRGroupJoinedBattleground(Packet packet)
        {
            var val = packet.ReadInt32();
            if (val < 1)
            {
                var result = (BattlegroundError)val;
                Console.WriteLine("Result: " + result);
                if (result == BattlegroundError.JoinFailedAsGroup ||
                    result == BattlegroundError.CouldntJoinQueueInTime)
                    packet.ReadGuid("GUID");
            }
            else
                Console.WriteLine("Result: Joined (BGType: " + Extensions.BattlegroundLine(val) + ")");
        }

        [Parser(Opcode.MSG_PVP_LOG_DATA)]
        public static void HandlePvPLogData(Packet packet)
        {
            var arena = packet.ReadBoolean("Arena");
            if (arena)
            {
                packet.ReadUInt32("[1] Points Lost");
                packet.ReadUInt32("[1] Points Gained");
                packet.ReadUInt32("[1] Matchmaker Rating");
                packet.ReadUInt32("[0] Points Lost");
                packet.ReadUInt32("[0] Points Gained");
                packet.ReadUInt32("[0] Matchmaker Rating");
                packet.ReadCString("[1] Name");
                packet.ReadCString("[0] Name");
            }

            var finished = packet.ReadBoolean("Finished");
            if (finished)
                packet.ReadByte("Winner");

            var count = packet.ReadUInt32("Score count");
            for (var i = 0; i < count; i++)
            {
                packet.ReadGuid("[" + i + "] Player GUID");
                packet.ReadUInt32("[" + i + "] Killing Blows");
                if (!arena)
                {
                    packet.ReadUInt32("[" + i + "] Honorable Kills");
                    packet.ReadUInt32("[" + i + "] Deaths");
                    packet.ReadUInt32("[" + i + "] Bonus Honor");
                }
                else
                    packet.ReadByte("[" + i + "] BG Team");

                packet.ReadUInt32("[" + i + "] Damage done");
                packet.ReadUInt32("[" + i + "] Healing done");

                var count2 = packet.ReadUInt32("[" + i + "] Extra values counter");

                for (var j = 0; j < count2; j++)
                    packet.ReadUInt32("[" + i + "][" + j + "] Value");
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
            packet.ReadInt32("Battle Id");
            packet.ReadInt32("Zone Id");
            packet.ReadTime("Invite lasts until");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE)]
        public static void HandleBattlefieldMgrQueueInvite(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_QUEUE_INVITE_RESPONSE)]
        public static void HandleBattlefieldMgrQueueInviteResponse(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadBoolean("Accepted");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_REQUEST_RESPONSE)]
        public static void HandleBattlefieldMgrQueueRequestResponse(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadInt32("Zone Id");
            packet.ReadByte("Accepted");
            packet.ReadByte("Logging In");
            packet.ReadByte("Warmup");

        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERED)]
        public static void HandleBattlefieldMgrEntered(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadByte("Unk Byte 1");
            packet.ReadByte("Unk Byte 2");
            packet.ReadByte("Clear AFK");

        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECTED)]
        public static void HandleBattlefieldMgrEjected(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadByte("Reason");
            packet.ReadByte("Battle Status");
            packet.ReadByte("Relocated");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_ENTRY_INVITE_RESPONSE)]
        public static void HandleBattlefieldMgrEntryInviteResponse(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadBoolean("Accepted");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_EXIT_REQUEST)]
        public static void HandleBattlefieldMgrExitRequest(Packet packet)
        {
            packet.ReadInt32("Battle Id");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_ROSTER)]
        public static void HandleArenaTeamRoster(Packet packet)
        {
            packet.ReadUInt32("Team Id");
            var unk = packet.ReadByte("Unk Byte");
            var count = packet.ReadUInt32("Member count");
            packet.ReadUInt32("Type");

            for (var i = 0; i < count; i++)
            {
                packet.ReadGuid("[" + i + "] GUID");
                packet.ReadBoolean("[" + i + "] Online");
                packet.ReadCString("[" + i + "] Name");
                packet.ReadUInt32("[" + i + "] Captain");
                packet.ReadByte("[" + i + "] Level");
                packet.ReadByte("[" + i + "] Class");
                packet.ReadUInt32("[" + i + "] Week Games");
                packet.ReadUInt32("[" + i + "] Week Win");
                packet.ReadUInt32("[" + i + "] Seasonal Games");
                packet.ReadUInt32("[" + i + "] Seasonal Wins");
                packet.ReadUInt32("[" + i + "] Personal Rating");
                if (unk > 0)
                {
                    packet.ReadSingle("[" + i + "] Unk float 1");
                    packet.ReadSingle("[" + i + "] Unk float 2");
                }

            }
        }

        [Parser(Opcode.SMSG_ARENA_OPPONENT_UPDATE)]
        public static void HandleArenaOpponentUpdate(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_STATS)]
        public static void HandleArenaTeamStats(Packet packet)
        {
            packet.ReadUInt32("Team Id");
            packet.ReadUInt32("Rating");
            packet.ReadUInt32("Week Games");
            packet.ReadUInt32("Week Win");
            packet.ReadUInt32("Seasonal Games");
            packet.ReadUInt32("Seasonal Wins");
            packet.ReadUInt32("Rank");
        }

        //[Parser(Opcode.CMSG_BATTLEFIELD_MGR_QUEUE_REQUEST)]
        //[Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECT_PENDING)]
        //[Parser(Opcode.CMSG_BATTLEFIELD_MANAGER_ADVANCE_STATE)]
        //[Parser(Opcode.CMSG_BATTLEFIELD_MANAGER_SET_NEXT_TRANSITION_TIME)]
        //[Parser(Opcode.SMSG_JOINED_BATTLEGROUND_QUEUE)]
        //[Parser(Opcode.SMSG_BATTLEGROUND_INFO_THROTTLED)]
        //[Parser(Opcode.SMSG_BATTLEFIELD_PORT_DENIED)]
        //[Parser(Opcode.CMSG_START_BATTLEFIELD_CHEAT)]
        //[Parser(Opcode.CMSG_END_BATTLEFIELD_CHEAT)]


    }
}
