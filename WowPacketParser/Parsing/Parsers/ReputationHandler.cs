using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ReputationHandler
    {
        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static  void HandleInitializeFactions(Packet packet)
        {
            var flags = packet.ReadInt32();
            Console.WriteLine("Flags: 0x" + flags.ToString("X8"));

            for (var i = 0; i < 128; i++)
            {
                var sFlags = (FactionFlag)packet.ReadByte();
                Console.WriteLine("Faction Flags " + i + ": " + sFlags);

                var sStand = packet.ReadInt32();
                Console.WriteLine("Faction Standing " + i + ": " + sStand);
            }
        }
    }
}
