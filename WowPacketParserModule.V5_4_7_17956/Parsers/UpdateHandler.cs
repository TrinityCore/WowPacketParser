using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;
using UpdateFields = WowPacketParser.Enums.Version.UpdateFields;

namespace WowPacketParserModule.V5_4_7_17956.Parsers
{
    public static class UpdateHandler
    {
        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED)]
        public static void HandleObjectUpdateFailed547(Packet packet)
        {
            var guid = packet.StartBitStream(4, 6, 3, 0, 7, 5, 1, 2);
            packet.ParseBitStream(guid, 4, 7, 0, 6, 5, 2, 1, 3);

            packet.WriteGuid("Object Guid", guid);
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject547(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            packet.ReadBit("Despawn Animation");

            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ParseBitStream(guid, 2, 1, 5, 4, 3, 6, 7, 0);

            packet.WriteGuid("Object Guid", guid);
        }
    }
}
