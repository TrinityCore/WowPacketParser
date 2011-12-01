using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.SMSG_AREA_SPIRIT_HEALER_TIME)]
        public static void HandleAreaSpiritHealerTime(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Timer");
        }

        [Parser(Opcode.MSG_BATTLEGROUND_PLAYER_POSITIONS)]
        public static void HandleBattlegrounPlayerPositions(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            var count1 = packet.ReadInt32("Count1");

            for (var i = 0; i < count1; i++)
            {
                packet.ReadGuid("[" + i + "] Player GUID");
                packet.Writer.WriteLine("[" + i + "] Position: " + packet.ReadVector2());
            }

            var count2 = packet.ReadInt32("Count2");

            for (var i = 0; i < count2; i++)
            {
                packet.ReadGuid("[" + i + "] Player GUID");
                packet.Writer.WriteLine("[" + i + "] Position: " + packet.ReadVector2());
            }
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldListClient(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BGType");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                return;

            packet.ReadBoolean("From UI");
            packet.ReadByte("Unk Byte (BattlefieldList)");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldListServer(Packet packet)
        {
            // TODO: 4.2.2 struct (can be found here: http://pastebin.com/S5gs2erB)
            packet.ReadGuid("GUID");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_0_14333))
                packet.ReadBoolean("From UI");

            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BGType");
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
            packet.ReadGuid("GUID");
            packet.ReadBoolean("Join BG");
        }

        [Parser(Opcode.CMSG_LEAVE_BATTLEFIELD)]
        public static void HandleBattlefieldLeave(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_LEAVE)] // Differences from above packet?
        [Parser(Opcode.CMSG_BATTLEFIELD_STATUS)]
        public static void HandleBGZeroLengthPackets(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS)]
        public static void HandleBattlefieldStatusServer(Packet packet)
        {
            var slot = packet.ReadUInt32("Queue Slot");
            var guid = packet.ReadGuid("GUID");

            if (slot == 1 && guid.GetLow() == 0)
                return;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
            {
                packet.ReadByte("Min Level");
                packet.ReadByte("Max Level");
            }

            if (!packet.CanRead())
                return;

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
                    packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_5_12213))
                        packet.ReadGuid("GUID");

                    packet.ReadUInt32("Time left");
                    break;
                case BattlegroundStatus.InProgress:
                    packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_5_12213))
                        packet.ReadGuid("GUID");

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
            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BGType");
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
                packet.Writer.WriteLine("Result: " + result);
                if (result == BattlegroundError.JoinFailedAsGroup ||
                    result == BattlegroundError.CouldntJoinQueueInTime)
                    packet.ReadGuid("GUID");
            }
            else
                packet.Writer.WriteLine("Result: Joined (BGType: " + StoreGetters.GetName(StoreNameType.Battleground, val) + ")");
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
                packet.ReadGuid("Player GUID", i);
                packet.ReadUInt32("Killing Blows", i);
                if (!arena)
                {
                    packet.ReadUInt32("Honorable Kills", i);
                    packet.ReadUInt32("Deaths", i);
                    packet.ReadUInt32("Bonus Honor", i);
                }
                else
                    packet.ReadByte("BG Team", i);

                packet.ReadUInt32("Damage done", i);
                packet.ReadUInt32("Healing done", i);

                var count2 = packet.ReadUInt32("Extra values counter", i);

                for (var j = 0; j < count2; j++)
                    packet.ReadUInt32("Value", i, j);
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

        [Parser(Opcode.CMSG_ARENA_TEAM_ROSTER)]
        [Parser(Opcode.CMSG_ARENA_TEAM_QUERY)]
        public static void HandleArenaTeamQuery(Packet packet)
        {
            packet.ReadUInt32("Team ID");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT)]
        public static void HandleArenaTeamCommandResult(Packet packet)
        {
            packet.ReadUInt32("Action"); // FIXME: Use enum
            packet.ReadCString("Team Name");
            packet.ReadCString("Player Name");
            packet.ReadUInt32("ErrorId"); // FIXME: Use enum
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_EVENT)]
        public static void HandleArenaTeamEvent(Packet packet)
        {
            packet.ReadByte("Event"); // FIXME: Use enum
            var count = packet.ReadByte("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadCString("Param", i);

            if (packet.CanRead())
                packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_QUERY_RESPONSE)]
        public static void HandleArenaTeamQueryResponse(Packet packet)
        {
            packet.ReadUInt32("Team ID");
            packet.ReadCString("Team Name");
            packet.ReadUInt32("Type");
            packet.ReadUInt32("Background Color");
            packet.ReadUInt32("Emblem Style");
            packet.ReadUInt32("Emblem Color");
            packet.ReadUInt32("Border Style");
            packet.ReadUInt32("Border Color");
        }

        [Parser(Opcode.SMSG_ARENA_OPPONENT_UPDATE)]
        public static void HandleArenaOpponentUpdate(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.MSG_INSPECT_ARENA_TEAMS)]
        public static void HandleInspectArenaTeams(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (packet.Direction == Direction.ClientToServer)
                return;

            packet.ReadByte("Slot");
            packet.ReadUInt32("Team Id");
            packet.ReadUInt32("Team Rating");
            packet.ReadUInt32("Team Season Games");
            packet.ReadUInt32("Team Season Wins");
            packet.ReadUInt32("Player Season Games");
            packet.ReadUInt32("Player Personal Rating");
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
