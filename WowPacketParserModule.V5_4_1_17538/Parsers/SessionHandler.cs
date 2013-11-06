using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class SessionHandler
    {

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            packet.ReadSingle("Unk Float");
            var guid = packet.StartBitStream(6, 7, 1, 5, 2, 4, 3, 0);
            packet.ParseBitStream(guid, 7, 6, 0, 1, 4, 3, 2, 5);
            CoreParsers.SessionHandler.LoginGuid = new Guid(BitConverter.ToUInt64(guid, 0));
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Key pt1");
            packet.ReadUInt32("Key pt2");
            packet.ReadUInt32("Key pt3");
            packet.ReadUInt32("Key pt4");
            packet.ReadUInt32("Key pt5");
            packet.ReadUInt32("Key pt6");
            packet.ReadUInt32("Key pt7");
            packet.ReadUInt32("Key pt8");
            packet.ReadUInt32("Server Seed");
        }

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("UInt32 2");
            sha[14] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            packet.ReadUInt32("UInt32 4");
            sha[10] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            packet.ReadByte("Unk Byte");
            sha[9] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            packet.ReadUInt32("Client seed");
            sha[5] = packet.ReadByte();
            sha[2] = packet.ReadByte();
            packet.ReadEnum<ClientVersionBuild>("Client Build", TypeCode.Int16);//20
            sha[12] = packet.ReadByte();
            packet.ReadUInt32("UInt32 3");
            sha[18] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            packet.ReadInt64("Int64");
            sha[7] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            packet.ReadByte("Unk Byte");
            sha[6] = packet.ReadByte();
            packet.ReadUInt32("UInt32 1");
            sha[15] = packet.ReadByte();
            //packet.ReadUInt32("UInt32 5");

            using (var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
            {
                var pkt2 = addons;
                CoreParsers.AddonHandler.ReadClientAddonsList(ref pkt2);
            }

            var size = (int)packet.ReadBits(11);
            packet.ReadBit("Unk bit");
            packet.ResetBitReader();
            packet.WriteLine("Account name: {0}", Encoding.UTF8.GetString(packet.ReadBytes(size)));
            packet.WriteLine("Proof SHA-1 Hash: " + Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.ReadBits("Line Count", 4);
            var lineLength = new int[lineCount];

            for (var i = 0; i < lineCount; i++)
                lineLength[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < lineCount; i++)
                packet.ReadWoWString("Line", lineLength[i], i);
        }
     }
}
