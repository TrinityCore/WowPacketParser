using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_8_18414.Enums;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
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

        [Parser(Opcode.CMSG_SET_PRIMARY_TALENT_TREE)]
        public static void HandleSetPrimaryTalentTreeSpec(Packet packet)
        {
            packet.ReadUInt32("Spec Tab Id");
        }

        [Parser(Opcode.SMSG_INSPECT_TALENT)]
        public static void HandleSInspectTalent(Packet packet)
        {
            var guid = new byte[8];
            var unk40 = packet.ReadBit("unk40");
            guid[2] = packet.ReadBit();
            var guid40 = new byte[8];
            if (unk40)
                guid40 = packet.StartBitStream(7, 0, 5, 3, 2, 4, 6, 1);
            guid[4] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var unk64 = packet.ReadBits("unk64", 20);
            guid[0] = packet.ReadBit();
            var guid64 = new byte[unk64][];
            var unk82 = new bool[unk64];
            var unk96 = new uint[unk64];
            var unk88 = new bool[unk64];
            for (var i = 0; i < unk64; i++)
            {
                guid64[i] = new byte[8];
                guid64[i][1] = packet.ReadBit();
                unk88[i] = packet.ReadBit("unk88", i);
                packet.ReadBit("unk112", i);
                guid64[i][3] = packet.ReadBit();
                unk96[i] = packet.ReadBits("unk96", 21, i);
                guid64[i][2] = packet.ReadBit();
                guid64[i][6] = packet.ReadBit();
                guid64[i][4] = packet.ReadBit();
                unk82[i] = packet.ReadBit("unk82");
                guid64[i][0] = packet.ReadBit();
                guid64[i][5] = packet.ReadBit();
                guid64[i][7] = packet.ReadBit();
            }
            var count48 = packet.ReadBits("unk48", 23);
            var count96 = packet.ReadBits("unk96", 23);
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            packet.ParseBitStream(guid, 1, 4, 2);
            var unkv42 = 0;
            for (var i = 0; i < unk64; i++)
            {
                if (unk82[i])
                    packet.ReadInt16("unk80", i);

                packet.ParseBitStream(guid64[i], 3);

                unkv42 = packet.ReadInt32("unkv42", i);
                packet.WriteLine("[{0}] {1}", i, Utilities.ByteArrayToHexString(packet.ReadBytes(unkv42)));
                for (var j = 0; j < unk96[i]; j++)
                {
                    packet.ReadInt32("unk100", i, j);
                    packet.ReadByte("unk104", i, j);
                }
                packet.ReadInt32("unk76", i);
                packet.ParseBitStream(guid64[i], 6, 4, 7, 2);
                if (unk88[i])
                    packet.ReadInt32("unk84", i);
                packet.ParseBitStream(guid64[i], 5);
                packet.ReadByte("unk92", i);
                packet.ParseBitStream(guid64[i], 0, 1);
                packet.WriteGuid("Guid64", guid64[i]);
            }
            if (unk40)
            {
                packet.ParseBitStream(guid40, 6, 2, 5, 0);
                packet.ReadInt32("unk36");
                packet.ParseBitStream(guid40, 4, 7);
                packet.ReadInt64("unk24");
                packet.ParseBitStream(guid40, 1);
                packet.ReadInt32("unk32");
                packet.ParseBitStream(guid40, 3);
                packet.WriteGuid("Guid40", guid40);
            }

            packet.ParseBitStream(guid, 5);

            for (var i = 0; i < count48; i++)
                packet.ReadInt16("unk52", i);

            packet.ParseBitStream(guid, 0);
            packet.ReadInt32("unk80");

            for (var i = 0; i < count96; i++)
                packet.ReadInt16("unk100", i);

            packet.ParseBitStream(guid, 7, 3, 6);
            packet.WriteGuid("Guid", guid);
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
                for (var j = 0; j < 6; ++j)
                    packet.ReadUInt16("Glyph", i, j);

                for (var j = 0; j < wpos[i]; ++j)
                    packet.ReadUInt16("TalentID", i, j);

                packet.ReadUInt32("Spec Id", i);
            }
        }
    }
}
