using System;
using System.Reflection.Emit;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class LfgHandler
    {
        [Parser(Opcode.SMSG_LFG_JOIN_RESULT)]
        public static void HandleLfgJoinResult(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet);

            packet.ReadByte("Result");
            packet.ReadByte("ResultDetail");

            var blackListCount = packet.ReadInt32("BlackListCount");
            var blackListNamesCount = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                blackListNamesCount = packet.ReadUInt32("BlackListNamesCount");

            for (int i = 0; i < blackListCount; i++)
            {
                packet.ReadPackedGuid128("Guid", i);

                var int160 = packet.ReadInt32("SlotsCount", i);

                for (int j = 0; j < int160; j++)
                {
                    packet.ReadInt32("Slot", i, j);
                    packet.ReadInt32("Reason", i, j);
                    packet.ReadInt32("SubReason1", i, j);
                    packet.ReadInt32("SubReason2", i, j);
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
            {
                int[] blackListNamesLengths = new int[blackListNamesCount];
                for (int i = 0; i < blackListNamesCount; i++)
                {
                    blackListNamesLengths[i] = (int) packet.ReadBits(24);
                }

                for (int i = 0; i < blackListNamesCount; i++)
                {
                    packet.ReadDynamicString(blackListNamesLengths[i]);
                }
            }
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHECK_UPDATE)]
        public static void HandleLfgRoleCheck(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadByteE<LfgRoleCheckStatus>("RoleCheckStatus");
            var joinSlotsCount = packet.ReadInt32("JoinSlotsCount");
            var bgQueueIdsCount = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                bgQueueIdsCount = packet.ReadUInt32("BgQueueIDCount");
            else
                packet.ReadUInt64("BgQueueID");
            packet.ReadInt32("ActivityID"); // NC
            var membersCount = packet.ReadInt32("MembersCount");

            for (var i = 0; i < joinSlotsCount; ++i) // JoinSlots
                packet.ReadUInt32("JoinSlot", i);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadUInt64("BgQueueID");

            for (var i = 0; i < membersCount; ++i) // Members
                V6_0_2_19033.Parsers.LfgHandler.ReadLFGRoleCheckUpdateMember(packet, i);

            packet.ReadBit("IsBeginning");
            packet.ReadBit("ShowRoleCheck"); // NC
        }
    }
}
