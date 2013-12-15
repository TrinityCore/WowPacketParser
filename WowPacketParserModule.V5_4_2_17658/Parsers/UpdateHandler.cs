using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;
using UpdateFields = WowPacketParser.Enums.Version.UpdateFields;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class UpdateHandler
    {
        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            var guid = new byte[8];
            
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ReadBit("Despawn Animation");
            guid[4] = packet.ReadBit();

            packet.ParseBitStream(guid, 1, 5, 3, 0, 2, 6, 7, 4);

            packet.WriteGuid("GUID", guid);
        }
    }
}
