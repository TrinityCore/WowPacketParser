using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class CharacterHandler
    {
        public static PlayerGuidLookupData ReadPlayerGuidLookupData(Packet packet, params object[] idx)
        {
            PlayerGuidLookupData data = new PlayerGuidLookupData();

            packet.ResetBitReader();
            packet.ReadBit("IsDeleted", idx);
            var nameLength = (int)packet.ReadBits(6);

            var count = new int[5];
            for (var i = 0; i < 5; ++i)
                count[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < 5; ++i)
                packet.ReadWoWString("Name Declined", count[i], i, idx);

            packet.ReadPackedGuid128("AccountID", idx);
            packet.ReadPackedGuid128("BnetAccountID", idx);
            packet.ReadPackedGuid128("Player Guid", idx);

            packet.ReadUInt64("GuildClubMemberID", idx);
            packet.ReadUInt32("VirtualRealmAddress", idx);

            data.Race = packet.ReadByteE<Race>("Race", idx);
            data.Gender = packet.ReadByteE<Gender>("Gender", idx);
            data.Class = packet.ReadByteE<Class>("Class", idx);
            data.Level = packet.ReadByte("Level", idx);
            packet.ReadByte("Unused915", idx);
            packet.ReadInt32("TimerunningSeasonID");

            data.Name = packet.ReadWoWString("Name", nameLength, idx);

            return data;
        }

        [Parser(Opcode.SMSG_FAILED_PLAYER_CONDITION)]
        public static void HandleFailedPlayerCondition(Packet packet)
        {
            packet.ReadInt32("Id");
        }

        [Parser(Opcode.SMSG_SET_PLAYER_DECLINED_NAMES_RESULT)]
        public static void HandleSetPlayerDeclinedNamesResult(Packet packet)
        {
            packet.ReadInt32("ResultCode");
            packet.ReadPackedGuid128("Player");
        }

        [Parser(Opcode.SMSG_PLAYER_AZERITE_ITEM_GAINS)]
        public static void HandlePlayerAzeriteItemGains(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
            packet.ReadUInt64("AzeriteXPGained");
        }

        [Parser(Opcode.SMSG_PLAYER_AZERITE_ITEM_EQUIPPED_STATUS_CHANGED)]
        public static void HandlePlayerAzeriteItemEquippedStatusChanged(Packet packet)
        {
            packet.ReadBit("IsHeartEquipped");
        }

        [Parser(Opcode.SMSG_PLAYER_CHOICE_CLEAR)]
        public static void HandleEmpty(Packet packet)
        {
        }
    }
}
