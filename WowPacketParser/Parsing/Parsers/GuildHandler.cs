using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRosterPacket(Packet packet)
        {
            var size = packet.ReadInt32("Number of members");
            packet.ReadCString("Motd");
            packet.ReadCString("Info");

            var numFields = packet.ReadInt32("Number of ranks");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadInt32("[" + i + "] Rights");
                packet.ReadInt32("[" + i + "] Money Per day");

                for (var j = 0; j < 6; j++)
                {
                    var tabrights = packet.ReadInt32();
                    var slots = packet.ReadInt32();
                    Console.WriteLine("[" + i + "][" + j + "] Tab Rights: " + tabrights + " | Slots: " + slots);
                }
            }

            for (var i = 0; i < size; i++)
            {
                packet.ReadGuid("[" + i + "] GUID");
                var online = packet.ReadBoolean("[" + i + "] Online");

                packet.ReadCString("[" + i + "] Name");
                packet.ReadInt32("[" + i + "] Rank id");
                packet.ReadByte("[" + i + "] Level");
                packet.ReadByte("[" + i + "] Class");
                packet.ReadByte("[" + i + "] Unk");
                packet.ReadInt32("[" + i + "] Zone id");

                if (!online)
                {
                    packet.ReadInt32("[" + i + "] Last online");
                }

                packet.ReadCString("[" + i + "] Public note");
                packet.ReadCString("[" + i + "] Officer note");
            }
        }
    }
}
