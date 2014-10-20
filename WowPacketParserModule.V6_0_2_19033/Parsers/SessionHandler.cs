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

        [Parser(Opcode.SMSG_SEND_SERVER_LOCATION)]
        public static void HandleSendServerLocation(Packet packet)
        {
            var len1 = packet.ReadBits(7);
            var len2 = packet.ReadBits(7);
            packet.ReadWoWString("Server Location", len1);
            packet.ReadWoWString("Server Location", len2);
        }

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("Grunt ServerId");
            packet.ReadEnum<ClientVersionBuild>("Client Build", TypeCode.Int16);
            packet.ReadUInt32("Region");
            packet.ReadUInt32("Battlegroup");
            packet.ReadUInt32("RealmIndex");
            packet.ReadByte("Login Server Type");
            packet.ReadByte("Unk");
            packet.ReadUInt32("Client Seed");
            packet.ReadUInt64("DosResponse");

            for (uint i = 0; i < 20; ++i)
                sha[i] = packet.ReadByte();

            var accountNameLength = packet.ReadBits("Account Name Length", 11);
            packet.ResetBitReader();
            packet.ReadWoWString("Account Name", accountNameLength);
            packet.ReadBit("UseIPv6");

            var addonSize = packet.ReadUInt32("Addons Size");
            
            if (addonSize > 0)
            {
                var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
                //CoreParsers.AddonHandler.ReadClientAddonsList(ref addons);
                addons.ClosePacket(false);
            }

            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }
    }
}
