using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            packet.ReadUInt32("MapId");
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();

            pos.O = packet.ReadSingle();
            packet.WriteLine("Position: {0}", pos);
        }
    }
}
