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
            var size = packet.ReadInt32();
            Console.WriteLine("Number of members: " + size);

            var motd = packet.ReadCString();
            Console.WriteLine("Motd: " + motd);

            var info = packet.ReadCString();
            Console.WriteLine("Info: " + info);

            var numFields = packet.ReadInt32();
            Console.WriteLine("Number of ranks: " + numFields);

            for (var i = 0; i < numFields; i++)
            {
                var rights = packet.ReadInt32();
                Console.WriteLine("[" + i + "] Rights: " + rights);

                var money = packet.ReadInt32();
                Console.WriteLine("[" + i + "] Money Per day: " + money);

                for (var j = 0; j < 6; j++)
                {
                    var tabrights = packet.ReadInt32();
                    var slots = packet.ReadInt32();
                    Console.WriteLine("[" + i + "][" + j + "] Tab Rights: " + tabrights + " | Slots: " + slots);
                }
            }

            for (var i = 0; i < size; i++)
            {
                var guid = packet.ReadGuid();
                Console.WriteLine("[" + i + "] GUID: " + guid);

                var online = packet.ReadByte();
                Console.WriteLine("[" + i + "] Online: " + online);

                var name = packet.ReadCString();
                Console.WriteLine("[" + i + "] Name: " + name);

                var rank = packet.ReadInt32();
                Console.WriteLine("[" + i + "] Rank id: " + rank);

                var level = packet.ReadByte();
                Console.WriteLine("[" + i + "] Level: " + level);

                var mclass = packet.ReadByte();
                Console.WriteLine("[" + i + "] Class: " + mclass);

                var unk = packet.ReadByte();
                Console.WriteLine("[" + i + "] Unk: " + unk);

                var zone = packet.ReadInt32();
                Console.WriteLine("[" + i + "] Zone id: " + zone);

                if (online == 0)
                {
                    var time = packet.ReadInt32();
                    Console.WriteLine("[" + i + "] Last online: " + time);
                }

                var pnote = packet.ReadCString();
                Console.WriteLine("[" + i + "] Public note: " + pnote);

                var onote = packet.ReadCString();
                Console.WriteLine("[" + i + "] Officer note: " + onote);
            }
		}
    }
}
