using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.SMSG_GET_ACCOUNT_CHARACTER_LIST_RESULT, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleGetAccountCharacterListResult(Packet packet)
        {
            packet.ReadUInt32("Token");
            var count = packet.ReadUInt32("CharactersCount");

            for (var i = 0u; i < count; ++i)
                V8_0_1_27101.Parsers.AccountDataHandler.ReadAccountCharacterData(packet, "Characters", i);

            packet.ResetBitReader();
            packet.ReadBit("ConsoleCommand");
        }

        [Parser(Opcode.SMSG_CACHE_INFO, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleCacheInfo(Packet packet)
        {
            var cacheInfoCount = packet.ReadUInt32("CacheInfoCount");

            for (var i = 0; i < cacheInfoCount; ++i)
                V7_0_3_22248.Parsers.AccountDataHandler.ReadCacheInfoEntry(packet, "Entries", i);

            packet.ResetBitReader();

            var signatureLen = packet.ReadBits(6);

            packet.ReadWoWString("Signature", signatureLen);
        }
    }
}
