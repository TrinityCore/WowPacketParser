using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
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

            if (!ClientVersion.RemovedInVersion(ClientBranch.Retail, ClientVersionBuild.V9_0_5_37503) &&
                ClientVersion.Expansion != ClientType.Classic)
                packet.ReadTime64("LastActiveTime", idx);
            else
                packet.ReadTime("LastActiveTime", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                packet.ReadInt32("ContentSetID", idx);

            packet.ResetBitReader();

            uint characterNameLength = packet.ReadBits(6);
            uint realmNameLength = packet.ReadBits(9);

            packet.ReadWoWString("Name", characterNameLength, idx);
            packet.ReadWoWString("RealmName", realmNameLength, idx);
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

        [Parser(Opcode.CMSG_REPORT_CLIENT_VARIABLES, ClientVersionBuild.V8_1_0_28724, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleSaveClientVarables(Packet packet)
        {
            var varablesCount = packet.ReadUInt32("VarablesCount");

            for (var i = 0; i < varablesCount; ++i)
            {
                var variableNameLen = packet.ReadBits(7);
                var valueLen = packet.ReadBits(11);
                packet.ResetBitReader();

                packet.WriteLine($"[{ i.ToString() }] VariableName: \"{ packet.ReadWoWString((int)variableNameLen) }\" Value: \"{ packet.ReadWoWString((int)valueLen) }\"");
            }
        }
    }
}
