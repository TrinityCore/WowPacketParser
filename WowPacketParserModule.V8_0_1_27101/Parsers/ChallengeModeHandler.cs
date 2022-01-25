using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class ChallengeModeHandler
    {
        public static void ReadUnk3ChallengeModeMapStats(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadInt32("Unk1", indexes);
            packet.ReadInt32("Unk2", indexes);
            packet.ReadTime("UnkTime3", indexes);
            packet.ReadTime("UnkTime4", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadTime("UnkTime5", indexes);
            packet.ResetBitReader();
            packet.ReadBit("UnkBit", indexes);
        }

        public static void ReadUnk2ChallengeModeMapStats(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadPackedGuid128("UnkGuid", indexes);
            packet.ReadPackedGuid128("UnkGuid2", indexes);
            packet.ReadUInt32("Unk1", indexes);
            packet.ReadUInt32("Unk2", indexes);
            packet.ReadInt16("Unk3", indexes);
            packet.ReadInt16("Unk4", indexes);
            packet.ReadInt32("Unk5", indexes);
        }

        public static void ReadUnkChallengeModeMapStats(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadInt32("Unk1", indexes);
            packet.ReadUInt32("Unk2", indexes);
            packet.ReadInt32("Unk3", indexes);
            packet.ReadTime("UnkTime", indexes);
            packet.ReadTime("UnkTime", indexes);

            for (int i = 0; i < 4; i++)
                packet.ReadUInt32("Unk6", indexes, i);

            var unkCount = packet.ReadUInt32("UnkCount", indexes);
            for (int i = 0; i < unkCount; i++)
                ReadUnk2ChallengeModeMapStats(packet, indexes, i);

            packet.ResetBitReader();
            packet.ReadBit("UnkBit", indexes);
        }

        [Parser(Opcode.CMSG_REQUEST_MYTHIC_PLUS_AFFIXES)]
        public static void HandleChallengeModeZero(Packet packet) { }

        [Parser(Opcode.SMSG_MYTHIC_PLUS_WEEKLY_REWARD_RESPONSE)]
        public static void HandleMythicPlusWeeklyRewardResponse(Packet packet)
        {
            packet.ReadBit("IsWeeklyRewardAvailable");

            packet.ReadInt32("LastWeekHighestKeyCompleted");
            packet.ReadInt32("LastWeekMapChallengeKeyEntry");
            packet.ReadInt32("CurrentWeekHighestKeyCompleted");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadInt32("UnkInt"); // always 13 for me
        }

        [Parser(Opcode.SMSG_MYTHIC_PLUS_ALL_MAP_STATS)]
        public static void HandleMythicPlusAllMapStats(Packet packet)
        {
            var unkCount = packet.ReadUInt32("Unk1");
            var unkCount2 = packet.ReadUInt32("Unk2");
            var unkCount3 = packet.ReadUInt32("Unk3");
            packet.ReadInt32("Unk5");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadInt32("Unk6");

            for (int i = 0; i < unkCount; i++)
                ReadUnkChallengeModeMapStats(packet, i);

            for (int i = 0; i < unkCount2; i++)
            {
                packet.ReadInt32("Unk7", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                    packet.ReadInt32("Unk8", i);
                ReadUnkChallengeModeMapStats(packet, i);
            }

            for (int i = 0; i < unkCount3; i++)
                ReadUnk3ChallengeModeMapStats(packet, i);
        }

        [Parser(Opcode.SMSG_MYTHIC_PLUS_CURRENT_AFFIXES)]
        public static void HandleMythicPlusCurrentAffixes(Packet packet)
        {
            var count = packet.ReadUInt32();
            for (int i = 0; i < count; i++)
            {
                packet.ReadInt32("KeystoneAffixID", i);
                packet.ReadInt32("RequiredSeason", i);
            }
        }
    }
}
