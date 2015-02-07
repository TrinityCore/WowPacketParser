using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            packet.ReadInt32("Id");
            packet.ReadInt64("Quantity");
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32("Flags");
            packet.ReadPackedTime("Date");
            packet.ReadTime("TimeFromStart");
            packet.ReadTime("TimeFromCreate");
        }

        [Parser(Opcode.SMSG_ACCOUNT_CRITERIA_UPDATE)]
        public static void HandleCriteriaUpdateAccount(Packet packet)
        {
            packet.ReadInt32("Id");
            packet.ReadInt64("Quantity");
            packet.ReadPackedGuid128("Guid");
            packet.ReadPackedTime("Date");
            packet.ReadTime("TimeFromStart");
            packet.ReadTime("TimeFromCreate");
            packet.ReadBits("Flags", 4); // some flag... & 1 -> delete
        }

        [Parser(Opcode.SMSG_ALL_ACCOUNT_CRITERIA)]
        public static void HandleAllAchievementCriteriaDataAccount(Packet packet)
        {
            var count = packet.ReadUInt32("Criteria count");

            // ClientAllAccountCriteria
            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Id", i);

                packet.ReadInt64("Quantity", i);
                packet.ReadPackedGuid128("Guid", i);

                packet.ReadPackedTime("Date", i);
                packet.ReadTime("TimeFromStart", i);
                packet.ReadTime("TimeFromCreate", i);

                packet.ResetBitReader();
                packet.ReadBits("Flags", 4, i); // some flag... & 1 -> delete
            }
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA)]
        public static void HandleAllAchievementDataPlayer(Packet packet)
        {
            var int10 = packet.ReadUInt32("EarnedAchievementCount");
            var int20 = packet.ReadUInt32("Criteria count");

            for (var i = 0; i < int10; ++i)
            {
                packet.ReadInt32("Id", i);
                packet.ReadPackedTime("Date", i);
                packet.ReadPackedGuid128("Owner", i);
                packet.ReadInt32("VirtualRealmAddress", i);
                packet.ReadInt32("NativeRealmAddress", i);
            }

            for (var i = 0; i < int20; ++i)
            {
                packet.ReadInt32("Id", i);

                packet.ReadInt64("Quantity", i);
                packet.ReadPackedGuid128("Guid", i);

                packet.ReadPackedTime("Date", i);
                packet.ReadTime("TimeFromStart", i);
                packet.ReadTime("TimeFromCreate", i);

                packet.ResetBitReader();
                packet.ReadBits("Flags", 4, i); // some flag... & 1 -> delete
            }
        }

        [Parser(Opcode.SMSG_ACHIEVEMENT_EARNED)]
        public static void HandleAchievementEarned(Packet packet)
        {
            packet.ReadPackedGuid128("Sender");
            packet.ReadPackedGuid128("Earner");
            packet.ReadUInt32("AchievementID");
            packet.ReadPackedTime("Time");
            packet.ReadUInt32("EarnerNativeRealm");
            packet.ReadUInt32("EarnerVirtualRealm");
            packet.ReadBit("Initial");
        }
    }
}
