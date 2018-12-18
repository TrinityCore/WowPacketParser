using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class AccountDataHandler
    {
        public static void ReadAccountCharacterList(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("WowAccountGUID", idx);
            packet.ReadPackedGuid128("CharacterGUID", idx);
            packet.ReadUInt32("VirtualRealmAddress", idx);
            packet.ReadByteE<Race>("Race" ,idx);
            packet.ReadByteE<Class>("Class", idx);
            packet.ReadByteE<Gender>("Gender", idx);
            packet.ReadByte("Level", idx);
            packet.ReadTime("LastLogin", idx);

            packet.ResetBitReader();

            uint characterNameLength = packet.ReadBits(6);
            uint realmNameLength = packet.ReadBits(9);

            packet.ReadWoWString("CharacterName", characterNameLength, idx);
            packet.ReadWoWString("RealmName", realmNameLength, idx);
        }

        [Parser(Opcode.SMSG_GET_ACCOUNT_CHARACTER_LIST_RESULT)]
        public static void HandleGetAccountCharacterListResult(Packet packet)
        {
            packet.ReadUInt32("Token");
            uint count = packet.ReadUInt32("CharacterCount");

            packet.ResetBitReader();

            packet.ReadBit("UnkBit");

            for (var i = 0; i < count; ++i)
            {
                ReadAccountCharacterList(packet, i);
            }
        }
    }
}
