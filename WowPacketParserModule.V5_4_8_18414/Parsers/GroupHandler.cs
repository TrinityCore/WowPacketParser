using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_GROUP_ACCEPT)]
        public static void HandleGroupAccept(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GROUP_ACCEPT_DECLINE)]
        public static void HandleGroupAcceptDecline(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GROUP_ASSISTANT_LEADER)]
        public static void HandleGroupAssistantLeader(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GROUP_CHANGE_SUB_GROUP)]
        public static void HandleGroupChangesubgroup(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GROUP_INVITE)]
        public static void HandleCGroupInvite(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GROUP_INVITE_RESPONSE)]
        public static void HandleGroupInviteResponse(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GROUP_RAID_CONVERT)]
        public static void HandleGroupRaidConvert(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GROUP_SET_LEADER)]
        public static void HandleGroupSetLeader(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GROUP_SET_ROLES)]
        public static void HandleGroupSetRoles(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GROUP_SWAP_SUB_GROUP)]
        public static void HandleGroupSwapSubGroup(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GROUP_UNINVITE_GUID)]
        public static void HandleGroupUninviteGuid(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_LFG_TELEPORT)]
        public static void HandleLFGTeleport(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            packet.ReadByte("Flags");
            var guid = new byte[8];
            packet.StartBitStream(guid, 7, 4, 0, 1, 3, 6, 2, 5);
            packet.ReadXORBytes(guid, 3, 6, 5, 2, 1, 4, 0, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_EVERYONE_IS_ASSISTANT)]
        public static void HandleEveryoneIsAssistant(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.MSG_MINIMAP_PING)]
        public static void HandleServerMinimapPing(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.MSG_PARTY_ASSIGNMENT)]
        public static void HandlePartyAssigment(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.MSG_RAID_READY_CHECK)]
        public static void HandleRaidReadyCheck(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.MSG_RAID_READY_CHECK_CONFIRM)]
        public static void HandleRaidReadyCheckConfirm(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.MSG_RANDOM_ROLL)]
        public static void HandleRandomRollPackets(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_GROUP_SET_LEADER)]
        [Parser(Opcode.SMSG_GROUP_DECLINE)]
        public static void HandleGroupDecline(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_GROUP_INVITE)]
        public static void HandleSGroupInvite(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_GROUP_SET_ROLE)]
        public static void HandleGroupSetRole(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_PARTY_COMMAND_RESULT)]
        public static void HandlePartyCommandResult(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS)]
        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS_FULL)]
        public static void HandlePartyMemberStats(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_RAID_GROUP_ONLY)]
        public static void HandleRaidGroupOnly(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_RAID_MARKERS_CHANGED)]
        public static void HandleRaidMarkersChanged(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_RAID_SUMMON_FAILED)]
        public static void HandleRaidSummonFailed(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_REAL_GROUP_UPDATE)]
        public static void HandleRealGroupUpdate(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GROUP_DISBAND)]
        [Parser(Opcode.SMSG_GROUP_DESTROYED)]
        [Parser(Opcode.SMSG_GROUP_UNINVITE)]
        [Parser(Opcode.CMSG_GROUP_DECLINE)]
        [Parser(Opcode.MSG_RAID_READY_CHECK_FINISHED)]
        [Parser(Opcode.CMSG_REQUEST_RAID_INFO)]
        [Parser(Opcode.CMSG_GROUP_REQUEST_JOIN_UPDATES)]
        public static void HandleGroupNull(Packet packet)
        {
        }
    }
}
