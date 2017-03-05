using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            packet.Translator.ReadUInt32("Key pt1");
            packet.Translator.ReadUInt32("Key pt2");
            packet.Translator.ReadUInt32("Key pt3");
            packet.Translator.ReadUInt32("Key pt4");
            packet.Translator.ReadUInt32("Key pt5");
            packet.Translator.ReadUInt32("Key pt6");
            packet.Translator.ReadUInt32("Key pt7");
            packet.Translator.ReadUInt32("Key pt8");
            packet.Translator.ReadUInt32("Server Seed");
            packet.Translator.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];
            packet.Translator.ReadUInt32("UInt32 2");//16
            sha[8] = packet.Translator.ReadByte();//40
            sha[13] = packet.Translator.ReadByte();//45
            sha[3] = packet.Translator.ReadByte();//35
            packet.Translator.ReadUInt32("UInt32 3");//28
            sha[6] = packet.Translator.ReadByte();//38
            packet.Translator.ReadInt16E<ClientVersionBuild>("Client Build");//20
            sha[2] = packet.Translator.ReadByte();//34
            sha[0] = packet.Translator.ReadByte();//32
            sha[7] = packet.Translator.ReadByte();//39
            sha[11] = packet.Translator.ReadByte();//43
            packet.Translator.ReadUInt32("UInt32 4");//56
            sha[5] = packet.Translator.ReadByte();//37
            sha[15] = packet.Translator.ReadByte();//47
            sha[14] = packet.Translator.ReadByte();//46
            sha[12] = packet.Translator.ReadByte();//44
            packet.Translator.ReadInt64("Int64");//64,68
            packet.Translator.ReadByte("Unk Byte");//61
            packet.Translator.ReadUInt32("Client seed");//52
            packet.Translator.ReadUInt32("UInt32 1");//24
            sha[1] = packet.Translator.ReadByte();//33
            sha[9] = packet.Translator.ReadByte();//41
            sha[4] = packet.Translator.ReadByte();//36
            sha[17] = packet.Translator.ReadByte();//49
            sha[16] = packet.Translator.ReadByte();//48
            sha[19] = packet.Translator.ReadByte();//51
            sha[18] = packet.Translator.ReadByte();//50
            sha[10] = packet.Translator.ReadByte();//42
            packet.Translator.ReadByte("Unk Byte");//60

            var addons = new Packet(packet.Translator.ReadBytes(packet.Translator.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Formatter, packet.FileName);
            CoreParsers.AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            packet.Translator.ReadBit("Unk bit");
            var size = (int)packet.Translator.ReadBits(11);
            packet.Translator.ResetBitReader();
            packet.Translator.ReadBytesString("Account name", size);
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var count = 0u;
            var count1 = 0u;
            var isQueued = packet.Translator.ReadBit("Is In Queue");

            if (isQueued)
                packet.Translator.ReadBit("unk0");

            var hasAccountData = packet.Translator.ReadBit("Has Account Data");

            if (hasAccountData)
            {
                packet.Translator.ReadBit("Unk 1");
                packet.Translator.ReadBit("Unk 2");
                count1 = packet.Translator.ReadBits("Race Activation Count", 23);
                packet.Translator.ReadBit("Unk 3");
                packet.Translator.ReadBits("Unkbits", 21);
                count = packet.Translator.ReadBits("Class Activation Count", 23);
                packet.Translator.ReadBits("Unkbits", 22);
                packet.Translator.ReadBit("Unk 4");

            }
            packet.Translator.ResetBitReader();

            if (hasAccountData)
            {
                packet.Translator.ReadByte("unk");
                for (var i = 0; i < count; ++i)
                {
                    packet.Translator.ReadByteE<ClientType>("Class Expansion", i);
                    packet.Translator.ReadByteE<Class>("Class", i);
                }

                packet.Translator.ReadInt16("UnkInt16 1");
                packet.Translator.ReadInt16("UnkInt16 2");

                for (var i = 0; i < count1; ++i)
                {
                    packet.Translator.ReadByteE<ClientType>("Race Expansion", i);
                    packet.Translator.ReadByteE<Race>("Race", i);
                }

                packet.Translator.ReadUInt32("Unk 8");
                packet.Translator.ReadUInt32("Unk 9");
                packet.Translator.ReadUInt32("Unk 10");


                packet.Translator.ReadByteE<ClientType>("Account Expansion");
                packet.Translator.ReadByteE<ClientType>("Player Expansion");

            }

            packet.Translator.ReadByteE<ResponseCode>("Auth Code");

            if (isQueued)
                packet.Translator.ReadUInt32("Unk 11");
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.Translator.ReadBits("Line Count", 4);
            var lineLength = new int[lineCount];
            for (var i = 0; i < lineCount; i++)
                lineLength[i] = (int)packet.Translator.ReadBits(7);

            for (var i = 0; i < lineCount; i++)
                packet.Translator.ReadWoWString("Line", lineLength[i], i);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            packet.Translator.ReadSingle("Unk Float");
            var guid = packet.Translator.StartBitStream(3, 4, 0, 6, 7, 1, 2, 5);
            packet.Translator.ParseBitStream(guid, 0, 3, 7, 6, 1, 2, 4, 5);
            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_LOGOUT_COMPLETE)]
        public static void HandleLogoutComplete(Packet packet)
        {
            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(0);
        }
    }
}
