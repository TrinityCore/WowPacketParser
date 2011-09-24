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
            var size = packet.ReadInt32("Number Of Members");
            packet.ReadCString("MOTD");
            packet.ReadCString("Info");

            var numFields = packet.ReadInt32("Number Of Ranks");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadInt32("[" + i + "] Rights");
                packet.ReadInt32("[" + i + "] Money Per Day");

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
                packet.ReadInt32("[" + i + "] Rank Id");
                packet.ReadByte("[" + i + "] Level");
                packet.ReadByte("[" + i + "] Class");
                packet.ReadByte("[" + i + "] Unk");
                packet.ReadInt32("[" + i + "] Zone Id");

                if (!online)
                {
                    packet.ReadInt32("[" + i + "] Last Online");
                }

                packet.ReadCString("[" + i + "] Public Note");
                packet.ReadCString("[" + i + "] Officer Note");
            }
        }
    }
}
