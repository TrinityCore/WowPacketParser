using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var code = packet.ReadEnum<ResponseCode>("Auth Code", TypeCode.Byte);
            var ok = packet.ReadBit("Success");
            var queued = packet.ReadBit("Queued");
            if (ok)
            {
                packet.ReadUInt32("VirtualRealmAddress");
                var realms = packet.ReadUInt32();
                packet.ReadUInt32("TimeRemaining");
                packet.ReadUInt32("TimeOptions");
                packet.ReadUInt32("TimeRested");
                packet.ReadByte("ActiveExpansionLevel");
                packet.ReadByte("AccountExpansionLevel");
                packet.ReadUInt32("TimeSecondsUntilPCKick");
                var races = packet.ReadUInt32("AvailableRaces");
                var classes = packet.ReadUInt32("AvailableClasses");
                var templates = packet.ReadUInt32("Templates");
                packet.ReadUInt32("AccountCurrency");

                for (var i = 0; i < realms; ++i)
                {
                    packet.ReadUInt32("VirtualRealmAddress", i);
                    packet.ResetBitReader();
                    packet.ReadBit("IsLocal", i);
                    packet.ReadBit("unk", i);
                    var nameLen1 = packet.ReadBits(8);
                    var nameLen2 = packet.ReadBits(8);
                    packet.ReadWoWString("RealmNameActual", nameLen1, i);
                    packet.ReadWoWString("RealmNameNormalized", nameLen2, i);
                }

                for (var i = 0; i < races; ++i)
                {
                    packet.ReadEnum<Race>("Race", TypeCode.Byte, i);
                    packet.ReadEnum<ClientType>("RequiredExpansion", TypeCode.Byte, i);
                }

                for (var i = 0; i < classes; ++i)
                {
                    packet.ReadEnum<Class>("Class", TypeCode.Byte, i);
                    packet.ReadEnum<ClientType>("RequiredExpansion", TypeCode.Byte, i);
                }

                for (var i = 0; i < templates; ++i)
                {
                    packet.ReadUInt32("TemplateSetId", i);
                    var templateClasses = packet.ReadUInt32();
                    for (var j = 0; j < templateClasses; ++j)
                    {
                        packet.ReadEnum<Class>("Class", TypeCode.Byte, i, j);
                        packet.ReadByte("FactionGroup", i, j);
                    }

                    packet.ResetBitReader();
                    var nameLen = packet.ReadBits(7);
                    var descLen = packet.ReadBits(10);
                    packet.ReadWoWString("Name", nameLen, i);
                    packet.ReadWoWString("Description", descLen, i);
                }

                packet.ResetBitReader();
                packet.ReadBit("Trial");
                packet.ReadBit("ForceCharacterTemplate");
                var horde = packet.ReadBit(); // NumPlayersHorde
                var alliance = packet.ReadBit(); // NumPlayersAlliance
                packet.ReadBit("IsVeteranTrial");

                if (horde)
                    packet.ReadUInt16("NumPlayersHorde");

                if (alliance)
                    packet.ReadUInt16("NumPlayersAlliance");
            }

            if (queued)
            {
                packet.ReadUInt32("QueuePos");
                packet.ResetBitReader();
                packet.ReadBit("HasFCM");
            }
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.ReadBits("Line Count", 4);

            packet.ResetBitReader();
            for (var i = 0; i < lineCount; i++)
            {
                var lineLength = (int)packet.ReadBits(7);
                packet.ResetBitReader();
                packet.ReadWoWString("Line", lineLength, i);
            }
        }
    }
}
