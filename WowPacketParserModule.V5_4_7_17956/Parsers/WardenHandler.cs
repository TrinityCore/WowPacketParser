using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_17956.Parsers
{
    public static class WardenHandler
    {
        [Parser(Opcode.CMSG_WARDEN_DATA, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_WARDEN_DATA, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleClientWardenData(Packet packet)
        {
            var Size = packet.ReadInt32();
            byte[] WardenDataBuffer = packet.ReadBytes(Size);

            Packet WardenData = new Packet(WardenDataBuffer, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            CoreParsers.WardenHandler.HandleClientWardenData(WardenData);
        }

        [Parser(Opcode.SMSG_WARDEN_DATA, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_WARDEN_DATA, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleServerWardenData(Packet packet)
        {
            var Size = packet.ReadInt32();
            byte[] WardenDataBuffer = packet.ReadBytes(Size);

            Packet WardenData = new Packet(WardenDataBuffer, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            CoreParsers.WardenHandler.HandleServerWardenData(WardenData);
        }
    }
}
