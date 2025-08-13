using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class AccountDataHandler
    {
        public static void ReadAccountCharacterData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("WowAccount", idx);
            packet.ReadPackedGuid128("Guid", idx);
            packet.ReadUInt32("VirtualRealmAddress", idx);
            packet.ReadByteE<Race>("RaceID", idx);
            packet.ReadByteE<Class>("ClassID", idx);
            packet.ReadByteE<Gender>("SexID", idx);
            packet.ReadByte("ExperienceLevel", idx);
            packet.ReadTime64("LastActiveTime", idx);
            packet.ReadInt32("ContentSetID", idx);

            packet.ResetBitReader();

            uint characterNameLength = packet.ReadBits(6);
            uint realmNameLength = packet.ReadBits(9);

            packet.ReadWoWString("CharacterName", characterNameLength, idx);
            packet.ReadWoWString("RealmName", realmNameLength, idx);
        }

        public static void ItemCollectionItemData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ID", idx);
            packet.ReadByte("Type", idx);
            packet.ReadInt64("Unknown1110", idx);
            packet.ReadInt32("Flags", idx);
        }

        [Parser(Opcode.SMSG_CACHE_INFO)]
        public static void HandleCacheInfo(Packet packet)
        {
            var cacheInfoCount = packet.ReadUInt32("CacheInfoCount");

            packet.ResetBitReader();

            var signatureLen = packet.ReadBits(6);

            for (var i = 0; i < cacheInfoCount; ++i)
            {
                packet.ResetBitReader();

                var variableNameLen = packet.ReadBits(6);
                var valueLen = packet.ReadBits(6);

                packet.WriteLine($"[{i.ToString()}] VariableName: \"{packet.ReadWoWString((int)variableNameLen)}\" Value: \"{packet.ReadWoWString((int)valueLen)}\"");
            }

            packet.ReadWoWString("Signature", signatureLen);
        }

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleServerUpdateAccountData(Packet packet)
        {
            packet.ReadTime64("Time");
            var decompCount = packet.ReadInt32();
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32E<AccountDataType>("DataType");
            var compCount = packet.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.ReadWoWString(decompCount);

            packet.AddValue("Account Data", data);
        }

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA_COMPLETE)]
        public static void HandleUpdateAccountDataComplete(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
            packet.ReadInt32E<AccountDataType>("DataType");
            packet.ReadInt32("Result");
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadTime64("ServerTime");

            for (var i = 0; i < 17; ++i)
                packet.ReadTime64($"[{(AccountDataType)i}] Time", i);
        }

        [Parser(Opcode.SMSG_GET_ACCOUNT_CHARACTER_LIST_RESULT)]
        public static void HandleGetAccountCharacterListResult(Packet packet)
        {
            packet.ReadUInt32("Token");
            uint count = packet.ReadUInt32("CharactersCount");

            packet.ResetBitReader();

            packet.ReadBit("ConsoleCommand");

            for (var i = 0; i < count; ++i)
                ReadAccountCharacterData(packet, "Characters", i);
        }

        [Parser(Opcode.SMSG_ACCOUNT_ITEM_COLLECTION_DATA)]
        public static void HandleAccountItemCollectionData(Packet packet)
        {
            packet.ReadUInt32("Unknown1110_1");
            packet.ReadByte("Type");
            uint count = packet.ReadUInt32("ItemsCount");

            for (var i = 0; i < count; ++i)
                ItemCollectionItemData(packet, "Items", i);

            packet.ResetBitReader();
            packet.ReadBit("Unknown1110_2");
        }

        [Parser(Opcode.SMSG_LOGOUT_CANCEL_ACK)]
        public static void HandleAccountNull(Packet packet)
        {
        }
    }
}
