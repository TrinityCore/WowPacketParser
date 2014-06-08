using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_7_18019.Enums;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class TalentHandler
    {
        [Parser(Opcode.CMSG_LEARN_TALENT)]
        public static void HandleLearnTalent(Packet packet)
        {
            var talentCount = packet.ReadBits("Talent Count", 23);

            for (int i = 0; i < talentCount; i++)
                packet.ReadUInt16("Talent Id", i);
        }

        [Parser(Opcode.SMSG_TALENTS_INFO)]
        public static void ReadTalentInfo(Packet packet)
        {
            packet.ReadByte("Active Spec Group");
            var specCount = packet.ReadBits("Spec Group count", 19);
            var wpos = new UInt32[specCount];
            for (var i = 0; i < specCount; ++i)
            {
                wpos[i] = packet.ReadBits("TalentCount", 23, i);
            }

            for (var i = 0; i < specCount; ++i)
            {
                for (var j = 0; j < wpos[i]; ++j)
                    packet.ReadUInt16("TalentID", i, j);

                packet.ReadUInt32("Spec Id", i);

                //var spentTalents = packet.ReadByte("Spec Talent Count", i);
                //var glyphCount = packet.ReadByte("Glyph count", i);
                for (var j = 0; j < 6; ++j)
                    packet.ReadUInt16("Glyph", i, j);
            }
        }
    }
}
