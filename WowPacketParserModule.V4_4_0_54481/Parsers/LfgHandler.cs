using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class LfgHandler
    {
        public static void ReadLFGListBlacklistEntry(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("ActivityID", indexes);
            packet.ReadInt32("Reason", indexes);
        }

        public static void ReadLFGBlackList(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            var hasPlayerGuid = packet.ReadBit("HasPlayerGuid", idx);
            var lfglackListCount = packet.ReadUInt32("LFGBlackListCount", idx);

            if (hasPlayerGuid)
                packet.ReadPackedGuid128("PlayerGuid", idx);

            for (var i = 0; i < lfglackListCount; ++i)
            {
                packet.ReadUInt32("Slot", idx, i);
                packet.ReadUInt32("Reason", idx, i);
                packet.ReadInt32("SubReason1", idx, i);
                packet.ReadInt32("SubReason2", idx, i);
                packet.ReadUInt32("SoftLock", idx, i);
            }
        }

        public static void ReadLfgPlayerQuestReward(Packet packet, params object[] idx)
        {
            packet.ReadByte("Mask", idx);
            packet.ReadInt32("RewardMoney", idx);
            packet.ReadInt32("RewardXP", idx);

            var itemCount = packet.ReadUInt32("ItemCount", idx);
            var currencyCount = packet.ReadUInt32("CurrencyCount", idx);
            var bonusCurrencyCount = packet.ReadUInt32("BonusCurrency", idx);

            // Item
            for (var k = 0; k < itemCount; ++k)
            {
                packet.ReadInt32<ItemId>("ItemID", idx, k);
                packet.ReadInt32("Quantity", idx, k);
            }

            // Currency
            for (var k = 0; k < currencyCount; ++k)
            {
                packet.ReadInt32("CurrencyID", idx, k);
                packet.ReadInt32("Quantity", idx, k);
            }

            // BonusCurrency
            for (var k = 0; k < bonusCurrencyCount; ++k)
            {
                packet.ReadInt32("CurrencyID", idx, k);
                packet.ReadInt32("Quantity", idx, k);
            }

            packet.ResetBitReader();

            var hasRewardSpellId = packet.ReadBit("HasRewardSpellID", idx);
            var hasUnused1 = packet.ReadBit();
            var hasUnused2 = packet.ReadBit();
            var hasHonor = packet.ReadBit("HasHonor", idx);

            if (hasRewardSpellId)
                packet.ReadInt32("RewardSpellID", idx);

            if (hasUnused1)
                packet.ReadInt32("Unused1", idx);

            if (hasUnused2)
                packet.ReadUInt64("Unused2");

            if (hasHonor)
                packet.ReadInt32("Honor", idx);
        }

        [Parser(Opcode.CMSG_DF_GET_SYSTEM_INFO)]
        public static void HandleLFGLockInfoRequest(Packet packet)
        {
            packet.ReadBit("Player");
            if (packet.ReadBit())
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_LFG_LIST_UPDATE_BLACKLIST)]
        public static void HandleLFGListUpdateBlacklist(Packet packet)
        {
            var count = packet.ReadInt32("BlacklistEntryCount");
            for (int i = 0; i < count; i++)
                ReadLFGListBlacklistEntry(packet, i, "ListBlacklistEntry");
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_INFO)]
        public static void HandleLfgPlayerLockInfoResponse(Packet packet)
        {
            var dungeonCount = packet.ReadInt32("DungeonCount");

            ReadLFGBlackList(packet, "LFGBlackList");

            // LfgPlayerDungeonInfo
            for (var i = 0; i < dungeonCount; ++i)
            {
                packet.ReadUInt32("Slot", i);
                packet.ReadInt32("CompletionQuantity", i);
                packet.ReadInt32("CompletionLimit", i);
                packet.ReadInt32("CompletionCurrencyID", i);
                packet.ReadInt32("SpecificQuantity", i);
                packet.ReadInt32("SpecificLimit", i);
                packet.ReadInt32("OverallQuantity", i);
                packet.ReadInt32("OverallLimit", i);
                packet.ReadInt32("PurseWeeklyQuantity", i);
                packet.ReadInt32("PurseWeeklyLimit", i);
                packet.ReadInt32("PurseQuantity", i);
                packet.ReadInt32("PurseLimit", i);
                packet.ReadInt32("Quantity", i);
                packet.ReadUInt32("CompletedMask", i);
                packet.ReadUInt32("EncounterMask", i);
                var shortageRewardCount = packet.ReadInt32("ShortageRewardCount", i);

                packet.ResetBitReader();

                packet.ReadBit("FirstReward", i);
                packet.ReadBit("ShortageEligible", i);

                ReadLfgPlayerQuestReward(packet, i, "Rewards");
                for (var j = 0; j < shortageRewardCount; ++j)
                    ReadLfgPlayerQuestReward(packet, i, j, "ShortageReward");
            }
        }

        [Parser(Opcode.CMSG_LFG_LIST_GET_STATUS)]
        [Parser(Opcode.CMSG_REQUEST_LFG_LIST_BLACKLIST)]
        [Parser(Opcode.CMSG_DF_GET_JOIN_STATUS)]
        public static void HandleLfgZero(Packet packet)
        {
        }
    }
}
