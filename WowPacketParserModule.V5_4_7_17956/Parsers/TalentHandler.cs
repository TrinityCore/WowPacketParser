using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_7_17956.Enums;

namespace WowPacketParserModule.V5_4_7_17956.Parsers
{
    public static class TalentHandler
    {
        [Parser(Opcode.CMSG_LEARN_TALENT, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_LEARN_TALENT)]
        public static void HandleLearnTalent547(Packet packet)
        {
            var talentCount = packet.ReadBits("Talent Count", 23);

            for (int i = 0; i < talentCount; i++)
                packet.ReadUInt16("Talent Id", i);
        }
    }
}
