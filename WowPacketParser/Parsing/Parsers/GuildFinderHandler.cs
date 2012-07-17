using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class GuildFinderHandler
    {
        [Parser(Opcode.CMSG_LF_GUILD_JOIN)]
        public static void HandleGuildFinderJoin(Packet packet)
        {
            packet.ReadBit("Join");
            packet.ReadEnum<GuildFinderOptionsAvailability>("Availability", TypeCode.UInt32);
            packet.ReadEnum<GuildFinderOptionsRoles>("Class Roles", TypeCode.UInt32);
            packet.ReadEnum<GuildFinderOptionsInterest>("Guild Interests", TypeCode.UInt32);
            packet.ReadEnum<GuildFinderOptionsLevel>("Level", TypeCode.UInt32);
            packet.ReadCString("Comment");
        }

        [Parser(Opcode.SMSG_LF_GUILD_POST_UPDATED)]
        public static void HandleGuildFinderPostUpdated(Packet packet)
        {
            var b = packet.ReadByte("Unk byte");

            if (b != 0)
            {
                packet.ReadInt32("Unk Int32");
                packet.ReadCString("Unk CString");
                packet.ReadInt32("Unk Int32");
                packet.ReadInt32("Unk Int32");
                packet.ReadInt32("Unk Int32");
                packet.ReadInt32("Unk Int32");
            }
        }

        //[Parser(Opcode.SMSG_LF_GUILD_RECRUIT_LIST_UPDATED)]
        //public static void HandleGuildFinderRecruitsUpdated(Packet packet)
        //{
        //    import_sub_6ECE50(); // bitshiffing
        //}

        [Parser(Opcode.SMSG_LF_GUILD_SEARCH_RESULT)]
        public static void HandleGuildFinderSearchResult(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            if (count == 0)
                return;
            var guids = new byte[count][];

            for (int x = 0; x < guids.Length; x++)
                guids[x] = new byte[8];

            for (var i = 0; i < count; ++i)
            {
                guids[i][7] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][4] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][5] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][0] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][2] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][6] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][1] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][3] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Guild Emblem Border Color", i);

                if (guids[i][4] != 0) // 12
                    guids[i][4] ^= packet.ReadByte();

                packet.ReadCString("Guild Description", i);

                if (guids[i][6] != 0) // 14
                    guids[i][6] ^= packet.ReadByte();

                packet.ReadInt32("Guild Emblem Texture File", i);
                packet.ReadInt32("Guild Level", i);

                if (guids[i][5] != 0) // 13
                    guids[i][5] ^= packet.ReadByte();

                packet.ReadInt32("Unk 2", i);
                packet.ReadEnum<GuildFinderOptionsRoles>("Class Roles", TypeCode.UInt32, i);
                packet.ReadCString("Guild Name", i);
                packet.ReadByte("Cached", i);

                if (guids[i][3] != 0) // 11
                    guids[i][3] ^= packet.ReadByte();

                packet.ReadInt32("Achievement Points", i);

                if (guids[i][0] != 0) // 8
                    guids[i][0] ^= packet.ReadByte();

                packet.ReadInt32("Guild Emblem Color", i);
                packet.ReadInt32("Guild Emblem Background Color", i);
                packet.ReadByte("Request Pending", i);
                packet.ReadEnum<GuildFinderOptionsInterest>("Guild Interests", TypeCode.UInt32, i);
                packet.ReadEnum<GuildFinderOptionsAvailability>("Availability", TypeCode.UInt32, i);

                if (guids[i][7] != 0) // 15
                    guids[i][7] ^= packet.ReadByte();

                packet.ReadInt32("Number of members", i);

                if (guids[i][2] != 0) // 10
                    guids[i][2] ^= packet.ReadByte();

                packet.ReadInt32("Unk 5", i);

                if (guids[i][1] != 0) // 9
                    guids[i][1] ^= packet.ReadByte();

                packet.WriteLine("[{0}] Guild Guid: {1}", i, new Guid(BitConverter.ToUInt64(guids[i], 0)));
            }
        }

    }
}