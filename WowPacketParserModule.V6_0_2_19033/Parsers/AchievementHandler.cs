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
            packet.Translator.ReadInt32("Id");
            packet.Translator.ReadInt64("Quantity");
            packet.Translator.ReadPackedGuid128("Guid");
            packet.Translator.ReadInt32("Flags");
            packet.Translator.ReadPackedTime("Date");
            packet.Translator.ReadTime("TimeFromStart");
            packet.Translator.ReadTime("TimeFromCreate");
        }

        [Parser(Opcode.SMSG_ACCOUNT_CRITERIA_UPDATE)]
        public static void HandleCriteriaUpdateAccount(Packet packet)
        {
            ReadCriteriaProgress(packet, "Progress");
        }

        public static void ReadCriteriaProgress(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("Id", idx);
            packet.Translator.ReadUInt64("Quantity", idx);
            packet.Translator.ReadPackedGuid128("Player", idx);
            packet.Translator.ReadPackedTime("Date", idx);
            packet.Translator.ReadTime("TimeFromStart", idx);
            packet.Translator.ReadTime("TimeFromCreate", idx);

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBits("Flags", 4, idx); // some flag... & 1 -> delete
        }

        public static void ReadEarnedAchievement(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("Id", idx);
            packet.Translator.ReadPackedTime("Date", idx);
            packet.Translator.ReadPackedGuid128("Owner", idx);
            packet.Translator.ReadInt32("VirtualRealmAddress", idx);
            packet.Translator.ReadInt32("NativeRealmAddress", idx);
        }

        [Parser(Opcode.SMSG_ALL_ACCOUNT_CRITERIA)]
        public static void HandleAllAchievementCriteriaDataAccount(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("ProgressCount");

            for (var i = 0; i < count; ++i)
                ReadCriteriaProgress(packet, "Progress", i);
        }

        public static void ReadAllAchievements(Packet packet, params object[] idx)
        {
            var earnedCount = packet.Translator.ReadUInt32("EarnedCount", idx);
            var progressCount = packet.Translator.ReadUInt32("ProgressCount", idx);

            for (var i = 0; i < earnedCount; ++i)
                ReadEarnedAchievement(packet, "Earned", i);

            for (var i = 0; i < progressCount; ++i)
                ReadCriteriaProgress(packet, "Progress", i);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA)]
        public static void HandleAllAchievementDataPlayer(Packet packet)
        {
            ReadAllAchievements(packet, "Data");
        }

        [Parser(Opcode.SMSG_ACHIEVEMENT_EARNED)]
        public static void HandleAchievementEarned(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Sender");
            packet.Translator.ReadPackedGuid128("Earner");
            packet.Translator.ReadUInt32("AchievementID");
            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadUInt32("EarnerNativeRealm");
            packet.Translator.ReadUInt32("EarnerVirtualRealm");
            packet.Translator.ReadBit("Initial");
        }

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS)]
        public static void HandleRespondInspectAchievements(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Player");
            ReadAllAchievements(packet, "Data");
        }
    }
}
