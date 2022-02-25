using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class QueryHandler
    {
        public static PacketQueryPlayerNameResponse ReadNameCacheLookupResult(Packet packet, params object[] idx)
        {
            var response = new PacketQueryPlayerNameResponse();
            packet.ResetBitReader();
            packet.ReadByte("Result", idx);
            packet.ReadPackedGuid128("Player", idx);
            var hasPlayerGuidLookupData = packet.ReadBit("HasPlayerGuidLookupData", idx);
            var hasThingy = packet.ReadBit("HasNameCacheUnused920", idx);

            if (hasPlayerGuidLookupData)
            {
                var data = V8_0_1_27101.Parsers.CharacterHandler.ReadPlayerGuidLookupData(packet, idx, "PlayerGuidLookupData");
                response.Race = (uint)data.Race;
                response.Gender = (uint)data.Gender;
                response.Class = (uint)data.Class;
                response.Level = data.Level;
                response.HasData = true;
            }

            if (hasThingy)
            {
                packet.ResetBitReader();
                packet.ReadUInt32("Unused1", idx, "NameCacheUnused920");
                packet.ReadPackedGuid128("Unused2", idx, "NameCacheUnused920");
                var length = packet.ReadBits(7);
                packet.ReadWoWString("Unused3", length, idx, "NameCacheUnused920");
            }

            return response;
        }

        [Parser(Opcode.CMSG_QUERY_PLAYER_NAMES)]
        public static void HandleNameQuery(Packet packet)
        {
            var count = packet.ReadUInt32();
            for (var i = 0; i < count; ++i)
                packet.ReadPackedGuid128("Players", i);
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAMES_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var response = packet.Holder.QueryPlayerNameResponse = new();

            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
                response.Responses.Add(ReadNameCacheLookupResult(packet, i));
        }
    }
}
