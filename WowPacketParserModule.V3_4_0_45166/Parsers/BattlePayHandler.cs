using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class BattlePayHandler
    {
        [Parser(Opcode.SMSG_BATTLE_PAY_ACK_FAILED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_BATTLE_PET_DELIVERED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_COLLECTION_ITEM_DELIVERED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_CONFIRM_PURCHASE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_DELIVERY_ENDED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_DELIVERY_STARTED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_DISTRIBUTION_ASSIGN_VAS_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_DISTRIBUTION_UNREVOKED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_DISTRIBUTION_UPDATE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_GET_DISTRIBUTION_LIST_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_GET_PRODUCT_LIST_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_GET_PURCHASE_LIST_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_MOUNT_DELIVERED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_PURCHASE_UPDATE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_START_CHECKOUT, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_START_DISTRIBUTION_ASSIGN_TO_TARGET_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_START_PURCHASE_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_VALIDATE_PURCHASE_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PAY_START_VAS_PURCHASE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_GET_VAS_ACCOUNT_CHARACTER_LIST, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_GET_VAS_TRANSFER_TARGET_REALM_LIST, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_UPDATE_VAS_PURCHASE_STATES, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_VAS_CHECK_TRANSFER_OK, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_VAS_GET_QUEUE_MINUTES, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_VAS_GET_SERVICE_STATUS, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_VAS_GUILD_FOLLOW_INFO, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PAY_VAS_GUILD_MASTER_LIST, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_ENUM_VAS_PURCHASE_STATES_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_GET_VAS_ACCOUNT_CHARACTER_LIST_RESULT, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_GET_VAS_TRANSFER_TARGET_REALM_LIST_RESULT, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_VAS_CHECK_TRANSFER_OK_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_VAS_GET_QUEUE_MINUTES_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_VAS_GET_SERVICE_STATUS_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_VAS_PURCHASE_COMPLETE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_VAS_PURCHASE_STATE_UPDATE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PAY_GET_PURCHASE_LIST, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PAY_GET_PRODUCT_LIST, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PAY_ACK_FAILED_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PAY_CANCEL_OPEN_CHECKOUT, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PAY_CONFIRM_PURCHASE_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PAY_DISTRIBUTION_ASSIGN_TO_TARGET, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PAY_DISTRIBUTION_ASSIGN_VAS, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PAY_OPEN_CHECKOUT, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PAY_REQUEST_PRICE_INFO, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PAY_START_PURCHASE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlePay(Packet packet)
        {
            // No BattlePay support, do not ask
            packet.ReadToEnd();
        }
    }
}
